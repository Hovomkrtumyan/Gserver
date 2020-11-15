using System.ComponentModel.DataAnnotations;

namespace DeviceMonitoring.Entities
{
    public class DeviceSettings : BaseEntity
    {
        [Required]
        public string DeviceId { get; set; }
        [Required]
        public double Flowhanac { get; set; }
        [Required]
        public double Flowmax { get; set; }
        [Required]
        public double Flowproc { get; set; }
        [Required]
        public double Dpgorcakic { get; set; }
        [Required]
        public double Kgorcakic { get; set; }
        [Required]
        public double Pressgorcakic { get; set; }
        [Required]
        public bool Onoff { get; set; }
    }
}