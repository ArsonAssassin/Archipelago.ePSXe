using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Archipelago.ePSXe.Models
{
    public class ItemReceivedEventArgs : EventArgs
    {
        public Item Item { get; set; }
        public int LocationId { get; set; }
    }
}
