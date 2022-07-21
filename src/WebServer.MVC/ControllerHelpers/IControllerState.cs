namespace WebServer.MVC.ControllerHelpers
{
    using WebServer.MVC.Validation;
    public interface IControllerState
    {
        ModelStateDictionary ModelState { get; set; }
        void Reset();

        void Initialize(Controller controller);

        void ChangeState(Controller controller);
    }
}
