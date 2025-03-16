using System;
using UniRx;
using Zenject;

namespace Runtime.Identifiers.Template
{
    public abstract class SignalListener : IInitializable, IDisposable
    {
        [Inject] protected readonly SignalBus _signalBus;
        protected readonly CompositeDisposable _disposables = new();
        protected abstract void SubscribeToSignals();
        
        public virtual void Initialize()
        {
            SubscribeToSignals();
        }

        public virtual void Dispose()
        {
            _disposables?.Dispose();
        }
    }
}