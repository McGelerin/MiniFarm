using Runtime.Currency.Model;
using Runtime.Identifiers;
using Runtime.Input.Raycasting;
using UnityEngine;
using Zenject;

namespace Runtime.Factory
{
    public class FactoryFacade : MonoBehaviour, IClickable
    {

        private FactoryView _factoryView;
        private CurrencyModel _currencyModel;
        private SignalBus _signalBus;

        [Inject]
        public void Construct(SignalBus signalBus,FactoryView factoryView, CurrencyModel currencyModel)
        {
            _signalBus = signalBus;
            _factoryView = factoryView;
            _currencyModel = currencyModel;
        }
        
        public IClickable OnClicked()
        {
            Debug.Log(_currencyModel.CurrencyValues[CurrencyTypes.Bread].ToString());
            Debug.Log(_factoryView.FactoryVo.GainedHarvestType.ToString());
            
            return this;
        }
    }
}