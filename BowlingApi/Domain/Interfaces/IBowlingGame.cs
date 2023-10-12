namespace BowlingApp.Domain.Interfaces
{
    public interface IBowlingGame
    {
        void Roll(int pins);
        int GetTotalScore();
    }
}
