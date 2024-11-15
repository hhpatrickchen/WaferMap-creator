using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp2
{
    public struct WaferProbingCoordinateSystemData
    {
        private const int XCoordinatesIncreaseDirectionSize = 1;
        private const int YCoordinatesIncreaseDirectionSize = 1;
        private const int ReferenceDieSettingProceduresSize = 1;
        private const int Reserved1Size = 1;
        private const int TargetDiePositionXSize = 4;
        private const int TargetDiePositionYSize = 4;

        private const int ReferenceDieCoordinatorXSize = 2;
        private const int ReferenceDieCoordinatorYSize = 2;
        private const int ProbingStartPositionSize = 1;
        private const int ProbingDirectionSize = 1;
        private const int Reserved2Size = 2;
        private const int DistanceXToWaferCenterDieOriginSize = 4;
        private const int DistanceYToWaferCenterDieOriginSize = 4;
        private const int CoordinatorXOfWaferCenterDieSize = 4;
        private const int CoordinatorYOfWaferCenterDieSize = 4;


        [MarshalAs(UnmanagedType.ByValArray, SizeConst = XCoordinatesIncreaseDirectionSize)]
        public byte[] X_Coordinates_Increase_Direction;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = YCoordinatesIncreaseDirectionSize)]
        public byte[] Y_Coordinates_Increase_Direction;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = ReferenceDieSettingProceduresSize)]
        public byte[] Reference_Die_Setting_Procedures;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = Reserved1Size)]
        public byte[] Reserved_1;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = TargetDiePositionXSize)]
        public byte[] Target_Die_Position_X;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = TargetDiePositionYSize)]
        public byte[] Target_Die_Position_Y;


        [MarshalAs(UnmanagedType.ByValArray, SizeConst = ReferenceDieCoordinatorXSize)]
        public byte[] Reference_Die_Coordinator_X;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = ReferenceDieCoordinatorYSize)]
        public byte[] Reference_Die_Coordinator_Y;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = ProbingStartPositionSize)]
        public byte[] Probing_Start_Position;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = ProbingDirectionSize)]
        public byte[] Probing_Direction;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = Reserved2Size)]
        public byte[] Reserved_2;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = DistanceXToWaferCenterDieOriginSize)]
        public byte[] Distance_X_To_Wafer_Center_Die_Origin;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = DistanceYToWaferCenterDieOriginSize)]
        public byte[] Distance_Y_To_Wafer_Center_Die_Origin;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = CoordinatorXOfWaferCenterDieSize)]
        public byte[] Coordinator_X_Of_Wafer_Center_Die;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = CoordinatorYOfWaferCenterDieSize)]
        public byte[] Coordinator_Y_Of_Wafer_Center_Die;


        public byte XCoordinatesIncreaseDirection
        {
            get
            {
                return X_Coordinates_Increase_Direction[0];
            }
        }

        public byte YCoordinatesIncreaseDirection
        {
            get
            {
                return Y_Coordinates_Increase_Direction[0];
            }
        }
        public byte ReferenceDieSettingProcedures
        {
            get
            {
                return Reference_Die_Setting_Procedures[0];
            }
        }

        public byte Reserved1
        {
            get
            {
                return Reserved_1[0];
            }
        }

        public Int32 TargetDiePositionX
        {
            get
            {
                return BitConverter.ToInt32(WaferMapUtility.ReverseArray(Target_Die_Position_X), 0);
            }
        }
        public Int32 TargetDiePositionY
        {
            get
            {
                return BitConverter.ToInt32(WaferMapUtility.ReverseArray(Target_Die_Position_Y), 0);
            }
        }


        public UInt16 ReferenceDieCoordinatorX
        {
            get
            {
                return BitConverter.ToUInt16(WaferMapUtility.ReverseArray(Reference_Die_Coordinator_X), 0);
            }
        }
        public UInt16 ReferenceDieCoordinatorY
        {
            get
            {
                return BitConverter.ToUInt16(WaferMapUtility.ReverseArray(Reference_Die_Coordinator_Y), 0);
            }
        }
        public byte ProbingStartPosition
        {
            get
            {
                return Probing_Start_Position[0];
            }
        }
        public byte ProbingDirection
        {
            get
            {
                return Probing_Direction[0];
            }
        }
        public UInt16 Reserved2
        {
            get
            {
                return BitConverter.ToUInt16(WaferMapUtility.ReverseArray(Reserved_2), 0);
            }
        }

        public Int32 DistanceXToWaferCenterDieOrigin
        {
            get
            {
                return BitConverter.ToInt32(WaferMapUtility.ReverseArray(Distance_X_To_Wafer_Center_Die_Origin), 0);
            }
        }

        public Int32 DistanceYToWaferCenterDieOrigin
        {
            get
            {
                return BitConverter.ToInt32(WaferMapUtility.ReverseArray(Distance_Y_To_Wafer_Center_Die_Origin), 0);
            }
        }

        public Int32 CoordinatorXOfWaferCenterDie
        {
            get
            {
                return BitConverter.ToInt32(WaferMapUtility.ReverseArray(Coordinator_X_Of_Wafer_Center_Die), 0);
            }
            set
            {
                Coordinator_X_Of_Wafer_Center_Die = WaferMapUtility.ReverseArray(BitConverter.GetBytes(value));
            }
        }

        public Int32 CoordinatorYOfWaferCenterDie
        {
            get
            {
                return BitConverter.ToInt32(WaferMapUtility.ReverseArray(Coordinator_Y_Of_Wafer_Center_Die), 0);
            }
            set
            {
                Coordinator_Y_Of_Wafer_Center_Die = WaferMapUtility.ReverseArray(BitConverter.GetBytes(value));
            }
        }
        public void printf()
        {
            Console.WriteLine($"XCoordinatesIncreaseDirection={XCoordinatesIncreaseDirection}");
            Console.WriteLine($"YCoordinatesIncreaseDirection={YCoordinatesIncreaseDirection}");
            Console.WriteLine($"ReferenceDieSettingProcedures={ReferenceDieSettingProcedures}");
            Console.WriteLine($"Reserved1={Reserved1}");
            Console.WriteLine($"TargetDiePositionX={TargetDiePositionX}");
            Console.WriteLine($"TargetDiePositionY={TargetDiePositionY}");

            Console.WriteLine($"ReferenceDieCoordinatorX={ReferenceDieCoordinatorX}");
            Console.WriteLine($"ReferenceDieCoordinatorY={ReferenceDieCoordinatorY}");
            Console.WriteLine($"ProbingStartPosition={ProbingStartPosition}");
            Console.WriteLine($"ProbingDirection={ProbingDirection}");
            Console.WriteLine($"Reserved2={Reserved2}");
            Console.WriteLine($"DistanceXToWaferCenterDieOrigin={DistanceXToWaferCenterDieOrigin}");
            Console.WriteLine($"DistanceYToWaferCenterDieOrigin={DistanceYToWaferCenterDieOrigin}");
            Console.WriteLine($"CoordinatorXOfWaferCenterDie={CoordinatorXOfWaferCenterDie}");
            Console.WriteLine($"CoordinatorYOfWaferCenterDie={CoordinatorYOfWaferCenterDie}");
        }

        internal void init()
        {
            X_Coordinates_Increase_Direction =new byte[XCoordinatesIncreaseDirectionSize];
            Y_Coordinates_Increase_Direction = new byte[YCoordinatesIncreaseDirectionSize];

            Reference_Die_Setting_Procedures = new byte[ReferenceDieSettingProceduresSize];
            Reserved_1 = new byte[Reserved1Size];
            Target_Die_Position_X = new byte[TargetDiePositionXSize];
            Target_Die_Position_Y = new byte[TargetDiePositionYSize];


            Reference_Die_Coordinator_X = new byte[ReferenceDieCoordinatorXSize];
            Reference_Die_Coordinator_Y = new byte[ReferenceDieCoordinatorYSize];
            Probing_Start_Position = new byte[ProbingStartPositionSize];
            Probing_Direction = new byte[ProbingDirectionSize];
            Reserved_2 = new byte[Reserved2Size];
            Distance_X_To_Wafer_Center_Die_Origin = new byte[DistanceXToWaferCenterDieOriginSize];
            Distance_Y_To_Wafer_Center_Die_Origin = new byte[DistanceYToWaferCenterDieOriginSize];

            Coordinator_X_Of_Wafer_Center_Die = new byte[CoordinatorXOfWaferCenterDieSize];
            Coordinator_Y_Of_Wafer_Center_Die = new byte[CoordinatorYOfWaferCenterDieSize];
            
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            
            sb.AppendLine($"XCoordinatesIncreaseDirection={XCoordinatesIncreaseDirection}");
            sb.AppendLine($"YCoordinatesIncreaseDirection={YCoordinatesIncreaseDirection}");
            sb.AppendLine($"ReferenceDieSettingProcedures={ReferenceDieSettingProcedures}");
            sb.AppendLine($"Reserved1={Reserved1}");
            sb.AppendLine($"TargetDiePositionX={TargetDiePositionX}");
            sb.AppendLine($"TargetDiePositionY={TargetDiePositionY}");
            sb.AppendLine($"ReferenceDieCoordinatorX={ReferenceDieCoordinatorX}");
            sb.AppendLine($"ReferenceDieCoordinatorY={ReferenceDieCoordinatorY}");
            sb.AppendLine($"ProbingStartPosition={ProbingStartPosition}");
            sb.AppendLine($"ProbingDirection={ProbingDirection}");
            sb.AppendLine($"Reserved2={Reserved2}");
            sb.AppendLine($"DistanceXToWaferCenterDieOrigin={DistanceXToWaferCenterDieOrigin}");
            sb.AppendLine($"DistanceYToWaferCenterDieOrigin={DistanceYToWaferCenterDieOrigin}");
            sb.AppendLine($"CoordinatorXOfWaferCenterDie={CoordinatorXOfWaferCenterDie}");
            sb.AppendLine($"CoordinatorYOfWaferCenterDie={CoordinatorYOfWaferCenterDie}");
            return sb.ToString();
        }

        internal IEnumerable<byte> toBinary()
        {
            List<byte> bytes = new List<byte>();

            bytes.AddRange(X_Coordinates_Increase_Direction);
            bytes.AddRange(Y_Coordinates_Increase_Direction);
            bytes.AddRange(Reference_Die_Setting_Procedures);
            bytes.AddRange(Reserved_1);
            bytes.AddRange(Target_Die_Position_X);
            bytes.AddRange(Target_Die_Position_Y);
            bytes.AddRange(Reference_Die_Coordinator_X);
            bytes.AddRange(Reference_Die_Coordinator_Y);
            bytes.AddRange(Probing_Start_Position);
            bytes.AddRange(Probing_Direction);
            bytes.AddRange(Reserved_2);
            bytes.AddRange(Distance_X_To_Wafer_Center_Die_Origin);
            bytes.AddRange(Distance_Y_To_Wafer_Center_Die_Origin);
            bytes.AddRange(Coordinator_X_Of_Wafer_Center_Die);
            bytes.AddRange(Coordinator_Y_Of_Wafer_Center_Die);


            return bytes;
        }
    }
}
