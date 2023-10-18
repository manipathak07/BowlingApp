namespace BowlingApp.Domain.Interfaces
{
    public interface IBowlingGameFactory
    {
       IBowlingGame CreateStandardBowlingGame();
    IBowlingGame CreateTwelvePinBowlingGame();
    }
}

