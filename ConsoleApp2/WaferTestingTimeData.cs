using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp2
{
    public struct WaferTestingTimeData
    {
        private const int YearSize = 2;
        private const int MonthSize = 2;
        private const int DaySize = 2;
        private const int HourSize = 2;
        private const int MinuteSize = 2;
        private const int Reserved1Size = 2;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = YearSize)]
        public byte[] Year_;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = MonthSize)]
        public byte[] Month_;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = DaySize)]
        public byte[] Day_;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = HourSize)]
        public byte[] Hour_;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = MinuteSize)]
        public byte[] Minute_;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = Reserved1Size)]
        public byte[] Reserved1_;


        public string StartTime
        {
            get
            {
                string year = Encoding.ASCII.GetString(Year_).TrimEnd('\0');
                string month = Encoding.ASCII.GetString(Month_).TrimEnd('\0');
                string day = Encoding.ASCII.GetString(Day_).TrimEnd('\0');
                string hour = Encoding.ASCII.GetString(Hour_).TrimEnd('\0');
                string min = Encoding.ASCII.GetString(Minute_).TrimEnd('\0');
                return $"{year}/{month}/{day}-{hour}:{min}";
            }
        }
       
        public UInt16 Reserved1
        {
            get
            {
                return BitConverter.ToUInt16(WaferMapUtility.ReverseArray(Reserved1_), 0);                
            }
        }

        public void printf(string title)
        {
            Console.WriteLine($"{title} Time={StartTime}");

            Console.WriteLine($"Reserved1={Reserved1}");
        }
        public string toString(string title)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"{title} Time={StartTime}");
            sb.AppendLine($"Reserved1={Reserved1}");
            return sb.ToString();
            
        }

        internal void init()
        {
            Year_ = new byte[YearSize];
            Month_ = new byte[MonthSize];
            Day_ = new byte[DaySize];
            Hour_ = new byte[HourSize];
            Minute_ = new byte[MinuteSize];
            Reserved1_ = new byte[Reserved1Size];
        }

        internal IEnumerable<byte> toBinary()
        {
            List<byte> bytes = new List<byte>();
            bytes.AddRange(Year_);
            bytes.AddRange(Month_);
            bytes.AddRange(Day_);
            bytes.AddRange(Hour_);
            bytes.AddRange(Minute_);
            bytes.AddRange(Reserved1_);
            return bytes;
        }
    }
}
