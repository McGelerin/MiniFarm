using Runtime.Audio.Data;
using UnityEngine;
using UnityEngine.Serialization;
using Zenject;

namespace Runtime.Installers.DataInstaller
{
    [CreateAssetMenu(fileName = "Game Data Installer", menuName = "Installers/Data/Game Data Installer")]
    public class GameDataInstaller : ScriptableObjectInstaller<GameDataInstaller>
    {
        [SerializeField] private AudioData audioData;
        
        public override void InstallBindings()
        {
            Container.BindInstances(audioData);
        }
    }
}