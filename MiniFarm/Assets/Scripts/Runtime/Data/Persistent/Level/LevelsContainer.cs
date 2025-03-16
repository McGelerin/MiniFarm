using UnityEngine;
using UnityEngine.Rendering;

namespace Runtime.Data.Level
{
    [CreateAssetMenu(fileName = "LevelsContainer", menuName = "Level/LevelsDataContainer", order = 0)]
    public class LevelsContainer : ScriptableObject
    {
        [SerializeField]private SerializedDictionary<int, LevelData> levelData = new SerializedDictionary<int, LevelData>();

        public SerializedDictionary<int, LevelData> LevelData => levelData;
    }
}