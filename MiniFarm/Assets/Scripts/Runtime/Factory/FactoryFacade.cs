using Runtime.Factory.FactoryProductionManager;
using Runtime.Factory.FactoryUI.ProductionButtons;
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
        [Inject] private OpenCloseProductionButtonsHandler _openCloseProductionButtonsHandler;

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
            _openCloseProductionButtonsHandler.OpenCloseButtons(true);
        }

        private void CollectProductions()
        {
            _factoryProductionCollectHandler.ProductionCollect();
        }
        
        private void OnAnotherAreaClickSignal(AnotherAreaClickSignal signal)
        {
            if (_isOpenedProductionButtons)
            {
                _isOpenedProductionButtons = false;
                _openCloseProductionButtonsHandler.OpenCloseButtons(false);
            }
        }

        private void OnDestroy()
        {
            _disposables?.Dispose();
        }
    }
}