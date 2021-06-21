using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Envision.WebApi.Models
{
    public class AircraftLogVM
    {
        public int Id { get; set; }
        public string Make { get; set; }
        public string Model { get; set; }
        public string Registration { get; set; }
        public string Location { get; set; }
        public string DateTime { get; set; }
        public int CreateBy { get; set; }
        public int ModifiedBy { get; set; }
        public string SeenDateTime { get; set; }
        public string base64image { get; set; }
    }
}
