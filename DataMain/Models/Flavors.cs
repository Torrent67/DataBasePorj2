using System.Collections.Generic;

namespace DataBase.Models
{
  public class Flavors
    {
        public Flavors()
        {
            this.Treats = new HashSet<FlavorTreat>();
        }

        public int FlavorsId { get; set; }
        public string Name { get; set; }
        public virtual ICollection<FlavorTreat> Treats { get; set; }
    }
}