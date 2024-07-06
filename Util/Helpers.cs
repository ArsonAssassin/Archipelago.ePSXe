using Archipelago.ePSXe.Models;
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
        public static async Task MonitorAddress(int address, LocationCheckCompareType compareType, bool reset = true)
        {
            var initialValue = Memory.ReadByte(address);
            var currentValue = initialValue;
            if (compareType == LocationCheckCompareType.Match)
            {
                while (!(initialValue == currentValue))
                {
                    currentValue = Memory.ReadByte(address);
                    Thread.Sleep(10);
                }
            }
            else if (compareType == LocationCheckCompareType.GreaterThan)
            {
                while (!(initialValue > currentValue))
                {
                    currentValue = Memory.ReadByte(address);
                    Thread.Sleep(10);
                }
            }
            else if (compareType == LocationCheckCompareType.LessThan)
            {
                while (!(initialValue < currentValue))
                {
                    currentValue = Memory.ReadByte(address);
                    Thread.Sleep(10);
                }
            }
            else if (compareType == LocationCheckCompareType.Range)
            {
                throw new NotImplementedException("Range checks are not supported yet");
            }
            if (reset)
            {
                Memory.WriteByte(address, initialValue);
            }
            Console.WriteLine($"Memory value changed at address {address.ToString("X8")}");
        }
        public static async Task MonitorAddress(int address, int valueToCheck, LocationCheckCompareType compareType, bool reset = true)
        {
            var initialValue = Memory.ReadByte(address);
            var currentValue = initialValue;
            if (compareType == LocationCheckCompareType.Match)
            {
                while (!(currentValue == valueToCheck))
                {
                    currentValue = Memory.ReadByte(address);
                    Thread.Sleep(10);
                }
            }
            else if(compareType == LocationCheckCompareType.GreaterThan)
            {
                while (!(currentValue > valueToCheck))
                {
                    currentValue = Memory.ReadByte(address);
                    Thread.Sleep(10);
                }
            }
            else if (compareType == LocationCheckCompareType.LessThan)
            {
                while (!(currentValue < valueToCheck))
                {
                    currentValue = Memory.ReadByte(address);
                    Thread.Sleep(10);
                }
            }
            else if (compareType == LocationCheckCompareType.Range)
            {
                throw new NotImplementedException("Range checks are not supported yet");

            }
            if (reset)
            {
                Memory.WriteByte(address, initialValue);
            }
        }
        public static async Task MonitorAddress(int address, int length, uint valueToCheck, LocationCheckCompareType compareType, bool reset = true)
        {
            var initialValue = BitConverter.ToUInt32(Memory.ReadByteArray(address, length));
            var currentValue = initialValue;
            if (compareType == LocationCheckCompareType.Match)
            {
                while (!(currentValue == valueToCheck))
                {
                    currentValue = BitConverter.ToUInt32(Memory.ReadByteArray(address, length));
                    Thread.Sleep(1);
                }
            }
            else if(compareType == LocationCheckCompareType.GreaterThan)
            {
                while (!(currentValue > valueToCheck))
                {
                    currentValue = BitConverter.ToUInt32(Memory.ReadByteArray(address, length));
                    Thread.Sleep(1);
                }

            }
            else if (compareType == LocationCheckCompareType.LessThan)
            {
                while (!(currentValue < valueToCheck))
                {
                    currentValue = BitConverter.ToUInt32(Memory.ReadByteArray(address, length));
                    Thread.Sleep(1);
                }
            }
            else if (compareType == LocationCheckCompareType.Range)
            {
                throw new NotImplementedException("Range checks are not supported yet");

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
