using System;
using Runtime.Identifiers.Template;
using Runtime.Signals;
using UniRx;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Runtime.CrossScene.SceneManagement
{
    public class SceneLoadingService : SignalListener
    {
        protected override void SubscribeToSignals()
        {
            _signalBus.GetStream<LoadSceneSignal>().Subscribe(OnLoadSceneSignal).AddTo(_disposables);
        }
        
        private void OnLoadSceneSignal(LoadSceneSignal signal)
        {
            if (!signal.UseNetworkManager) //offline loading
            {
                AsyncOperation asyncOperation = SceneManager.LoadSceneAsync((int)signal.SceneIdid);
                
                _signalBus.Fire(new ChangeLoadingScreenActivationSignal(isActive: true, asyncOperation));
            }
            else //use network manager
            {
                throw new Exception("NETWORK LOADING HAS NOT YET BEEN IMPLEMENTED!");
            }
        }
    }
}