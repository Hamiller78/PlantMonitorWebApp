using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlantMonitorWebApp.Shared.Models
{
    public class StoredImage
    {
        public int Id { get; set; }
        public string FileName { get; set; } = string.Empty;
        public string MediaContentType { get; set; } = string.Empty;
    }
}
