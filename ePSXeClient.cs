using Archipelago.ePSXe.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Archipelago.ePSXe
{
    public class ePSXeClient
    {
    public bool IsConnected { get; set; }
        public ePSXeClient()
        {
            
        }
        public bool Connect()
        {
            Console.WriteLine("Connecting to ePSXe");
            var pid = Memory.EPSXE_PROCESSID;
            if (pid == 0)
            {
                Console.WriteLine("ePSXe not found.");
                Console.WriteLine("Press any key to exit.");
                Console.Read();
                System.Environment.Exit(0);
                return false;
            }
            return true;
        }
    }
}
