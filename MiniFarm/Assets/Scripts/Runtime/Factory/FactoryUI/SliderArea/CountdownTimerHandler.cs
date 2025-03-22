using System.Threading;
using Cysharp.Threading.Tasks;
using Runtime.Identifiers.Template;
using Runtime.Signals.SliderArea;
using UniRx;
using UnityEngine;

namespace Runtime.Factory.FactoryUI.SliderArea
{
    public class CountdownTimerHandler : SignalListener
    {
        
        private float _remainingTime;
        private CancellationTokenSource _cts;
        
        private readonly SliderAreaView _sliderAreaView;
        private readonly FactoryView _factoryView;
        
        private const string IS_TASK_START = "IsTaskStart";
        
        private int isTaskStart = Animator.StringToHash(IS_TASK_START);

        public CountdownTimerHandler(SliderAreaView sliderAreaView, FactoryView factoryView)
        {
            _sliderAreaView = sliderAreaView;
            _factoryView = factoryView;
        }
        
        protected override void SubscribeToSignals()
        {
            _signalBus.GetStream<StartTimerSignal>()
                .Subscribe(OnStartTimerSignal)
                .AddTo(_disposables);
            
            _signalBus.GetStream<StopTimerSignal>()
                .Subscribe(OnStopTimerSignal)
                .AddTo(_disposables);
        }
        
        private async UniTaskVoid StartCountdown(CancellationToken token)
        {
            while (_remainingTime > 0)
            {
                _remainingTime -= Time.deltaTime;

                var harvestingTime = _factoryView.FactoryVo.HarvestingTime;
                float sliderValue  = (harvestingTime - _remainingTime) / harvestingTime;

                _sliderAreaView.CountdownSlider.value = sliderValue;
                _sliderAreaView.CountdownText.SetText(_remainingTime.ToString("F0"));
                
                await UniTask.Yield(PlayerLoopTiming.Update, token);
            }
            
            _sliderAreaView.CountdownText.SetText("0");
            
            _signalBus.Fire(new CompleteTimerSignal());
        }

        private void StopTimer()
        {
            _factoryView.FactoryVo.TaskAnimator.SetBool(isTaskStart, false);
            _cts?.Cancel();
        }
        
        private void OnStartTimerSignal(StartTimerSignal signal)
        {
            _remainingTime = signal.CountdownTime;
            _factoryView.FactoryVo.TaskAnimator.SetBool(isTaskStart, true);
            _cts?.Cancel();
            _cts = new CancellationTokenSource();
            StartCountdown(_cts.Token).Forget();
        }
        
        private void OnStopTimerSignal(StopTimerSignal signal)
        {
            StopTimer();
        }

        public override void Dispose()
        {
            base.Dispose();
            
            _cts?.Cancel();
        }
    }
}