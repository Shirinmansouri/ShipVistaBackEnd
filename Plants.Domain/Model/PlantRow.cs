using Plants.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Plants.Domain.Model
{
    public class PlantRow
    {
        public string Name { get; set; }
        public DateTime LastWateringDate { get; set; }
        public byte[] plantImage { get; set; }
        public long Id { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string UpdatedBy { get; set; }
        public PlantsStatus plantsStatus { get; set; }
        public string statusName { get; set; }
    }
}
