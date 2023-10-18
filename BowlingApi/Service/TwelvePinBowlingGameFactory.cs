using BowlingApp.Domain.Interfaces;

namespace BowlingApp.Service
{
    public class TwelvePinBowlingGameFactory :BaseBowlingGameFactory
    {
        public override IBowlingGame CreateBowlingGame()
        {
            return CreateTwelvePinBowlingGame();
        }
    }
}
