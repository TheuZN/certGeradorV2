namespace CertGerador
{
    partial class Form1
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            progressBar = new ProgressBar();
            btnCsv = new Button();
            listBoxCSV = new ListBox();
            labelCsv = new Label();
            btnDocx = new Button();
            listBoxDOCX = new ListBox();
            labelDocx = new Label();
            btnFolder = new Button();
            listBoxFOLDER = new ListBox();
            labelFolder = new Label();
            labelFileNameField = new Label();
            comboFileNameField = new ComboBox();
            btnGerar = new Button();
            btnCancelar = new Button();
            SuspendLayout();
            //
            // labelCsv
            //
            labelCsv.AutoSize = true;
            labelCsv.Location = new Point(20, 20);
            labelCsv.Name = "labelCsv";
            labelCsv.Size = new Size(120, 15);
            labelCsv.Text = "1. Arquivo CSV";
            //
            // btnCsv
            //
            btnCsv.Cursor = Cursors.Hand;
            btnCsv.Location = new Point(20, 40);
            btnCsv.Name = "btnCsv";
            btnCsv.Size = new Size(150, 30);
            btnCsv.Text = "Selecionar CSV";
            btnCsv.UseVisualStyleBackColor = true;
            btnCsv.Click += btnCsv_Click;
            //
            // listBoxCSV
            //
            listBoxCSV.Location = new Point(180, 40);
            listBoxCSV.Name = "listBoxCSV";
            listBoxCSV.Size = new Size(400, 30);
            //
            // labelDocx
            //
            labelDocx.AutoSize = true;
            labelDocx.Location = new Point(20, 80);
            labelDocx.Name = "labelDocx";
            labelDocx.Size = new Size(140, 15);
            labelDocx.Text = "2. Template (.docx)";
            //
            // btnDocx
            //
            btnDocx.Cursor = Cursors.Hand;
            btnDocx.Location = new Point(20, 100);
            btnDocx.Name = "btnDocx";
            btnDocx.Size = new Size(150, 30);
            btnDocx.Text = "Selecionar Template";
            btnDocx.UseVisualStyleBackColor = true;
            btnDocx.Click += btnDocx_Click;
            //
            // listBoxDOCX
            //
            listBoxDOCX.Location = new Point(180, 100);
            listBoxDOCX.Name = "listBoxDOCX";
            listBoxDOCX.Size = new Size(400, 30);
            //
            // labelFolder
            //
            labelFolder.AutoSize = true;
            labelFolder.Location = new Point(20, 140);
            labelFolder.Name = "labelFolder";
            labelFolder.Size = new Size(140, 15);
            labelFolder.Text = "3. Pasta de destino";
            //
            // btnFolder
            //
            btnFolder.Cursor = Cursors.Hand;
            btnFolder.Location = new Point(20, 160);
            btnFolder.Name = "btnFolder";
            btnFolder.Size = new Size(150, 30);
            btnFolder.Text = "Selecionar Pasta";
            btnFolder.UseVisualStyleBackColor = true;
            btnFolder.Click += btnFolder_Click;
            //
            // listBoxFOLDER
            //
            listBoxFOLDER.Location = new Point(180, 160);
            listBoxFOLDER.Name = "listBoxFOLDER";
            listBoxFOLDER.Size = new Size(400, 30);
            //
            // labelFileNameField
            //
            labelFileNameField.AutoSize = true;
            labelFileNameField.Location = new Point(20, 200);
            labelFileNameField.Name = "labelFileNameField";
            labelFileNameField.Size = new Size(220, 15);
            labelFileNameField.Text = "4. Campo usado como nome do arquivo";
            //
            // comboFileNameField
            //
            comboFileNameField.DropDownStyle = ComboBoxStyle.DropDownList;
            comboFileNameField.Location = new Point(20, 220);
            comboFileNameField.Name = "comboFileNameField";
            comboFileNameField.Size = new Size(300, 23);
            //
            // btnGerar
            //
            btnGerar.BackColor = SystemColors.ActiveCaption;
            btnGerar.Cursor = Cursors.Hand;
            btnGerar.Location = new Point(20, 270);
            btnGerar.Name = "btnGerar";
            btnGerar.Size = new Size(150, 35);
            btnGerar.Text = "Gerar Certificados";
            btnGerar.UseVisualStyleBackColor = false;
            btnGerar.Click += btnGerar_Click;
            //
            // btnCancelar
            //
            btnCancelar.Enabled = false;
            btnCancelar.Location = new Point(180, 270);
            btnCancelar.Name = "btnCancelar";
            btnCancelar.Size = new Size(120, 35);
            btnCancelar.Text = "Cancelar";
            btnCancelar.UseVisualStyleBackColor = true;
            btnCancelar.Click += btnCancelar_Click;
            //
            // progressBar
            //
            progressBar.Location = new Point(20, 320);
            progressBar.Name = "progressBar";
            progressBar.Size = new Size(560, 25);
            //
            // Form1
            //
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(620, 380);
            Controls.Add(labelCsv);
            Controls.Add(btnCsv);
            Controls.Add(listBoxCSV);
            Controls.Add(labelDocx);
            Controls.Add(btnDocx);
            Controls.Add(listBoxDOCX);
            Controls.Add(labelFolder);
            Controls.Add(btnFolder);
            Controls.Add(listBoxFOLDER);
            Controls.Add(labelFileNameField);
            Controls.Add(comboFileNameField);
            Controls.Add(btnGerar);
            Controls.Add(btnCancelar);
            Controls.Add(progressBar);
            Name = "Form1";
            Text = "Gerador de Certificados";
            ResumeLayout(false);
            PerformLayout();
        }

        private ProgressBar progressBar;
        private Button btnCsv;
        private ListBox listBoxCSV;
        private Label labelCsv;
        private Button btnDocx;
        private ListBox listBoxDOCX;
        private Label labelDocx;
        private Button btnFolder;
        private ListBox listBoxFOLDER;
        private Label labelFolder;
        private Label labelFileNameField;
        private ComboBox comboFileNameField;
        private Button btnGerar;
        private Button btnCancelar;
    }
}