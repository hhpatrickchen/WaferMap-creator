using ConsoleApp2;
using HedgeHulkApp;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsWaferMap
{
    public enum DiePosition
    {
        Edge,       // 在邊緣
        InsideEdge, // 在邊緣內
        OutsideEdge // 在邊緣外
    }


    public class WafeMapSetting
    {

        public int wafer_diemeter = 200;
        public int wafer_borderness = 1;
        public int col = 10;
        public int row = 10;

        //[Category("Zoom"), Description("Zoom...")]
        public int axisX = 0;
        public int axisY = 0;
        public int factor = 1;
        public int startPosition = 1;
        public int endPosition = 10;


        public int dwidth = 2000;
        public int dheight = 2000;

        [Category("Zoom"), DisplayName("Axis X")]
        public int AxisX
        {
            set
            {
                if (axisX != value)
                {
                    axisX = value;
                    OnPropertyChanged(nameof(AxisX));
                }
            }

            get => axisX;
        }
        [Category("Zoom"), DisplayName("Axis Y")]
        public int AxisY
        {
            set
            {
                if (axisY != value)
                {
                    axisY = value;
                    OnPropertyChanged(nameof(AxisY));
                }
            }

            get => axisY;
        }

        [Category("Zoom"), DisplayName("Factor")]
        public int Factor
        {
            set
            {
                if (factor != value)
                {
                    factor = value;
                    OnPropertyChanged(nameof(Factor));
                }
            }

            get => factor;
        }


        [Category("Demo"), DisplayName("1.Start Position"),]
        public int StartPosition
        {
            set
            {
                if (startPosition != value)
                {
                    startPosition = value;
                    OnPropertyChanged(nameof(StartPosition));
                }
            }

            get => startPosition;
        }
        [Category("Demo"), DisplayName("2.End Position"), ]
        public int EndPosition
        {
            set
            {
                if (endPosition != value)
                {
                    endPosition = value;
                    OnPropertyChanged(nameof(EndPosition));
                }
            }

            get => endPosition;
        }
        

        [Category("Die"), DisplayName("Width(nm)")]
        public int DieWitdh
        {
            set
            {
                if (dwidth != value)
                {
                    dwidth = value;
                    OnPropertyChanged(nameof(DieWitdh));
                }
            }

            get => dwidth;
        }
        [Category("Die"), DisplayName("Height(nm)")]
        public int DieHeight
        {
            set
            {
                if (dheight != value)
                {
                    dheight = value;
                    OnPropertyChanged(nameof(DieHeight));
                }
            }

            get => dheight;
        }


        [Category("Wafer"), DisplayName("Diameter")]
        public int Diemeter
        {
            set
            {
                if (wafer_diemeter != value)
                {
                    wafer_diemeter = value;
                    OnPropertyChanged(nameof(Diemeter));
                }
            }

            get => wafer_diemeter;
        }

        
        
        [Category("Wafer"), DisplayName("Border")]
        public int Border
        {
            set
            {
                if (wafer_borderness != value)
                {
                    wafer_borderness = value;
                    OnPropertyChanged(nameof(Border));
                }
            }

            get => wafer_borderness;
        }

        [Category("Wafer"), ReadOnly(true), DisplayName("xRow")]
        public int Row
        {
            set
            {
                if (row != value)
                {
                    row = value;
                    OnPropertyChanged(nameof(Row));
                }
            }
            get => row;
        }
        [Category("Wafer"), ReadOnly(true), DisplayName("xCol")]
        public int Col
        {
            set
            {
                if (col != value)
                {
                    col = value;
                    OnPropertyChanged(nameof(Col));
                }
            }
            get => col;
        }


        //[Category("Selected")]
        //public SelectedDie selectedDie { get; set; } = new SelectedDie();

        //[Category("Selected Device")]
        
        [TypeConverter(typeof(ExpandableObjectConverter))]
        [Category("Selected Device"), DisplayName("Position")]
        public SelectedDie selectedDie { get; set; } = new SelectedDie();

        
        [Category("Selected Device"), DisplayName("Number")]
        public int Number { get; set; }



        [Category("Header Data"), DisplayName("CASSETTE_NUMBER")]
        public String CASSETTE_NUMBER { get; set; }

        [Category("Header Data"), DisplayName("DEVICE_NAME")]
        public String DEVICE_NAME { get; set; }

        [Category("Header Data"), DisplayName("LOT_ID")]
        public String LOT_ID { get; set; }


        [Category("Header Data"), DisplayName("MACHINE_NUMBER")]
        public String MACHINE_NUMBER { get; set; }


        [Category("Header Data"), DisplayName("OPERATOR_NAME")]
        public String OPERATOR_NAME { get; set; }


        [Category("Header Data"), DisplayName("SLOT_NUMBER")]
        public String SLOT_NUMBER { get; set; }

        [Category("Header Data"), DisplayName("WAFER_ID")]
        public String WAFER_ID { get; set; }

        public List<Die> dieData = new List<Die>();

        public event PropertyChangedEventHandler PropertyChanged;

        // 通知屬性已更改
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public void LoadWafer(WaferMapAgtent waferMapAgtent)
        {
            
            //int OtopLeftX = 0;
            //int OtopLeftY = 0;
            //uint dieWitdh = (uint)dwidth;  //um
            //uint dieheight = (uint)dheight;//um

            LoadWaferAgent(waferMapAgtent);

            InitHeaderInformation(waferMapAgtent);

            

        }

        private void InitHeaderInformation(WaferMapAgtent waferMapAgtent)
        {
            DieWitdh = (int)waferMapAgtent.mapData.header_Information.wafer_Testing.IndexSizeX / 100;
            DieHeight = (int)waferMapAgtent.mapData.header_Information.wafer_Testing.IndexSizeY / 100;

            int wafersize = waferMapAgtent.mapData.header_Information.wafer_Testing.WaferSize;
            if (wafersize == 80)
            {
                Diemeter = 200;
            }


            CASSETTE_NUMBER = waferMapAgtent.mapData.header_Information.wafer_Specific_Data.CassetteNo.ToString("");
            DEVICE_NAME = waferMapAgtent.mapData.header_Information.wafer_Testing.DeviceName;
            LOT_ID = waferMapAgtent.mapData.header_Information.wafer_Specific_Data.LotNo;
            MACHINE_NUMBER = waferMapAgtent.mapData.header_Information.wafer_Testing.MachineNo.ToString("");
            OPERATOR_NAME = waferMapAgtent.mapData.header_Information.wafer_Testing.OperatorName;
            SLOT_NUMBER = waferMapAgtent.mapData.header_Information.wafer_Specific_Data.LotNo;
            WAFER_ID = waferMapAgtent.mapData.header_Information.wafer_Specific_Data.WaferID;
        }

        public void initWafer()
        {
            dieData.Clear();

            int OtopLeftX = 0;
            int OtopLeftY = 0;
            uint dieWitdh = (uint)dwidth;  //um
            uint dieheight = (uint)dheight;//um

            WaferMapAgtent newwafer = WaferMapAgtent.CreateWaferMap(dieWitdh, dieheight,(uint)wafer_diemeter, OtopLeftX, OtopLeftY);

            LoadWaferAgent(newwafer);

            InitHeaderInformation(newwafer);
        }

        private void LoadWaferAgent(WaferMapAgtent waferAgent)
        {

            dieData.Clear();

            Col = waferAgent.mapData.header_Information.MapDataAreaRowSize;
            Row = waferAgent.mapData.header_Information.MapDataAreaLineSize;

            int CwaferX = waferAgent.mapData.header_Information.waferProbingCoordinateSystemData.CoordinatorXOfWaferCenterDie;
            int CwaferY = waferAgent.mapData.header_Information.waferProbingCoordinateSystemData.CoordinatorYOfWaferCenterDie;

            double mmwitdh = dwidth / 1000.0;
            double mmheight = dheight / 1000.0;

            for (int i = 0; i < waferAgent.mapData.test_Result_Per_Dice.Count(); i++)
            {
                Test_Result_per_Die tdie = waferAgent.mapData.test_Result_Per_Dice[i];

                Rectangle dieRect = new Rectangle(tdie.Xaxis, tdie.Yaxis, 0, 0);
                //bool isedge = IsEdgeDie(axisX, axisY, waferC.centerX, waferC.centerY, wafersize, ddieWidth, ddieHeight, wafer_borderness);
                //DiePosition DiePos = GetDiePosition(tdie.Xaxis, tdie.Yaxis, CwaferX, CwaferY, wafer_diemeter, mmwitdh, mmheight, wafer_borderness);

                //bool isedge = DiePos == DiePosition.Edge ? true : false;
                bool isedge = tdie.IsEdge;

                DiePosition DiePos = DiePosition.OutsideEdge;

                if(tdie.DieProperty == 0)
                {
                    DiePos = DiePosition.OutsideEdge;
                }
                else if(tdie.DieProperty == 1)
                {
                    DiePos = DiePosition.InsideEdge;
                }
                else
                {
                    DiePos = DiePosition.Edge;
                }
                //if (tdie.IsEdge)
                //{
                //    DiePos = DiePosition.InsideEdge;
                    
                //    if (tdie.DieProperty == 2)
                //    {
                //        DiePos = DiePosition.Edge;
                //    }
                //}
                //else
                //{         
                //    DiePos = DiePosition.OutsideEdge;

                //}

                Die die = new Die(dieRect);
                die.Number = (int)tdie.Bin;
                die.diePos = DiePos;
                die.Column = tdie.Xaxis;
                die.Row = tdie.Yaxis;
                dieData.Add(die);

            }
        }

        public void initWaferList()
        {
            dieData.Clear();

            int wafersize = wafer_diemeter;


            double ddieWidth = (double)dwidth / 1000.0;
            double ddieHeight = (double)dheight / 1000.0;


            Col = Convert.ToInt32(Math.Floor((double)wafer_diemeter / ddieWidth));

            Row = Convert.ToInt32(Math.Floor((double)wafer_diemeter / ddieHeight));


            int left_top_x = 0;
            int left_top_y = 0;
            int axisX = left_top_x;
            int axisY = left_top_y;


            var waferC = CalculateWaferCenter(axisX, axisY, col, row);
            
            Console.WriteLine($"waferCX={waferC.centerX},waferCY={waferC.centerY}");

            for (int i = 0; i < col * row; i++)
            {
                
                Rectangle dieRect = new Rectangle(axisX, axisY, (int)ddieWidth, (int)ddieHeight);
                //bool isedge = IsEdgeDie(axisX, axisY, waferC.centerX, waferC.centerY, wafersize, ddieWidth, ddieHeight, wafer_borderness);
                DiePosition DiePos = GetDiePosition(axisX, axisY, waferC.centerX, waferC.centerY, wafersize, ddieWidth, ddieHeight, wafer_borderness);

                //Console.WriteLine($"col={axisX},row={axisY},isedge={isedge}");
                bool isedge = DiePos == DiePosition.Edge ? true : false;
                Die die = new Die(dieRect);
                die.diePos = DiePos;
                die.Column = axisX;
                die.Row = axisY;
                dieData.Add(die);

                //X-------------->
                //35 34 33 ...............0 -------------- -5 -6 
                axisX--;
                if ((i + 1) % col == 0)
                {
                    //next row
                    axisX = left_top_x;
                    axisY--;
                }

            }


        }

        public static (int centerX, int centerY) CalculateWaferCenter(double topLeftX, double topLeftY, int rows, int columns)
        {
            // 计算晶圆的总宽度和总高度
            double totalWidth = rows;
            double totalHeight = columns;

            // 计算圆心坐标
            double centerX = topLeftX - (totalWidth / 2);
            double centerY = topLeftY - (totalHeight / 2);

            return ((int)centerX, (int)centerY);
        }

        public static bool IsEdgeDie(int row, int col, int centerX, int centerY, double waferDiameter, double dieWidth, double dieHeight, int edgeThreshold)
        {
            // 计算晶圆半径
            double waferRadius = waferDiameter / 2.0;


            // 将行列坐标转换为实际的物理 X, Y 坐标（Die 中心坐标）
            //double dieCenterX = topLeftX - row * dieWidth - dieWidth / 2.0;
            //double dieCenterY = topLeftY - col * dieHeight - dieHeight / 2.0;

            double dieCenterX = (row) * dieWidth;
            double dieCenterY = (col) * dieHeight;

            // 计算 Die 中心到晶圆圆心的距离
            double distanceToCenter = Math.Sqrt(Math.Pow(dieCenterX - centerX * dieWidth, 2) + Math.Pow(dieCenterY - centerY * dieHeight, 2));

            // 判断是否为边缘 Die
            return distanceToCenter >= (waferRadius - edgeThreshold) && distanceToCenter <= (waferRadius + edgeThreshold);
        }
        public static DiePosition GetDiePosition(
            int row,
            int col,
            int centerX,
            int centerY,
            double waferDiameter,
            double dieWidth,
            double dieHeight,
            int edgeThreshold)
        {
            // 計算晶圓半徑
            double waferRadius = waferDiameter / 2.0;

            // 將行列坐標轉換為實際的物理 X, Y 坐標（Die 中心坐標）
            double dieCenterX = row * dieWidth;
            double dieCenterY = col * dieHeight;

            // 計算 Die 中心到晶圓圓心的距離
            double distanceToCenter = Math.Sqrt(
                Math.Pow(dieCenterX - centerX * dieWidth, 2) +
                Math.Pow(dieCenterY - centerY * dieHeight, 2)
            );

            // 判斷 Die 的位置狀態
            if (distanceToCenter >= (waferRadius - edgeThreshold) && distanceToCenter <= (waferRadius + edgeThreshold))
            {
                return DiePosition.Edge; // 在邊緣
            }
            else if (distanceToCenter < (waferRadius - edgeThreshold))
            {
                
                return DiePosition.InsideEdge; // 在邊緣內
            }
            else
            {
                return DiePosition.OutsideEdge; // 在邊緣外
            }
        }

        private bool IsPointInsideEllipse(Point point, Rectangle ellipse)
        {
            //float dx = pt.X - bounds.Width / 2;
            //float dy = pt.Y - bounds.Height / 2;
            //return (dx * dx + dy * dy) <= (bounds.Width * bounds.Width) / 4;


            double a = ellipse.Width / 2.0;
            double b = ellipse.Height / 2.0;
            double x = point.X - ellipse.X - a;
            double y = point.Y - ellipse.Y - b;

            return (x * x) / (a * a) + (y * y) / (b * b) <= 1;
        }
    }
}
