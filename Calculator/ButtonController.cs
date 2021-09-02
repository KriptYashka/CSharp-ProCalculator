﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Calculator{
    class ButtonController{
        /* Класс обработчика событий с кнопок калькулятора */
        private string strRes;
        private double number1, number2;
        private double numberMemory;
        private bool isSelect;
        private bool hasDot;
        private int select; // Select: 1(+), 2(-), 3(*), 4(/), 5(div), 6(mod)
        private bool proMode;

        private bool IsValidCountOfNumber(bool isequal = false) {
            bool flag = true, dot = false;
            int countBeforeDot = 0, countAfterDot = 0;
            int reqCountBeforeDot = 10, reqCountAfterDot = 15;
            if (isequal) {
                reqCountBeforeDot++;
                reqCountAfterDot++;
            }
            foreach (char symbol in strRes) {
                if (Char.IsDigit(symbol)){
                    if (!dot) {
                        countBeforeDot++;
                    } else {
                        countAfterDot++;
                    }
                } else {
                    if (symbol == ',') {
                        dot = true;
                        hasDot = true;
                    }
                }
            }
            if (!hasDot) {
                if (countBeforeDot >= reqCountBeforeDot) {
                    flag = false;
                }
            } else {
                if (countAfterDot >= reqCountAfterDot) {
                    flag = false;
                }
            }
            return flag;
        }

        private bool IsValidRes(bool isDel = false) {
            bool flag = double.TryParse(strRes, out double outDouble);
            if (strRes.Length == 2 && isDel) {
                flag = flag && (strRes.Substring(0, 1) != "-");
            }
            flag = (flag && !(strRes == "0" || strRes == "-0." || 
                strRes == "0." || strRes == "" || strRes == "-"));
            return flag;
        }

        public ButtonController() {
            strRes = "0";
            number1 = 0;
            number2 = 0;
            numberMemory = 0;
            select = 0;
            hasDot = false;
            isSelect = false;
            proMode = false;
        }

        private double GetCalculatorResult() {
            double res = 0;
            if (IsValidRes()) {
                number2 = Convert.ToDouble(strRes);
                switch (select) {
                    case 1:
                        res = number1 + number2;
                        break;
                    case 2:
                        res = number1 - number2;
                        break;
                    case 3:
                        res = number1 * number2;
                        break;
                    case 4:
                        res = number1 / number2;
                        break;
                    case 5:
                        res = (int)(number1 / number2);
                        break;
                    case 6:
                        res = number1 % number2;
                        break;
                }
            }
            select = 0;
            return res;
        }

        public void OnClickButtonEqual(object sender, EventArgs eventArgs) {
            if (IsValidRes() && (select != 0)) {
                double res = GetCalculatorResult();
                strRes = Convert.ToString(res);
                if (!IsValidCountOfNumber(true)) {
                    strRes = "Error: количество цифр до или после запятой превышает допустимое";
                }
            } else {
                strRes = "Error";
            }
        }

        public void OnClickButtonNumber(object sender, EventArgs eventArgs) {
            var btn = (Button)sender;
            string strDigit = btn.Text;
            if (((!IsValidCountOfNumber() && (strDigit != ",")) || (hasDot && strDigit == ",")) && !isSelect) {
                /* Если длина числа несоответствующая и набираешь не запятную или есть 
                 * запятая и пытаешься ввести её снова, тогда игнорируй кнопку */
                return;
            }
            if ((!IsValidRes() && strDigit != ",") || isSelect) {
                strRes = "";
                hasDot = false;
                isSelect = false;
            }
            if (strDigit == ",") {
                hasDot = true;
            }
            strRes += strDigit;
        }

        public void OnClickButtonSelect(object sender, EventArgs eventArgs) {
            var btn = (Button)sender;
            string strSel = btn.Text;
            number2 = Convert.ToDouble(strRes);
            switch (strSel) {
                case "+":
                    select = 1;
                    break;
                case "-":
                    select = 2;
                    break;
                case "*":
                    select = 3;
                    break;
                case "/":
                    select = 4;
                    break;
                case "Без остатка":
                    select = 5;
                    break;
                case "Остаток":
                    select = 6;
                    break;
            }
            int save = select;
            number1 = GetCalculatorResult();
            select = save;
            isSelect = true;
        }

        public void OnClickButtonDel(object sender, EventArgs eventArgs) {
            if (IsValidRes(true) && (strRes.Length > 1)) {
                string strDigit = strRes.Substring(strRes.Length - 1);
                strRes = strRes.Substring(0, strRes.Length - 1);
                if (strDigit == ",") {
                    hasDot = false;
                }
            } else {
                strRes = "0";
                hasDot = false;
            }
        }

        public void OnClickButtonChange(object sender, EventArgs eventArgs) {
            var btn = (Button)sender;
            string strSel = btn.Text;
            bool flagNeg = false;
            if (IsValidRes()) {
                number1 = Convert.ToDouble(strRes);
                switch (strSel) {
                    case "^2":
                        number1 *= number1;
                        break;
                    case "√":
                        if (number1 >= 0) {
                            number1 = Math.Sqrt(number1);
                        } else {
                            flagNeg = true;
                        }
                        break;
                    default:
                        break;
                }
                strRes = Convert.ToString(number1);
                if (!IsValidCountOfNumber()) {
                    strRes = "Error: количество цифр до или после запятой превышает допустимое";
                } else if (flagNeg) {
                    strRes = "Error: отрицательное число под корнем";
                }
                
            }
        }

        public string GetRes() {
            return strRes;
        }

        /* Работа с памятью */

        public void OnClickButtonClearMemory(object sender, EventArgs eventArgs) {
            numberMemory = 0;
        }

        public void OnClickButtonPlusNumberMemory(object sender, EventArgs eventArgs) {
            if (IsValidRes() && IsValidCountOfNumber()) {
                numberMemory += Convert.ToDouble(strRes);
            }
        }

        public void OnClickButtonMinusNumberMemory(object sender, EventArgs eventArgs) {
            if (IsValidRes() && IsValidCountOfNumber()) {
                numberMemory -= Convert.ToDouble(strRes);
            }
        }

        public void OnClickButtonReadNumberMemory(object sender, EventArgs eventArgs) {
            if (IsValidRes() && IsValidCountOfNumber()) {
                numberMemory = Convert.ToDouble(strRes);
            }
        }

        public string GetMemoryRes() {
            strRes = Convert.ToString(numberMemory);
            return strRes;
        }

        public void OnClickProButton(object sender, EventArgs eventArgs) {
            proMode = !proMode;
        }

        public bool GetPro() {
            return proMode;
        }
    }
}
