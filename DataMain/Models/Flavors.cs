using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace DataBase.Models
{
    
    public class Flavors
    {
        [Key]      
        public int FlavorId { get; set; }
        public string Description { get; set; }

        public virtual ApplicationUser User { get; set; }

        public virtual ICollection<FlavorTreat> Treats { get; }


        public Flavors()
        {
            this.Treats = new HashSet<FlavorTreat>();
        }
    }

}