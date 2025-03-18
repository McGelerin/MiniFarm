namespace Runtime.Signals.SliderArea
{
    public readonly struct StartTimerSignal
    {
        private readonly float _countdownTime;
        public float CountdownTime => _countdownTime;

        public StartTimerSignal(float countdownTime)
        {
            _countdownTime = countdownTime;
        }
    }
}