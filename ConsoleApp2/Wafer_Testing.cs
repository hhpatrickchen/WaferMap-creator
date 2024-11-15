using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp2
{
    public struct Wafer_Testing
    {

        private const int OperatorNameSize = 20;
        private const int DeviceNameSize = 16;
        private const int WaferSizeSize = 2;
        private const int MachineNoSize = 2;
        private const int IndexSizeXSize = 4;
        private const int IndexSizeYSize = 4;
        private const int StandardOrientationFlatDirectionSize = 2;
        private const int FinalEditingMachineTypeSize = 1;


        [MarshalAs(UnmanagedType.ByValArray, SizeConst = OperatorNameSize)]
        public byte[] Operator_Name;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = DeviceNameSize)]
        public byte[] Device_Name;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = WaferSizeSize)]
        public byte[] Wafer_Size;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = MachineNoSize)]
        public byte[] Machine_No;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = IndexSizeXSize)]
        public byte[] Index_Size_X;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = IndexSizeYSize)]
        public byte[] Index_Size_Y;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = StandardOrientationFlatDirectionSize)]
        public byte[] Standard_Orientation_Flat_Direction;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = FinalEditingMachineTypeSize)]
        public byte[] Final_Editing_Machine_type;

        public string OperatorName
        {
            get
            {

                return Encoding.ASCII.GetString(Operator_Name).TrimEnd('\0');
            }
            set
            {

                Operator_Name = Encoding.ASCII.GetBytes(value.PadRight(OperatorNameSize, '\0')).Take(OperatorNameSize).ToArray();
            }
        }
        public string DeviceName
        {
            get
            {

                return Encoding.ASCII.GetString(Device_Name).TrimEnd('\0');
            }
            set
            {
                
                Device_Name = Encoding.ASCII.GetBytes(value.PadRight(DeviceNameSize, '\0')).Take(DeviceNameSize).ToArray();
            }
        }


        public UInt16 WaferSize
        {
            get
            {                
                return BitConverter.ToUInt16(WaferMapUtility.ReverseArray(Wafer_Size), 0);
            }
            set
            {
                Wafer_Size = WaferMapUtility.ReverseArray(BitConverter.GetBytes(value));
            }
        }
        public UInt16 MachineNo
        {
            get
            {
                return BitConverter.ToUInt16(WaferMapUtility.ReverseArray(Machine_No), 0);
            }
            set
            {
                Machine_No = WaferMapUtility.ReverseArray(BitConverter.GetBytes(value));
            }
        }

        public UInt32 IndexSizeX
        {
            get
            {
                return BitConverter.ToUInt32(WaferMapUtility.ReverseArray(Index_Size_X), 0);
            }
            set
            {
                Index_Size_X = WaferMapUtility.ReverseArray(BitConverter.GetBytes(value));
            }
        }
        public UInt32 IndexSizeY
        {
            get
            {
                return BitConverter.ToUInt32(WaferMapUtility.ReverseArray(Index_Size_Y), 0);
            }
            set
            {
                Index_Size_Y = WaferMapUtility.ReverseArray(BitConverter.GetBytes(value));
            }
        }
        public UInt16 StandardOrientationFlatDirection
        {
            get
            {
                return BitConverter.ToUInt16(WaferMapUtility.ReverseArray(Standard_Orientation_Flat_Direction), 0);
            }
            set
            {
                Standard_Orientation_Flat_Direction = WaferMapUtility.ReverseArray(BitConverter.GetBytes(value));
            }
        }
        public byte FinalEditingMachinetype
        {
            get
            {
                return Final_Editing_Machine_type[0];
            }
            set
            {
                Final_Editing_Machine_type = WaferMapUtility.ReverseArray(BitConverter.GetBytes(value));
            }
        }


        public void printf()
        {
            
            Console.WriteLine($"OperatorName={OperatorName}");
            Console.WriteLine($"OperatorName={DeviceName}");

            Console.WriteLine($"WaferSize={WaferSize}");

            Console.WriteLine($"MachineNo={MachineNo}");
            Console.WriteLine($"IndexSizeX={IndexSizeX}");
            Console.WriteLine($"IndexSizeY={IndexSizeY}");
            Console.WriteLine($"StandardOrientationFlatDirection={StandardOrientationFlatDirection}");
            Console.WriteLine($"FinalEditingMachinetype={FinalEditingMachinetype}");


        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();

            
            sb.AppendLine($"OperatorName={OperatorName}");
            sb.AppendLine($"OperatorName={DeviceName}");
            sb.AppendLine($"WaferSize={WaferSize}");
            sb.AppendLine($"MachineNo={MachineNo}");
            sb.AppendLine($"IndexSizeX={IndexSizeX}");
            sb.AppendLine($"IndexSizeY={IndexSizeY}");
            sb.AppendLine($"StandardOrientationFlatDirection={StandardOrientationFlatDirection}");
            sb.AppendLine($"FinalEditingMachinetype={FinalEditingMachinetype}");

            return sb.ToString();
        }

        internal byte[] toBinary()
        {
            List<byte> bins = new List<byte>();
            bins.AddRange(Operator_Name);
            bins.AddRange(Device_Name);
            bins.AddRange(Wafer_Size);
            bins.AddRange(Machine_No);
            bins.AddRange(Index_Size_X);
            bins.AddRange(Index_Size_Y);
            bins.AddRange(Standard_Orientation_Flat_Direction);
            bins.AddRange(Final_Editing_Machine_type);

            return bins.ToArray();
        }
        public void init()
        {
            Operator_Name = new byte[OperatorNameSize];           
            Device_Name =  new byte[DeviceNameSize];
            Wafer_Size = new byte[WaferSizeSize];
            Machine_No = new byte[MachineNoSize];
            Index_Size_X = new byte[IndexSizeXSize];
            Index_Size_Y = new byte[IndexSizeYSize];
            Standard_Orientation_Flat_Direction = new byte[StandardOrientationFlatDirectionSize];
            Final_Editing_Machine_type = new byte [FinalEditingMachineTypeSize];
    }
    }
}
