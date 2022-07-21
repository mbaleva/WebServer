namespace WebServer.MVC.ViewEngine
{
    using Microsoft.CodeAnalysis;
    using Microsoft.CodeAnalysis.CSharp;
    using Microsoft.CodeAnalysis.Emit;
    using System;
    using System.Linq;
    using System.IO;
    using System.Reflection;
    using System.Text;
    using System.Text.RegularExpressions;
    using System.Collections.Generic;
    using WebServer.MVC.Validation;
    using WebServer.MVC.Identity.EntityFrameworkCore.Models;

    public class ViewEngine : IViewEngine
    {
        private const string VIEWS_DLL_NAME = "WS.Views.dll";
        private const string VIEWS_NAMESPACE_NAME = "WS.ViewsNamespace";
        private List<Type> typesToAddAsReferences = new List<Type>();
        private List<ViewDescriptor> viewsDescriptors = new List<ViewDescriptor>();
        public ViewEngine()
        {
            this.CreateInitViewsDll();
        }
        public void CreateInitViewsDll() 
        {
            this.PassSpecialViewsThroughRendering();
            List<Type> controllerTypes = this.ExtractControllerTypes();

            foreach (var controller in controllerTypes)
            {
                List<MethodInfo> methods = controller.GetMethods()
                    .Where(x => x.IsPublic && !x.IsStatic &&
                    x.DeclaringType == controller && !x.IsSpecialName)
                    .ToList();

                foreach (var action in methods)
                {
                    string controllerName = controller.Name.Replace("Controller", "");
                    string viewPath = $"Views/{controllerName}/{action.Name}.cshtml";
                    string viewTemplate = "";
                    string viewName = $"{controllerName}_{action.Name}";
                    try
                    {
                        viewTemplate = File.ReadAllText(viewPath);
                    }
                    catch (Exception)
                    {
                        continue;
                    }
                    if (this.viewsDescriptors.Any(x => x.ViewName == viewName))
                    {
                        continue;
                    }
                    string viewModelName = this.TryFindViewModelTypeName(viewTemplate);
                    string viewAsCSharp = this.GenerateCSharp(viewTemplate, viewModelName, viewName);
                    
                    this.viewsDescriptors.Add(new ViewDescriptor 
                    {
                        CSharpCode = viewAsCSharp,
                        IsSpecial = false,
                        ViewName = viewName,
                        ViewModelName = viewModelName,
                        ViewModelType = this.ExtractViewModelTypeFromString(viewModelName)
                    });
                }
            }
            this.GenerateDllCode();
            this.ExtractViewsInstances();
        }
        private Type ExtractViewModelTypeFromString(string viewModelName) 
        {
            if (viewModelName == "System.Object")
            {
                return typeof(object);
            }
            return Assembly.GetEntryAssembly()?.GetTypes()
                .Where(x => x.IsClass && !x.IsAbstract && x.IsPublic && x.FullName == viewModelName)
                .FirstOrDefault();
        }
        private string TryFindViewModelTypeName(string viewTemplate)
        {
            string[] lines = viewTemplate
                .Split("\r\n".ToArray(), StringSplitOptions.RemoveEmptyEntries);

            if (lines.Any(x => x.StartsWith("@model")))
            {
                string line = lines.First(x => x.StartsWith("@model"));
                line = line.Replace("@model", "");
                line = line.Trim();
                return line;
            }
            return "System.Object";
        }

        private void PassSpecialViewsThroughRendering() 
        {
            var layout = File.ReadAllText("Views/Shared/_Layout.cshtml");
            layout = layout.Replace("@RenderBody", "//RenderBody");
            var layoutAsCSharp = this.GenerateCSharp(layout, this.TryFindViewModelTypeName(layout), "Layout");
            this.viewsDescriptors.Add(new ViewDescriptor
            {
                CSharpCode = layoutAsCSharp,
                IsSpecial = true,
                ViewName = "Layout",
                ViewModelName = "System.Object",
                ViewModelType = typeof(object)
            });
        }
        private List<Type> ExtractControllerTypes() 
        {
            return Assembly
                .GetEntryAssembly()?
                .GetTypes()
                .Where(x => x.IsClass && !x.IsAbstract && x.IsSubclassOf(typeof(Controller)))
                .ToList();
        }
        public string Html(object viewModel, IdentityUser user,
            ModelStateDictionary ModelState, dynamic viewBag, string viewName)
        {
            return this.viewsDescriptors.Where(x => x.ViewName == viewName).FirstOrDefault()?.Instance
                ?.GenerateHtml(viewModel, user, ModelState, viewBag);
        }
        private string GenerateCSharp(string code, string viewModelType, string viewClassName)
        {
            string htmlContent = string.Empty;
            htmlContent = this.CheckForPartialViews(code);
            htmlContent = GetMethodBody(htmlContent);
            

            string cSharpCode = @"
using System;
using System.Text;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebServer.MVC.ViewEngine;
using WebServer.MVC.Validation;
using WebServer.MVC.Identity.EntityFrameworkCore.Models;

namespace " + VIEWS_NAMESPACE_NAME + @"
{
    public class " + viewClassName + @" : IView
    {
        public string GenerateHtml(object viewModel, IdentityUser user, 
                                    ModelStateDictionary ModelStateDict, dynamic viewBag)
        {
            var html = new StringBuilder();
            var ModelState = ModelStateDict;
            var User = user;
            var ViewBag = viewBag;
            var Model = viewModel as " +  viewModelType + @";
            " + htmlContent + @"
            return html.ToString();
        }
    }
}
";
            return cSharpCode;
        }

        private string CheckForPartialViews(string code)
        {
            List<IPartialView> views = Assembly
               .GetEntryAssembly()?
               .GetTypes()
               .Where(type => typeof(IPartialView).IsAssignableFrom(type))
               .Select(x => (IPartialView)Activator.CreateInstance(x))
               .ToList();

            if (views == null || views.Count == 0)
            {
                return code;
            }

            string partialViewsPrefix = "@PartialViews.";

            foreach (var view in views)
            {
                code = code.Replace($"{partialViewsPrefix}{view.GetType().Name}", view.RenderWidget());
            }

            return code;
        }

        private void GenerateDllCode()
        {
            var compileResult = CSharpCompilation.Create(VIEWS_DLL_NAME)
                .WithOptions(new CSharpCompilationOptions(OutputKind.DynamicallyLinkedLibrary))
                .AddReferences(MetadataReference.CreateFromFile(typeof(object).Assembly.Location))
                .AddReferences(MetadataReference.CreateFromFile(typeof(Object).Assembly.Location))
                .AddReferences(MetadataReference.CreateFromFile(typeof(IView).Assembly.Location))
                .AddReferences(MetadataReference.CreateFromFile(Assembly.Load(new AssemblyName("netstandard")).Location));

            foreach (var item in this.typesToAddAsReferences)
            {
                compileResult = compileResult.AddReferences(
                    MetadataReference.CreateFromFile(Assembly.Load(item.Assembly.FullName).Location));
            }
            var libraries = Assembly.Load(new AssemblyName("netstandard")).GetReferencedAssemblies();

            foreach (var item in libraries)
            {
                compileResult =
                    compileResult.AddReferences(MetadataReference.CreateFromFile(Assembly.Load(item).Location));
            }

            compileResult = compileResult.AddReferences(
                MetadataReference.CreateFromFile(Assembly.Load("Microsoft.CSharp").Location));

            foreach (var currentViewDescriptor in this.viewsDescriptors)
            {
                File.AppendAllText("logs.txt", currentViewDescriptor.CSharpCode);
                for (int i = 0; i < 10; i++)
                {
                    File.AppendAllText("logs.txt", "\r\n");
                }
                compileResult = compileResult.AddReferences(MetadataReference.CreateFromFile(
                    currentViewDescriptor.ViewModelType.Assembly.Location));

                compileResult =
                    compileResult.AddSyntaxTrees(SyntaxFactory.ParseSyntaxTree(currentViewDescriptor.CSharpCode));
            }
            var emitResult = compileResult.Emit(VIEWS_DLL_NAME);
            if (!emitResult.Success)
            {
                foreach (var error in emitResult.Diagnostics.Where(x => x.Severity == DiagnosticSeverity.Error))
                {
                    Console.WriteLine(error.GetMessage());
                }
            }
        }
        private void ExtractViewsInstances() 
        {
            Assembly viewsAssembly = Assembly.LoadFrom(VIEWS_DLL_NAME);

            foreach (var viewDescriptor in this.viewsDescriptors)
            {
                var type = viewsAssembly.GetType($"{VIEWS_NAMESPACE_NAME}.{viewDescriptor.ViewName}");
                viewDescriptor.Instance = Activator.CreateInstance(type) as IView;
            }
        }
        private string GetMethodBody(string viewCode)
        {
            Regex csharpCodeRegex = new Regex(@"[^\""\s&\'\<]+");
            var supportedOperators = new List<string> { "for", "while", "if", "else", "foreach", "else if" };
            StringBuilder csharpCode = new StringBuilder();
            StringReader sr = new StringReader(viewCode);
            string line;
            while ((line = sr.ReadLine()) != null)
            {
                if (supportedOperators.Any(x => line.TrimStart().StartsWith("@" + x)))
                {
                    var atIndex = line.IndexOf("@");
                    line = line.Remove(atIndex, 1);
                    csharpCode.AppendLine(line);
                }
                else if (line.TrimStart().StartsWith("{") ||
                    line.TrimStart().StartsWith("}"))
                {
                    csharpCode.AppendLine(line);
                }
                else if (line.TrimStart().StartsWith("@model"))
                {
                }
                
                else
                {
                    csharpCode.Append($"html.AppendLine(@\"");

                    while (line.Contains("@"))
                    {
                        var atSignLocation = line.IndexOf("@");
                        var htmlBeforeAtSign = line.Substring(0, atSignLocation);
                        csharpCode.Append(htmlBeforeAtSign.Replace("\"", "\"\"") + "\" + ");
                        var lineAfterAtSign = line.Substring(atSignLocation + 1);
                        var code = csharpCodeRegex.Match(lineAfterAtSign).Value;
                        csharpCode.Append(code + " + @\"");
                        line = lineAfterAtSign.Substring(code.Length);
                    }

                    csharpCode.AppendLine(line.Replace("\"", "\"\"") + "\");");
                }
            }

            return csharpCode.ToString();
        }
    }
}

