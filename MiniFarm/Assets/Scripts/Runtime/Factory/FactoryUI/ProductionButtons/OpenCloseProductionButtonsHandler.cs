using Runtime.Factory.Data.UnityObject;
using Runtime.Signals.ProductionButtons;
using UnityEngine;
using Zenject;

namespace Runtime.Factory.FactoryUI.ProductionButtons
{
    public class OpenCloseProductionButtonsHandler : IInitializable
    {
        private readonly ProductionButtonsView _productionButtonsView;
        private readonly FactoryView _factoryView;
        private readonly ButtonSpriteContainerSO _buttonSpriteContainerSo;
        private readonly SignalBus _signalBus;

        public const string OPEN_BUTTONS = "OpenButtons";
        public const string CLOSE_BUTTONS = "CloseButtons";
        
        private int openButton_hash = Animator.StringToHash(OPEN_BUTTONS);
        private int closeButton_hash = Animator.StringToHash(CLOSE_BUTTONS);
        
        public OpenCloseProductionButtonsHandler(ProductionButtonsView productionButtonsView, FactoryView factoryView, ButtonSpriteContainerSO buttonSpriteContainerSo, SignalBus signalBus)
        {
            _productionButtonsView = productionButtonsView;
            _factoryView = factoryView;
            _buttonSpriteContainerSo = buttonSpriteContainerSo;
            _signalBus = signalBus;
        }

        public void Initialize()
        {
            _productionButtonsView.ConsumedResourcesAmountText.SetText("x" + _factoryView.FactoryVo.ConsumedResourcesAmount);
            Sprite consumedResourcesTypeSprite = _buttonSpriteContainerSo.ResourcesTypeSprites[_factoryView.FactoryVo.ConsumedResourcesType];
            _productionButtonsView.ConsumedResourcesTypeImage.sprite = consumedResourcesTypeSprite;
        }
        
        public void OpenCloseButtons(bool isOpen)
        {
            if (isOpen)
                OpenButtons();
            else
                CloseButtons();
        }

        private void OpenButtons()
        {
            _signalBus.Fire(new CheckButtonsInteractableSignal());
            //Butonların tıklanabilirliği kontrol edilecek
            _productionButtonsView.ProductionButtonsAnimator.SetTrigger(openButton_hash);
        }
        
        private void CloseButtons()
        {
            _productionButtonsView.IssueOrderButton.interactable = false;
            _productionButtonsView.RevokeOrderButton.interactable = false;
            
            _productionButtonsView.ProductionButtonsAnimator.SetTrigger(closeButton_hash);
        }
    }
}