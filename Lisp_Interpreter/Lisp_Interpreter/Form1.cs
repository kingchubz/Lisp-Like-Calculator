using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lisp_Interpreter
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            String src = textBox1.Text;
            Interpreter.init_enviroment(ref global_env);
            String output = Interpreter.go("(begin " + src + ")",ref global_env);
            label1.Text = output;
        }
        Enviroment global_env;
    }
}
