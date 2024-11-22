using ConsoleApp2;
using HedgeHulkApp.Usercontrol;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsWaferMap
{
    public partial class Form1 : Form
    {
        private StatusStrip statusStrip;
        private ToolStripStatusLabel mousePositionLabel;

        WafeMapSetting wsetting = new WafeMapSetting();     

        CallbackDelegateGUI callbackDelegateGUI;
        public Form1()
        {
            InitializeComponent();

            InitMenu();
        }

        private void InitMenu()
        {
            // 初始化 MenuStrip
            MenuStrip menuStrip = new MenuStrip();

            // 建立 "File" 功能表
            ToolStripMenuItem fileMenu = new ToolStripMenuItem("File");
            ToolStripMenuItem NewItem = new ToolStripMenuItem("New", null, NewFile);
            ToolStripMenuItem openItem = new ToolStripMenuItem("Open", null, OpenFile);
            ToolStripMenuItem saveItem = new ToolStripMenuItem("Save", null, SaveFile);
            ToolStripMenuItem exitItem = new ToolStripMenuItem("Exit", null, ExitApp);

            // 將子選單加到 File
            fileMenu.DropDownItems.Add(NewItem);
            fileMenu.DropDownItems.Add(openItem);
            fileMenu.DropDownItems.Add(saveItem);
            fileMenu.DropDownItems.Add(new ToolStripSeparator()); // 分隔線
            fileMenu.DropDownItems.Add(exitItem);

            // 建立 "Zoom" 功能表
            ToolStripMenuItem ViewMenu = new ToolStripMenuItem("View");
            ToolStripMenuItem ZoomInItem = new ToolStripMenuItem("Zoom In", null, ZoomIn);
            ToolStripMenuItem ZoomOutItem = new ToolStripMenuItem("Zoom Out", null, ZoomOut);
            ToolStripMenuItem JumpItem = new ToolStripMenuItem("Jump", null, JumpPosition);

            // 將子選單加到 Edit
            ViewMenu.DropDownItems.Add(ZoomInItem);
            ViewMenu.DropDownItems.Add(ZoomOutItem);
            ViewMenu.DropDownItems.Add(JumpItem);


            // 建立 "Function" 功能表
            ToolStripMenuItem FunctionMenu = new ToolStripMenuItem("Function");
            ToolStripMenuItem DemoInItem = new ToolStripMenuItem("Demo", null, ZoomIn);


            // 將子選單加到 Function
            FunctionMenu.DropDownItems.Add(DemoInItem);
            


            // 建立 "Help" 功能表
            ToolStripMenuItem helpMenu = new ToolStripMenuItem("Help");
            ToolStripMenuItem aboutItem = new ToolStripMenuItem("About", null, ShowAbout);

            // 將子選單加到 Help
            helpMenu.DropDownItems.Add(aboutItem);

            // 將功能表加到 MenuStrip
            menuStrip.Items.Add(fileMenu);
            menuStrip.Items.Add(ViewMenu);
            menuStrip.Items.Add(FunctionMenu);            
            menuStrip.Items.Add(helpMenu);

            // 設定 MenuStrip 到表單
            this.MainMenuStrip = menuStrip;
            this.Controls.Add(menuStrip);

            // 設定表單屬性
            this.Text = "Wafer Map Editor";
            //this.Width = 800;
            //this.Height = 600;


            //menu
            // 初始化 StatusStrip
            statusStrip = new StatusStrip();

            // 添加一個 ToolStripStatusLabel 用於顯示滑鼠座標
            mousePositionLabel = new ToolStripStatusLabel
            {
                Text = "Mouse Position: (0, 0)",
                TextAlign = System.Drawing.ContentAlignment.MiddleRight,
                Spring = true // 使用 Spring 屬性讓 Label 撐滿空間並將文字置右
            };

            // 將 Label 添加到狀態欄
            statusStrip.Items.Add(mousePositionLabel);

            // 將狀態欄添加到表單
            this.Controls.Add(statusStrip);


        }



        public void SetCallBackFun(CallbackDelegateGUI callback, WafeMapSetting setting)
        {
            callbackDelegateGUI = callback;
            wsetting = setting;
        }
        private void Form1_Load(object sender, EventArgs e)
        {

            
            SetCallBackFun(userControlDisplayWaferMap1.commandcallback, wsetting);

            //userControlDisplayWaferMap1.Dock = DockStyle.Fill;
            userControlDisplayWaferMap1.SetCallback(callback);
            userControlDisplayWaferMap1.SetMouseMoveCallback(mousemovecallback);
            propertyGrid.SelectedObject = wsetting;

           


        }

        private void mousemovecallback(int x, int y)
        {
            mousePositionLabel.Text = $"Mouse Position: ({x}, {y})";
        }

        private void callback(string message, int selIndex)
        {
            Console.WriteLine($"this is callback messge:{message}, index={selIndex}");

            if (wsetting.dieData.Count() == 0) return;

            if(selIndex!= -1)
            {
                Console.WriteLine($"X={wsetting.dieData[selIndex].Column}, Y={wsetting.dieData[selIndex].Row}");

                wsetting.selectedDie.Column = wsetting.dieData[selIndex].Column;
                wsetting.selectedDie.Row = wsetting.dieData[selIndex].Row;
                wsetting.selectedDie.Index = selIndex;
                wsetting.Number = wsetting.dieData[selIndex].Number;
                propertyGrid.Refresh();
            }
            
        }
        private void NewFile(object sender, EventArgs e)
        {
            callbackDelegateGUI?.Invoke(COMMANDCODE.NEW_MAP, wsetting);

            propertyGrid.Refresh();

        }
        private void OpenFile(object sender, EventArgs e)
        {

            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "TSK Files (*.dat)|*.dat|All Files (*.*)|*.*";
                openFileDialog.Title = "Open a DAT File";

                // 如果用戶選擇了文件並按下確定
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        WaferMapAgtent waferMapAgtent = new WaferMapAgtent();

                        waferMapAgtent.ReadFile(openFileDialog.FileName);
                        // 讀取選擇的文件內容

                        wsetting.LoadWafer(waferMapAgtent);

                        callbackDelegateGUI?.Invoke(COMMANDCODE.OPEN_MAP, wsetting);
                        propertyGrid.Refresh();
                    }
                    catch (Exception ex)
                    {
                        // 顯示錯誤訊息
                        MessageBox.Show($"Error reading file: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void SaveFile(object sender, EventArgs e)
        {
            MessageBox.Show("儲存檔案功能尚未實作。", "提示");
        }

        private void ExitApp(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void ZoomIn(object sender, EventArgs e)
        {
            callbackDelegateGUI?.Invoke(COMMANDCODE.WAFER_ZOOM_IN, wsetting);
        }

        private void ZoomOut(object sender, EventArgs e)
        {
            callbackDelegateGUI?.Invoke(COMMANDCODE.WAFER_ZOOM_OUT, wsetting);
        }
        private void JumpPosition(object sender, EventArgs e)
        {
            callbackDelegateGUI?.Invoke(COMMANDCODE.JUMP_POSITION, wsetting);
        }
        private void ShowAbout(object sender, EventArgs e)
        {
            MessageBox.Show("This is a wafer map editor", "About");
        }
    }
}
