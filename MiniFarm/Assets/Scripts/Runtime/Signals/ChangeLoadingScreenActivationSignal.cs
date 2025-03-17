using UnityEngine;

namespace Runtime.Signals
{
    public readonly struct ChangeLoadingScreenActivationSignal
    {
        private readonly bool _isActive;
        private readonly AsyncOperation _asyncOperation;
        
        public bool IsActive => _isActive;
        public AsyncOperation AsyncOperation => _asyncOperation;

        public ChangeLoadingScreenActivationSignal(bool isActive, AsyncOperation asyncOperation)
        {
            _isActive = isActive;
            _asyncOperation = asyncOperation;
        }

    }
}