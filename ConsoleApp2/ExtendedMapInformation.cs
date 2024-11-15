using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp2
{
    public struct ExtendedMapInformation
    {
        private const int MapFileConfigurationSize = 2;
        private const int MaxMultiSiteSize = 2;
        private const int MaxCategoriesSize = 2;
        private const int DoNotUseReservedSize = 2;


        [MarshalAs(UnmanagedType.ByValArray, SizeConst = MapFileConfigurationSize)]
        public byte[] Map_File_Configuration;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = MaxMultiSiteSize)]
        public byte[] Max_Multi_Site;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = MaxCategoriesSize)]
        public byte[] Max_Categories;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = DoNotUseReservedSize)]
        public byte[] Do_Not_Use_Reserved;

        public UInt16 MapFileConfiguration
        {
            get
            {
                return BitConverter.ToUInt16(WaferMapUtility.ReverseArray(Map_File_Configuration), 0);
            }
        }

        public UInt16 MaxMultiSite
        {
            get
            {
                return BitConverter.ToUInt16(WaferMapUtility.ReverseArray(Max_Multi_Site), 0);
            }
        }

        public UInt16 MaxCategories
        {
            get
            {
                return BitConverter.ToUInt16(WaferMapUtility.ReverseArray(Max_Categories), 0);
            }
        }
        public UInt16 DoNotUseReserved
        {
            get
            {
                return BitConverter.ToUInt16(WaferMapUtility.ReverseArray(Do_Not_Use_Reserved), 0);
            }
        }

        public void printf()
        {
            Console.WriteLine($"MapFileConfiguration={MapFileConfiguration}");
            Console.WriteLine($"MaxMultiSite={MaxMultiSite}");
            Console.WriteLine($"MaxMultiSite={MaxMultiSite}");
            Console.WriteLine($"DoNotUseReserved={DoNotUseReserved}");
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            
            sb.AppendLine($"MapFileConfiguration={MapFileConfiguration}");
            sb.AppendLine($"MaxMultiSite={MaxMultiSite}");
            sb.AppendLine($"MaxMultiSite={MaxMultiSite}");
            sb.AppendLine($"DoNotUseReserved={DoNotUseReserved}");
            return sb.ToString();
        }

        internal void init()
        {
           
            Map_File_Configuration = new byte[MapFileConfigurationSize];

            Max_Multi_Site = new byte[MaxMultiSiteSize];

            Max_Categories = new byte[MaxCategoriesSize];

            Do_Not_Use_Reserved = new byte[DoNotUseReservedSize];
     
        }

        internal IEnumerable<byte> toBinary()
        {
            List<byte> bytes = new List<byte>();
            bytes.AddRange(Map_File_Configuration);
            bytes.AddRange(Max_Multi_Site);
            bytes.AddRange(Max_Categories);
            bytes.AddRange(Do_Not_Use_Reserved);

            return bytes;
        }
    }
}
