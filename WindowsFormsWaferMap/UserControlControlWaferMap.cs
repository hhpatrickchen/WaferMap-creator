
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
    public enum COMMANDCODE
    {
        WAFER_ZOOM_IN = 0,
        WAFER_ZOOM_OUT,
        WAFER_RUN,
        WAFER_RESET,
        WAFER_RESET_ZOOM,
        JUMP_POSITION,
        NEW_MAP,
        OPEN_MAP,
    }

    public delegate void CallbackDelegateGUI(COMMANDCODE commmand, WafeMapSetting setting=null);
    public partial class UserControlControlWaferMap : UserControl
    {
        

        public CallbackDelegateGUI callbackDelegateGUI;

        WafeMapSetting wsetting;
        public UserControlControlWaferMap()
        {
            InitializeComponent();
        }
        public void SetCallBackFun(CallbackDelegateGUI callback, WafeMapSetting setting)
        {
            callbackDelegateGUI = callback;
            wsetting = setting;
        }

        private void ZoomIn_Click(object sender, EventArgs e)
        {
            callbackDelegateGUI?.Invoke(COMMANDCODE.WAFER_ZOOM_IN);
        }

        private void ZoomOut_Click(object sender, EventArgs e)
        {
            callbackDelegateGUI?.Invoke(COMMANDCODE.WAFER_ZOOM_OUT);
        }

        private void buttonRun_Click(object sender, EventArgs e)
        {
            Random rnd = new Random();
            //wsetting.count = rnd.Next(80, 100);
            wsetting.startPosition = 94;
            Thread thread = new Thread(() =>
            {

                int x = 40;
                int y = 10;
                for (int i = 0; i < 20; i++)
                {

                    if (i % 2==0)
                    {
                        x += 25;
                       
                        int factor = 4;
                        wsetting.axisX = x;
                        wsetting.axisY = y;
                        wsetting.factor = factor;
                        callbackDelegateGUI?.Invoke(COMMANDCODE.JUMP_POSITION, wsetting);
                        Thread.Sleep(600);
                    }
                    else 
                    {
              
                        int factor = 1;
                        wsetting.axisX = x;
                        wsetting.axisY = y;
                        wsetting.factor = factor;
                        callbackDelegateGUI?.Invoke(COMMANDCODE.JUMP_POSITION, wsetting);
                        Thread.Sleep(600);
                    }
                    
                    
                    callbackDelegateGUI?.Invoke(COMMANDCODE.WAFER_RUN, wsetting);
                    Thread.Sleep(600);
                    Console.WriteLine($"Count={wsetting.startPosition}");
                    wsetting.startPosition++;
                    



     
                }

            });

            thread.Start();


        }
        private void buttonResetWafer_Click(object sender, EventArgs e)
        {
            callbackDelegateGUI?.Invoke(COMMANDCODE.WAFER_RESET);
        }

        public void Execute()
        {
            callbackDelegateGUI?.Invoke(COMMANDCODE.WAFER_RESET);
            callbackDelegateGUI?.Invoke(COMMANDCODE.WAFER_RUN);
        }

        private void buttonResetZoom_Click(object sender, EventArgs e)
        {
            callbackDelegateGUI?.Invoke(COMMANDCODE.WAFER_RESET_ZOOM);
        }

        private void buttonJump_Click(object sender, EventArgs e)
        {

            //int x = Convert.ToInt32(textBoxAxisX.Text);
            //int y = Convert.ToInt32(textBoxAxisY.Text);
            //int factor = Convert.ToInt32(textBoxFactor.Text);
            
            //wsetting.axisX = x;
            //wsetting.axisY = y;
            //wsetting.factor = factor;
            callbackDelegateGUI?.Invoke(COMMANDCODE.JUMP_POSITION, wsetting);
        }

        private void buttonNewMap_Click(object sender, EventArgs e)
        {

            //int dwidth = Convert.ToInt32(textBoxDieWidth.Text);
            //int dheight = Convert.ToInt32(textBoxDieHeight.Text);

            //int diemeter = Convert.ToInt32(textBoxWaferDiemeter.Text);

            //int borderness = Convert.ToInt32(textBoxWaferBorderness.Text);
            //wsetting.wafer_borderness = borderness;
            //wsetting.dwidth = dwidth;
            //wsetting.dheight = dheight;
            //wsetting.wafer_diemeter = diemeter;
            callbackDelegateGUI?.Invoke(COMMANDCODE.NEW_MAP, wsetting);
        }
    }
}
