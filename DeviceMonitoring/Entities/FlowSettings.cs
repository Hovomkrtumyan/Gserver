using System.ComponentModel.DataAnnotations;

namespace DeviceMonitoring.Entities
{
    public class FlowSettings : BaseEntity
    {
        [Required]
        public double FlowAuto { get; set; }
        [Required]
        public bool On { get; set; }
    }
}