using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace LuckyLottoApp.Utilities
{
    public class SecureRandom : RandomNumberGenerator
    {
        private readonly RandomNumberGenerator random = Create();

        public int Next()
        {
            var data = new byte[sizeof(int)];
            random.GetBytes(data);
            return BitConverter.ToInt32(data, 0) & (int.MaxValue - 1);
        }

        public int Next(int maxValue)
        {
            return Next(0, maxValue);
        }

        public int Next(int minValue, int maxValue)
        {
            if (minValue >= maxValue)
            {
                throw new ArgumentOutOfRangeException();
            }

            maxValue = maxValue - 1;
            var bytes = new byte[sizeof(uint)];
            random.GetNonZeroBytes(bytes);
            uint val = BitConverter.ToUInt32(bytes);
            var result = ((val - minValue) % (maxValue - minValue + 1) + (maxValue - minValue + 1)) % (maxValue - minValue + 1) + minValue;
            return (int)result;
            //return (int)Math.Floor((minValue + ((double)maxValue - minValue) * NextDouble()));
        }

        public double NextDouble()
        {
            var data = new byte[sizeof(uint)];
            random.GetBytes(data);
            var randUInt = BitConverter.ToUInt32(data, 0);
            return randUInt / (uint.MaxValue + 1.0);
        }

        public override void GetNonZeroBytes(byte[] data)
        {
            random.GetNonZeroBytes(data);
        }

        public override void GetBytes(byte[] data)
        {
            random.GetBytes(data);
        }
    }
}
