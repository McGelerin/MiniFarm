using System;
using Cysharp.Threading.Tasks;
using Runtime.Identifiers.Template;
using Runtime.Signals;
using UniRx;
using UnityEngine;

namespace Runtime.CrossScene.LoadingScreen
{
    public class LoadingScreenController : SignalListener
    {
        private readonly LoadingScreenMediator _mediator;

        private AsyncOperation _loadOperation;
        private bool _loadingCompleted = false;
        
        public LoadingScreenController(LoadingScreenMediator mediator)
        {
            _mediator = mediator;
        }

        protected override void SubscribeToSignals()
        {
            _signalBus.GetStream<ChangeLoadingScreenActivationSignal>().Subscribe(OnChangeLoadingScreenActivationSignal).AddTo(_disposables);
        }
        
        private async void OnChangeLoadingScreenActivationSignal(ChangeLoadingScreenActivationSignal signal)
        {
            bool isActive = signal.IsActive;

            if (isActive)
            {
                ResetProgressBar();
                _loadOperation = signal.AsyncOperation;
                _mediator.ChangePanelActivation(true);
                WaitUntilSceneIsLoaded();
            }
            else
            {
                await UniTask.WaitUntil(() => _loadingCompleted);
                _mediator.ChangePanelActivation(false);
            }
        }
        
        private void ResetProgressBar()
        {
            _mediator.View.FillImage.fillAmount = 0f;
            _loadOperation = null;
            _loadingCompleted = false;
        }
        
        private async void WaitUntilSceneIsLoaded()
        {
            try
            {
                while (!_loadOperation.isDone)
                {

                    float progress = _loadOperation.progress;
                    float targetProgress = _loadOperation.isDone ? 1f : progress;

                    // Lerp fill amount towards target progress
                    LerpProgressBar(targetProgress);

                    await UniTask.Yield();
                }

                //async operation finishes at 90%, lerp to full value for a while
                float time = 0.5f;
                while (time > 0)
                {
                    time -= Time.deltaTime;
                    // Lerp fill amount towards target progress
                    LerpProgressBar(1f);

                    await UniTask.Yield();
                }
            
                await UniTask.Yield();

                _loadingCompleted = true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
        
        private void LerpProgressBar(float targetProgress)
        {
            _mediator.View.FillImage.fillAmount = Mathf.Lerp(_mediator.View.FillImage.fillAmount, targetProgress,
                Time.fixedDeltaTime * 10f);
        }
    }
}