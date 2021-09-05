using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Calculator{
    class ButtonController{
        /* Класс обработчика событий с кнопок калькулятора */
        private CalculatorModel model;

        public ButtonController(){
            model = new CalculatorModel();
        }

        private bool IsValidCountOfNumber(bool isequal = false) {
            bool flag = true, dot = false;
            int countBeforeDot = 0, countAfterDot = 0;
            int reqCountBeforeDot = 10, reqCountAfterDot = 15;
            if (isequal) {
                reqCountBeforeDot++;
                reqCountAfterDot++;
            }
            foreach (char symbol in model.GetStrRes()) {
                if (Char.IsDigit(symbol)){
                    if (!dot) {
                        countBeforeDot++;
                    } else {
                        countAfterDot++;
                    }
                } else {
                    if (symbol == ',') {
                        dot = true;
                        model.SetHasDot(true);
                    }
                }
            }
            if (!model.GetHasDot()) {
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
            string strRes = model.GetStrRes();
            bool flag = double.TryParse(strRes, out double outDouble);
            if (strRes.Length == 2 && isDel) {
                flag = flag && (strRes.Substring(0, 1) != "-");
            }
            flag = (flag && !(strRes == "0" || strRes == "-0." ||
                strRes == "0." || strRes == "" || strRes == "-"));
            return flag;
        }

        private double GetCalculatorResult() {
            double res = 0;
            if (IsValidRes()) {
                model.SetNumber2(Convert.ToDouble(model.GetStrRes()));
                if (model.GetFirst()) {
                    res = model.GetNumber2();
                } else {
                    double number1 = model.GetNumber1();
                    double number2 = model.GetNumber2();
                    switch (model.GetSelect()) {
                        
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
            }
            model.SetSelect(0);
            return res;
        }

        public void OnClickButtonEqual(object sender, EventArgs eventArgs) {
            if (IsValidRes() && (model.GetSelect() != 0)) {
                model.SetNumber1(GetCalculatorResult());
                model.SetStrRes(Convert.ToString(model.GetNumber1()));
                model.SetFirst(true);
                if (!IsValidCountOfNumber(true)) {
                    model.SetStrRes("Error: количество цифр до или после запятой превышает допустимое");
                }
            } else {
                model.SetStrRes("Error");
            }
        }

        public void OnClickButtonNumber(object sender, EventArgs eventArgs) {
            var btn = (Button)sender;
            string strDigit = btn.Text;
            if (((!IsValidCountOfNumber() && (strDigit != ",")) || (model.GetHasDot() && strDigit == ",")) && !model.GetIsSelect()) {
                /* Если длина числа несоответствующая и набираешь не запятную или есть 
                 * запятая и пытаешься ввести её снова, тогда игнорируй кнопку */
                return;
            }
            if ((!IsValidRes() && strDigit != ",") || model.GetIsSelect()) {
                model.SetStrRes("");
                model.SetHasDot(false);
                model.SetIsSelect(false);
            }
            if (strDigit == ",") {
                model.SetHasDot(true);
            }
            string newStr = model.GetStrRes() + strDigit;
            model.SetStrRes(newStr);
        }

        public void OnClickButtonSelect(object sender, EventArgs eventArgs) {
            var btn = (Button)sender;
            string strSel = btn.Text;
            double num = Convert.ToDouble(model.GetStrRes());
            model.SetNumber2(num);
            if (model.GetFirst()) {
                model.SetNumber1(num);
            }
            switch (strSel) {
                case "+":
                    model.SetSelect(1);
                    break;
                case "-":
                    model.SetSelect(2);
                    break;
                case "*":
                    model.SetSelect(3);
                    break;
                case "/":
                    model.SetSelect(4);
                    break;
                case "Без остатка":
                    model.SetSelect(5);
                    break;
                case "Остаток":
                    model.SetSelect(6);
                    break;
            }
            int sel = model.GetSelect();
            if (sel >= 4 && sel <= 6 && model.GetFirst()) {
                model.SetSelect(1);
            }
            if (!model.GetFirst()) {
                model.SetNumber1(GetCalculatorResult());
            }
            model.SetSelect(sel);
            model.SetIsSelect(true);
            model.SetFirst(false);
        }

        public void OnClickButtonDel(object sender, EventArgs eventArgs) {
            string strRes = model.GetStrRes();
            if (IsValidRes(true) && (strRes.Length > 1) && !model.GetIsSelect()) {
                string strDigit = strRes.Substring(strRes.Length - 1);
                model.SetStrRes(strRes.Substring(0, strRes.Length - 1));
                if (strDigit == ",") {
                    model.SetHasDot(false);
                }
            } else {
                model.SetStrRes("0");
                model.SetHasDot(false);
            }
        }

        public void OnClickButtonChange(object sender, EventArgs eventArgs) {
            var btn = (Button)sender;
            string strSel = btn.Text;
            bool flagNeg = false;
            if (IsValidRes()) {
                model.SetNumber1(Convert.ToDouble(model.GetStrRes()));
                switch (strSel) {
                    case "^2":
                        model.SetNumber1(model.GetNumber1() * model.GetNumber1()); ;
                        break;
                    case "√":
                        if (model.GetNumber1() >= 0) {
                            model.SetNumber1(Math.Sqrt(model.GetNumber1()));
                        } else {
                            flagNeg = true;
                        }
                        break;
                    default:
                        break;
                }
                model.SetStrRes(Convert.ToString(model.GetNumber1()));
                if (!IsValidCountOfNumber()) {
                    model.SetStrRes("Error: количество цифр до или после запятой превышает допустимое");
                } else if (flagNeg) {
                    model.SetStrRes("Error: отрицательное число под корнем");
                }
                
            }
        }

        public string GetRes() {
            return model.GetStrRes();
        }

        /* Работа с памятью */

        public void OnClickButtonClearMemory(object sender, EventArgs eventArgs) {
            model.SetNumberMemory(0);
        }

        public void OnClickButtonPlusNumberMemory(object sender, EventArgs eventArgs) {
            if (IsValidRes() && IsValidCountOfNumber()) {
                model.SetNumberMemory(Convert.ToDouble(model.GetStrRes()));
            }
        }

        public void OnClickButtonMinusNumberMemory(object sender, EventArgs eventArgs) {
            if (IsValidRes() && IsValidCountOfNumber()) {
                model.SetNumberMemory(Convert.ToDouble(model.GetStrRes()));
            }
        }

        public void OnClickButtonReadNumberMemory(object sender, EventArgs eventArgs) {
            if (IsValidRes() && IsValidCountOfNumber()) {
                model.SetNumberMemory(Convert.ToDouble(model.GetStrRes()));
            }
        }

        public string GetMemoryRes(bool change = false) {
            if (change) {
                model.SetStrRes(Convert.ToString(model.GetNumberMemory()));
            }
            return model.GetStrRes();
        }

        public void OnClickProButton(object sender, EventArgs eventArgs) {
            bool proMode = model.GetProMode();
            model.SetProMode(!proMode);
        }

        public bool GetPro() {
            return model.GetProMode();
        }
    }
}
