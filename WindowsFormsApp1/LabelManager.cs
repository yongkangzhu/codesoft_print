using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1
{
    public class LabelManager
    {
        //引用codesoft 参考,构造函数初始化
        public LabelManager(string LabelPath)
        {
            //lb = document.open(LabelPath);
        }

        public void SetVariableValue(string name, string value)
        {
            //lb.variable[name] = value;
        }

        public string[] GetVariableName()
        {
            // return lb.variable;
            return new string[] { };
        }

        public void Print()
        {
            //lb.print();
        }
    }
}
