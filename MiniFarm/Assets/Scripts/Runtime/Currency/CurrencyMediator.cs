using System.Linq;
using Runtime.Currency.Model;
using Runtime.Identifiers;
using Runtime.Identifiers.Template;
using Runtime.Signals.Currency;
using UniRx;
using Zenject;

namespace Runtime.Currency
{
    public class CurrencyMediator : SignalListener, ITickable
    {
        #region ReadonlyFields
        private readonly CurrencyView _view;
        private readonly CurrencyModel _currencyModel;
        #endregion

        #region Constructor
        public CurrencyMediator(CurrencyView view, CurrencyModel currencyModel)
        {
            _view = view;
            _currencyModel = currencyModel;
        }
        #endregion
        
        #region Core
        public override void Initialize()
        {
            base.Initialize();
            
            RefreshAllCurrencyVisual();
        }
        protected override void SubscribeToSignals()
        {
            _signalBus.GetStream<RefreshCurrencyVisualSignal>()
                .Subscribe(OnRefreshCurrencyVisualSignal)
                .AddTo(_disposables);
        }
        #endregion

        #region SignalReceivers
        private void OnRefreshCurrencyVisualSignal(RefreshCurrencyVisualSignal signal)
        {
            RefreshCurrencyVisual(signal.CurrencyType);
        }
        #endregion

        #region Executes
        private void RefreshAllCurrencyVisual()
        {
            _currencyModel.CurrencyValues.Keys.ToList().ForEach(RefreshCurrencyVisual);
        }
        private void RefreshCurrencyVisual(CurrencyTypes currencyType)
        {
            int value = _currencyModel.CurrencyValues[currencyType];
            
            _view.CurrencyTexts[currencyType].SetText(TextFormatter.FormatNumber(value));
        }
        #endregion

        public void Tick()
        {
            throw new System.NotImplementedException();
        }
    }
}