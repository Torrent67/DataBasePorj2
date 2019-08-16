using System.Collections.Generic;

namespace DataBase.Models
{
    public class Flavors
    {
        public Flavors()
        {
            this.Treats = new HashSet<FlavorsTreat>();
        }

        public int FlavorsId { get; set; }
        public string Name { get; set; }
        public virtual ICollection<FlavorsTreat> Treats { get; set; }
    }
}