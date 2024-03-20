using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.Diagnostics;
using System.IO;

namespace Lisp_Interpreter
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            numberedOutputBox.ReadOnly = true;
            numberedInputBox.OutsideBackColor = numberedInputBox.BackColor;
            numberedOutputBox.OutsideBackColor = numberedInputBox.BackColor;
        }
        
        private void evalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            String src = numberedInputBox.Text;
            
            try
            {
                String output = interpreter.go(src);
                numberedOutputBox.BackColor = numberedInputBox.BackColor;
                numberedOutputBox.Text = output;
            }
            catch (InterpError error) {
                numberedOutputBox.BackColor = Color.FromArgb(255,120,120);
                numberedOutputBox.Text = error.Message;
            }
        }

        private void loadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();

            openFileDialog.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
            openFileDialog.FilterIndex = 2;
            openFileDialog.RestoreDirectory = true;

            if (openFileDialog.ShowDialog() == DialogResult.OK) {
                Stream stream = openFileDialog.OpenFile();

                StreamReader reader = new StreamReader(stream);

                numberedInputBox.Text = reader.ReadToEnd();

                reader.Close();
                stream.Close();
            }
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();

            saveFileDialog1.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
            saveFileDialog1.FilterIndex = 2;
            saveFileDialog1.RestoreDirectory = true;


            Stream stream;
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                if ((stream = saveFileDialog1.OpenFile()) != null)
                {
                    StreamWriter writer = new StreamWriter(stream);

                    writer.Write(numberedInputBox.Text);

                    writer.Close();
                    stream.Close();
                }
            }
        }

        Interpreter interpreter = new Interpreter();

        private void helpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Help.ShowHelp(this, "./help/help.chm");
        }
    }
}
