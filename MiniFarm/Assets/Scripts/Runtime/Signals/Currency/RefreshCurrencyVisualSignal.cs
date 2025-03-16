using Runtime.Identifiers;

namespace Runtime.Signals.Currency
{
    public readonly struct RefreshCurrencyVisualSignal
    {
        #region Fields
        private readonly CurrencyTypes _currencyType;
        #endregion

        #region Getters
        public CurrencyTypes CurrencyType => _currencyType;
        #endregion

        #region Constructor
        public RefreshCurrencyVisualSignal(CurrencyTypes currencyType)
        {
            _currencyType = currencyType;
        }
        #endregion
    }
}