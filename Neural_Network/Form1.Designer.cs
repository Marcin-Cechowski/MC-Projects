namespace Neural_Network
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.LearningRate = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.Epochs = new System.Windows.Forms.TextBox();
            this.networkModel = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.Learning_Rate = new System.Windows.Forms.Label();
            this.Epochs_Number = new System.Windows.Forms.Label();
            this.Network_Model = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.ground_truth = new System.Windows.Forms.TextBox();
            this.Train_btn = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.generateModel = new System.Windows.Forms.Button();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.fileName = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.ofd = new System.Windows.Forms.OpenFileDialog();
            this.title = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.LearningRate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(100, 23);
            this.label1.TabIndex = 14;
            // 
            // LearningRate
            // 
            this.LearningRate.DecimalPlaces = 1;
            this.LearningRate.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.LearningRate.Location = new System.Drawing.Point(150, 134);
            this.LearningRate.Maximum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.LearningRate.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.LearningRate.Name = "LearningRate";
            this.LearningRate.Size = new System.Drawing.Size(120, 23);
            this.LearningRate.TabIndex = 9;
            this.LearningRate.Value = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.LearningRate.ValueChanged += new System.EventHandler(this.numericUpDown1_ValueChanged);
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(0, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(100, 23);
            this.label3.TabIndex = 13;
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(0, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(100, 23);
            this.label2.TabIndex = 12;
            // 
            // Epochs
            // 
            this.Epochs.Location = new System.Drawing.Point(150, 182);
            this.Epochs.Name = "Epochs";
            this.Epochs.Size = new System.Drawing.Size(120, 23);
            this.Epochs.TabIndex = 10;
            this.Epochs.Text = "100";
            this.Epochs.TextChanged += new System.EventHandler(this.Epochs_TextChanged);
            // 
            // networkModel
            // 
            this.networkModel.Location = new System.Drawing.Point(150, 223);
            this.networkModel.Name = "networkModel";
            this.networkModel.Size = new System.Drawing.Size(120, 23);
            this.networkModel.TabIndex = 11;
            this.networkModel.Text = "2,2,5,1";
            this.networkModel.TextChanged += new System.EventHandler(this.textBox3_TextChanged);
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(0, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(100, 23);
            this.label4.TabIndex = 0;
            // 
            // Learning_Rate
            // 
            this.Learning_Rate.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.Learning_Rate.Location = new System.Drawing.Point(22, 134);
            this.Learning_Rate.Name = "Learning_Rate";
            this.Learning_Rate.Size = new System.Drawing.Size(122, 23);
            this.Learning_Rate.TabIndex = 15;
            this.Learning_Rate.Text = "Learning Rate";
            this.Learning_Rate.Click += new System.EventHandler(this.label5_Click);
            // 
            // Epochs_Number
            // 
            this.Epochs_Number.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.Epochs_Number.Location = new System.Drawing.Point(47, 182);
            this.Epochs_Number.Name = "Epochs_Number";
            this.Epochs_Number.Size = new System.Drawing.Size(100, 23);
            this.Epochs_Number.TabIndex = 15;
            this.Epochs_Number.Text = "Epochs";
            this.Epochs_Number.Click += new System.EventHandler(this.label5_Click);
            // 
            // Network_Model
            // 
            this.Network_Model.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.Network_Model.Location = new System.Drawing.Point(15, 223);
            this.Network_Model.Name = "Network_Model";
            this.Network_Model.Size = new System.Drawing.Size(132, 23);
            this.Network_Model.TabIndex = 15;
            this.Network_Model.Text = "Network Model";
            this.Network_Model.Click += new System.EventHandler(this.label5_Click);
            // 
            // label5
            // 
            this.label5.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label5.Location = new System.Drawing.Point(1024, 100);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(238, 23);
            this.label5.TabIndex = 15;
            this.label5.Text = "Data Instances";
            this.label5.Click += new System.EventHandler(this.label5_Click);
            // 
            // label6
            // 
            this.label6.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label6.Location = new System.Drawing.Point(1030, 313);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(238, 23);
            this.label6.TabIndex = 15;
            this.label6.Text = "Ground Truth";
            this.label6.Click += new System.EventHandler(this.label5_Click);
            // 
            // ground_truth
            // 
            this.ground_truth.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.ground_truth.Location = new System.Drawing.Point(1024, 337);
            this.ground_truth.Name = "ground_truth";
            this.ground_truth.Size = new System.Drawing.Size(145, 26);
            this.ground_truth.TabIndex = 20;
            this.ground_truth.Text = "0; 1; 0; 1;";
            this.ground_truth.TextChanged += new System.EventHandler(this.ground_truth_TextChanged);
            // 
            // Train_btn
            // 
            this.Train_btn.Location = new System.Drawing.Point(1024, 378);
            this.Train_btn.Name = "Train_btn";
            this.Train_btn.Size = new System.Drawing.Size(151, 51);
            this.Train_btn.TabIndex = 22;
            this.Train_btn.Text = "Train";
            this.Train_btn.UseVisualStyleBackColor = true;
            this.Train_btn.Click += new System.EventHandler(this.Train_btn_Click);
            // 
            // textBox1
            // 
            this.textBox1.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.textBox1.Location = new System.Drawing.Point(1024, 126);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(133, 105);
            this.textBox1.TabIndex = 23;
            this.textBox1.Text = "0 0\r\n0 1\r\n1 1\r\n1 0";
            this.textBox1.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // generateModel
            // 
            this.generateModel.Location = new System.Drawing.Point(155, 337);
            this.generateModel.Name = "generateModel";
            this.generateModel.Size = new System.Drawing.Size(115, 39);
            this.generateModel.TabIndex = 24;
            this.generateModel.Text = "Generate Model";
            this.generateModel.UseVisualStyleBackColor = true;
            this.generateModel.Click += new System.EventHandler(this.generateModel_Click);
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(334, 72);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(562, 604);
            this.dataGridView1.TabIndex = 26;
            this.dataGridView1.Text = "dataGridView1";
            this.dataGridView1.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellContentClick);
            // 
            // fileName
            // 
            this.fileName.Location = new System.Drawing.Point(150, 274);
            this.fileName.Name = "fileName";
            this.fileName.Size = new System.Drawing.Size(120, 23);
            this.fileName.TabIndex = 27;
            this.fileName.Text = "D:\\testFile.txt";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(15, 337);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(126, 39);
            this.button1.TabIndex = 28;
            this.button1.Text = "Wages From File";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label7
            // 
            this.label7.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label7.Location = new System.Drawing.Point(22, 274);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(110, 23);
            this.label7.TabIndex = 29;
            this.label7.Text = "Filename";
            this.label7.Click += new System.EventHandler(this.label7_Click);
            // 
            // ofd
            // 
            this.ofd.FileName = "ofd";
            // 
            // title
            // 
            this.title.Font = new System.Drawing.Font("Arial", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.title.Location = new System.Drawing.Point(475, 23);
            this.title.Name = "title";
            this.title.Size = new System.Drawing.Size(421, 46);
            this.title.TabIndex = 33;
            this.title.Text = "Neural Network Generator";
            this.title.Click += new System.EventHandler(this.label10_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1230, 721);
            this.Controls.Add(this.title);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.fileName);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.generateModel);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.Train_btn);
            this.Controls.Add(this.ground_truth);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.Network_Model);
            this.Controls.Add(this.Epochs_Number);
            this.Controls.Add(this.Learning_Rate);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.networkModel);
            this.Controls.Add(this.Epochs);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.LearningRate);
            this.Controls.Add(this.label1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.LearningRate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown LearningRate;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox Epochs;
        private System.Windows.Forms.TextBox networkModel;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label Learning_Rate;
        private System.Windows.Forms.Label Epochs_Number;
        private System.Windows.Forms.Label Network_Model;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox ground_truth;
        private System.Windows.Forms.Button Train_btn;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button generateModel;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.TextBox fileName;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.OpenFileDialog ofd;
        private System.Windows.Forms.Label title;
    }
}

