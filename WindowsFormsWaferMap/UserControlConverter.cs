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
    public partial class UserControlConverter : UserControl
    {
        public UserControlConverter()
        {
            InitializeComponent();
        }

        private void UserControlConverter_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;

            // 1. 翻轉 X 和 Y 軸（向右為負，向下為負）
            g.ScaleTransform(-1, -1);

            // 2. 平移，使左上角仍為 (0, 0)
            g.TranslateTransform(-50, -50);

            // 3. 繪製線條
            int ox = 0;
            int oy = 0;
            g.DrawLine(Pens.Red, ox, oy, -100, 0);   // 向右 (實際是 X 減少)
            g.DrawLine(Pens.Blue, ox, oy, 0, -100); // 向下 (實際是 Y 減少)
        }

        private void UserControlConverter_MouseClick(object sender, MouseEventArgs e)
        {

        }
    }
}
