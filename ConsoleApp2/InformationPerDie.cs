using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp2
{
    public struct InformationPerDie
    {
        private const int FirstDieCoordinatorXSize = 4;
        private const int FirstDieCoordinatorYSize = 4;


        [MarshalAs(UnmanagedType.ByValArray, SizeConst = FirstDieCoordinatorXSize)]
        public byte[] First_Die_Coordinator_X;

        
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = FirstDieCoordinatorYSize)]
        public byte[] First_Die_Coordinator_Y;

        public UInt32 FirstDieCoordinatorX
        {
            get
            {
                return BitConverter.ToUInt32(WaferMapUtility.ReverseArray(First_Die_Coordinator_X), 0);
            }
        }
        public UInt32 FirstDieCoordinatorY
        {
            get
            {
                return BitConverter.ToUInt32(WaferMapUtility.ReverseArray(First_Die_Coordinator_Y), 0);
            }
        }

        public void printf()
        {
            Console.WriteLine($"FirstDieCoordinatorX={FirstDieCoordinatorX}");
            Console.WriteLine($"FirstDieCoordinatorY={FirstDieCoordinatorY}");
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"FirstDieCoordinatorX={FirstDieCoordinatorX}");
            sb.AppendLine($"FirstDieCoordinatorY={FirstDieCoordinatorY}");
            return sb.ToString();
        }

        internal void init()
        {
            First_Die_Coordinator_X = new byte[FirstDieCoordinatorXSize];
            First_Die_Coordinator_Y = new byte[FirstDieCoordinatorYSize];
        }

        internal IEnumerable<byte> toBinary()
        {
            List<byte> bytes = new List<byte>();
            bytes.AddRange(First_Die_Coordinator_X);
            bytes.AddRange(First_Die_Coordinator_Y);
            return bytes;
        }
    }
}
