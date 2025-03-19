using Runtime.Creation;
using Runtime.Currency;
using Runtime.Factory.Data.UnityObject;
using Runtime.Factory.Model;
using Runtime.GameArea;
using Runtime.Signals;
using UnityEngine;
using Zenject;

namespace Runtime.Installers.GameAreaScene
{
    public class GameAreaSceneInstaller : MonoInstaller<GameAreaSceneInstaller>
    {
        [SerializeField] private ButtonSpriteContainerSO _buttonSpriteContainerSo;
        
        public override void InstallBindings()
        {
            Container.Install<ViewMediatorInstallerNotTick<CurrencyView,CurrencyMediator>>();
            Container.BindInterfacesAndSelfTo<GameAreaInitializer>().AsSingle();
            Container.BindInstance(_buttonSpriteContainerSo).AsSingle();

            BindFactoryModel();

            BindSignals();
        }
        
        private void BindFactoryModel()
        {
            Container.BindInterfacesAndSelfTo<FactoryModel>().AsSingle();
        }

        private void BindSignals()
        {
            Container.DeclareSignal<AnotherAreaClickSignal>();
        }
    }
}