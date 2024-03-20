
namespace Lisp_Interpreter.numberedtextbox
{
    partial class NumberedTextBox
    {
        /// <summary> 
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором компонентов

        /// <summary> 
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.editBox = new Lisp_Interpreter.numberedtextbox.RichTextBoxEx();
            this.SuspendLayout();
            // 
            // editBox
            // 
            this.editBox.AcceptsTab = true;
            this.editBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.editBox.BackColor = System.Drawing.SystemColors.Window;
            this.editBox.Location = new System.Drawing.Point(27, 3);
            this.editBox.Name = "editBox";
            this.editBox.ScrollPos = new System.Drawing.Point(0, 0);
            this.editBox.Size = new System.Drawing.Size(120, 115);
            this.editBox.TabIndex = 0;
            this.editBox.Text = "";
            this.editBox.WordWrap = false;
            // 
            // NumberedTextBox
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.editBox);
            this.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.Name = "NumberedTextBox";
            this.Size = new System.Drawing.Size(150, 121);
            this.ResumeLayout(false);

        }

        #endregion

        private RichTextBoxEx editBox;
    }
}
