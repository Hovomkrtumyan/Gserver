using System;

namespace DeviceMonitoring.Entities
{
    public class BaseEntity
    {
        public long Id { get; set; }
        public DateTime CreatedDt { get; set; }
        public DateTime UpdatedDt { get; set; }
    }
}