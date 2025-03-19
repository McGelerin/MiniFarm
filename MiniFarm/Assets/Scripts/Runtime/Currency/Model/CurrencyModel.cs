using System.Collections.Generic;
using Runtime.Currency.Data;
using Runtime.Identifiers;

namespace Runtime.Currency.Model
{
    public class CurrencyModel
    {
        #region Constants
        private const string CURRENCY_PATH = "CURRENCY_PATH";
        #endregion

        #region ReadonlyFields
        private readonly CurrencyDataSO _currencyData;
        #endregion

        #region Fields
        private Dictionary<CurrencyTypes, int> _currencyValues;
        #endregion

        #region Getters
        public Dictionary<CurrencyTypes, int> CurrencyValues => _currencyValues;
        #endregion

        #region Constructor
        public CurrencyModel(CurrencyDataSO currencyData)
        {
            _currencyData = currencyData;

            _currencyValues = ES3.Load(nameof(_currencyValues), CURRENCY_PATH, new Dictionary<CurrencyTypes, int>(currencyData.DefaultCurrencyValues));

            SaveCurrencyValues();
        }
        #endregion

        #region Executes
        public void ChangeCurrencyValue(CurrencyTypes currencyType, int amount, bool isSet)
        {
            int lasValue = _currencyValues[currencyType];
            _currencyValues[currencyType] = isSet ? amount : lasValue + amount;
            
            SaveCurrencyValues();
        }
        private void SaveCurrencyValues() => ES3.Save(nameof(_currencyValues), _currencyValues, CURRENCY_PATH);
        #endregion
    }
}