using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Calculator {
    public partial class FormCalculator : Form {
        private Button[] numberButtons;
        private Button[] selectButtons;
        private ButtonController btnController;

        private void ChangeData(object sender, EventArgs arg) {
            string res = btnController.GetRes();
            this.labelResult.Text = res;
        }

        private void ChangeDataFromMemory(object sender, EventArgs eventArgs) {
            string res = btnController.GetMemoryRes(true);
            this.labelResult.Text = res;
        }

        private void ChangeDataFieldMemory(object sender, EventArgs eventArgs) {
            string res = btnController.GetMemoryRes();
            this.labelMemory.Text = res;
        }

        private void ProMode(object sender, EventArgs eventArgs) {
            Color color;
            if (btnController.GetPro()) {
                color = Color.Red;
                string digits = "0123456789";
                Random r = new Random();
                for (int i = 0; i < 10; ++i) {
                    int index = r.Next(digits.Length);
                    char digitChr = digits[index];
                    digits = digits.Remove(index, 1);
                    numberButtons[i].Text = Convert.ToString(digitChr);
                }

            } else {
                color = Color.White;
                for (int i = 0; i < 10; ++i) {
                    numberButtons[i].Text = Convert.ToString(i);
                }
            }
            btnPro.BackColor = color;
        }

        public FormCalculator() {
            InitializeComponent();
            btnController = new ButtonController();
            numberButtons = new Button[] { btn0, btn1, btn2, btn3, btn4, btn5,
                                           btn6, btn7, btn8, btn9, btnDot }; // btnDot - исключение
            selectButtons = new Button[] { btnPlus, btnMinus, btnMultiply, btnRealDiv, btnDiv, btnMod };
            Button[] changeButtons = new Button[] { btnSqr, btnSqrt };
            foreach (Button btn in numberButtons) {
                btn.Click += btnController.OnClickButtonNumber;
                btn.Click += ChangeData;
                btn.Click += ProMode;
            }
            foreach (Button btn in selectButtons) {
                btn.Click += btnController.OnClickButtonSelect;
            }
            foreach (Button btn in changeButtons) {
                btn.Click += btnController.OnClickButtonChange;
                btn.Click += ChangeData;
            }
            btnDel.Click += btnController.OnClickButtonDel;
            btnDel.Click += ChangeData;
            btnEqual.Click += btnController.OnClickButtonEqual;
            btnEqual.Click += ChangeData;

            btnMR.Click += btnController.OnClickButtonReadNumberMemory;
            btnMC.Click += btnController.OnClickButtonClearMemory;
            btnMPlus.Click += btnController.OnClickButtonPlusNumberMemory;
            btnMMinus.Click += btnController.OnClickButtonMinusNumberMemory;
            btnMW.Click += ChangeDataFromMemory;
            Button[] btnMemory = { btnMR, btnMC, btnMPlus, btnMMinus, btnMW };
            foreach (Button btn in btnMemory) {
                btn.Click += ChangeDataFieldMemory;
            }

            btnPro.Click += btnController.OnClickProButton;
            btnPro.Click += ProMode;
        }

    }
}