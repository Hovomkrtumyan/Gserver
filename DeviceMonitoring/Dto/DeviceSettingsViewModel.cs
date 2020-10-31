﻿using System;

namespace DeviceMonitoring.Dto
{
    public class DeviceSettingsViewModel
    {
        public long _id { get; set; }
        public string Id { get; set; }
        public double Flowhanac { get; set; }
        public double Flowmax { get; set; }
        public double Flowproc { get; set; }
        public double Dpgorcakic { get; set; }
        public double Kgorcakic { get; set; }
        public double Pressgorcakic { get; set; }
        public bool Restart { get; set; }
        public bool Onoff { get; set; }
        public DateTime Date { get; set; }
    }
}