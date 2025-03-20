using Runtime.Factory.Data;
using Runtime.Factory.FactoryUI;
using Runtime.Factory.Model;
using Runtime.Identifiers;
using Runtime.Signals.Currency;
using Runtime.Signals.Production;
using Zenject;

namespace Runtime.Factory.FactoryProductionManager
{
    public class FactoryProductionCollectHandler
    {
        private readonly SignalBus _signalBus;
        private readonly FactoryModel _factoryModel;
        private readonly FactoryView _factoryView;
        private readonly SliderAreaView _sliderAreaView;

        public FactoryProductionCollectHandler(SignalBus signalBus, FactoryModel factoryModel, FactoryView factoryView, SliderAreaView sliderAreaView)
        {
            _signalBus = signalBus;
            _factoryModel = factoryModel;
            _factoryView = factoryView;
            _sliderAreaView = sliderAreaView;
        }
        
        public void ProductionCollect()
        {
            if (!_factoryModel.FactorySaveValues.ContainsKey(_factoryView.FactoryVo.FactoryID)) return;
            
            FactoryVO factoryVo = _factoryView.FactoryVo;

            int completedTaskAmount = _factoryModel.FactorySaveValues[_factoryView.FactoryVo.FactoryID].CompletedTaskAmount;
            var type = _factoryView.FactoryVo.GainedHarvestType;
            
            _signalBus.Fire(new ChangeCurrencyValueSignal((CurrencyTypes)type, completedTaskAmount * _factoryView.FactoryVo.GainedHarvestAmount, false));
            
            _factoryModel.FactoryProductionCollect(_factoryView.FactoryVo.FactoryID, _factoryView.FactoryVo.HarvestingTime, _factoryView.FactoryVo.ConsumedResourcesType == ResourcesType.None);

            if (factoryVo.ConsumedResourcesType == ResourcesType.None)
            {
            }
            
            if (!_factoryModel.FactorySaveValues.ContainsKey(factoryVo.FactoryID))
            {
                _sliderAreaView.CountdownSlider.value = 0;
                _sliderAreaView.StockText.SetText("0");
                _sliderAreaView.CountdownText.SetText("");
                _sliderAreaView.CountdownSlider.gameObject.SetActive(false);
            }
            
            _signalBus.Fire(new CheckFactoryProductionSignal());
        }
    }
}