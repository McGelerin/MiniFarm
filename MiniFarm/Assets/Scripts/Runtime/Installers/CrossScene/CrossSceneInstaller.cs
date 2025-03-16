using Runtime.CrossScene.SceneManagement;
using Runtime.Currency;
using Runtime.Currency.Data;
using Runtime.Currency.Model;
using UnityEngine;
using Zenject;

namespace Runtime.Installers.CrossScene
{
    public class CrossSceneInstaller : MonoInstaller<CrossSceneInstaller>
    {
        public override void InstallBindings()
        {
            BindSceneLoading();
            BindCurrency();
        }
        
        private void BindSceneLoading()
        {
            Container.BindInterfacesAndSelfTo<SceneLoadingService>().AsSingle();
        }
        
        private void BindCurrency()
        {
            Container.Bind<CurrencyModel>().AsSingle();
            Container.BindInterfacesAndSelfTo<CurrencyController>().AsSingle();
        }
    }
}