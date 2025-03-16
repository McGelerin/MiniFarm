namespace Runtime.Models
{
    public interface IGameProgressModel
    {
        int Level { get; }

        void IncreaseLevel();
    }
}