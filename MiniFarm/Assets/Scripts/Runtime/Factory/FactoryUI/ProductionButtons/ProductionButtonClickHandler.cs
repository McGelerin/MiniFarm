using Runtime.Factory.Data;
using Runtime.Factory.Model;
using Runtime.Identifiers;
using Runtime.Signals.Currency;
using Runtime.Signals.Production;
using Runtime.Signals.ProductionButtons;
using Runtime.Signals.SliderArea;
using Zenject;

namespace Runtime.Factory.FactoryUI.ProductionButtons
{
    public class ProductionButtonClickHandler
    {
        private readonly SignalBus _signalBus;
        private readonly FactoryModel _factoryModel;
        private readonly FactoryView _factoryView;
        private readonly SliderAreaView _sliderAreaView;

        public ProductionButtonClickHandler(SignalBus signalBus, FactoryModel factoryModel, FactoryView factoryView, SliderAreaView sliderAreaView)
        {
            _signalBus = signalBus;
            _factoryModel = factoryModel;
            _factoryView = factoryView;
            _sliderAreaView = sliderAreaView;
        }

        public void IssueOrderButtonClick()
        {
            FactoryVO factoryVo = _factoryView.FactoryVo;
            
            _factoryModel.FactoryIncreaseTask(_factoryView.FactoryVo.FactoryID);
            
            _signalBus.Fire(new ChangeCurrencyValueSignal((CurrencyTypes)factoryVo.ConsumedResourcesType, -factoryVo.ConsumedResourcesAmount, false));
            _signalBus.Fire(new CheckFactoryProductionSignal());
            _signalBus.Fire(new CheckButtonsInteractableSignal());
        }

        public void RevokeOrderButtonClick()
        {
            FactoryVO factoryVo = _factoryView.FactoryVo;
            
            _factoryModel.FactoryDecreaseTask(_factoryView.FactoryVo.FactoryID);
            _signalBus.Fire(new ChangeCurrencyValueSignal((CurrencyTypes)factoryVo.ConsumedResourcesType, factoryVo.ConsumedResourcesAmount, false));
            
            if (_factoryModel.FactorySaveValues.ContainsKey(_factoryView.FactoryVo.FactoryID))
            {
                _signalBus.Fire(new CheckFactoryProductionSignal());
            }
            else
            {
                _signalBus.Fire(new StopTimerSignal());
                _sliderAreaView.CountdownSlider.gameObject.SetActive(false);
            }
            
            _signalBus.Fire(new CheckButtonsInteractableSignal());
        }
    }
}