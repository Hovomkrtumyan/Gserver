using System;

namespace DeviceMonitoring.Dto
{
    public class DeviceDataViewModel
    {
        public string _id { get; set; }
        public string Id { get; set; }
        public double Flowpast { get; set; }
        public double Flowsarqac { get; set; }
        public double Flowhanac { get; set; }
        public double Flowmax { get; set; }
        public double Flowproc { get; set; }
        public double Dppastaci { get; set; }
        public double Dpdrac { get; set; }
        public double Dpgorcakic { get; set; }
        public double Kgorcakic { get; set; }
        public double Onoff { get; set; }
        public double Selfonoff { get; set; }
        public double Presspastaci { get; set; }
        public double Pressgorcakic { get; set; }
        public DateTime Date { get; set; }
        public bool Disconnected { get; set; }
    }
}