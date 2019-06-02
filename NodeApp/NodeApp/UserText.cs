using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
namespace NodeApp
{
    [Serializable()]
    public class UserText
    {        
        public string FontFamily { set; get; }

        public double FontSize { set; get; }

        public string Text { set; get; }
    }
}
