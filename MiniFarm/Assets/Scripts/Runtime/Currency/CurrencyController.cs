using Runtime.Currency.Model;
using Runtime.Identifiers;
using Runtime.Identifiers.Template;
using Runtime.Signals.Currency;
using UniRx;

namespace Runtime.Currency
{
    public class CurrencyController : SignalListener
    {
        #region ReadonlyFields
        private readonly CurrencyModel _currencyModel;
        #endregion

        #region Constructor
        public CurrencyController(CurrencyModel currencyModel)
        {
            _currencyModel = currencyModel;
        }
        #endregion
        
        #region Core
        protected override void SubscribeToSignals()
        {
            _signalBus.GetStream<ChangeCurrencyValueSignal>()
                .Subscribe(OnChangeCurrencyValueSignal)
                .AddTo(_disposables);
        }
        #endregion

        #region SignalReceivers
        private void OnChangeCurrencyValueSignal(ChangeCurrencyValueSignal signal)
        {
            ChangeCurrencyValue(signal.CurrencyType, signal.Amount, signal.IsSet);
        }
        
        #endregion

        #region Executes
        private void ChangeCurrencyValue(CurrencyTypes currencyType, int amount, bool isSet)
        {
            _currencyModel.ChangeCurrencyValue(currencyType, amount, isSet);

            _signalBus.Fire(new RefreshCurrencyVisualSignal(currencyType));
        }
        #endregion
    }
}