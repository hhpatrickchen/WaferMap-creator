using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using WindowsFormsWaferMap;

namespace HedgeHulkApp.Usercontrol
{
    public partial class UserControlDisplayWaferMap : UserControl
    {
        public delegate void CallbackDelegate(string message);

        CallbackDelegate callbackfun;

        private Bitmap bufferBitmap; // 建立一個緩衝區圖像

        int gDieCols = 30;
        int gDieRows = 20;

        private Point startPoint;
        private Point endPoint;

        private Rectangle selectionRect = Rectangle.Empty;
        private bool isSelecting = false;

       // private float zoomScaleFactor = 1f; // 局部縮放的倍率
        private Point zoomOrigin = Point.Empty; // 放大顯示的起始原點
        private int scaleFactor = 1; // 默认缩放比例

        List<Die> dieList = new List<Die>();
        List<Die> dieData = new List<Die>();

       
        DIESTATE testst = DIESTATE.PASS;
        public UserControlDisplayWaferMap()
        {
            InitializeComponent();
            this.Paint += PanelWaferMap_Paint;
            this.Resize += UserControlWaferMap_Resize; // 添加Resize事件
            this.Load += UserControlDisplayWaferMap_Load; // 添加Load事件

            this.MouseWheel += PanelWaferMap_MouseWheel;

            //this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.PanelWaferMap_MouseDown);
            //this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.PanelWaferMap_MouseMove);
            //this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.PanelWaferMap_MouseUp);
        }
        private void PanelWaferMap_MouseWheel(object sender, MouseEventArgs e)
        {
            if (e.Delta > 0)
            {
                ZoomIn(e.Location); // 使用鼠标位置作为缩放中心
            }
            else
            {
                ZoomOut(e.Location);
            }
        }

        private void ZoomIn(Point mousePosition, bool usermode= false)
        {
            // 计算缩放比例的变化
            float scaleFactorChange = 2.0f;
            if(usermode)
            {               
                // 调整 zoomOrigin 基于当前鼠标位置
                zoomOrigin.X = (int)(mousePosition.X );
                zoomOrigin.Y = (int)(mousePosition.Y );
            }
            else
            {
                scaleFactor *= (int)scaleFactorChange;

                // 调整 zoomOrigin 基于当前鼠标位置
                zoomOrigin.X = (int)(mousePosition.X - (mousePosition.X - zoomOrigin.X) / scaleFactorChange);
                zoomOrigin.Y = (int)(mousePosition.Y - (mousePosition.Y - zoomOrigin.Y) / scaleFactorChange);
            }
    


            Console.WriteLine($"ZoomIn zoomOrigin.X={zoomOrigin.X}, zoomOrigin.Y={zoomOrigin.Y}");
            Console.WriteLine($"ZoomOut mousePosition.X={mousePosition.X}, mousePosition.Y={mousePosition.Y}");
            this.Invalidate();
        }

        private void ZoomOut(Point mousePosition)
        {
            if (scaleFactor > 1)
            {
                float scaleFactorChange = 0.5f;
                scaleFactor = Math.Max(1, (int)(scaleFactor * scaleFactorChange));
                
                // 调整 zoomOrigin 基于当前鼠标位置
                zoomOrigin.X = (int)(mousePosition.X - (mousePosition.X - zoomOrigin.X) / scaleFactorChange);
                zoomOrigin.Y = (int)(mousePosition.Y - (mousePosition.Y - zoomOrigin.Y) / scaleFactorChange);
                this.Invalidate();
            }
            else
            {
                // 缩放回默认
                ResetZoom();
            }

            Console.WriteLine($"ZoomOut zoomOrigin.X={zoomOrigin.X}, zoomOrigin.Y={zoomOrigin.Y}");
            Console.WriteLine($"ZoomOut mousePosition.X={mousePosition.X}, mousePosition.Y={mousePosition.Y}");
        }
        private void ResetZoom()
        {
            scaleFactor = 1;
            //zoomScaleFactor = 1f; // 恢复局部放大为 1
            zoomOrigin = Point.Empty; // 恢复局部放大的起点
            
            this.Invalidate();
        }

        public void SetCallback(CallbackDelegate callback)
        {
            callbackfun = callback;
        }

        public void commandcallback(COMMANDCODE command, WafeMapSetting wafeMapSetting)
        {
            Console.WriteLine(command);
            switch (command)
            {
                case COMMANDCODE.WAFER_ZOOM_IN: ZoomIn(); break;
                case COMMANDCODE.WAFER_ZOOM_OUT: ZoomOut(); break;                
                case COMMANDCODE.WAFER_RESET_ZOOM: ResetZoom(); break; 
                case COMMANDCODE.WAFER_RUN: Run(wafeMapSetting); break;
                case COMMANDCODE.JUMP_POSITION: JumpPosition(wafeMapSetting); break;
                case COMMANDCODE.WAFER_RESET: WaferReset();break;
                case COMMANDCODE.NEW_MAP: NewMap(wafeMapSetting);break;
                default: throw new Exception("Unexpected command=" + command);
            }
        }

        private void NewMap(WafeMapSetting wafeMapSetting)
        {
            int col = wafeMapSetting.col;
            int row = wafeMapSetting.row;

            gDieCols = col;
            gDieRows = row;

            InitWaferList();
            ReDrawWaferMap(); // 重绘晶圆图

            Console.WriteLine($"Width={Width}, Height={Height}");
        }

        private void WaferReset()
        {
            InitWaferList(); // 初始化 Die 数据
            ReDrawWaferMap(); // 重绘晶圆图

            Console.WriteLine($"Width={Width}, Height={Height}");
        }

        private void JumpPosition(WafeMapSetting wsetting)
        {
            Console.WriteLine($"Jump to x={wsetting.axisX},y={wsetting.axisY}");

            Point point = new Point(wsetting.axisX, wsetting.axisY);

            scaleFactor = wsetting.factor;
            ZoomIn(point, true);


        
            
        }

        private void Run(WafeMapSetting wafeMapSetting)
        {
            int count = wafeMapSetting.count;
            Console.WriteLine("Demo Run count");

            while (dieList[count].IsLightGreen == false)
            {
                count++;
            }
            dieList[count].PassFail = testst;
            if (testst == DIESTATE.PASS)
            {
                testst = DIESTATE.FAIL;
            }
            else
            {
                testst = DIESTATE.PASS;
            }


            // 计算出需更新的 Die 的区域
            //var updateRegion = new Rectangle(
            //    Math.Min(dieList[9].Bounds.X, dieList[10].Bounds.X), // 左上角X
            //    Math.Min(dieList[9].Bounds.Y, dieList[10].Bounds.Y), // 左上角Y
            //    Math.Abs(dieList[12].Bounds.Right - dieList[9].Bounds.Left), // 宽度
            //    Math.Abs(dieList[12].Bounds.Bottom - dieList[9].Bounds.Top)  // 高度
            //);

            Rectangle adjustedBounds = AdjustedBounds(dieList[count].Bounds);

            // 只更新部分区域
            Invalidate(adjustedBounds);


  
        }
        // 根據 scaleFactor 和 zoomOrigin 調整 die 的範圍
        private Rectangle AdjustedBounds(Rectangle originalBounds)
        {
            int adjustedX = (int)((originalBounds.X - zoomOrigin.X) * scaleFactor);
            int adjustedY = (int)((originalBounds.Y - zoomOrigin.Y) * scaleFactor);
            int adjustedWidth = (int)(originalBounds.Width * scaleFactor);
            int adjustedHeight = (int)(originalBounds.Height * scaleFactor);

            return new Rectangle(adjustedX, adjustedY, adjustedWidth, adjustedHeight);
        }
        private void ZoomIn()
        {
            Console.WriteLine("ZoomIn");
            scaleFactor *= 2;
            this.Invalidate();
        }

        private void ZoomOut()
        {
            if (scaleFactor == 1) return;
            scaleFactor  /=2;  // 返回原始大小
            this.Invalidate();
        }

        private void PanelWaferMap_Paint(object sender, PaintEventArgs e)
        {
            bufferBitmap?.Dispose();
            bufferBitmap = new Bitmap(this.ClientSize.Width, this.ClientSize.Height);

            using (Graphics bufferGraphics = Graphics.FromImage(bufferBitmap))
            {
                e.Graphics.Clear(Color.White);
                Graphics g = e.Graphics;
                int panelWidth = this.Width;
                int panelHeight = this.Height;

                // 计算基本的 Die 尺寸
                int dieWidth = panelWidth / gDieCols;
                int dieHeight = panelHeight / gDieRows;

                //int scaledDieWidth = (int)(dieWidth * scaleFactor);
                //int scaledDieHeight = (int)(dieHeight * scaleFactor);

                // 如果当前进行局部放大（Zoom In），则使用框选区域进行缩放
                if (scaleFactor > 1)
                {
                    // 使用框选区域进行缩放和平移
                    bufferGraphics.ScaleTransform(scaleFactor, scaleFactor);
                    Console.WriteLine($"scaleFactor={scaleFactor}");
                    bufferGraphics.TranslateTransform(-zoomOrigin.X, -zoomOrigin.Y);
                }

                // 绘制每个 Die
                foreach (Die die in dieList)
                {
                    int dieX = (int)(die.Bounds.X);
                    int dieY = (int)(die.Bounds.Y);
                    Rectangle dieRect = new Rectangle(dieX, dieY, dieWidth, dieHeight);

                    if (die.IsLightGreen)
                    {
                        if (die.PassFail != DIESTATE.IDLE)
                        {
                            if (die.PassFail == DIESTATE.PASS)
                            {
                                bufferGraphics.FillRectangle(Brushes.Green, dieRect);
                            }
                            else
                            {
                                bufferGraphics.FillRectangle(Brushes.Red, dieRect);
                            }
                        }
                        else
                        {
                            if (die.IsLightGreen)
                            {
                                bufferGraphics.FillRectangle(Brushes.LightYellow, dieRect);
                            }
                            else
                            {
                                bufferGraphics.FillRectangle(Brushes.LightGray, dieRect);
                            }

                        }

                    }
                    else
                    {
                        bufferGraphics.FillRectangle(Brushes.LightGray, dieRect);
                    }

                    // 绘制 Die 的边框
                    bufferGraphics.DrawRectangle(Pens.Black, dieRect);
                }


                int waferRadius = Math.Min(dieWidth * gDieCols, dieHeight * gDieRows) / 2;
                int waferCenterX = panelWidth / 2 ;
                int waferCenterY = panelHeight / 2 ;
                int scaledWaferRadius = waferRadius;

                // 定義晶圓的矩形區域
                int waferX = waferCenterX - scaledWaferRadius;
                int waferY = waferCenterY - scaledWaferRadius;
                //Console.WriteLine($"waferX={waferX}, waferY={waferY}");
                Rectangle waferRect = new Rectangle(waferX, waferY, scaledWaferRadius * 2, scaledWaferRadius * 2);

                int dotSize = 10; // 根據 scaleFactor 放大圓心大小

                bufferGraphics.FillEllipse(Brushes.Red, waferCenterX - dotSize / 2, waferCenterY - dotSize / 2, dotSize, dotSize); // 繪製圓心

                //// 绘制圆心的圆（确保它不受局部放大的影响）
                //int waferCenterX = panelWidth / 2;  // 圆心的X坐标（不受缩放影响）
                //int waferCenterY = panelHeight / 2; // 圆心的Y坐标（不受缩放影响）
                //int centerCircleRadius = 5; // 固定半径
                //bufferGraphics.FillEllipse(Brushes.Red, waferCenterX - centerCircleRadius, waferCenterY - centerCircleRadius, centerCircleRadius * 2, centerCircleRadius * 2);

                //// 绘制晶圆的轮廓
                int scaledWaferWidth = dieWidth * gDieCols / 2;
                int scaledWaferHeight = dieHeight * gDieRows/ 2;
                Rectangle scaledWaferRect = new Rectangle(
                    waferCenterX - scaledWaferWidth,
                    waferCenterY - scaledWaferHeight,
                    scaledWaferWidth * 2,
                    scaledWaferHeight * 2
                );
                bufferGraphics.DrawEllipse(Pens.Blue, scaledWaferRect);

                // 绘制框选区域（如果正在框选）
                if (isSelecting)
                {
                    bufferGraphics.DrawRectangle(Pens.Red, selectionRect); // 使用红色框选
                }

                // 将缓冲区的内容绘制到屏幕上
                g.DrawImage(bufferBitmap, Point.Empty);
            }
        }

        private void InitWaferList()
        {
            int panelWidth = this.Width;
            int panelHeight = this.Height;

            int dieWidth = panelWidth / gDieCols;
            int dieHeight = panelHeight / gDieRows;

            int waferRadius = Math.Min(dieWidth * gDieCols, dieHeight * gDieRows) / 2;

            dieList.Clear();
            dieData.Clear();

            for (int i = 0; i < gDieRows; i++)
            {
                for (int j = 0; j < gDieCols; j++)
                {
                    int dieX = (j * dieWidth) + ((panelWidth - gDieCols * dieWidth) / 2);
                    int dieY = (i * dieHeight) + ((panelHeight - gDieRows * dieHeight) / 2);
                    bool isInSideWafer = IsPointInsideEllipse(new Point(dieX + dieWidth / 2, dieY + dieHeight / 2), new Rectangle(0, 0, panelWidth, panelHeight));

                    Rectangle dieRect = new Rectangle(dieX, dieY, dieWidth, dieHeight);
                    Die die = new Die(dieRect, isInSideWafer);
                    dieData.Add(die);
                }
            }
        }

        private void UpdateDiePositions()
        {
            if (dieData.Count == 0) return;

            dieList.Clear();
            int panelWidth = this.Width;
            int panelHeight = this.Height;

            int dieWidth = panelWidth / gDieCols;
            int dieHeight = panelHeight / gDieRows;

            for (int i = 0; i < gDieRows; i++)
            {
                for (int j = 0; j < gDieCols; j++)
                {
                    int dieX = (j * dieWidth) + ((panelWidth - gDieCols * dieWidth) / 2);
                    int dieY = (i * dieHeight) + ((panelHeight - gDieRows * dieHeight) / 2);
                    Rectangle dieRect = new Rectangle(dieX, dieY, dieWidth, dieHeight);
                    Die die = new Die(dieRect, dieData[j + i * gDieCols].IsLightGreen);
                    dieList.Add(die);
                }
            }
        }

        private void ReDrawWaferMap()
        {
            UpdateDiePositions();
            this.Invalidate();
        }

        private void PanelWaferMap_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                startPoint = e.Location;
                Console.WriteLine($"-----------startPointX={startPoint.X}, startPointY={startPoint.Y}");
                isSelecting = true;
            }
        }

        private void PanelWaferMap_MouseMove(object sender, MouseEventArgs e)
        {
            if (isSelecting)
            {
                endPoint = e.Location;
                selectionRect = new Rectangle(
                    Math.Min(startPoint.X, endPoint.X),
                    Math.Min(startPoint.Y, endPoint.Y),
                    Math.Abs(startPoint.X - endPoint.X),
                    Math.Abs(startPoint.Y - endPoint.Y)
                );

                Console.WriteLine($"endPoint={endPoint.X}, endPoint={endPoint.Y}");
                this.Invalidate();
            }
        }

        private void PanelWaferMap_MouseUp(object sender, MouseEventArgs e)
        {
            if (isSelecting)
            {
                isSelecting = false;

                if (selectionRect.Width > 0 && selectionRect.Height > 0)
                {

                    ZoomIn(new Point(selectionRect.X, selectionRect.Y));                   
                                       
                }

                selectionRect = Rectangle.Empty;
                this.Invalidate();
            }
        }

        // 判断点是否在椭圆内
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

        // 加载时初始化
        private void UserControlDisplayWaferMap_Load(object sender, EventArgs e)
        {
            InitWaferList(); // 初始化 Die 数据
            ReDrawWaferMap(); // 重绘晶圆图

            Console.WriteLine($"Width={Width}, Height={Height}");


        }

        // 调整控件大小时重新绘制
        private void UserControlWaferMap_Resize(object sender, EventArgs e)
        {
            ReDrawWaferMap();
        }
    }


}
