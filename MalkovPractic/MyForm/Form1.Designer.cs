namespace MyForm
{
    partial class MainForm
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
            tabData = new TabPage();
            checkedListBox1 = new CheckedListBox();
            label12 = new Label();
            label11 = new Label();
            ResultTextBox = new TextBox();
            TypeKermel = new ComboBox();
            label10 = new Label();
            label9 = new Label();
            label8 = new Label();
            progressBar1 = new ProgressBar();
            dataGridView1 = new DataGridView();
            StudyModelButton = new Button();
            label7 = new Label();
            numEpochs = new NumericUpDown();
            label6 = new Label();
            numLearningRate = new NumericUpDown();
            label5 = new Label();
            numBandwidth = new NumericUpDown();
            label4 = new Label();
            numK = new NumericUpDown();
            MetricCombobox = new ComboBox();
            label3 = new Label();
            algorytmComboBox = new ComboBox();
            label2 = new Label();
            label1 = new Label();
            TypeCombobox = new ComboBox();
            chosefileButton = new Button();
            tab = new TabControl();
            tabData.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numEpochs).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numLearningRate).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numBandwidth).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numK).BeginInit();
            tab.SuspendLayout();
            SuspendLayout();
            // 
            // tabData
            // 
            tabData.Controls.Add(checkedListBox1);
            tabData.Controls.Add(label12);
            tabData.Controls.Add(label11);
            tabData.Controls.Add(ResultTextBox);
            tabData.Controls.Add(TypeKermel);
            tabData.Controls.Add(label10);
            tabData.Controls.Add(label9);
            tabData.Controls.Add(label8);
            tabData.Controls.Add(progressBar1);
            tabData.Controls.Add(dataGridView1);
            tabData.Controls.Add(StudyModelButton);
            tabData.Controls.Add(label7);
            tabData.Controls.Add(numEpochs);
            tabData.Controls.Add(label6);
            tabData.Controls.Add(numLearningRate);
            tabData.Controls.Add(label5);
            tabData.Controls.Add(numBandwidth);
            tabData.Controls.Add(label4);
            tabData.Controls.Add(numK);
            tabData.Controls.Add(MetricCombobox);
            tabData.Controls.Add(label3);
            tabData.Controls.Add(algorytmComboBox);
            tabData.Controls.Add(label2);
            tabData.Controls.Add(label1);
            tabData.Controls.Add(TypeCombobox);
            tabData.Controls.Add(chosefileButton);
            tabData.Location = new Point(4, 24);
            tabData.Name = "tabData";
            tabData.Padding = new Padding(3);
            tabData.Size = new Size(1292, 731);
            tabData.TabIndex = 0;
            tabData.Text = "Данные";
            tabData.UseVisualStyleBackColor = true;
            // 
            // checkedListBox1
            // 
            checkedListBox1.FormattingEnabled = true;
            checkedListBox1.Location = new Point(12, 319);
            checkedListBox1.Name = "checkedListBox1";
            checkedListBox1.Size = new Size(202, 40);
            checkedListBox1.TabIndex = 26;
            // 
            // label12
            // 
            label12.AutoSize = true;
            label12.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 204);
            label12.Location = new Point(30, 297);
            label12.Name = "label12";
            label12.Size = new Size(155, 21);
            label12.TabIndex = 25;
            label12.Text = "Выберите признаки:";
            // 
            // label11
            // 
            label11.AutoSize = true;
            label11.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 204);
            label11.Location = new Point(688, 507);
            label11.Name = "label11";
            label11.Size = new Size(186, 21);
            label11.TabIndex = 24;
            label11.Text = "Результат предсказания:";
            // 
            // ResultTextBox
            // 
            ResultTextBox.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point, 204);
            ResultTextBox.Location = new Point(236, 531);
            ResultTextBox.Multiline = true;
            ResultTextBox.Name = "ResultTextBox";
            ResultTextBox.ReadOnly = true;
            ResultTextBox.Size = new Size(1046, 156);
            ResultTextBox.TabIndex = 23;
            // 
            // TypeKermel
            // 
            TypeKermel.FormattingEnabled = true;
            TypeKermel.Location = new Point(12, 271);
            TypeKermel.Name = "TypeKermel";
            TypeKermel.Size = new Size(202, 23);
            TypeKermel.TabIndex = 22;
            // 
            // label10
            // 
            label10.AutoSize = true;
            label10.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 204);
            label10.Location = new Point(69, 247);
            label10.Name = "label10";
            label10.Size = new Size(77, 21);
            label10.TabIndex = 21;
            label10.Text = "Тип ядра:";
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 204);
            label9.Location = new Point(468, 297);
            label9.Name = "label9";
            label9.Size = new Size(64, 21);
            label9.TabIndex = 20;
            label9.Text = "Статус: ";
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 204);
            label8.Location = new Point(716, 6);
            label8.Name = "label8";
            label8.Size = new Size(139, 21);
            label8.TabIndex = 19;
            label8.Text = "Просмотр данных";
            // 
            // progressBar1
            // 
            progressBar1.Location = new Point(241, 321);
            progressBar1.Name = "progressBar1";
            progressBar1.Size = new Size(1041, 156);
            progressBar1.TabIndex = 18;
            // 
            // dataGridView1
            // 
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Location = new Point(236, 30);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.Size = new Size(1046, 254);
            dataGridView1.TabIndex = 17;
            // 
            // StudyModelButton
            // 
            StudyModelButton.BackColor = Color.LightGray;
            StudyModelButton.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 204);
            StudyModelButton.Location = new Point(12, 599);
            StudyModelButton.Name = "StudyModelButton";
            StudyModelButton.Size = new Size(202, 60);
            StudyModelButton.TabIndex = 16;
            StudyModelButton.Text = "Обучить модель";
            StudyModelButton.UseVisualStyleBackColor = false;
            StudyModelButton.Click += StudyModel_Click;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 204);
            label7.Location = new Point(78, 546);
            label7.Name = "label7";
            label7.Size = new Size(57, 21);
            label7.TabIndex = 15;
            label7.Text = "Эпохи:";
            // 
            // numEpochs
            // 
            numEpochs.DecimalPlaces = 1;
            numEpochs.Location = new Point(12, 570);
            numEpochs.Maximum = new decimal(new int[] { 10000, 0, 0, 0 });
            numEpochs.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            numEpochs.Name = "numEpochs";
            numEpochs.Size = new Size(202, 23);
            numEpochs.TabIndex = 14;
            numEpochs.Value = new decimal(new int[] { 1, 0, 0, 0 });
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 204);
            label6.Location = new Point(30, 489);
            label6.Name = "label6";
            label6.Size = new Size(153, 21);
            label6.TabIndex = 13;
            label6.Text = "Скорость обучения:";
            // 
            // numLearningRate
            // 
            numLearningRate.DecimalPlaces = 4;
            numLearningRate.Location = new Point(12, 513);
            numLearningRate.Maximum = new decimal(new int[] { 1, 0, 0, 0 });
            numLearningRate.Minimum = new decimal(new int[] { 1, 0, 0, 262144 });
            numLearningRate.Name = "numLearningRate";
            numLearningRate.Size = new Size(202, 23);
            numLearningRate.TabIndex = 12;
            numLearningRate.Value = new decimal(new int[] { 1, 0, 0, 196608 });
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 204);
            label5.Location = new Point(24, 428);
            label5.Name = "label5";
            label5.Size = new Size(190, 21);
            label5.TabIndex = 11;
            label5.Text = "Пропускная способность";
            // 
            // numBandwidth
            // 
            numBandwidth.DecimalPlaces = 2;
            numBandwidth.Location = new Point(12, 452);
            numBandwidth.Maximum = new decimal(new int[] { 10, 0, 0, 0 });
            numBandwidth.Minimum = new decimal(new int[] { 1, 0, 0, 65536 });
            numBandwidth.Name = "numBandwidth";
            numBandwidth.Size = new Size(202, 23);
            numBandwidth.TabIndex = 10;
            numBandwidth.Value = new decimal(new int[] { 1, 0, 0, 0 });
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 204);
            label4.Location = new Point(30, 366);
            label4.Name = "label4";
            label4.Size = new Size(157, 21);
            label4.TabIndex = 9;
            label4.Text = "Количество соседей:";
            // 
            // numK
            // 
            numK.Location = new Point(12, 390);
            numK.Maximum = new decimal(new int[] { 50, 0, 0, 0 });
            numK.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            numK.Name = "numK";
            numK.Size = new Size(202, 23);
            numK.TabIndex = 8;
            numK.Value = new decimal(new int[] { 3, 0, 0, 0 });
            // 
            // MetricCombobox
            // 
            MetricCombobox.FormattingEnabled = true;
            MetricCombobox.Location = new Point(12, 213);
            MetricCombobox.Name = "MetricCombobox";
            MetricCombobox.Size = new Size(202, 23);
            MetricCombobox.TabIndex = 7;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 204);
            label3.Location = new Point(30, 189);
            label3.Name = "label3";
            label3.Size = new Size(147, 21);
            label3.TabIndex = 6;
            label3.Text = "Выберите метрику:";
            // 
            // algorytmComboBox
            // 
            algorytmComboBox.FormattingEnabled = true;
            algorytmComboBox.Location = new Point(12, 159);
            algorytmComboBox.Name = "algorytmComboBox";
            algorytmComboBox.Size = new Size(202, 23);
            algorytmComboBox.TabIndex = 5;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 204);
            label2.Location = new Point(30, 135);
            label2.Name = "label2";
            label2.Size = new Size(154, 21);
            label2.TabIndex = 4;
            label2.Text = "Выберите алгоритм:";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 204);
            label1.Location = new Point(30, 77);
            label1.Name = "label1";
            label1.Size = new Size(166, 21);
            label1.TabIndex = 3;
            label1.Text = "Выберите тип задачи:\r\n";
            // 
            // TypeCombobox
            // 
            TypeCombobox.FormattingEnabled = true;
            TypeCombobox.Location = new Point(12, 101);
            TypeCombobox.Name = "TypeCombobox";
            TypeCombobox.Size = new Size(202, 23);
            TypeCombobox.TabIndex = 2;
            // 
            // chosefileButton
            // 
            chosefileButton.BackColor = Color.LightGray;
            chosefileButton.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 204);
            chosefileButton.Location = new Point(8, 6);
            chosefileButton.Name = "chosefileButton";
            chosefileButton.Size = new Size(206, 60);
            chosefileButton.TabIndex = 1;
            chosefileButton.Text = "Выберите файл для обучения";
            chosefileButton.UseVisualStyleBackColor = false;
            chosefileButton.Click += chosefileButton_Click;
            // 
            // tab
            // 
            tab.Controls.Add(tabData);
            tab.Location = new Point(0, 1);
            tab.Name = "tab";
            tab.SelectedIndex = 0;
            tab.Size = new Size(1300, 759);
            tab.TabIndex = 0;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.PaleGreen;
            ClientSize = new Size(1298, 759);
            Controls.Add(tab);
            Name = "MainForm";
            Text = "MainForm";
            tabData.ResumeLayout(false);
            tabData.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            ((System.ComponentModel.ISupportInitialize)numEpochs).EndInit();
            ((System.ComponentModel.ISupportInitialize)numLearningRate).EndInit();
            ((System.ComponentModel.ISupportInitialize)numBandwidth).EndInit();
            ((System.ComponentModel.ISupportInitialize)numK).EndInit();
            tab.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private TabPage tabData;
        private CheckedListBox checkedListBox1;
        private Label label12;
        private Label label11;
        private TextBox ResultTextBox;
        private ComboBox TypeKermel;
        private Label label10;
        private Label label9;
        private Label label8;
        private ProgressBar progressBar1;
        private DataGridView dataGridView1;
        private Button StudyModelButton;
        private Label label7;
        private NumericUpDown numEpochs;
        private Label label6;
        private NumericUpDown numLearningRate;
        private Label label5;
        private NumericUpDown numBandwidth;
        private Label label4;
        private NumericUpDown numK;
        private ComboBox MetricCombobox;
        private Label label3;
        private ComboBox algorytmComboBox;
        private Label label2;
        private Label label1;
        private ComboBox TypeCombobox;
        private Button chosefileButton;
        private TabControl tab;
    }
}
