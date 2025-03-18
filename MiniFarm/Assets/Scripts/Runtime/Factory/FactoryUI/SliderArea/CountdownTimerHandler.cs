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
        
        private float remainingTime;
        private CancellationTokenSource cts;
        
        private readonly SliderAreaView _sliderAreaView;
        private readonly FactoryView _factoryView;

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
            while (remainingTime > 0)
            {
                remainingTime -= Time.deltaTime;

                var harvestingTime = _factoryView.FactoryVo.HarvestingTime;
                float sliderValue  = (harvestingTime - remainingTime) / harvestingTime;

                _sliderAreaView.CountdownSlider.value = sliderValue;
                _sliderAreaView.CountdownText.SetText(remainingTime.ToString("F0"));
                
                await UniTask.Yield(PlayerLoopTiming.Update, token);
            }
            
            _sliderAreaView.CountdownText.SetText("0");
            
            //await UniTask.Delay(100, cancellationToken: token);
            
            _signalBus.Fire(new CompleteTimerSignal());
        }

        private void StopTimer()
        {
            cts?.Cancel();
            
            Debug.Log("Timer sıfırlandı!");
        }
        
        private void OnStartTimerSignal(StartTimerSignal signal)
        {
            remainingTime = signal.CountdownTime;
            cts?.Cancel();
            cts = new CancellationTokenSource();
            StartCountdown(cts.Token).Forget();
        }
        
        private void OnStopTimerSignal(StopTimerSignal signal)
        {
            StopTimer();
        }

        public override void Dispose()
        {
            base.Dispose();
            
            cts?.Cancel();
        }
    }
}