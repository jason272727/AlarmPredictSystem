using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace AMS_SideProject
{
    public class ReceiveParameter
    {
        public string ProductID { get; set; }
        public string Type { get; set; }
        public string Time { get; set; }
        public double Air_temperature { get; set; }
        public double Process_temperature { get; set; }
        public double Rotational_speed { get; set; }
        public double Torque { get; set; }
        public double Tool_wear { get; set; }       
    }
}
