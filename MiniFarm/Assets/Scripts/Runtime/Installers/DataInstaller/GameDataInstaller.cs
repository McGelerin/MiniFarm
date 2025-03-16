using Runtime.Audio.Data;
using Runtime.Currency.Data;
using UnityEngine;
using UnityEngine.Serialization;
using Zenject;

namespace Runtime.Installers.DataInstaller
{
    [CreateAssetMenu(fileName = "Game Data Installer", menuName = "Installers/Data/Game Data Installer")]
    public class GameDataInstaller : ScriptableObjectInstaller<GameDataInstaller>
    {
        [SerializeField] private AudioData audioData;
        [SerializeField] private CurrencyDataSO _currencyData;
        
        public override void InstallBindings()
        {
            Container.BindInstances(audioData);
            Container.BindInstances(_currencyData);
        }
    }
}