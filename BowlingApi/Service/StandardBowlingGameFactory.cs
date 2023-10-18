using BowlingApp.Domain.Interfaces;

namespace BowlingApp.Service
{
    public class StandardBowlingGameFactory : BaseBowlingGameFactory
    {
        public override IBowlingGame CreateBowlingGame()
        {
            return CreateStandardBowlingGame();
        }
    }
}
