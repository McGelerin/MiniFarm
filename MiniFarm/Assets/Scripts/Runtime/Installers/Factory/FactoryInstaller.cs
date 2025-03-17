using Runtime.Factory.FactoryUI;
using Runtime.Signals;
using Runtime.Signals.ProductionButtons;
using UnityEngine;
using Zenject;

namespace Runtime.Installers.Factory
{
    public class FactoryInstaller : MonoInstaller<FactoryInstaller>
    {
        [SerializeField] private GameObject productionButtonsControllerGameObject;
        [SerializeField] private GameObject sliderAreaControllerGameObject;
        
        
        public override void InstallBindings()
        {
            Container.Bind<ProductionButtonsController>().FromComponentOn(productionButtonsControllerGameObject).AsSingle();
            Container.Bind<SliderAreaController>().FromComponentOn(sliderAreaControllerGameObject).AsSingle();
        }

        private void BindSignals()
        {
            Container.DeclareSignal<BoostProductionButtonClickSignal>();
            Container.DeclareSignal<ReduceProductionButtonClickSignal>();
        }
    }
}