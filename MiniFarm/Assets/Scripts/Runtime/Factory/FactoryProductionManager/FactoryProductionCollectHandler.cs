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

        public FactoryProductionCollectHandler(SignalBus signalBus, FactoryModel factoryModel, FactoryView factoryView)
        {
            _signalBus = signalBus;
            _factoryModel = factoryModel;
            _factoryView = factoryView;
        }
        
        public void ProductionCollect()
        {
            if (!_factoryModel.FactorySaveValues.ContainsKey(_factoryView.FactoryVo.FactoryID)) return;
            
            int completedTaskAmount = _factoryModel.FactorySaveValues[_factoryView.FactoryVo.FactoryID].CompletedTaskAmount;
            var type = _factoryView.FactoryVo.GainedHarvestType;
            
            _signalBus.Fire(new ChangeCurrencyValueSignal((CurrencyTypes)type, completedTaskAmount * _factoryView.FactoryVo.GainedHarvestAmount, false));
            
            _factoryModel.FactoryProductionCollect(_factoryView.FactoryVo.FactoryID);
            
            _signalBus.Fire(new CheckFactoryProductionSignal());
        }
    }
}