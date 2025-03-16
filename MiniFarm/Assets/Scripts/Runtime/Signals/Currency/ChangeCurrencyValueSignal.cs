using Runtime.Identifiers;

namespace Runtime.Signals.Currency
{
    public readonly struct ChangeCurrencyValueSignal
    {
        #region Fields
        private readonly CurrencyTypes _currencyType;
        private readonly int _amount;
        private readonly bool _isSet;
        #endregion

        #region Getters
        public CurrencyTypes CurrencyType => _currencyType;
        public int Amount => _amount;
        public bool IsSet => _isSet;
        #endregion

        #region Constructor
        public ChangeCurrencyValueSignal(CurrencyTypes currencyType, int amount, bool isSet)
        {
            _currencyType = currencyType;
            _amount = amount;
            _isSet = isSet;
        }
        #endregion
    }
}