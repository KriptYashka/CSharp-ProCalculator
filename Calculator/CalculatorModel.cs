using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator {
    
    class CalculatorModel {
        private string strRes;
        private double number1, number2;
        private double numberMemory;
        private bool isSelect;
        private bool hasDot;
        private int select; // Select: 1(+), 2(-), 3(*), 4(/), 5(div), 6(mod) ENUM
        private bool proMode;
        private bool first;

        public CalculatorModel() {
            strRes = "0";
            number1 = 0;
            number2 = 0;
            numberMemory = 0;
            select = 0;
            hasDot = false;
            isSelect = false;
            proMode = false;
            first = true;
        }

        public void SetStrRes(string newData) {
            strRes = newData;
        }

        public void SetNumber1(double newData) {
            number1 = newData;
        }

        public void SetNumber2(double newData) {
            number2 = newData;
        }

        public void SetNumberMemory(double newData) {
            numberMemory = newData;
        }

        public void SetSelect(int newData) {
            select = newData;
        }

        public void SetHasDot(bool newData){
            hasDot = newData;
        }

        public void SetIsSelect(bool newData) {
            isSelect = newData;
        }
        public void SetProMode(bool newData) {
            proMode = newData;
        }
        public void SetFirst(bool newData) {
            first = newData;
        }

        /* Возврат полей */

        public string GetStrRes() {
            return strRes;
        }

        public double GetNumber1() {
            return number1;
        }

        public double GetNumber2() {
            return number2;
        }

        public double GetNumberMemory() {
            return numberMemory;
        }

        public int GetSelect() {
            return select;
        }

        public bool GetHasDot() {
            return hasDot;
        }

        public bool GetProMode() {
            return proMode;
        }

        public bool GetFirst() {
            return first;
        }

        public bool GetIsSelect() {
            return isSelect;
        }
    }
}
