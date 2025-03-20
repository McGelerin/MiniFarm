using Runtime.Factory.FactoryProductionManager;
using Runtime.Factory.FactoryUI;
using Runtime.Factory.FactoryUI.ProductionButtons;
using Runtime.Factory.FactoryUI.SliderArea;
using Runtime.Signals.Production;
using Runtime.Signals.ProductionButtons;
using Runtime.Signals.SliderArea;
using UnityEngine;
using Zenject;

namespace Runtime.Installers.Factory
{
    public class FactoryInstaller : MonoInstaller<FactoryInstaller>
    {
        [SerializeField] private GameObject productionButtonsControllerGameObject;
        [SerializeField] private GameObject sliderAreaViewGameObject;
        
        
        
        public override void InstallBindings()
        {
            BindProductionButtons();
            BindSliderArea();
            BindFactoryProduction();
            BindSignals();
        }
        
        private void BindSliderArea()
        {
            Container.Bind<SliderAreaView>().FromComponentOn(sliderAreaViewGameObject).AsSingle();
            Container.BindInterfacesAndSelfTo<CountdownTimerHandler>().AsSingle();
        }

        private void BindFactoryProduction()
        {
            Container.BindInterfacesAndSelfTo<FactoryProductionManager>().AsSingle();
            Container.BindInterfacesAndSelfTo<InitProductionHandler>().AsSingle();
            Container.Bind<FactoryProductionCollectHandler>().AsSingle();
        }

        private void BindProductionButtons()
        {
            Container.Bind<ProductionButtonClickHandler>().AsSingle();
            Container.Bind<ProductionButtonsView>().FromComponentOn(productionButtonsControllerGameObject).AsSingle();
            Container.BindInterfacesAndSelfTo<OpenCloseProductionButtonsHandler>().AsSingle();
            Container.BindInterfacesAndSelfTo<ButtonsInteractableHandle>().AsSingle();
        }
        
        private void BindSignals()
        {
            Container.DeclareSignal<StartTimerSignal>();
            Container.DeclareSignal<StopTimerSignal>();
            Container.DeclareSignal<CompleteTimerSignal>();
            
            Container.DeclareSignal<CheckFactoryProductionSignal>();
            Container.DeclareSignal<CheckButtonsInteractableSignal>();
        }
    }
}