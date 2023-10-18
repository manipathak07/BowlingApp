using BowlingApp.Domain.Interfaces;

namespace BowlingApp.Service
{
    public class TwelvePinBowling : IBowlingGame
    {
        private List<int> rolls; // Instance member to maintain state within a single request

        public TwelvePinBowling()
        {
            // Initialize the rolls list when a new instance is created for a request
            rolls = new List<int>();
        }

        public void Roll(int pins)
        {

            rolls.Add(pins);
        }

        public int GetTotalScore()
        {
            try
            {
                int totalScore = 0;
                int rollIndex = 0;

                for (int frame = 0; frame < 9; frame++)
                {
                    if (IsStrike(rollIndex))
                    {
                        totalScore += 12 + GetStrikeBonus(rollIndex);
                        rollIndex += 2;
                    }
                    else if (IsSpare(rollIndex))
                    {
                        totalScore += 12 + GetSpareBonus(rollIndex);
                        rollIndex += 2;
                    }
                    else
                    {
                        totalScore += GetFrameScore(rollIndex);
                        rollIndex += 2;
                    }
                }

                // handled the last frame so that it will not send the the range out of bound
                while (rollIndex < rolls.Count)
                {
                    totalScore += rolls[rollIndex];
                    rollIndex++;
                }

                return totalScore;
            }
            catch (Exception)
            {
                throw;
            }
        }


        private bool IsStrike(int rollIndex)
        {
            if (rollIndex < 0 || rollIndex >= rolls.Count)
            {
                return false; // This is not a valid rollIndex
            }
            return rolls[rollIndex] == 12;
        }

        private bool IsSpare(int rollIndex)
        {
            return rolls[rollIndex] + rolls[rollIndex + 1] == 12;
        }

        private int GetStrikeBonus(int rollIndex)
        {
            return rolls[rollIndex + 2] + rolls[rollIndex + 3];
        }

        private int GetSpareBonus(int rollIndex)
        {
            return rolls[rollIndex + 2];
        }

        private int GetFrameScore(int rollIndex)
        {
            return rolls[rollIndex] + rolls[rollIndex + 1];
        }
    }


}
