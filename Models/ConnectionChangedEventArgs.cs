using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Archipelago.ePSXe.Models
{
    public class ConnectionChangedEventArgs : EventArgs
    {
        public bool Connected { get; set; }
        public ConnectionChangedEventArgs(bool connected)
        {
            Connected = connected;
        }
    }
}
