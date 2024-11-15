using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp2
{
    public struct WaferSpecificData
    {
        private const int WaferIDSize = 21;
        private const int NumberOfProbingSize =  1;
        private const int LotNoSize = 18;
        private const int CassetteNoSize =  2;
        private const int SlotNoSize =  2;
        
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = WaferIDSize)]
        public byte[] Wafer_ID;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = NumberOfProbingSize)]
        public byte[] Number_Of_Probing;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = LotNoSize)]
        public byte[] Lot_No;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = CassetteNoSize)]
        public byte[] Cassette_No;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = SlotNoSize)]
        public byte[] Slot_No;


        public string WaferID
        {
            get
            {
                return Encoding.ASCII.GetString(Wafer_ID).TrimEnd('\0');                
            }
        }

        public byte NumberOfProbing
        {
            get
            {
                return Number_Of_Probing[0];
            }
        }
        public string LotNo
        {
            get
            {
                return Encoding.ASCII.GetString(Lot_No).TrimEnd('\0');
            }
        }

        public UInt16 CassetteNo
        {
            get
            {
                return BitConverter.ToUInt16(WaferMapUtility.ReverseArray(Cassette_No), 0);
            }
        }
        public UInt16 SlotNo
        {
            get
            {
                return BitConverter.ToUInt16(WaferMapUtility.ReverseArray(Slot_No), 0);
            }
        }

        public void printf()
        {
            Console.WriteLine($"WaferID={WaferID}");
            Console.WriteLine($"NumberOfProbing={NumberOfProbing}");
            Console.WriteLine($"LotNo={LotNo}");
            Console.WriteLine($"CassetteNo={CassetteNo}");
            Console.WriteLine($"SlotNo={SlotNo}");
        }
        internal byte[] toBinary()
        {
            List<byte> bins = new List<byte>();
            bins.AddRange(Wafer_ID);
            bins.AddRange(Number_Of_Probing);
            bins.AddRange(Lot_No);
            bins.AddRange(Cassette_No);
            bins.AddRange(Slot_No);

            return bins.ToArray();
        }
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine($"WaferID={WaferID}");
            sb.AppendLine($"NumberOfProbing={NumberOfProbing}");
            sb.AppendLine($"LotNo={LotNo}");
            sb.AppendLine($"CassetteNo={CassetteNo}");
            sb.AppendLine($"SlotNo={SlotNo}");
            return sb.ToString();
        }

        internal void init()
        {
            Wafer_ID = new byte[WaferIDSize];
            Number_Of_Probing = new byte[NumberOfProbingSize];
            Lot_No = new byte[LotNoSize];
            Cassette_No = new byte[CassetteNoSize];
            Slot_No = new byte[SlotNoSize];
        }
    }
}
