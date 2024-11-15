using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp2
{
    public struct Test_Result_per_Die
    {
        //fist word
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
        public byte[] Word_1;
        //second word
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
        public byte[] Word_2;
        //third word

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
        public byte[] Word_3;



        public uint Word1
        {
            get
            {
                return BitConverter.ToUInt16(WaferMapUtility.ReverseArray(Word_1), 0);                
            }
            set
            {
                Word_1 = WaferMapUtility.ReverseArray(BitConverter.GetBytes(value));
            }
        }

        internal IEnumerable<byte> ToBinary()
        {
            List<byte> bytes = new List<byte>();
            bytes.AddRange(Word_1);
            bytes.AddRange(Word_2);
            bytes.AddRange(Word_3);

            return bytes;
        }

        public uint Word2
        {
            get
            {
                return BitConverter.ToUInt16(WaferMapUtility.ReverseArray(Word_2), 0);
            }
            set
            {
                Word_2 = WaferMapUtility.ReverseArray(BitConverter.GetBytes(value));
            }
        }
        public uint Word3
        {
            get
            {
                return BitConverter.ToUInt16(WaferMapUtility.ReverseArray(Word_3), 0);
            }
            set
            {
                Word_3 = WaferMapUtility.ReverseArray(BitConverter.GetBytes(value));
            }
        }

        public int Xaxis
        {
            get
            {
                string sign = CodeBitofCoordinatorValueX == 0 ? "+" : "-";
                string data = $"{sign}{DieCoordinatorValueX}";
                return Convert.ToInt32(data);
            }
            set
            {
                
                if (value >= 0)
                {
                    CodeBitofCoordinatorValueX = 0;
                    DieCoordinatorValueX = (uint)value;
                }
                else
                {
                    CodeBitofCoordinatorValueX = 1;
                    DieCoordinatorValueX = (uint)(0 - value);
                }

            }
        }
        public int Yaxis
        {
            get
            {
                string sign = CodeBitofCoordinatorValueY == 0 ? "+" : "-";
                string data = $"{sign}{DieCoordinatorValueY}";
                return Convert.ToInt32(data);
            }
            set
            {

                if (value >= 0)
                {
                    CodeBitofCoordinatorValueY = 0;
                    DieCoordinatorValueY = (uint)value;
                }
                else
                {
                    CodeBitofCoordinatorValueY = 1;
                    DieCoordinatorValueY = (uint)(0 - value);
                }

            }
        }



        public bool IsEdge
        {
            get
            {
                return DieTestResult == 0 && (DieProperty == 1 || DieProperty == 2);
            }
            set
            {
                DieProperty = value == true ? (uint)1 : 0;
            }
        }          

        //Die Test Result
        //0: Not Tested / 1: Pass Die / 2: Fail 1 Die / 3: Fail 2 Die
        public uint DieTestResult
        {
            get
            {
                return WaferMapUtility.GetBits(Word1, 14, 15);
                
            }
            set
            {
                Word1 = WaferMapUtility.SetBits(Word1, 14, 15, value);
            }
        }

        //Marking
        //0: No / Yes (After Marking: 1)
        public uint Marking
        {
            get
            {
                return WaferMapUtility.GetBit(Word1,13);

            }
            set
            {
                Word1 = WaferMapUtility.SetBit(Word1, 13,value);
            }
        }

        //Fail Mark Inspection
        //0: Yes / 1: No
        public uint FailMarkInspection
        {
            get
            {
                return WaferMapUtility.GetBit(Word1, 12);

            }
            set
            {
                Word1 = WaferMapUtility.SetBit(Word1, 12, value);
            }
        }

        //Re-Probing Result
        //0: Not Re-Probed
        //1: Passed at re-probing
        //2: Failed at re-probing
        //3: Reserved( for further extension)
        public uint ReProbingResult
        {
            get
            {
                return WaferMapUtility.GetBits(Word1, 10,11);

            }
            set
            {
                Word1 = WaferMapUtility.SetBits(Word1, 10,11, value);
            }
        }

        //Needle Mark Inspection Result(added Jan/23/’96)(Not handled)
        //0: OK / 1: NG
        public uint NeedleMarkInspectionResult
        {
            get
            {
                return WaferMapUtility.GetBit(Word1, 9);

            }
            set
            {
                Word1 = WaferMapUtility.SetBit(Word1, 9, value);
            }
        }

        public uint DieCoordinatorValueX
        {
            get
            {
                return WaferMapUtility.GetBits(Word1, 0, 8);

            }
            set
            {
                Word1 = WaferMapUtility.SetBits(Word1, 0, 8, value);
            }
        }

        //Die Property
        //0: Skip Die / 1: Probing Die / 2: Compulsory Marking Die
        public uint DieProperty
        {
            get
            {
                return WaferMapUtility.GetBits(Word2, 14, 15);

            }
            set
            {
                Word2 = WaferMapUtility.SetBits(Word2, 14, 15, value);
            }
        }
        //Needle Marking Inspection Execution Die Selection
        //0: No / 1: Yes
        public uint NeedleMarkingInspectionExecutionDieSelection
        {
            get
            {
                return WaferMapUtility.GetBit(Word2, 13);

            }
            set
            {
                Word2 = WaferMapUtility.SetBit(Word2, 13,value);
            }
        }

        //Sampling Die
        public uint SamplingDie
        {
            get
            {
                return WaferMapUtility.GetBit(Word2, 12);

            }
            set
            {
                Word2 = WaferMapUtility.SetBit(Word2, 12, value);
            }
        }

        //Code Bit of Coordinator Value X
        //0: + data / 1: – data
        public uint CodeBitofCoordinatorValueX
        {
            get
            {
                return WaferMapUtility.GetBit(Word2, 11);

            }
            set
            {
                Word2 = WaferMapUtility.SetBit(Word2, 11, value);
            }
        }
        //Code Bit of Coordinator Value Y
        //0: + data / 1: – data
        public uint CodeBitofCoordinatorValueY
        {
            get
            {
                return WaferMapUtility.GetBit(Word2, 10);

            }
            set
            {
                Word2 = WaferMapUtility.SetBit(Word2, 10, value);
            }
        }

        //Dummy Data (except wafer)
        public uint DummyData
        {
            get
            {
                return WaferMapUtility.GetBit(Word2, 9);

            }
            set
            {
                Word2 = WaferMapUtility.SetBit(Word2, 9, value);
            }
        }

        //Die Coordinator Value Y
        public uint DieCoordinatorValueY
        {
            get
            {
                return WaferMapUtility.GetBits(Word2, 0,8);

            }
            set
            {
                Word2 = WaferMapUtility.SetBits(Word2,0, 8, value);
            }
        }


        //Measurement Finish Flag at “No-Over-Travel” Probing
        //0: Not Tested 1: Tested
        public uint MeasurementFinishFlag
        {
            get
            {
                return WaferMapUtility.GetBit(Word3, 15);

            }
            set
            {
                Word3 = WaferMapUtility.SetBit(Word3,15, value);
            }
        }
        //Reject Chip Flag(User Special)
        //Peripheral Probing Die(Standard specification)
        //Ink Die(User Special)
        //Partial P/W(User Special)
        public uint UserFlag
        {
            get
            {
                return WaferMapUtility.GetBit(Word3, 14);

            }
            set
            {
                Word3 = WaferMapUtility.SetBit(Word3, 14, value);
            }
        }

        //Test Execution Site No. (0 to 63)
        public uint TestExecutionSiteNo
        {
            get
            {
                return WaferMapUtility.GetBits(Word3, 8,13);

            }
            set
            {
                Word3 = WaferMapUtility.SetBits(Word3, 8,13, value);
            }
        }
        //Block Area Judgement Function
        //1: Block 1
        //2: Block 2
        //3: Block 3
        public uint BlockAreaJudgementFunction
        {
            get
            {
                return WaferMapUtility.GetBits(Word3, 6, 7);

            }
            set
            {
                Word3 = WaferMapUtility.SetBits(Word3, 6, 7, value);
            }
        }

        //Category Data (0 to 63)
        public uint Bin
        {
            get
            {
                return WaferMapUtility.GetBits(Word3, 0, 5);
            }
            set
            {
                Word3 = WaferMapUtility.SetBits(Word3,0,5,value);
            }

        }

     

        public override string ToString()
        {  
     
            StringBuilder sb = new StringBuilder();
            sb.Append($"DieTestResult={DieTestResult},");
            sb.Append($"Marking={Marking},");
            sb.Append($"DieProperty={DieProperty},");



            sb.Append($"Axis X={Xaxis},");
            sb.Append($"Axis Y={Yaxis},");
            sb.Append($"Bin={Bin}");

           
            return sb.ToString();
        }

        internal void init()
        {
            Word_1 = new byte[2];
            Word_2 = new byte[2];
            Word_3 = new byte[2];
        }
    }


}
