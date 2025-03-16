using Runtime.Identifiers.Template;
using Runtime.Models;
using Runtime.Signals;
using UniRx;

namespace Runtime.Infrastructures
{
    public class GameProgressController : SignalListener
    {
        private readonly IGameProgressModel _gameProgressModel;

        private GameProgressController(IGameProgressModel model)
        {
            _gameProgressModel = model;
        }

        private void UpdateLastCompletedLevel()
        {
            _gameProgressModel.IncreaseLevel();
        }

        protected override void SubscribeToSignals()
        {
             _signalBus.GetStream<LevelSuccessSignal>().Subscribe(OnLevelSuccessSignal).AddTo(_disposables);
        }
        
        private void OnLevelSuccessSignal(LevelSuccessSignal signal)
        {
            UpdateLastCompletedLevel();
        }
    }
}