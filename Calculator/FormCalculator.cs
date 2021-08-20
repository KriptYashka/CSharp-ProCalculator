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
        private ButtonController btnController;
        public void ChangeData(object s, EventArgs arg) {
            string res = btnController.GetRes();
            this.labelResult.Text = res;
        }
        public FormCalculator() {
            InitializeComponent();
            btnController = new ButtonController();
            numberButtons = new Button[] { btn1, btn2, btn3, btn4, btn5,
                                           btn6, btn7, btn8, btn9, btn0 };
            foreach (Button btn in numberButtons) {
                btn.Click += btnController.ButtonNumberOnClick;
                btn.Click += ChangeData;
            }
        }

    }
}
