using BowlingApp.Domain.Interfaces;

namespace BowlingApp.Service
{
    public abstract class BaseBowlingGameFactory : IBowlingGameFactory
    {
        public abstract IBowlingGame CreateBowlingGame();

        public IBowlingGame CreateStandardBowlingGame()
        {
            return new StandardBowling(); // Or create the instance of your standard game class
        }

        public IBowlingGame CreateTwelvePinBowlingGame()
        {
            return new TwelvePinBowling(); // Create the instance of your 12-pin game class
        }
    }
}
