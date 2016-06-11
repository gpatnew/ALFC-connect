using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace ALFCConnect.Common
{
    public class ExtendedButton : Button
    {
        object buttonValue;

        public object Value
        {
            get { return buttonValue; }
            set { buttonValue = value; }
        }
    }
}
