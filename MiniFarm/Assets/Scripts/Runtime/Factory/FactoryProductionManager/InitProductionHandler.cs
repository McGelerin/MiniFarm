using Runtime.Factory.Model;
using Runtime.Identifiers;
using Runtime.Signals.Production;
using Zenject;

namespace Runtime.Factory.FactoryUI.SliderArea
{
    public class InitProductionHandler: IInitializable
    {
        private readonly FactoryView _factoryView;
        private readonly FactoryModel _factoryModel;
        private readonly SignalBus _signalBus;
        
        public InitProductionHandler(FactoryView factoryView, FactoryModel factoryModel, SignalBus signalBus)
        {
            _factoryView = factoryView;
            _factoryModel = factoryModel;
            _signalBus = signalBus;
        }
        public void Initialize()
        {
            var factoryModelFactorySaveValues = _factoryModel.FactorySaveValues;
            
            if (factoryModelFactorySaveValues.ContainsKey(_factoryView.FactoryVo.FactoryID))
            {
                InitProduction();
            }
            else
            {
                if (_factoryView.FactoryVo.ConsumedResourcesType == ResourcesType.None)
                {
                    _factoryModel.FactoryFirstTask(_factoryView.FactoryVo.FactoryID, _factoryView.FactoryVo.HarvestCapacity);
                    InitProduction();
                }
            }
        }
        
        private void InitProduction()
        {
            _signalBus.Fire(new CheckFactoryProductionSignal());
        }
    }
}