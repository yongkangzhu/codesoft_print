using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace myControl
{
    public partial class textForm: UserControl
    {
        public textForm()
        {
            InitializeComponent();
        }

        public textForm(string label,string content)
        {
            InitializeComponent();
            this.label1.Text = label;
            this.textBox1.Text = content;
        }

        public string GetTitle() => label1.Text;

        public string GetContent() => textBox1.Text;


        public void SetContent(string content)
        {
            textBox1.Text = content;
        }

        public void Clear()
        {
            textBox1.Text = string.Empty;
        }


    }
}
