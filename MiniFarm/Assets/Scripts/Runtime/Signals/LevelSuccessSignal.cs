namespace Runtime.Signals
{
    public readonly struct LevelSuccessSignal
    {
        private readonly int _levelIndex;
        public int LevelIndex => _levelIndex;

        public LevelSuccessSignal(int levelIndex)
        {
            _levelIndex = levelIndex;
        }
    }
}