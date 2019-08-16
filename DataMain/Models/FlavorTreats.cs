namespace DataBase.Models
{
    public class FlavorTreats
    {
        public int FlavorTreatsId { get; set; }
        public int TreatId { get; set; }
        public int FlavorId { get; set; }
        public Book Treat { get; set; }
        public Genre Flavor { get; set; }
    }
}