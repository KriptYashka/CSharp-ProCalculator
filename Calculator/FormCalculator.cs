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
            string res = btnController.GetMemoryRes();
            this.labelResult.Text = res;
        }

        public FormCalculator() {
            InitializeComponent();
            btnController = new ButtonController();
            numberButtons = new Button[] { btn1, btn2, btn3, btn4, btn5,
                                           btn6, btn7, btn8, btn9, btn0, btnDot }; // btnDot - исключение
            selectButtons = new Button[] { btnPlus, btnMinus, btnMultiply, btnRealDiv, btnDiv, btnMod };
            Button[] changeButtons = new Button[] { btnSqr, btnSqrt };
            foreach (Button btn in numberButtons) {
                btn.Click += btnController.OnClickButtonNumber;
                btn.Click += ChangeData;
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
        }

    }
}