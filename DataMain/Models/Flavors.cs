using System.Collections.Generic;

namespace DataBase.Models
{
    [Table("Flavors")]
    public class Flavor
    {
        [Key]
        public int FlavorId { get; set; }
        public string Description { get; set; }

        public virtual ApplicationUser User { get; set; }

        public ICollection<FlavorTreat> Treats { get; }


        public Flavor()
        {
            this.Treats = new HashSet<FlavorTreat>();
        }
    }

}