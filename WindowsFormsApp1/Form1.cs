using LabelManager2;
using myControl;
using Serilog;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        public List<ParamPair> Params;

        private LabelManager Manager;

        private ILogger logger;

        private int PrintQty => Convert.ToInt32(tb_print_qty.Text);
        //private ILogger logger1;

        private string pathBase = Directory.GetCurrentDirectory();
        public Form1()
        {
            InitializeComponent();
            Params = new List<ParamPair>();

            //Params = new List<ParamPair>()
            //{
            //    new ParamPair {name="vendor",value = "v1" },
            //    new ParamPair {name="partno",value = "v1" },
            //    new ParamPair {name="datecode",value = "v1" },
            //    new ParamPair {name="qty",value = "v1" },
            //};

            logger = new LoggerConfiguration()
                .WriteTo.File(path:"log\\log_.txt",rollingInterval: RollingInterval.Day,retainedFileCountLimit:31)
                .CreateLogger();
            // 针对log 在不同的文件夹命名 创建多个log实例
            //logger1 = new LoggerConfiguration()
            //    .WriteTo.File(path: "log\\part2\\log_.txt", rollingInterval: RollingInterval.Day, retainedFileCountLimit: 31)
            //    .CreateLogger();
        }

        

        private void Form1_Load(object sender, EventArgs e)
        {
            //TestFormInit();
            LoadLabelFile();
            ts_log.Text = "please choose label file!!!";

            logger.Information("app start!!");
            //logger1.Information("app start!!");
            //Params.ForEach(x =>
            //{
            //    flowLayoutPanel1.Controls.Add(new textForm(x.name, x.value));
            //});
            
        }



        private void LoadLabelFile() 
        {

            cb_labels.Items.Clear();

            var labs = Directory.GetFiles(pathBase, "*.lab");

            foreach (var lab in labs)
            {           
                cb_labels.Items.Add(Path.GetFileName(lab));         
            }
            //cb_labels.SelectedIndex = 0;

        }


        private void LabelInit()
        {

            string labelPath = Path.Combine(pathBase, "BM_Default.LAB");

            Manager = new LabelManager(labelPath);
        }


        private void button2_Click(object sender, EventArgs e)
        {

            #region 获取表单维护的参数,并更新模板变量值

            try
            {
                Params = flowLayoutPanel1.Controls.Cast<textForm>().ToList().Select(
                  x => new ParamPair() { name = x.GetTitle(), value = x.GetContent() }
                  ).ToList();

                Params.ForEach(x =>
                {
                    //ducoment.params[x.name] = x.value;
                    Manager.SetVariableValue(x.name, x.value);
                    logger.Information($"set {x.name} = {x.value} ");
                });

                Manager.Print(PrintQty);

                ts_log.Text = $"print  finished!!!";
                logger.Information($"print  finished!!!");
            }
            catch (Exception ex)
            {
                logger.Error( $"{ex.Message} - {ex.StackTrace}");
            }

            

            #endregion
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

        private void cb_labels_SelectedValueChanged(object sender, EventArgs e)
        {
            flowLayoutPanel1.Controls.Clear();

            string labelPath = Path.Combine(pathBase, cb_labels.Text);

            try
            {
                Manager = new LabelManager(labelPath);

                Manager.GetAllVariableName().ToList().ForEach(x =>
                {
                    flowLayoutPanel1.Controls.Add(new textForm(x, string.Empty));

                    logger.Information($"variable name:{x}");
                });

                


            }
            catch(Exception ex)
            {
                logger.Error($"{ex.Message} -- {ex.StackTrace}");
                MessageBox.Show(ex.Message);
            }

        }

        private void tb_print_qty_Leave(object sender, EventArgs e)
        {
            var tb = sender as TextBox;

            var check = int.TryParse(tb.Text, out int print_qty) && print_qty > 0;

            if (!check)
            {
                MessageBox.Show("print qty unavaliable!");
                tb.Focus();
                tb.SelectAll();
            }
           
        }
    }
}
