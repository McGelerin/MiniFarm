using Runtime.Factory.FactoryProductionManager;
using Runtime.Factory.FactoryUI;
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
            Container.Bind<ProductionButtonsController>().FromComponentOn(productionButtonsControllerGameObject).AsSingle();
            
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
        }
        
        private void BindSignals()
        {
            Container.DeclareSignal<IssueOrderButtonClickSignal>();
            Container.DeclareSignal<RevokeOrderButtonClickSignal>();
            
            Container.DeclareSignal<StartTimerSignal>();
            Container.DeclareSignal<StopTimerSignal>();
            Container.DeclareSignal<CompleteTimerSignal>();
            
            Container.DeclareSignal<CheckFactoryProductionSignal>();
        }
    }
}