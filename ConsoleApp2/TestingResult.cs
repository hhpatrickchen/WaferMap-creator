using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp2
{
    public struct TestingResult
    {
        private const int TestingEndInformationSize = 1;
        private const int Reserved1Size = 1;
        private const int TotalTestedDiceSize = 2;
        private const int TotalPassDiceSize = 2;
        private const int TotalFailDiceSize = 2;


        [MarshalAs(UnmanagedType.ByValArray, SizeConst = TestingEndInformationSize)]
        public byte[] Testing_End_Information;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = Reserved1Size)]
        public byte[] Reserved_1;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = TotalTestedDiceSize)]
        public byte[] Total_Tested_Dice;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = TotalPassDiceSize)]
        public byte[] Total_Pass_Dice;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = TotalFailDiceSize)]
        public byte[] Total_Fail_Dice;

        public byte TestingEndInformation
        {
            get
            {
                return Testing_End_Information[0];
            }
        }

        public byte Reserved1
        {
            get
            {
                return Reserved_1[0];
            }
        }
        public UInt16 TotalTestedDice
        {
            get
            {
                return BitConverter.ToUInt16(WaferMapUtility.ReverseArray(Total_Tested_Dice), 0);
            }
        }

        public UInt16 TotalPassDice
        {
            get
            {
                return BitConverter.ToUInt16(WaferMapUtility.ReverseArray(Total_Pass_Dice), 0);
            }
        }

        public UInt16 TotalFailDice
        {
            get
            {
                return BitConverter.ToUInt16(WaferMapUtility.ReverseArray(Total_Fail_Dice), 0);
            }
        }

        public void printf()
        {
            Console.WriteLine($"TestingEndInformation={TestingEndInformation}");
            Console.WriteLine($"Reserved1={Reserved1}");
            Console.WriteLine($"TotalTestedDice={TotalTestedDice}");
            Console.WriteLine($"TotalPassDice={TotalPassDice}");
            Console.WriteLine($"TotalFailDice={TotalFailDice}");
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine($"TestingEndInformation={TestingEndInformation}");
            sb.AppendLine($"Reserved1={Reserved1}");
            sb.AppendLine($"TotalTestedDice={TotalTestedDice}");
            sb.AppendLine($"TotalPassDice={TotalPassDice}");
            sb.AppendLine($"TotalFailDice={TotalFailDice}");
            return sb.ToString();
        }

        internal void init()
        {
            Testing_End_Information = new byte[TestingEndInformationSize];
            Reserved_1 = new byte[Reserved1Size];
            Total_Tested_Dice = new byte[TotalTestedDiceSize];
            Total_Pass_Dice = new byte[TotalPassDiceSize];
            Total_Fail_Dice = new byte[TotalFailDiceSize];

        }

        internal IEnumerable<byte> toBinary()
        {
            List<byte> bytes = new List<byte>();
            bytes.AddRange(Testing_End_Information);
            bytes.AddRange(Reserved_1);
            bytes.AddRange(Total_Tested_Dice);
            bytes.AddRange(Total_Pass_Dice);
            bytes.AddRange(Total_Fail_Dice);

            return bytes;
        }
    }
}
