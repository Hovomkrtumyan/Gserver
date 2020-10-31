using System.ComponentModel.DataAnnotations;

namespace DeviceMonitoring.Entities
{
    public class DeviceData : BaseEntity
    {
        public string DeviceId { get; set; }
        [Required]
        public double Flowpast { get; set; }
        [Required]
        public double Flowsarqac { get; set; }
        [Required]
        public double Flowhanac { get; set; }
        [Required]
        public double Flowmax { get; set; }
        [Required]
        public double Flowproc { get; set; }
        [Required]
        public double Dppastaci { get; set; }
        [Required]
        public double Dpdrac { get; set; }
        [Required]
        public double Dpgorcakic { get; set; }
        [Required]
        public double Kgorcakic { get; set; }
        [Required]
        public double Onoff { get; set; }
        [Required]
        public double Selfonoff { get; set; }
        [Required]
        public double Presspastaci { get; set; }
        [Required]
        public double Pressgorcakic { get; set; }
        [Required]
        public double FlowAuto { get; set; }
    }
}