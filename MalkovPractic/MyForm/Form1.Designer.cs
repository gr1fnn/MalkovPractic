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
            checkedListBox1Label = new Label();
            ResultTextBoxLabel = new Label();
            ResultTextBox = new TextBox();
            TypeKermel = new ComboBox();
            TypeKermelLabel = new Label();
            progressBar1Label = new Label();
            dataGridView1Label = new Label();
            progressBar1 = new ProgressBar();
            dataGridView1 = new DataGridView();
            StudyModelButton = new Button();
            numEpochsLabel = new Label();
            numEpochs = new NumericUpDown();
            numLearningRateLabel = new Label();
            numLearningRate = new NumericUpDown();
            numBandwidthLabel = new Label();
            numBandwidth = new NumericUpDown();
            numKLabel = new Label();
            numK = new NumericUpDown();
            MetricCombobox = new ComboBox();
            MetricComboboxLabel = new Label();
            algorytmComboBox = new ComboBox();
            algorytmComboBoxlabel = new Label();
            TypeCombobxLabel = new Label();
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
            tabData.Controls.Add(checkedListBox1Label);
            tabData.Controls.Add(ResultTextBoxLabel);
            tabData.Controls.Add(ResultTextBox);
            tabData.Controls.Add(TypeKermel);
            tabData.Controls.Add(TypeKermelLabel);
            tabData.Controls.Add(progressBar1Label);
            tabData.Controls.Add(dataGridView1Label);
            tabData.Controls.Add(progressBar1);
            tabData.Controls.Add(dataGridView1);
            tabData.Controls.Add(StudyModelButton);
            tabData.Controls.Add(numEpochsLabel);
            tabData.Controls.Add(numEpochs);
            tabData.Controls.Add(numLearningRateLabel);
            tabData.Controls.Add(numLearningRate);
            tabData.Controls.Add(numBandwidthLabel);
            tabData.Controls.Add(numBandwidth);
            tabData.Controls.Add(numKLabel);
            tabData.Controls.Add(numK);
            tabData.Controls.Add(MetricCombobox);
            tabData.Controls.Add(MetricComboboxLabel);
            tabData.Controls.Add(algorytmComboBox);
            tabData.Controls.Add(algorytmComboBoxlabel);
            tabData.Controls.Add(TypeCombobxLabel);
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
            // checkedListBox1Label
            // 
            checkedListBox1Label.AutoSize = true;
            checkedListBox1Label.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 204);
            checkedListBox1Label.Location = new Point(30, 297);
            checkedListBox1Label.Name = "checkedListBox1Label";
            checkedListBox1Label.Size = new Size(155, 21);
            checkedListBox1Label.TabIndex = 25;
            checkedListBox1Label.Text = "Выберите признаки:";
            // 
            // ResultTextBoxLabel
            // 
            ResultTextBoxLabel.AutoSize = true;
            ResultTextBoxLabel.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 204);
            ResultTextBoxLabel.Location = new Point(688, 507);
            ResultTextBoxLabel.Name = "ResultTextBoxLabel";
            ResultTextBoxLabel.Size = new Size(186, 21);
            ResultTextBoxLabel.TabIndex = 24;
            ResultTextBoxLabel.Text = "Результат предсказания:";
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
            // TypeKermelLabel
            // 
            TypeKermelLabel.AutoSize = true;
            TypeKermelLabel.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 204);
            TypeKermelLabel.Location = new Point(69, 247);
            TypeKermelLabel.Name = "TypeKermelLabel";
            TypeKermelLabel.Size = new Size(77, 21);
            TypeKermelLabel.TabIndex = 21;
            TypeKermelLabel.Text = "Тип ядра:";
            // 
            // progressBar1Label
            // 
            progressBar1Label.AutoSize = true;
            progressBar1Label.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 204);
            progressBar1Label.Location = new Point(468, 297);
            progressBar1Label.Name = "progressBar1Label";
            progressBar1Label.Size = new Size(64, 21);
            progressBar1Label.TabIndex = 20;
            progressBar1Label.Text = "Статус: ";
            // 
            // dataGridView1Label
            // 
            dataGridView1Label.AutoSize = true;
            dataGridView1Label.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 204);
            dataGridView1Label.Location = new Point(716, 6);
            dataGridView1Label.Name = "dataGridView1Label";
            dataGridView1Label.Size = new Size(139, 21);
            dataGridView1Label.TabIndex = 19;
            dataGridView1Label.Text = "Просмотр данных";
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
            // numEpochsLabel
            // 
            numEpochsLabel.AutoSize = true;
            numEpochsLabel.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 204);
            numEpochsLabel.Location = new Point(78, 546);
            numEpochsLabel.Name = "numEpochsLabel";
            numEpochsLabel.Size = new Size(57, 21);
            numEpochsLabel.TabIndex = 15;
            numEpochsLabel.Text = "Эпохи:";
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
            // numLearningRateLabel
            // 
            numLearningRateLabel.AutoSize = true;
            numLearningRateLabel.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 204);
            numLearningRateLabel.Location = new Point(30, 489);
            numLearningRateLabel.Name = "numLearningRateLabel";
            numLearningRateLabel.Size = new Size(153, 21);
            numLearningRateLabel.TabIndex = 13;
            numLearningRateLabel.Text = "Скорость обучения:";
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
            // numBandwidthLabel
            // 
            numBandwidthLabel.AutoSize = true;
            numBandwidthLabel.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 204);
            numBandwidthLabel.Location = new Point(24, 428);
            numBandwidthLabel.Name = "numBandwidthLabel";
            numBandwidthLabel.Size = new Size(190, 21);
            numBandwidthLabel.TabIndex = 11;
            numBandwidthLabel.Text = "Пропускная способность";
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
            // numKLabel
            // 
            numKLabel.AutoSize = true;
            numKLabel.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 204);
            numKLabel.Location = new Point(30, 366);
            numKLabel.Name = "numKLabel";
            numKLabel.Size = new Size(157, 21);
            numKLabel.TabIndex = 9;
            numKLabel.Text = "Количество соседей:";
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
            // MetricComboboxLabel
            // 
            MetricComboboxLabel.AutoSize = true;
            MetricComboboxLabel.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 204);
            MetricComboboxLabel.Location = new Point(30, 189);
            MetricComboboxLabel.Name = "MetricComboboxLabel";
            MetricComboboxLabel.Size = new Size(147, 21);
            MetricComboboxLabel.TabIndex = 6;
            MetricComboboxLabel.Text = "Выберите метрику:";
            // 
            // algorytmComboBox
            // 
            algorytmComboBox.FormattingEnabled = true;
            algorytmComboBox.Location = new Point(12, 159);
            algorytmComboBox.Name = "algorytmComboBox";
            algorytmComboBox.Size = new Size(202, 23);
            algorytmComboBox.TabIndex = 5;
            // 
            // algorytmComboBoxlabel
            // 
            algorytmComboBoxlabel.AutoSize = true;
            algorytmComboBoxlabel.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 204);
            algorytmComboBoxlabel.Location = new Point(30, 135);
            algorytmComboBoxlabel.Name = "algorytmComboBoxlabel";
            algorytmComboBoxlabel.Size = new Size(154, 21);
            algorytmComboBoxlabel.TabIndex = 4;
            algorytmComboBoxlabel.Text = "Выберите алгоритм:";
            // 
            // TypeCombobxLabel
            // 
            TypeCombobxLabel.AutoSize = true;
            TypeCombobxLabel.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 204);
            TypeCombobxLabel.Location = new Point(30, 77);
            TypeCombobxLabel.Name = "TypeCombobxLabel";
            TypeCombobxLabel.Size = new Size(166, 21);
            TypeCombobxLabel.TabIndex = 3;
            TypeCombobxLabel.Text = "Выберите тип задачи:\r\n";
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
        private Label checkedListBox1Label;
        private Label ResultTextBoxLabel;
        private TextBox ResultTextBox;
        private ComboBox TypeKermel;
        private Label TypeKermelLabel;
        private Label progressBar1Label;
        private Label dataGridView1Label;
        private ProgressBar progressBar1;
        private DataGridView dataGridView1;
        private Button StudyModelButton;
        private Label numEpochsLabel;
        private NumericUpDown numEpochs;
        private Label numLearningRateLabel;
        private NumericUpDown numLearningRate;
        private Label numBandwidthLabel;
        private NumericUpDown numBandwidth;
        private Label numKLabel;
        private NumericUpDown numK;
        private ComboBox MetricCombobox;
        private Label MetricComboboxLabel;
        private ComboBox algorytmComboBox;
        private Label algorytmComboBoxlabel;
        private Label TypeCombobxLabel;
        private ComboBox TypeCombobox;
        private Button chosefileButton;
        private TabControl tab;
    }
}
