using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp2
{
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct Header_Information
    {
        private const int MapDataAreaRowSizeSize = 2;
        private const int MapDataAreaLineSizeSize = 2;

        private const int MapDataFormSize = 4;

        private const int MachineNo1Size = 4;
        private const int MachineNo2Size = 4;
        private const int SpecialCharactersSize = 4;
        private const int TestDieInformationAddressSize = 4;
        private const int NumberOfLineCategoryDataSize = 4;
        private const int LineCategoryAddressSize = 4;
        

        //Content
        public Wafer_Testing wafer_Testing;
        
        public byte Map_Version;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = MapDataAreaRowSizeSize)]
        public byte[] Map_Data_Area_Row_Size;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = MapDataAreaLineSizeSize)]
        public byte[] Map_Data_Area_Line_Size;

        
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = MapDataFormSize)]
        public byte[] Map_Data_Form;

        public WaferSpecificData wafer_Specific_Data;
        public WaferProbingCoordinateSystemData waferProbingCoordinateSystemData;
        public InformationPerDie informationPerDie;
        public WaferTestingTimeData waferTestingStartTimeData;
        public WaferTestingTimeData waferTestingEndTimeData;
        public WaferTestingTimeData WaferLoadingTimeData;
        public WaferTestingTimeData WaferUnLoadingTimeData;


        [MarshalAs(UnmanagedType.ByValArray, SizeConst = MachineNo1Size)]
        public byte[] Machine_No1;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = MachineNo2Size)]
        public byte[] Machine_No2;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = SpecialCharactersSize)]
        public byte[] Special_Characters;

        public TestingResult testingResult;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = TestDieInformationAddressSize)]
        public byte[] Test_Die_Information_Address;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = NumberOfLineCategoryDataSize)]
        public byte[] Number_Of_Line_Category_Data;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = LineCategoryAddressSize)]
        public byte[] Line_Category_Address;



        public ExtendedMapInformation extendedMapInformation;

        //Property
        public UInt16 MapDataAreaRowSize
        { 
            get
            {
                return BitConverter.ToUInt16(WaferMapUtility.ReverseArray(Map_Data_Area_Row_Size), 0);
            }
            set
            {
                Map_Data_Area_Row_Size = WaferMapUtility.ReverseArray(BitConverter.GetBytes(value));
            }
        }
        public UInt16 MapDataAreaLineSize
        {
            get
            {
                return BitConverter.ToUInt16(WaferMapUtility.ReverseArray(Map_Data_Area_Line_Size), 0);
            }
            set
            {
                Map_Data_Area_Line_Size = WaferMapUtility.ReverseArray(BitConverter.GetBytes(value));
            }
        }

        /// <summary>
        /// //0:6 byte, 1:1 byte, 2:2 byte, 3:3 byte
        /// </summary>
        ///
        public UInt32 MapDataForm
        {
            get
            {
                return BitConverter.ToUInt32(WaferMapUtility.ReverseArray(Map_Data_Form), 0);
            }
            set
            {
                Map_Data_Form = WaferMapUtility.ReverseArray(BitConverter.GetBytes(value));
            }
        }
        public UInt32 MachineNo1
        {
            get
            {
                return BitConverter.ToUInt32(WaferMapUtility.ReverseArray(Machine_No1), 0);
            }
            set
            {
                Machine_No1 = WaferMapUtility.ReverseArray(BitConverter.GetBytes(value));
            }
        }

        public UInt32 MachineNo2
        {
            get
            {
                return BitConverter.ToUInt32(WaferMapUtility.ReverseArray(Machine_No2), 0);
            }
            set
            {
                Machine_No2 = WaferMapUtility.ReverseArray(BitConverter.GetBytes(value));
            }
        }

        public UInt32 SpecialCharacters
        {
            get
            {
                return BitConverter.ToUInt32(WaferMapUtility.ReverseArray(Special_Characters), 0);
            }
            set
            {
                Special_Characters = WaferMapUtility.ReverseArray(BitConverter.GetBytes(value));
            }
        }


           
        public UInt32 TestDieInformationAddress
        {
            get
            {
                return BitConverter.ToUInt32(WaferMapUtility.ReverseArray(Test_Die_Information_Address), 0);
            }
            set
            {
                Test_Die_Information_Address = WaferMapUtility.ReverseArray(BitConverter.GetBytes(value));
            }
        }

        public UInt32 NumberO_LineCategoryData
        {
            get
            {
                return BitConverter.ToUInt32(WaferMapUtility.ReverseArray(Number_Of_Line_Category_Data), 0);
            }
            set
            {
                Number_Of_Line_Category_Data = WaferMapUtility.ReverseArray(BitConverter.GetBytes(value));
            }
        }

        public UInt32 LineCategoryAddress
        {
            get
            {
                return BitConverter.ToUInt32(WaferMapUtility.ReverseArray(Line_Category_Address), 0);
            }
            set
            {
                Line_Category_Address = WaferMapUtility.ReverseArray(BitConverter.GetBytes(value));
            }
        }
        public void init(ushort row, ushort line)
        {
            wafer_Testing.init();
            Map_Version = 0x0;

            Map_Data_Area_Row_Size = new byte[MapDataAreaRowSizeSize];
                        
            Map_Data_Area_Line_Size = new byte[MapDataAreaLineSizeSize];


            Map_Data_Form = new byte[MapDataFormSize];
            

            wafer_Specific_Data.init();
            waferProbingCoordinateSystemData.init();

            informationPerDie.init();

            waferTestingStartTimeData.init();
            waferTestingEndTimeData.init();

            WaferLoadingTimeData.init();
            WaferUnLoadingTimeData.init();


            Machine_No1 = new byte[MachineNo1Size];
            Machine_No2 = new byte[MachineNo2Size];

            Special_Characters = new byte[SpecialCharactersSize];
            

            testingResult.init();

            Test_Die_Information_Address = new byte[TestDieInformationAddressSize];
            Number_Of_Line_Category_Data = new byte[NumberOfLineCategoryDataSize];
            Line_Category_Address = new byte[LineCategoryAddressSize];


            extendedMapInformation.init();

          
            //set wafer map size
            MapDataAreaRowSize = row;
            MapDataAreaLineSize = line;


        }
        public void printf()
        {
            wafer_Testing.printf();

            Console.WriteLine($"Map_Version={Map_Version}");
            Console.WriteLine($"MapDataAreaRowSize={MapDataAreaRowSize}");
            Console.WriteLine($"MapDataAreaLineSize={MapDataAreaLineSize}");
            Console.WriteLine($"MapMapDataForm_Version={MapDataForm}");


            wafer_Specific_Data.printf();
            waferProbingCoordinateSystemData.printf();

            informationPerDie.printf();
            waferTestingStartTimeData.printf("start");
            waferTestingEndTimeData.printf("end");

            WaferLoadingTimeData.printf("load");
            WaferUnLoadingTimeData.printf("unload");

            Console.WriteLine($"MachineNo1={MachineNo1}");
            Console.WriteLine($"MachineNo2={MachineNo2}");
            Console.WriteLine($"SpecialCharacters={SpecialCharacters}");

            testingResult.printf();

            Console.WriteLine($"TestDieInformationAddress={TestDieInformationAddress}");
            Console.WriteLine($"NumberO_LineCategoryData={NumberO_LineCategoryData}");
            Console.WriteLine($"LineCategoryAddress={LineCategoryAddress}");

            extendedMapInformation.printf();
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(wafer_Testing.ToString());
            
            sb.AppendLine($"Map_Version={Map_Version}");
            sb.AppendLine($"MapDataAreaRowSize={MapDataAreaRowSize}");
            sb.AppendLine($"MapDataAreaLineSize={MapDataAreaLineSize}");
            sb.AppendLine($"MapMapDataForm_Version={MapDataForm}");


            sb.AppendLine(wafer_Specific_Data.ToString());
            sb.AppendLine(waferProbingCoordinateSystemData.ToString());

            sb.AppendLine(informationPerDie.ToString());
            sb.AppendLine(waferTestingStartTimeData.toString("start"));
            sb.AppendLine(waferTestingEndTimeData.toString("end"));

            sb.AppendLine(WaferLoadingTimeData.toString("load"));
            sb.AppendLine(WaferUnLoadingTimeData.toString("unload"));

            sb.AppendLine($"MachineNo1={MachineNo1}");
            sb.AppendLine($"MachineNo2={MachineNo2}");
            sb.AppendLine($"SpecialCharacters={SpecialCharacters}");

            sb.AppendLine(testingResult.ToString());

            sb.AppendLine($"TestDieInformationAddress={TestDieInformationAddress}");
            sb.AppendLine($"NumberO_LineCategoryData={NumberO_LineCategoryData}");
            sb.AppendLine($"LineCategoryAddress={LineCategoryAddress}");

            sb.AppendLine(extendedMapInformation.ToString());

            return sb.ToString();
        }

        internal List<byte> toBinary()
        {
            List<byte> bytes = new List<byte>();
            bytes.AddRange(wafer_Testing.toBinary());
            bytes.Add(Map_Version);
            bytes.AddRange(Map_Data_Area_Row_Size);
            bytes.AddRange(Map_Data_Area_Line_Size);
            bytes.AddRange(Map_Data_Form);


            bytes.AddRange(wafer_Specific_Data.toBinary());
            bytes.AddRange(waferProbingCoordinateSystemData.toBinary());


            bytes.AddRange(informationPerDie.toBinary());
            bytes.AddRange(waferTestingStartTimeData.toBinary());
            bytes.AddRange(waferTestingEndTimeData.toBinary());
            bytes.AddRange(WaferLoadingTimeData.toBinary());
            bytes.AddRange(WaferUnLoadingTimeData.toBinary());

            bytes.AddRange(Machine_No1);
            bytes.AddRange(Machine_No2);
            bytes.AddRange(Special_Characters);


            bytes.AddRange(testingResult.toBinary());

            bytes.AddRange(Test_Die_Information_Address);
            bytes.AddRange(Number_Of_Line_Category_Data);
            bytes.AddRange(Line_Category_Address);


            bytes.AddRange(extendedMapInformation.toBinary());

            Console.WriteLine($"Header information size={bytes.Count}");
            return bytes;
        }
    }
}
