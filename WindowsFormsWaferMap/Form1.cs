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

        WafeMapSetting wsetting = new WafeMapSetting();
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

            userControlControlWaferMap1.SetCallBackFun(userControlDisplayWaferMap1.commandcallback, wsetting);


            userControlDisplayWaferMap1.Dock = DockStyle.Fill;
            userControlDisplayWaferMap1.SetCallback(callback);

            
        }

        private void callback(string message)
        {
            Console.WriteLine("this is callback messge:" + message);
        }
    }
}
