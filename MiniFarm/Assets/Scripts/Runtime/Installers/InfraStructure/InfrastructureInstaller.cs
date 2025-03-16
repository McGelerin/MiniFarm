using Runtime.Audio;
using Runtime.Audio.Model;
using Runtime.Haptic;
using Runtime.Haptic.Model;
using UnityEngine;
using Zenject;

namespace Runtime.Installers.InfraStructure
{
    public class InfrastructureInstaller : MonoInstaller<InfrastructureInstaller>
    {
        [SerializeField] private GameObject _audioPrefab;
        
        public override void InstallBindings()
        {
            InstallModules();
            
            BindAudio();
        }
        
        private void InstallModules()
        {
            SignalBusInstaller.Install(Container);
        }
        
        private void BindAudio()
        {
            Container.BindInterfacesAndSelfTo<AudioModel>().AsSingle();
            Container.Bind<AudioView>().FromComponentInNewPrefab(_audioPrefab).AsSingle();
            Container.BindInterfacesAndSelfTo<AudioMediator>().AsSingle();
            Container.BindInterfacesAndSelfTo<AudioController>().AsSingle().NonLazy();
        }
        
        private void BindHaptic()
        {
            Container.BindInterfacesTo<HapticModel>().AsSingle();
            Container.BindInterfacesTo<HapticController>().AsSingle();
        }
    }
}