using BowlingApp.Domain.Interfaces;
using System;
using System.Collections.Generic;

namespace BowlingApp.Service
{
    public class Bowling: IBowlingGame
    {
        private List<int> rolls; // Instance member to maintain state within a single request

        public Bowling()
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

                for (int frame = 0; frame < 10; frame++)
                {
                    if (IsStrike(rollIndex) && rollIndex < rolls.Count - 2)
                    {
                        totalScore += 10 + GetStrikeBonus(rollIndex);
                        rollIndex++;
                    }
                    else if (IsSpare(rollIndex) && rollIndex < rolls.Count - 1)
                    {
                        totalScore += 10 + GetSpareBonus(rollIndex);
                        rollIndex += 2;
                    }
                    else if (rollIndex < rolls.Count - 1)
                    {
                        totalScore += GetFrameScore(rollIndex);
                        rollIndex += 2;
                    }
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
            return rolls[rollIndex] == 10;
        }

        private bool IsSpare(int rollIndex)
        {
            return rolls[rollIndex] + rolls[rollIndex + 1] == 10;
        }

        private int GetStrikeBonus(int rollIndex)
        {
            return rolls[rollIndex + 1] + rolls[rollIndex + 2];
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
