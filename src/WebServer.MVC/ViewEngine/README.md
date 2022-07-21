### ViewEngine description
View engines are really important part of each framework. This framework has it's own view engine too. WS.ViewEngine follows very simple rules:
<ol>
  <li>It's a must for the view engine to declare your view model on each view example <code>@model Template.ViewModels.User.LoginInputModel</code>
  if you do not declare your model, but still pass it in the controller  - <code>return this.View(viewModel);</code> - it won't work. If you want to have view model
  working correctly, just declare it :)</li>
  <li>
    You have 2 ways to pass data to the view engine - you can pass view model in the controller or you can add dynamic properties to the ViewBag. 
    You have access to the view model via Model variable
    Recomendation: Do not use ViewBag
  </li>
  <li>Every csharp code line should start with @ sign.
  For now WS.ViewEngine has list of supported operators - for, foreach, if, else, else if, while. In the future more operators needs to be added.
  Every line which starts with any of the supported operators or with { or } - the whole line is considered as csharp code, so if you want to declare a variable you
    you should do it like this <code>{ var someVariable = 6; }</code>
  </li>
  <li>
  If you want to start typing csharp code in the html and you don't want the whole line to be considered like csharp you should do it like this: 
    <code>&lt;p&gt;@DateTime.Now.Year&lt;p/&gt;</code> you can write csharp expressions wherever you want. You just need to use @ sign before each
    csharp expession. Important: <, >  and spaces means end of csharp expression.
  </li>
</ol>
