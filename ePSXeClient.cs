using Archipelago.Core;
using Archipelago.Core.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Archipelago.ePSXe
{
    public class ePSXeClient : IGameClient
    {
        public bool IsConnected { get; set; }
        public int ProcId { get; set; }
        public string ProcessName { get; set; }

        public ePSXeClient()
        {
            ProcessName = "ePSXe";
            ProcId = Memory.GetProcIdFromExe(ProcessName);
        }
        public bool Connect()
        {
            Console.WriteLine($"Connecting to {ProcessName}");            
            if (ProcId == 0)
            {
                Console.WriteLine($"{ProcessName} not found.");
                return false;
            }
            return true;
        }
    }
}
