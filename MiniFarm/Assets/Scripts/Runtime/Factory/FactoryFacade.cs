using Runtime.Currency.Model;
using Runtime.Factory.Model;
using Runtime.Identifiers;
using Runtime.Input.Raycasting;
using UnityEngine;
using Zenject;

namespace Runtime.Factory
{
    public class FactoryFacade : MonoBehaviour, IClickable
    {
        [Inject]private FactoryView _factoryView;
        [Inject]private CurrencyModel _currencyModel;

        
        public IClickable OnClicked()
        {
            Debug.Log(_currencyModel.CurrencyValues[CurrencyTypes.Bread].ToString());
            Debug.Log(_factoryView.FactoryVo.GainedHarvestType.ToString());
            
            return this;
        }
    }
}