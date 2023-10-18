namespace BowlingApp.Domain.Models
{
    public class RollInputModel
    {

        public int[] Rolls { get; set; }

        public bool RetrieveScore { get; set; }
        public bool UseTwelvePins { get; set; }
    }
}
