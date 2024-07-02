using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Archipelago.ePSXe.Util
{
    public static class Helpers
    {

        public static T Random<T>(this IEnumerable<T> list) where T : struct
        {
            return list.ToList()[new Random().Next(0, list.Count())];
        }
        public static string OpenEmbeddedResource(string resourceName)
        {
            var assembly = Assembly.GetExecutingAssembly();
            using (Stream stream = assembly.GetManifestResourceStream(resourceName))
            using (StreamReader reader = new StreamReader(stream))
            {
                string jsonFile = reader.ReadToEnd();
                return jsonFile;
            }
        }
        public static async Task MonitorAddress(int address, bool reset = true)
        {
            var initialValue = Memory.ReadByte(address);
            var currentValue = initialValue;
            while (initialValue == currentValue)
            {
                currentValue = Memory.ReadByte(address);
                Thread.Sleep(10);
            }
            if (reset)
            {
                Memory.WriteByte(address, initialValue);
            }
            Console.WriteLine($"Memory value changed at address {address.ToString("X8")}");
        }
        public static async Task MonitorAddress(int address, int valueToCheck, bool reset = true)
        {
            var initialValue = Memory.ReadByte(address);
            var currentValue = initialValue;
            while (currentValue != valueToCheck)
            {
                currentValue = Memory.ReadByte(address);
                Thread.Sleep(10);
            }
            if (reset)
            {
                Memory.WriteByte(address, initialValue);
            }
        }
        public static async Task MonitorAddress(int address, int length, uint valueToCheck, bool reset = true)
        {
            var initialValue = BitConverter.ToUInt32(Memory.ReadByteArray(address, length));
            var currentValue = initialValue;
            while (currentValue != valueToCheck)
            {
                currentValue = BitConverter.ToUInt32(Memory.ReadByteArray(address, length));
                Thread.Sleep(1);
            }
            if (reset)
            {
                Memory.WriteByteArray(address, BitConverter.GetBytes(initialValue));
            }
        }
        public static async Task MonitorAddressBit(int address, int bit, bool reset = true)
        {
            byte initialValue = Memory.ReadByte(address);
            byte currentValue = initialValue;
            bool initialBitValue = GetBitValue(initialValue, bit);
            bool currentBitValue = initialBitValue;

            while (!currentBitValue)
            {
                currentValue = Memory.ReadByte(address);
                currentBitValue = GetBitValue(currentValue, bit);
                Thread.Sleep(10);
            }
            if (reset)
            {
                Memory.WriteByte(address, initialValue);
            }
            Console.WriteLine($"Memory value changed at address {address.ToString("X8")}, bit {bit}");
        }
        private static bool GetBitValue(byte value, int bitIndex)
        {
            return (value & (1 << bitIndex)) != 0;
        }

    }
}
