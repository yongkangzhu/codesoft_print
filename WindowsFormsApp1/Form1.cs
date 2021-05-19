using myControl;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        public List<ParamPair> Params;

        public Form1()
        {
            InitializeComponent();

            Params = new List<ParamPair>()
            {
                new ParamPair {name="vendor",value = "v1" },
                new ParamPair {name="partno",value = "v1" },
                new ParamPair {name="datecode",value = "v1" },
                new ParamPair {name="qty",value = "v1" },
            };
        }

        

        private void Form1_Load(object sender, EventArgs e)
        {
            TestFormInit();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            flowLayoutPanel1.Controls.Add(new textForm("姓名", "啦啦啦啦"));
        }

        private void TestFormInit()
        {

            #region 从codesoft 模板获取变量名,动态生产参数表单

            Params.ForEach(x =>
            {
                flowLayoutPanel1.Controls.Add(new textForm(x.name,x.value));
            });

            #endregion
        }

        private void button2_Click(object sender, EventArgs e)
        {

            #region 获取表单维护的参数,并更新模板变量值
            Params = flowLayoutPanel1.Controls.Cast<textForm>().ToList().Select(
                  x => new ParamPair() { name = x.GetTitle(), value = x.GetContent() }
                  ).ToList();

            Params.ForEach(x =>
            {
                //ducoment.params[x.name] = x.value;

;            });
            #endregion
        }

        private void SelectLabel(string path)
        {
            LabelManager manager = new LabelManager(path);

            manager.GetVariableName().ToList().ForEach(x =>
            {
                flowLayoutPanel1.Controls.Add(new textForm(x, string.Empty));
            });

        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            var rb =  sender as RadioButton;

            var data = flowLayoutPanel1.Controls.Cast<textForm>().ToList().Select(
                 x => new ParamPair() { name = x.GetTitle(), value = x.GetContent() }
                 ).ToList();

            if (rb.Checked)
            {
                flowLayoutPanel1.Controls.Cast<textForm>().ToList().ForEach(x =>
                {
                    x.Clear();
                });
            }
            else
            {
                flowLayoutPanel1.Controls.Cast<textForm>().ToList().ForEach(x =>
                {
                    x.SetContent(Params.FirstOrDefault(z=>z.name == x.GetTitle())?.value);
                });
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            var cb = sender as CheckBox;

            var data = flowLayoutPanel1.Controls.Cast<textForm>().ToList().Select(
                 x => new ParamPair() { name = x.GetTitle(), value = x.GetContent() }
                 ).ToList();

            if (cb.Checked)
            {
                flowLayoutPanel1.Controls.Cast<textForm>().ToList().ForEach(x =>
                {
                    x.Clear();
                });
                flowLayoutPanel1.Controls.Cast<textForm>().First().Focus();
            }
            else
            {
                flowLayoutPanel1.Controls.Cast<textForm>().ToList().ForEach(x =>
                {
                    x.SetContent(Params.FirstOrDefault(z => z.name == x.GetTitle())?.value);
                });
            }

        }
    }
}
