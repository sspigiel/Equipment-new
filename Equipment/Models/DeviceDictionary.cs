using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;

namespace Equipment.Models
{
    public class DeviceDictionary
    {      
        public int DeviceDictionaryId { get; set; }
        [Required]
        public string DeviceManufacturer { get; set; }
        [Required]
        public string DeviceName { get; set; }
    }

}