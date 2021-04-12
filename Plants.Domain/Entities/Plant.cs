using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using System.Text;

namespace Plants.Domain.Entities
{
    public class Plant : AuditableEntity<long>
    {
        [StringLength(100)]
        [DataMember]
        [Required]
        public string Name { get; set; }
        public DateTime LastWateringDate { get; set; }
        public byte[] plantImage { get; set; }
    }
}