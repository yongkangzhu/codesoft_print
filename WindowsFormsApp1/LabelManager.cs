using LabelManager2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1
{
    public class LabelManager
    {
     

        private static ApplicationClass lbl = new ApplicationClass();
     
        private Document doc;

        //引用codesoft 参考,构造函数初始化
        public LabelManager(string labelPath)
        {

            lbl.Documents.Open(labelPath, false); // 開啟label文件

            doc = lbl.ActiveDocument;

        }

        public void SetVariableValue(string name, string value)
        {
            doc.Variables.FormVariables.Item(name).Value = value;
        }

        public string[] GetAllVariableName()
        {
            string[] sLabelParam = new string[doc.Variables.FormVariables.Count];
       
            for (int i = 1; i <= doc.Variables.FormVariables.Count; i++)
            {
                sLabelParam[i - 1] = doc.Variables.FormVariables.Item(i).Name;
            }

            return sLabelParam;
        }

        public void Print(int qty)
        {
            doc.PrintDocument(qty);
        }
    }
}
