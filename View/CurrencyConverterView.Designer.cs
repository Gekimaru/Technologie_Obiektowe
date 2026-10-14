namespace CurrencyConverter.View
{
    partial class CurrencyConverterView
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            label1 = new Label();
            label2 = new Label();
            sourceComboBox = new ComboBox();
            amountTextBox = new TextBox();
            convertButton = new Button();
            resultLabel = new Label();
            targetComboBox = new ComboBox();
            label4 = new Label();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(79, 11);
            label1.Name = "label1";
            label1.Size = new Size(64, 15);
            label1.TabIndex = 8;
            label1.Text = "Typ waluty";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(79, 55);
            label2.Name = "label2";
            label2.Size = new Size(50, 15);
            label2.TabIndex = 9;
            label2.Text = "Wartoœæ";
            // 
            // sourceComboBox
            // 
            sourceComboBox.DisplayMember = "Code";
            sourceComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            sourceComboBox.FormattingEnabled = true;
            sourceComboBox.Location = new Point(79, 29);
            sourceComboBox.Name = "sourceComboBox";
            sourceComboBox.Size = new Size(267, 23);
            sourceComboBox.TabIndex = 11;
            // 
            // amountTextBox
            // 
            amountTextBox.Location = new Point(79, 73);
            amountTextBox.Name = "amountTextBox";
            amountTextBox.Size = new Size(267, 23);
            amountTextBox.TabIndex = 12;
            // 
            // convertButton
            // 
            convertButton.Location = new Point(79, 146);
            convertButton.Name = "convertButton";
            convertButton.Size = new Size(267, 23);
            convertButton.TabIndex = 13;
            convertButton.Text = "Konwertuj walutê";
            convertButton.UseVisualStyleBackColor = true;
            // 
            // resultLabel
            // 
            resultLabel.AutoSize = true;
            resultLabel.Location = new Point(123, 172);
            resultLabel.Name = "resultLabel";
            resultLabel.Size = new Size(173, 15);
            resultLabel.TabIndex = 14;
            resultLabel.Text = "Wpisz kwote i kliknij w przycisk!";
            resultLabel.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // targetComboBox
            // 
            targetComboBox.DisplayMember = "Code";
            targetComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            targetComboBox.FormattingEnabled = true;
            targetComboBox.Location = new Point(79, 117);
            targetComboBox.Name = "targetComboBox";
            targetComboBox.Size = new Size(267, 23);
            targetComboBox.TabIndex = 15;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(79, 99);
            label4.Name = "label4";
            label4.Size = new Size(117, 15);
            label4.TabIndex = 16;
            label4.Text = "Docelowy typ waluty";
            // 
            // CurrencyConverterView
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(409, 334);
            Controls.Add(label4);
            Controls.Add(targetComboBox);
            Controls.Add(resultLabel);
            Controls.Add(convertButton);
            Controls.Add(amountTextBox);
            Controls.Add(sourceComboBox);
            Controls.Add(label2);
            Controls.Add(label1);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            Name = "CurrencyConverterView";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Currency Converter";
            Load += Form1_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private Label label1;
        private Label label2;
        private ComboBox sourceComboBox;
        private TextBox amountTextBox;
        private Button convertButton;
        private Label resultLabel;
        private ComboBox targetComboBox;
        private Label label4;
    }
}
