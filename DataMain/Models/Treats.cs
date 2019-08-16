using System.Collections.Generic;

namespace DataBase.Models
{
    public class Treat
    {
        public Treat()
        {
            this.Flavor = new HashSet<FlavorTreat>();
        }

        public int TreatId { get; set; }
        public string Title { get; set; }

        public ICollection<FlavorTreat> Flavor { get; }
    }
}