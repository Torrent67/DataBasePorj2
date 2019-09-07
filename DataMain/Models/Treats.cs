using System;
using System.Collections.Generic;

namespace DataBase.Models
{
    public class Treat
    {
        public Treat()
        {
            this.Flavors = new HashSet<FlavorTreat>();
        }

        public int TreatId { get; set; }
        public string Name { get; set; }
        public virtual ApplicationUser User { get; set; }

        public virtual ICollection<FlavorTreat> Flavors { get; }
    }
}