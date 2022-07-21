using WebServer.MVC.Validation;

namespace WebServer.MVC.ControllerHelpers
{
    public class ControllerState : IControllerState
    {
        public ControllerState()
        {
            this.Reset();
        }
        public ModelStateDictionary ModelState { get; set; }

        public void ChangeState(Controller controller)
        {
            controller.ModelState = this.ModelState;
        }

        public void Initialize(Controller controller)
        {
            this.ModelState = controller.ModelState;
        }

        public void Reset()
        {
            this.ModelState = new ModelStateDictionary();
        }
    }
}
