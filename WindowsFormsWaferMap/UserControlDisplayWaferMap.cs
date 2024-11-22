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
        public delegate void CallbackDelegate(string message, int selIndex);

        public delegate void CallbackMouseMove(int x, int y);

        CallbackDelegate callbackfun;

        CallbackMouseMove callbacmousemove;

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
        //List<Die> dieData = new List<Die>();

       
        DIESTATE testst = DIESTATE.PASS;
        public UserControlDisplayWaferMap()
        {
            InitializeComponent();
            this.Paint += PanelWaferMap_Paint;
            this.Resize += UserControlWaferMap_Resize; // 添加Resize事件
            this.Load += UserControlDisplayWaferMap_Load; // 添加Load事件

            this.MouseWheel += PanelWaferMap_MouseWheel;
            this.MouseMove += PanelWaferMap_MouseMove;

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
        public void SetMouseMoveCallback(CallbackMouseMove callback)
        {
            callbacmousemove = callback;
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
                case COMMANDCODE.WAFER_RESET: WaferReset(wafeMapSetting);break;
                case COMMANDCODE.NEW_MAP: NewMap(wafeMapSetting);break;
                case COMMANDCODE.OPEN_MAP: OpenMap(wafeMapSetting); break;
                default: throw new Exception("Unexpected command=" + command);
            }
        }

        private void OpenMap(WafeMapSetting wafeMapSetting)
        {
            dieList.Clear();            
            dieList.AddRange(wafeMapSetting.dieData);


            gDieCols = wafeMapSetting.col;
            gDieRows = wafeMapSetting.row;

            ReDrawWaferMap(); // 重绘晶圆图

            Console.WriteLine($"Width={Width}, Height={Height}");
        }

        private void NewMap(WafeMapSetting wafeMapSetting)
        {

            InitWaferList(wafeMapSetting);

            gDieCols = wafeMapSetting.col;
            gDieRows = wafeMapSetting.row;

            ReDrawWaferMap(); // 重绘晶圆图

            Console.WriteLine($"Width={Width}, Height={Height}");


        }

        private void WaferReset(WafeMapSetting wafeMapSetting)
        {
            InitWaferList(wafeMapSetting); // 初始化 Die 数据
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
            int count = wafeMapSetting.startPosition;
            Console.WriteLine("Demo Run count");

            while (dieList[count].diePos != DiePosition.Edge)
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
        private Rectangle RestoreBounds(Rectangle adjustedBounds)
        {
            // 還原 X 和 Y，反向處理縮放
            int restoredX = (int)((adjustedBounds.X / scaleFactor) + zoomOrigin.X);
            int restoredY = (int)((adjustedBounds.Y / scaleFactor) + zoomOrigin.Y);

            // 還原 Width 和 Height，反向處理縮放
            int restoredWidth = (int)(adjustedBounds.Width / scaleFactor);
            int restoredHeight = (int)(adjustedBounds.Height / scaleFactor);

            return new Rectangle(restoredX, restoredY, restoredWidth, restoredHeight);
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

               // bufferGraphics.ScaleTransform(-1, -1);

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
                    if (die.diePos == DiePosition.OutsideEdge)
                    {
                        continue;
                    }

                    int dieX = (int)(die.Bounds.X);
                    int dieY = (int)(die.Bounds.Y);
                    Rectangle dieRect = new Rectangle(dieX, dieY, dieWidth, dieHeight);

                    if (die.diePos == DiePosition.Edge)
                    {
                        //if (die.PassFail != DIESTATE.IDLE)
                        //{
                        //    if (die.PassFail == DIESTATE.PASS)
                        //    {
                        //        bufferGraphics.FillRectangle(Brushes.Green, dieRect);
                        //    }
                        //    else
                        //    {
                        //        bufferGraphics.FillRectangle(Brushes.Red, dieRect);
                        //    }
                        //}
                        //else
                        {
         

                            bufferGraphics.FillRectangle(Brushes.LightGray, dieRect);
                        }

                    }
                    else if (die.diePos == DiePosition.InsideEdge)
                    {
                        bufferGraphics.FillRectangle(Brushes.LightYellow, dieRect);
                    }
                    else
                    {
                        bufferGraphics.FillRectangle(Brushes.WhiteSmoke, dieRect);
                    }

                    // 绘制 Die 的边框
                    bufferGraphics.DrawRectangle(Pens.Black, dieRect);

                    // 在 Die 中心繪製編號
                    if(scaleFactor>1)
                    {
                        // 動態計算字體大小
                        float fontSize = Math.Min(dieWidth, dieHeight) * 0.5f; // 字體大小是 Die 寬高的 50%
                        if (fontSize < 1) fontSize = 1;
                  
                        // 在 Die 中心繪製編號
                        string dieNumber = die.Number.ToString("").PadLeft(2, ' '); // 假設 Die 中有 Number 屬性
                        if (die.diePos == DiePosition.Edge)
                        {
                            dieNumber = "ED";
                        }

                        using (Font font = new Font("Arial", fontSize, FontStyle.Bold))
                        using (StringFormat format = new StringFormat())
                        {
                            format.Alignment = StringAlignment.Center; // 文字水平居中
                            format.LineAlignment = StringAlignment.Center; // 文字垂直居中

                            bufferGraphics.DrawString(dieNumber, font, Brushes.Black, dieRect, format);
                        }
                    }
             
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

        private void InitWaferList(WafeMapSetting wafeMapSetting)
        {
           // int waferRadius = Math.Min(dieWidth * gDieCols, dieHeight * gDieRows) / 2;

            dieList.Clear();

            //afeMapSetting.initWaferList();



            wafeMapSetting.initWafer();

            dieList.AddRange(wafeMapSetting.dieData);


            //for (int i = 0; i < gDieRows; i++)
            //{
            //    for (int j = 0; j < gDieCols; j++)
            //    {
            //        int dieX = (j * dieWidth) + ((panelWidth - gDieCols * dieWidth) / 2);
            //        int dieY = (i * dieHeight) + ((panelHeight - gDieRows * dieHeight) / 2);
            //        bool isInSideWafer = IsPointInsideEllipse(new Point(dieX + dieWidth / 2, dieY + dieHeight / 2), new Rectangle(0, 0, panelWidth, panelHeight));

            //        Rectangle dieRect = new Rectangle(dieX, dieY, dieWidth, dieHeight);
            //        Die die = new Die(dieRect, isInSideWafer);
            //        dieData.Add(die);
            //    }
            //}
        }

        private void UpdateDiePositions()
        {
            if (dieList.Count == 0) return;

            //dieList.Clear();
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
                    //Die die = new Die(dieRect, dieData[j + i * gDieCols].IsLightGreen);
                    //dieList[j + i * gDieCols] = new Die(dieRect, dieList[j + i * gDieCols].IsEdge);
                    dieList[j + i * gDieCols].Bounds = dieRect;
                    
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


            callbacmousemove?.Invoke(e.X, e.Y);
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
            //InitWaferList(wafeMapSetting); // 初始化 Die 数据
           // ReDrawWaferMap(); // 重绘晶圆图

            Console.WriteLine($"Width={Width}, Height={Height}");


        }

        // 调整控件大小时重新绘制
        private void UserControlWaferMap_Resize(object sender, EventArgs e)
        {
            ReDrawWaferMap();
            ResetZoom();
            Console.WriteLine($"Width={Width}, Height={Height}");
        }

        private void UserControlDisplayWaferMap_MouseClick(object sender, MouseEventArgs e)
        {
            // 將點擊位置根據縮放比例還原

            Console.WriteLine($"e.Location.X={e.Location.X},e.Location.Y={e.Location.Y}");
            //Point originalClickPoint = new Point(e.Location.X , e.Location.Y );

            int restoredX = (int)((e.Location.X / scaleFactor) + zoomOrigin.X);
            int restoredY = (int)((e.Location.Y / scaleFactor) + zoomOrigin.Y);
            Point originalClickPoint = new Point(restoredX, restoredY);

            Console.WriteLine($"originalClickPoint.X={originalClickPoint.X},restoredY={originalClickPoint.Y}");

            // Die 數量（列和行）
            int dieColumns = gDieCols; // 水平方向的 Die 數量
            int dieRows = gDieRows;       // 垂直方向的 Die 數量

            // Die 的寬和高（像素）
            int dieWidth = this.Width / dieColumns;
            int dieHeight = this.Height / dieRows;

           // Console.WriteLine($"Panel Width: {this.Width}, Height: {this.Height}");
           // Console.WriteLine($"Die Width: {dieWidth}, Height: {dieHeight}");

            // 計算點擊的 Die
            int clickedDieIndex = GetClickedDie(originalClickPoint, dieWidth, dieHeight, dieColumns, dieRows);

            if (clickedDieIndex != -1)
            {
                // 點擊到了某個 Die
                callbackfun($"您點擊了 die {clickedDieIndex}", clickedDieIndex);

                if (dieList.Count() == 0) return;

                if(dieList[clickedDieIndex].diePos == DiePosition.Edge)
                {
                    dieList[clickedDieIndex].diePos =  DiePosition.InsideEdge;
                }
                else if(dieList[clickedDieIndex].diePos == DiePosition.InsideEdge)
                {
                    dieList[clickedDieIndex].diePos = DiePosition.Edge;
                }


                Rectangle adjustedBounds = AdjustedBounds(dieList[clickedDieIndex].Bounds);


                Console.WriteLine($"Post={dieList[clickedDieIndex].diePos},Index={clickedDieIndex}, Bounds.X={adjustedBounds.X},Bounds.Y={adjustedBounds.Y}" +
                                    $"Width={adjustedBounds.Width}, Height={adjustedBounds.Height}");
                // 只更新部分区域
                Invalidate(adjustedBounds);
                


            }
            else
            {
                // 未點擊到任何 Die
                callbackfun("您沒有點擊到任何 die", clickedDieIndex);
            }
        }

        private int GetClickedDie(Point clickPoint, int dieWidth, int dieHeight, int dieColumns, int dieRows)
        {
            // Panel 的寬和高
            int panelWidth = dieColumns * dieWidth;
            int panelHeight = dieRows * dieHeight;

            // 計算點擊位置相對於 Panel 的座標
            int relativeX = clickPoint.X - (this.Width - panelWidth) / 2;
            int relativeY = clickPoint.Y - (this.Height - panelHeight) / 2;

            // 確認點擊是否在 Panel 範圍內
            if (relativeX >= 0 && relativeX < panelWidth &&
                relativeY >= 0 && relativeY < panelHeight)
            {
                // 計算點擊位置對應的 Die 的索引
                int clickedDieX = relativeX / dieWidth;
                int clickedDieY = relativeY / dieHeight;

                // 返回 Die 的索引
                return clickedDieX + clickedDieY * dieColumns;
            }

            // 如果點擊位置在範圍外，返回 -1
            return -1;
        }
    }


}
