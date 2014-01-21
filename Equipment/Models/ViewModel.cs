using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Equipment.Models
{
    public class ViewModel
    {
        public List<InnerClass> data { get; set; }
        public string DeviceUser  {get;set;}
        public int DeviceId {get;set;}
        public int number {get;set;}
       
    }
    public class InnerClass
    {
        public string DeviceSerialNumber { get; set; }
        public string Batch { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
    }
}

