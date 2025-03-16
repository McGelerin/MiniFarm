using UniRx;
using UnityEngine;
using Zenject;

namespace Runtime.Identifiers.Template
{
    public abstract class MonoSignalListener : MonoBehaviour
    {
        [Inject] protected readonly SignalBus _signalBus;

        protected readonly CompositeDisposable _disposables = new();
        protected abstract void SubscribeToSignals();

        protected virtual void Awake()
        {
            SubscribeToSignals();
        }

        protected virtual void OnDestroy()
        {
            _disposables?.Dispose();
        }
    }
}