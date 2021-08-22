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
        private bool hasDot;
        private int countBeforeDot, countAfterDot;
        private int select; // Select: 1(+), 2(-), 3(*), 4(/), 5(div), 6(mod)

        private bool IsValidCountOfNumber() {
            bool flag = true;
            if (!hasDot) {
                if (countBeforeDot >= 8) {
                    flag = false;
                }
            } else {
                if (countAfterDot >= 7) {
                    flag = false;
                }
            }
            return flag;
        }

        private bool IsValidRes() {
            double outDouble;
            bool flag = double.TryParse(strRes, out outDouble);
            if (flag && (strRes == "0" || strRes == "-0." || strRes == "0." || strRes == "")) {
                flag = false;
            }
            return flag;
        }

        private void ChangeCountOfDigit(int sel=1) {
            if (sel > 0) {
                sel = 1;
            }
            if (sel < 0) {
                sel = -1;
            }
            if (!hasDot) {
                countBeforeDot += sel;
            } else {
                countAfterDot += sel;
            }
        }

        public ButtonController() {
            strRes = "0";
            number1 = 0;
            number2 = 0;
            countBeforeDot = 1;
            countAfterDot = 0;
            numberMemory = 0;
            select = 0;
            hasDot = false;
        }

        public void OnClickButtonNumber(object sender, EventArgs eventArgs) {
            var btn = (Button)sender;
            string strDigit = btn.Text;
            if ((!IsValidCountOfNumber() && (strDigit != ",")) || (hasDot && strDigit == ",")) {
                return;
            }
            if (!IsValidRes() && strDigit != ",") {
                strRes = "";
                countBeforeDot = 0;
                countAfterDot = 0;
            }
            if (strDigit == ",") {
                hasDot = true;
            } else {
                ChangeCountOfDigit(1);
            }
            strRes += strDigit;
        }

        public void OnClickButtonDel(object sender, EventArgs eventArgs) {
            if (IsValidRes() && strRes != "0") {
                string strDigit = strRes.Substring(strRes.Length - 1);
                strRes = strRes.Substring(0, strRes.Length - 1);
                if (strDigit == ",") {
                    hasDot = false;
                } else {
                    ChangeCountOfDigit(-1);
                }
            } else {
                strRes = "0";
                hasDot = false;
                countAfterDot = 0;
                countBeforeDot = 1;
            }
        }

        public void OnClickButtonSelect(object sender, EventArgs eventArgs) {
            if (select == 0) {

            }
        }

        public string GetRes() {
            return strRes;
        }
    }
}
