using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsWaferMap
{

    public class SelectedDie
    {
        // 確保這些屬性是 public
        [Category("Sub1"), DisplayName("Column")]
        public int Column { get; set; }

        [Category("Sub1"), DisplayName("Row")]
        public int Row { get; set; }

        [Category("Sub1"), DisplayName("Index")]
        public int Index { get; set; }


        public override string ToString()
        {
            return $"[{Column},{Row}]";
        }
    }

}
