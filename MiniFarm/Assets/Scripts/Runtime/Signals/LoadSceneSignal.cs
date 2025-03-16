using Runtime.Identifiers;

namespace Runtime.Signals
{
    public readonly struct LoadSceneSignal
    {
        private readonly SceneID _sceneIDID;
        private readonly bool _useNetworkManager;
        public SceneID SceneIdid => _sceneIDID;
        public bool UseNetworkManager => _useNetworkManager;
        
        public LoadSceneSignal(SceneID sceneIDID, bool useNetworkManager = false)
        {
            _sceneIDID = sceneIDID;
            _useNetworkManager = useNetworkManager;
        }
    }
}