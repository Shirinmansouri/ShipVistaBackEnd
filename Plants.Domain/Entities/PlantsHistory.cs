using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Plants.Domain.Entities
{
    public class PlantsHistory : AuditableEntity<long>
    {
     
 
        public Plant plants { get; set; }
        [ForeignKey("plantsId")]
        public long plantsId { get; set; }
    }
}
