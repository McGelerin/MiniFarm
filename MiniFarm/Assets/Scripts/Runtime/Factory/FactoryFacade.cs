using Runtime.Factory.FactoryProductionManager;
using Runtime.Identifiers;
using Runtime.Input.Raycasting;
using Runtime.Signals;
using UniRx;
using UnityEngine;
using Zenject;

namespace Runtime.Factory
{
    public class FactoryFacade : MonoBehaviour, IClickable
    {
        [Inject] private FactoryView _factoryView;
        [Inject] private SignalBus _signalBus;
        [Inject] private FactoryProductionCollectHandler _factoryProductionCollectHandler;

        private readonly CompositeDisposable _disposables = new();
        private bool _isOpenedProductionButtons = false;
        
        private void Start()
        {
             _signalBus.GetStream<AnotherAreaClickSignal>()
                .Subscribe(OnAnotherAreaClickSignal)
                .AddTo(_disposables);
        }

        public void OnClicked()
        {
            if (_factoryView.FactoryVo.ConsumedResourcesType == ResourcesType.None)
            {
                CollectProductions();
            }
            else if (_isOpenedProductionButtons)
            {
                CollectProductions();
            }
            else
            {
                OpenProductionButtons();
            }
        }

        private void OpenProductionButtons()
        {
            _isOpenedProductionButtons = true;
            Debug.Log("open button");
        }

        private void CollectProductions()
        {
            _factoryProductionCollectHandler.ProductionCollect();
        }
        
        private void OnAnotherAreaClickSignal(AnotherAreaClickSignal signal)
        {
            if (_isOpenedProductionButtons)
            {
                //butonlar kapanır
                
                _isOpenedProductionButtons = false;
                Debug.Log("Close button");
            }
        }

        private void OnDestroy()
        {
            _disposables?.Dispose();
        }
    }
}