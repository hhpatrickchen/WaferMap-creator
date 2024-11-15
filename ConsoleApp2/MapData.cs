using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp2
{
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct MapData
    {
        public Header_Information header_Information;

        //[MarshalAs(UnmanagedType.ByValArray, SizeConst = 6)]
        public Test_Result_per_Die[] test_Result_Per_Dice;

        public byte[] ToBinary()
        {
            List<byte> bytes = new List<byte>();

            List<byte> headerbin = header_Information.toBinary();
            bytes.AddRange(headerbin);

            for (int i = 0; i < test_Result_Per_Dice.Length; i++)
            {
                bytes.AddRange(test_Result_Per_Dice[i].ToBinary());
            }
            

            return bytes.ToArray();
        }

    }

    
    
}
