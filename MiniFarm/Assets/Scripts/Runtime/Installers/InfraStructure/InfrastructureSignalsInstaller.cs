using Runtime.Audio.Signal.Audio;
using Runtime.Haptic.Signal;
using Runtime.Signals;
using Zenject;

namespace Runtime.Installers.InfraStructure
{
    public class InfrastructureSignalsInstaller : MonoInstaller<InfrastructureSignalsInstaller>
    {
        public override void InstallBindings()
        {
            //Game flow
            Container.DeclareSignal<LevelSuccessSignal>();
            //Haptic
            Container.DeclareSignal<VibrateSignal>();
            //Audio
            Container.DeclareSignal<AudioPlaySignal>();
            Container.DeclareSignal<AudioButtonTypeSignal>();
        }
    }
}