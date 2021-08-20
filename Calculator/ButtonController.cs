using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Calculator{
    class ButtonController{
        /* Класс обработчика событий с кнопок калькулятора */
        private string strRes;
        //private double res;
        private double number1, number2;
        private double numberMemory;
        private int select; // Select: 1(+), 2(-), 3(*), 4(/), 5(div), 6(mod)

        private bool IsValidRes() {
            double outDouble;
            bool flag = double.TryParse(strRes, out outDouble);
            if (flag && (strRes == "0" || strRes == "-0." || strRes == "0." || strRes == "")) {
                flag = false;
            }
            return flag;
        }

        public ButtonController() {
            strRes = "0";
            number1 = 0;
            number2 = 0;
            numberMemory = 0;
            select = 0;
        }

        public void ButtonNumberOnClick(object sender, EventArgs eventArgs) {
            var btn = (Button)sender;
            string strDigit = btn.Text;
            if (!IsValidRes()) {
                strRes = "";
            }
            strRes += strDigit;
        }

        public string GetRes() {
            return strRes;
        }
    }
}
