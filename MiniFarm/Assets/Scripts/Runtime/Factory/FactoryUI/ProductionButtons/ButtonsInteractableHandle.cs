using Runtime.Currency.Model;
using Runtime.Factory.Model;
using Runtime.Identifiers;
using Runtime.Identifiers.Template;
using Runtime.Signals.ProductionButtons;
using UniRx;

namespace Runtime.Factory.FactoryUI.ProductionButtons
{
    public class ButtonsInteractableHandle : SignalListener
    {
        private readonly FactoryView _factoryView;
        private readonly ProductionButtonsView _productionButtonsView;
        private readonly FactoryModel _factoryModel;
        private readonly CurrencyModel _currencyModel;

        public ButtonsInteractableHandle(ProductionButtonsView productionButtonsView, FactoryModel factoryModel, FactoryView factoryView, CurrencyModel currencyModel)
        {
            _productionButtonsView = productionButtonsView;
            _factoryModel = factoryModel;
            _factoryView = factoryView;
            _currencyModel = currencyModel;
        }
        
        protected override void SubscribeToSignals()
        {
            _signalBus.GetStream<CheckButtonsInteractableSignal>()
                .Subscribe(OnCheckButtonsInteractableSignal)
                .AddTo(_disposables);
        }
        
        private void OnCheckButtonsInteractableSignal(CheckButtonsInteractableSignal signal)
        {
            CheckIssueOrderButton();
            CheckRevokeButton();
        }

        private void CheckIssueOrderButton()
        {
            ResourcesType consumedResourcesType = _factoryView.FactoryVo.ConsumedResourcesType;
            int consumedResourcesAmount = _factoryView.FactoryVo.ConsumedResourcesAmount;
            int currencyAmount = _currencyModel.CurrencyValues[(CurrencyTypes)consumedResourcesType];
            
            if (consumedResourcesAmount > currencyAmount)
            {
                _productionButtonsView.IssueOrderButton.interactable = false;
            }
            else if (_factoryModel.FactorySaveValues.TryGetValue(_factoryView.FactoryVo.FactoryID, out var value))
            {
                int taskAmount = value.TaskAmount;

                _productionButtonsView.IssueOrderButton.interactable = taskAmount != _factoryView.FactoryVo.HarvestCapacity;
            }
            else
            {
                _productionButtonsView.IssueOrderButton.interactable = true;
            }
        }

        private void CheckRevokeButton()
        {
            if (_factoryModel.FactorySaveValues.TryGetValue(_factoryView.FactoryVo.FactoryID, out var value))
            {
                int taskAmount = value.TaskAmount;
                _productionButtonsView.RevokeOrderButton.interactable = taskAmount > value.CompletedTaskAmount;
            }
            else
            {
                _productionButtonsView.RevokeOrderButton.interactable = false;
            }
        }
    }
}