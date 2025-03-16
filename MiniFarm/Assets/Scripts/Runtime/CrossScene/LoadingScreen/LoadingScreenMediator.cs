using Runtime.Extensions;

namespace Runtime.CrossScene.LoadingScreen
{
    public class LoadingScreenMediator
    {
        private readonly LoadingScreenView _view;
        public LoadingScreenView View => _view;

        public LoadingScreenMediator(LoadingScreenView view)
        {
            _view = view;
        }


        public void ChangePanelActivation(bool isActive)
        {
            _view.CanvasGroup.ChangeActivation(isActive);
        }
    }
}