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
            TabKnn = new TabPage();
            ShowPlotButton = new Button();
            PredictButton = new Button();
            problemTypeComboBox = new ComboBox();
            targetComboBox = new ComboBox();
            featuresCheckedListBox = new CheckedListBox();
            numKNN = new NumericUpDown();
            label1 = new Label();
            ResultTextBoxLabel = new Label();
            ResultTextBox = new TextBox();
            progressBar1Label = new Label();
            dataGridView1Label = new Label();
            progressBar1 = new ProgressBar();
            dataGridView1 = new DataGridView();
            TrainButton = new Button();
            chosefileButton = new Button();
            Tab = new TabControl();
            WeightKnnPage = new TabPage();
            PredictButtonWeightKNN = new Button();
            targetComboBoxWeightKNN = new ComboBox();
            problemTypeComboBoxWeightKNN = new ComboBox();
            featuresCheckedListBox1 = new CheckedListBox();
            numWeightKNN = new NumericUpDown();
            label3 = new Label();
            label2 = new Label();
            ResultTextBoxWeighKNN = new TextBox();
            label4 = new Label();
            label5 = new Label();
            progressBar2 = new ProgressBar();
            dataGridView2 = new DataGridView();
            TrainButtonWeightKNN = new Button();
            STOLPage = new TabPage();
            label11 = new Label();
            label10 = new Label();
            label9 = new Label();
            confidenceThresholdSTOL = new NumericUpDown();
            maxSamplesSTOL = new NumericUpDown();
            PredictButtonSTOL = new Button();
            problemTypeComboBoxSTOL = new ComboBox();
            targetComboBoxSTOL = new ComboBox();
            featuresCheckedListBox2 = new CheckedListBox();
            numSTOL = new NumericUpDown();
            label8 = new Label();
            label14 = new Label();
            ResultTextBoxSTOL = new TextBox();
            label16 = new Label();
            label17 = new Label();
            progressBar3 = new ProgressBar();
            dataGridView3 = new DataGridView();
            TrainButtonSTOL = new Button();
            SVMPage = new TabPage();
            label18 = new Label();
            label12 = new Label();
            label13 = new Label();
            label15 = new Label();
            lambdaSVM = new NumericUpDown();
            learningRateSVM = new NumericUpDown();
            cParamSVM = new NumericUpDown();
            PredictButtonSVM = new Button();
            problemTypeComboBoxSVM = new ComboBox();
            targetComboBoxSVM = new ComboBox();
            featuresCheckedListBox3 = new CheckedListBox();
            numSVM = new NumericUpDown();
            label7 = new Label();
            label26 = new Label();
            ResultTextBoxSVM = new TextBox();
            label28 = new Label();
            label29 = new Label();
            progressBar4 = new ProgressBar();
            dataGridView4 = new DataGridView();
            TrainButtonSVM = new Button();
            TabNadarayaWatsona = new TabPage();
            label20 = new Label();
            kernelTypeNadaraya = new ComboBox();
            label19 = new Label();
            PredictButtonNadaraya = new Button();
            problemTypeComboBoxNadaraya = new ComboBox();
            targetComboBoxNadaraya = new ComboBox();
            featuresCheckedListBox4 = new CheckedListBox();
            numNadaraya = new NumericUpDown();
            label6 = new Label();
            label38 = new Label();
            ResultTextBoxNadaraya = new TextBox();
            label40 = new Label();
            label41 = new Label();
            progressBar5 = new ProgressBar();
            dataGridView5 = new DataGridView();
            TrainButtonNadaraya = new Button();
            normalizationComboBoxKNN = new ComboBox();
            label21 = new Label();
            label22 = new Label();
            normalizationComboBoxWeightKNN = new ComboBox();
            label23 = new Label();
            normalizationComboBoxSTOL = new ComboBox();
            label24 = new Label();
            normalizationComboBoxSVM = new ComboBox();
            label25 = new Label();
            normalizationComboBoxNadaraya = new ComboBox();
            TabKnn.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)numKNN).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            Tab.SuspendLayout();
            WeightKnnPage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)numWeightKNN).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dataGridView2).BeginInit();
            STOLPage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)confidenceThresholdSTOL).BeginInit();
            ((System.ComponentModel.ISupportInitialize)maxSamplesSTOL).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numSTOL).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dataGridView3).BeginInit();
            SVMPage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)lambdaSVM).BeginInit();
            ((System.ComponentModel.ISupportInitialize)learningRateSVM).BeginInit();
            ((System.ComponentModel.ISupportInitialize)cParamSVM).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numSVM).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dataGridView4).BeginInit();
            TabNadarayaWatsona.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)numNadaraya).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dataGridView5).BeginInit();
            SuspendLayout();
            // 
            // TabKnn
            // 
            TabKnn.Controls.Add(label21);
            TabKnn.Controls.Add(normalizationComboBoxKNN);
            TabKnn.Controls.Add(ShowPlotButton);
            TabKnn.Controls.Add(PredictButton);
            TabKnn.Controls.Add(problemTypeComboBox);
            TabKnn.Controls.Add(targetComboBox);
            TabKnn.Controls.Add(featuresCheckedListBox);
            TabKnn.Controls.Add(numKNN);
            TabKnn.Controls.Add(label1);
            TabKnn.Controls.Add(ResultTextBoxLabel);
            TabKnn.Controls.Add(ResultTextBox);
            TabKnn.Controls.Add(progressBar1Label);
            TabKnn.Controls.Add(dataGridView1Label);
            TabKnn.Controls.Add(progressBar1);
            TabKnn.Controls.Add(dataGridView1);
            TabKnn.Controls.Add(TrainButton);
            TabKnn.Controls.Add(chosefileButton);
            TabKnn.Location = new Point(4, 24);
            TabKnn.Name = "TabKnn";
            TabKnn.Padding = new Padding(3);
            TabKnn.Size = new Size(1292, 808);
            TabKnn.TabIndex = 0;
            TabKnn.Text = "КНН";
            TabKnn.UseVisualStyleBackColor = true;
            // 
            // ShowPlotButton
            // 
            ShowPlotButton.BackColor = Color.LightGray;
            ShowPlotButton.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 204);
            ShowPlotButton.Location = new Point(241, 690);
            ShowPlotButton.Name = "ShowPlotButton";
            ShowPlotButton.Size = new Size(202, 60);
            ShowPlotButton.TabIndex = 31;
            ShowPlotButton.Text = "Построить график";
            ShowPlotButton.UseVisualStyleBackColor = false;
            ShowPlotButton.Click += ShowPlotButton_Click;
            // 
            // PredictButton
            // 
            PredictButton.BackColor = Color.LightGray;
            PredictButton.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 204);
            PredictButton.Location = new Point(12, 690);
            PredictButton.Name = "PredictButton";
            PredictButton.Size = new Size(202, 60);
            PredictButton.TabIndex = 30;
            PredictButton.Text = "Предсказать";
            PredictButton.UseVisualStyleBackColor = false;
            // 
            // problemTypeComboBox
            // 
            problemTypeComboBox.FormattingEnabled = true;
            problemTypeComboBox.Location = new Point(12, 359);
            problemTypeComboBox.Name = "problemTypeComboBox";
            problemTypeComboBox.Size = new Size(202, 23);
            problemTypeComboBox.TabIndex = 29;
            // 
            // targetComboBox
            // 
            targetComboBox.FormattingEnabled = true;
            targetComboBox.Location = new Point(12, 328);
            targetComboBox.Name = "targetComboBox";
            targetComboBox.Size = new Size(202, 23);
            targetComboBox.TabIndex = 28;
            // 
            // featuresCheckedListBox
            // 
            featuresCheckedListBox.FormattingEnabled = true;
            featuresCheckedListBox.Location = new Point(12, 174);
            featuresCheckedListBox.Name = "featuresCheckedListBox";
            featuresCheckedListBox.Size = new Size(202, 148);
            featuresCheckedListBox.TabIndex = 27;
            // 
            // numKNN
            // 
            numKNN.Location = new Point(12, 144);
            numKNN.Name = "numKNN";
            numKNN.Size = new Size(202, 23);
            numKNN.TabIndex = 26;
            numKNN.Value = new decimal(new int[] { 1, 0, 0, 0 });
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 204);
            label1.Location = new Point(12, 116);
            label1.Name = "label1";
            label1.Size = new Size(208, 25);
            label1.TabIndex = 25;
            label1.Text = "К-ближайших соседей";
            // 
            // ResultTextBoxLabel
            // 
            ResultTextBoxLabel.AutoSize = true;
            ResultTextBoxLabel.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 204);
            ResultTextBoxLabel.Location = new Point(669, 392);
            ResultTextBoxLabel.Name = "ResultTextBoxLabel";
            ResultTextBoxLabel.Size = new Size(186, 21);
            ResultTextBoxLabel.TabIndex = 24;
            ResultTextBoxLabel.Text = "Результат предсказания:";
            // 
            // ResultTextBox
            // 
            ResultTextBox.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point, 204);
            ResultTextBox.Location = new Point(236, 416);
            ResultTextBox.Multiline = true;
            ResultTextBox.Name = "ResultTextBox";
            ResultTextBox.ReadOnly = true;
            ResultTextBox.Size = new Size(1046, 268);
            ResultTextBox.TabIndex = 23;
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
            progressBar1.Size = new Size(1041, 68);
            progressBar1.TabIndex = 18;
            // 
            // dataGridView1
            // 
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Location = new Point(236, 30);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.RowHeadersWidth = 51;
            dataGridView1.Size = new Size(1046, 254);
            dataGridView1.TabIndex = 17;
            // 
            // TrainButton
            // 
            TrainButton.BackColor = Color.LightGray;
            TrainButton.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 204);
            TrainButton.Location = new Point(12, 624);
            TrainButton.Name = "TrainButton";
            TrainButton.Size = new Size(202, 60);
            TrainButton.TabIndex = 16;
            TrainButton.Text = "Обучить модель";
            TrainButton.UseVisualStyleBackColor = false;
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
            // Tab
            // 
            Tab.Controls.Add(TabKnn);
            Tab.Controls.Add(WeightKnnPage);
            Tab.Controls.Add(STOLPage);
            Tab.Controls.Add(SVMPage);
            Tab.Controls.Add(TabNadarayaWatsona);
            Tab.Location = new Point(0, 1);
            Tab.Name = "Tab";
            Tab.SelectedIndex = 0;
            Tab.Size = new Size(1300, 836);
            Tab.TabIndex = 0;
            // 
            // WeightKnnPage
            // 
            WeightKnnPage.Controls.Add(label22);
            WeightKnnPage.Controls.Add(normalizationComboBoxWeightKNN);
            WeightKnnPage.Controls.Add(PredictButtonWeightKNN);
            WeightKnnPage.Controls.Add(targetComboBoxWeightKNN);
            WeightKnnPage.Controls.Add(problemTypeComboBoxWeightKNN);
            WeightKnnPage.Controls.Add(featuresCheckedListBox1);
            WeightKnnPage.Controls.Add(numWeightKNN);
            WeightKnnPage.Controls.Add(label3);
            WeightKnnPage.Controls.Add(label2);
            WeightKnnPage.Controls.Add(ResultTextBoxWeighKNN);
            WeightKnnPage.Controls.Add(label4);
            WeightKnnPage.Controls.Add(label5);
            WeightKnnPage.Controls.Add(progressBar2);
            WeightKnnPage.Controls.Add(dataGridView2);
            WeightKnnPage.Controls.Add(TrainButtonWeightKNN);
            WeightKnnPage.Location = new Point(4, 24);
            WeightKnnPage.Margin = new Padding(3, 2, 3, 2);
            WeightKnnPage.Name = "WeightKnnPage";
            WeightKnnPage.Padding = new Padding(3, 2, 3, 2);
            WeightKnnPage.Size = new Size(1292, 808);
            WeightKnnPage.TabIndex = 1;
            WeightKnnPage.Text = "Взвешенный КНН";
            WeightKnnPage.UseVisualStyleBackColor = true;
            // 
            // PredictButtonWeightKNN
            // 
            PredictButtonWeightKNN.BackColor = Color.LightGray;
            PredictButtonWeightKNN.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 204);
            PredictButtonWeightKNN.Location = new Point(14, 686);
            PredictButtonWeightKNN.Name = "PredictButtonWeightKNN";
            PredictButtonWeightKNN.Size = new Size(202, 60);
            PredictButtonWeightKNN.TabIndex = 57;
            PredictButtonWeightKNN.Text = "Предсказать";
            PredictButtonWeightKNN.UseVisualStyleBackColor = false;
            // 
            // targetComboBoxWeightKNN
            // 
            targetComboBoxWeightKNN.FormattingEnabled = true;
            targetComboBoxWeightKNN.Location = new Point(20, 280);
            targetComboBoxWeightKNN.Name = "targetComboBoxWeightKNN";
            targetComboBoxWeightKNN.Size = new Size(202, 23);
            targetComboBoxWeightKNN.TabIndex = 56;
            // 
            // problemTypeComboBoxWeightKNN
            // 
            problemTypeComboBoxWeightKNN.FormattingEnabled = true;
            problemTypeComboBoxWeightKNN.Location = new Point(20, 309);
            problemTypeComboBoxWeightKNN.Name = "problemTypeComboBoxWeightKNN";
            problemTypeComboBoxWeightKNN.Size = new Size(202, 23);
            problemTypeComboBoxWeightKNN.TabIndex = 55;
            // 
            // featuresCheckedListBox1
            // 
            featuresCheckedListBox1.FormattingEnabled = true;
            featuresCheckedListBox1.Location = new Point(20, 126);
            featuresCheckedListBox1.Name = "featuresCheckedListBox1";
            featuresCheckedListBox1.Size = new Size(202, 148);
            featuresCheckedListBox1.TabIndex = 54;
            // 
            // numWeightKNN
            // 
            numWeightKNN.Location = new Point(20, 97);
            numWeightKNN.Name = "numWeightKNN";
            numWeightKNN.Size = new Size(202, 23);
            numWeightKNN.TabIndex = 53;
            numWeightKNN.Value = new decimal(new int[] { 1, 0, 0, 0 });
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 204);
            label3.Location = new Point(34, 69);
            label3.Name = "label3";
            label3.Size = new Size(164, 25);
            label3.TabIndex = 51;
            label3.Text = "Взвешенный КНН";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 204);
            label2.Location = new Point(690, 528);
            label2.Name = "label2";
            label2.Size = new Size(186, 21);
            label2.TabIndex = 50;
            label2.Text = "Результат предсказания:";
            // 
            // ResultTextBoxWeighKNN
            // 
            ResultTextBoxWeighKNN.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point, 204);
            ResultTextBoxWeighKNN.Location = new Point(238, 552);
            ResultTextBoxWeighKNN.Multiline = true;
            ResultTextBoxWeighKNN.Name = "ResultTextBoxWeighKNN";
            ResultTextBoxWeighKNN.ReadOnly = true;
            ResultTextBoxWeighKNN.Size = new Size(1046, 156);
            ResultTextBoxWeighKNN.TabIndex = 49;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 204);
            label4.Location = new Point(470, 318);
            label4.Name = "label4";
            label4.Size = new Size(64, 21);
            label4.TabIndex = 46;
            label4.Text = "Статус: ";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 204);
            label5.Location = new Point(718, 27);
            label5.Name = "label5";
            label5.Size = new Size(139, 21);
            label5.TabIndex = 45;
            label5.Text = "Просмотр данных";
            // 
            // progressBar2
            // 
            progressBar2.Location = new Point(242, 342);
            progressBar2.Name = "progressBar2";
            progressBar2.Size = new Size(1041, 156);
            progressBar2.TabIndex = 44;
            // 
            // dataGridView2
            // 
            dataGridView2.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView2.Location = new Point(238, 51);
            dataGridView2.Name = "dataGridView2";
            dataGridView2.RowHeadersWidth = 51;
            dataGridView2.Size = new Size(1046, 254);
            dataGridView2.TabIndex = 43;
            // 
            // TrainButtonWeightKNN
            // 
            TrainButtonWeightKNN.BackColor = Color.LightGray;
            TrainButtonWeightKNN.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 204);
            TrainButtonWeightKNN.Location = new Point(14, 620);
            TrainButtonWeightKNN.Name = "TrainButtonWeightKNN";
            TrainButtonWeightKNN.Size = new Size(202, 60);
            TrainButtonWeightKNN.TabIndex = 42;
            TrainButtonWeightKNN.Text = "Обучить модель";
            TrainButtonWeightKNN.UseVisualStyleBackColor = false;
            // 
            // STOLPage
            // 
            STOLPage.Controls.Add(label23);
            STOLPage.Controls.Add(normalizationComboBoxSTOL);
            STOLPage.Controls.Add(label11);
            STOLPage.Controls.Add(label10);
            STOLPage.Controls.Add(label9);
            STOLPage.Controls.Add(confidenceThresholdSTOL);
            STOLPage.Controls.Add(maxSamplesSTOL);
            STOLPage.Controls.Add(PredictButtonSTOL);
            STOLPage.Controls.Add(problemTypeComboBoxSTOL);
            STOLPage.Controls.Add(targetComboBoxSTOL);
            STOLPage.Controls.Add(featuresCheckedListBox2);
            STOLPage.Controls.Add(numSTOL);
            STOLPage.Controls.Add(label8);
            STOLPage.Controls.Add(label14);
            STOLPage.Controls.Add(ResultTextBoxSTOL);
            STOLPage.Controls.Add(label16);
            STOLPage.Controls.Add(label17);
            STOLPage.Controls.Add(progressBar3);
            STOLPage.Controls.Add(dataGridView3);
            STOLPage.Controls.Add(TrainButtonSTOL);
            STOLPage.Location = new Point(4, 24);
            STOLPage.Margin = new Padding(3, 2, 3, 2);
            STOLPage.Name = "STOLPage";
            STOLPage.Padding = new Padding(3, 2, 3, 2);
            STOLPage.Size = new Size(1292, 808);
            STOLPage.TabIndex = 2;
            STOLPage.Text = "STOL";
            STOLPage.UseVisualStyleBackColor = true;
            // 
            // label11
            // 
            label11.AutoSize = true;
            label11.BackColor = Color.Transparent;
            label11.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point, 204);
            label11.Location = new Point(47, 190);
            label11.Name = "label11";
            label11.Size = new Size(146, 20);
            label11.TabIndex = 66;
            label11.Text = "Порог уверенности";
            // 
            // label10
            // 
            label10.AutoSize = true;
            label10.BackColor = Color.Transparent;
            label10.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point, 204);
            label10.Location = new Point(47, 137);
            label10.Name = "label10";
            label10.Size = new Size(162, 20);
            label10.TabIndex = 65;
            label10.Text = "Количество образцов";
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.BackColor = Color.Transparent;
            label9.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point, 204);
            label9.Location = new Point(81, 86);
            label9.Name = "label9";
            label9.Size = new Size(78, 20);
            label9.TabIndex = 64;
            label9.Text = "к-соседей";
            // 
            // confidenceThresholdSTOL
            // 
            confidenceThresholdSTOL.Increment = new decimal(new int[] { 1, 0, 0, 131072 });
            confidenceThresholdSTOL.Location = new Point(20, 213);
            confidenceThresholdSTOL.Maximum = new decimal(new int[] { 1, 0, 0, 0 });
            confidenceThresholdSTOL.Name = "confidenceThresholdSTOL";
            confidenceThresholdSTOL.Size = new Size(202, 23);
            confidenceThresholdSTOL.TabIndex = 63;
            confidenceThresholdSTOL.Value = new decimal(new int[] { 5, 0, 0, 65536 });
            // 
            // maxSamplesSTOL
            // 
            maxSamplesSTOL.Location = new Point(20, 160);
            maxSamplesSTOL.Maximum = new decimal(new int[] { 10000, 0, 0, 0 });
            maxSamplesSTOL.Minimum = new decimal(new int[] { 100, 0, 0, 0 });
            maxSamplesSTOL.Name = "maxSamplesSTOL";
            maxSamplesSTOL.Size = new Size(202, 23);
            maxSamplesSTOL.TabIndex = 62;
            maxSamplesSTOL.Value = new decimal(new int[] { 100, 0, 0, 0 });
            // 
            // PredictButtonSTOL
            // 
            PredictButtonSTOL.BackColor = Color.LightGray;
            PredictButtonSTOL.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 204);
            PredictButtonSTOL.Location = new Point(20, 743);
            PredictButtonSTOL.Name = "PredictButtonSTOL";
            PredictButtonSTOL.Size = new Size(202, 60);
            PredictButtonSTOL.TabIndex = 61;
            PredictButtonSTOL.Text = "Предсказать";
            PredictButtonSTOL.UseVisualStyleBackColor = false;
            // 
            // problemTypeComboBoxSTOL
            // 
            problemTypeComboBoxSTOL.FormattingEnabled = true;
            problemTypeComboBoxSTOL.Location = new Point(20, 410);
            problemTypeComboBoxSTOL.Name = "problemTypeComboBoxSTOL";
            problemTypeComboBoxSTOL.Size = new Size(202, 23);
            problemTypeComboBoxSTOL.TabIndex = 60;
            // 
            // targetComboBoxSTOL
            // 
            targetComboBoxSTOL.FormattingEnabled = true;
            targetComboBoxSTOL.Location = new Point(20, 439);
            targetComboBoxSTOL.Name = "targetComboBoxSTOL";
            targetComboBoxSTOL.Size = new Size(202, 23);
            targetComboBoxSTOL.TabIndex = 59;
            // 
            // featuresCheckedListBox2
            // 
            featuresCheckedListBox2.FormattingEnabled = true;
            featuresCheckedListBox2.Location = new Point(20, 256);
            featuresCheckedListBox2.Name = "featuresCheckedListBox2";
            featuresCheckedListBox2.Size = new Size(202, 148);
            featuresCheckedListBox2.TabIndex = 58;
            // 
            // numSTOL
            // 
            numSTOL.Location = new Point(20, 109);
            numSTOL.Name = "numSTOL";
            numSTOL.Size = new Size(202, 23);
            numSTOL.TabIndex = 57;
            numSTOL.Value = new decimal(new int[] { 1, 0, 0, 0 });
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Font = new Font("Segoe UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 204);
            label8.Location = new Point(47, 12);
            label8.Name = "label8";
            label8.Size = new Size(142, 25);
            label8.TabIndex = 54;
            label8.Text = "Алгоритм STOL";
            // 
            // label14
            // 
            label14.AutoSize = true;
            label14.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 204);
            label14.Location = new Point(690, 528);
            label14.Name = "label14";
            label14.Size = new Size(186, 21);
            label14.TabIndex = 50;
            label14.Text = "Результат предсказания:";
            // 
            // ResultTextBoxSTOL
            // 
            ResultTextBoxSTOL.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point, 204);
            ResultTextBoxSTOL.Location = new Point(238, 552);
            ResultTextBoxSTOL.Multiline = true;
            ResultTextBoxSTOL.Name = "ResultTextBoxSTOL";
            ResultTextBoxSTOL.ReadOnly = true;
            ResultTextBoxSTOL.Size = new Size(1046, 156);
            ResultTextBoxSTOL.TabIndex = 49;
            // 
            // label16
            // 
            label16.AutoSize = true;
            label16.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 204);
            label16.Location = new Point(470, 318);
            label16.Name = "label16";
            label16.Size = new Size(64, 21);
            label16.TabIndex = 46;
            label16.Text = "Статус: ";
            // 
            // label17
            // 
            label17.AutoSize = true;
            label17.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 204);
            label17.Location = new Point(718, 27);
            label17.Name = "label17";
            label17.Size = new Size(139, 21);
            label17.TabIndex = 45;
            label17.Text = "Просмотр данных";
            // 
            // progressBar3
            // 
            progressBar3.Location = new Point(242, 342);
            progressBar3.Name = "progressBar3";
            progressBar3.Size = new Size(1041, 156);
            progressBar3.TabIndex = 44;
            // 
            // dataGridView3
            // 
            dataGridView3.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView3.Location = new Point(238, 51);
            dataGridView3.Name = "dataGridView3";
            dataGridView3.RowHeadersWidth = 51;
            dataGridView3.Size = new Size(1046, 254);
            dataGridView3.TabIndex = 43;
            // 
            // TrainButtonSTOL
            // 
            TrainButtonSTOL.BackColor = Color.LightGray;
            TrainButtonSTOL.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 204);
            TrainButtonSTOL.Location = new Point(20, 677);
            TrainButtonSTOL.Name = "TrainButtonSTOL";
            TrainButtonSTOL.Size = new Size(202, 60);
            TrainButtonSTOL.TabIndex = 42;
            TrainButtonSTOL.Text = "Обучить модель";
            TrainButtonSTOL.UseVisualStyleBackColor = false;
            // 
            // SVMPage
            // 
            SVMPage.Controls.Add(label24);
            SVMPage.Controls.Add(normalizationComboBoxSVM);
            SVMPage.Controls.Add(label18);
            SVMPage.Controls.Add(label12);
            SVMPage.Controls.Add(label13);
            SVMPage.Controls.Add(label15);
            SVMPage.Controls.Add(lambdaSVM);
            SVMPage.Controls.Add(learningRateSVM);
            SVMPage.Controls.Add(cParamSVM);
            SVMPage.Controls.Add(PredictButtonSVM);
            SVMPage.Controls.Add(problemTypeComboBoxSVM);
            SVMPage.Controls.Add(targetComboBoxSVM);
            SVMPage.Controls.Add(featuresCheckedListBox3);
            SVMPage.Controls.Add(numSVM);
            SVMPage.Controls.Add(label7);
            SVMPage.Controls.Add(label26);
            SVMPage.Controls.Add(ResultTextBoxSVM);
            SVMPage.Controls.Add(label28);
            SVMPage.Controls.Add(label29);
            SVMPage.Controls.Add(progressBar4);
            SVMPage.Controls.Add(dataGridView4);
            SVMPage.Controls.Add(TrainButtonSVM);
            SVMPage.Location = new Point(4, 24);
            SVMPage.Margin = new Padding(3, 2, 3, 2);
            SVMPage.Name = "SVMPage";
            SVMPage.Padding = new Padding(3, 2, 3, 2);
            SVMPage.Size = new Size(1292, 808);
            SVMPage.TabIndex = 3;
            SVMPage.Text = "SVM";
            SVMPage.UseVisualStyleBackColor = true;
            // 
            // label18
            // 
            label18.AutoSize = true;
            label18.BackColor = Color.Transparent;
            label18.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point, 204);
            label18.Location = new Point(70, 274);
            label18.Name = "label18";
            label18.Size = new Size(92, 20);
            label18.TabIndex = 73;
            label18.Text = "Параметр С";
            // 
            // label12
            // 
            label12.AutoSize = true;
            label12.BackColor = Color.Transparent;
            label12.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point, 204);
            label12.Location = new Point(27, 224);
            label12.Name = "label12";
            label12.Size = new Size(189, 20);
            label12.TabIndex = 72;
            label12.Text = "Параметр регуляризации";
            // 
            // label13
            // 
            label13.AutoSize = true;
            label13.BackColor = Color.Transparent;
            label13.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point, 204);
            label13.Location = new Point(41, 171);
            label13.Name = "label13";
            label13.Size = new Size(144, 20);
            label13.TabIndex = 71;
            label13.Text = "Скорость обучения";
            // 
            // label15
            // 
            label15.AutoSize = true;
            label15.BackColor = Color.Transparent;
            label15.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point, 204);
            label15.Location = new Point(58, 116);
            label15.Name = "label15";
            label15.Size = new Size(126, 20);
            label15.TabIndex = 70;
            label15.Text = "Количество эпох";
            // 
            // lambdaSVM
            // 
            lambdaSVM.Increment = new decimal(new int[] { 1, 0, 0, 131072 });
            lambdaSVM.Location = new Point(14, 247);
            lambdaSVM.Maximum = new decimal(new int[] { 1, 0, 0, 0 });
            lambdaSVM.Name = "lambdaSVM";
            lambdaSVM.Size = new Size(202, 23);
            lambdaSVM.TabIndex = 69;
            lambdaSVM.Value = new decimal(new int[] { 5, 0, 0, 65536 });
            // 
            // learningRateSVM
            // 
            learningRateSVM.Location = new Point(14, 194);
            learningRateSVM.Maximum = new decimal(new int[] { 10000, 0, 0, 0 });
            learningRateSVM.Minimum = new decimal(new int[] { 100, 0, 0, 0 });
            learningRateSVM.Name = "learningRateSVM";
            learningRateSVM.Size = new Size(202, 23);
            learningRateSVM.TabIndex = 68;
            learningRateSVM.Value = new decimal(new int[] { 100, 0, 0, 0 });
            // 
            // cParamSVM
            // 
            cParamSVM.Location = new Point(14, 297);
            cParamSVM.Name = "cParamSVM";
            cParamSVM.Size = new Size(202, 23);
            cParamSVM.TabIndex = 67;
            cParamSVM.Value = new decimal(new int[] { 1, 0, 0, 0 });
            // 
            // PredictButtonSVM
            // 
            PredictButtonSVM.BackColor = Color.LightGray;
            PredictButtonSVM.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 204);
            PredictButtonSVM.Location = new Point(14, 743);
            PredictButtonSVM.Name = "PredictButtonSVM";
            PredictButtonSVM.Size = new Size(202, 60);
            PredictButtonSVM.TabIndex = 61;
            PredictButtonSVM.Text = "Предсказать";
            PredictButtonSVM.UseVisualStyleBackColor = false;
            // 
            // problemTypeComboBoxSVM
            // 
            problemTypeComboBoxSVM.FormattingEnabled = true;
            problemTypeComboBoxSVM.Location = new Point(14, 480);
            problemTypeComboBoxSVM.Name = "problemTypeComboBoxSVM";
            problemTypeComboBoxSVM.Size = new Size(202, 23);
            problemTypeComboBoxSVM.TabIndex = 60;
            // 
            // targetComboBoxSVM
            // 
            targetComboBoxSVM.FormattingEnabled = true;
            targetComboBoxSVM.Location = new Point(14, 509);
            targetComboBoxSVM.Name = "targetComboBoxSVM";
            targetComboBoxSVM.Size = new Size(202, 23);
            targetComboBoxSVM.TabIndex = 59;
            // 
            // featuresCheckedListBox3
            // 
            featuresCheckedListBox3.FormattingEnabled = true;
            featuresCheckedListBox3.Location = new Point(14, 326);
            featuresCheckedListBox3.Name = "featuresCheckedListBox3";
            featuresCheckedListBox3.Size = new Size(202, 148);
            featuresCheckedListBox3.TabIndex = 58;
            // 
            // numSVM
            // 
            numSVM.Location = new Point(14, 139);
            numSVM.Name = "numSVM";
            numSVM.Size = new Size(202, 23);
            numSVM.TabIndex = 57;
            numSVM.Value = new decimal(new int[] { 1, 0, 0, 0 });
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Font = new Font("Segoe UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 204);
            label7.Location = new Point(45, 23);
            label7.Name = "label7";
            label7.Size = new Size(139, 25);
            label7.TabIndex = 53;
            label7.Text = "Алгоритм SVM";
            // 
            // label26
            // 
            label26.AutoSize = true;
            label26.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 204);
            label26.Location = new Point(690, 528);
            label26.Name = "label26";
            label26.Size = new Size(186, 21);
            label26.TabIndex = 50;
            label26.Text = "Результат предсказания:";
            // 
            // ResultTextBoxSVM
            // 
            ResultTextBoxSVM.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point, 204);
            ResultTextBoxSVM.Location = new Point(238, 552);
            ResultTextBoxSVM.Multiline = true;
            ResultTextBoxSVM.Name = "ResultTextBoxSVM";
            ResultTextBoxSVM.ReadOnly = true;
            ResultTextBoxSVM.Size = new Size(1046, 156);
            ResultTextBoxSVM.TabIndex = 49;
            // 
            // label28
            // 
            label28.AutoSize = true;
            label28.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 204);
            label28.Location = new Point(470, 318);
            label28.Name = "label28";
            label28.Size = new Size(64, 21);
            label28.TabIndex = 46;
            label28.Text = "Статус: ";
            // 
            // label29
            // 
            label29.AutoSize = true;
            label29.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 204);
            label29.Location = new Point(718, 27);
            label29.Name = "label29";
            label29.Size = new Size(139, 21);
            label29.TabIndex = 45;
            label29.Text = "Просмотр данных";
            // 
            // progressBar4
            // 
            progressBar4.Location = new Point(242, 342);
            progressBar4.Name = "progressBar4";
            progressBar4.Size = new Size(1041, 156);
            progressBar4.TabIndex = 44;
            // 
            // dataGridView4
            // 
            dataGridView4.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView4.Location = new Point(238, 51);
            dataGridView4.Name = "dataGridView4";
            dataGridView4.RowHeadersWidth = 51;
            dataGridView4.Size = new Size(1046, 254);
            dataGridView4.TabIndex = 43;
            // 
            // TrainButtonSVM
            // 
            TrainButtonSVM.BackColor = Color.LightGray;
            TrainButtonSVM.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 204);
            TrainButtonSVM.Location = new Point(14, 677);
            TrainButtonSVM.Name = "TrainButtonSVM";
            TrainButtonSVM.Size = new Size(202, 60);
            TrainButtonSVM.TabIndex = 42;
            TrainButtonSVM.Text = "Обучить модель";
            TrainButtonSVM.UseVisualStyleBackColor = false;
            // 
            // TabNadarayaWatsona
            // 
            TabNadarayaWatsona.Controls.Add(label25);
            TabNadarayaWatsona.Controls.Add(normalizationComboBoxNadaraya);
            TabNadarayaWatsona.Controls.Add(label20);
            TabNadarayaWatsona.Controls.Add(kernelTypeNadaraya);
            TabNadarayaWatsona.Controls.Add(label19);
            TabNadarayaWatsona.Controls.Add(PredictButtonNadaraya);
            TabNadarayaWatsona.Controls.Add(problemTypeComboBoxNadaraya);
            TabNadarayaWatsona.Controls.Add(targetComboBoxNadaraya);
            TabNadarayaWatsona.Controls.Add(featuresCheckedListBox4);
            TabNadarayaWatsona.Controls.Add(numNadaraya);
            TabNadarayaWatsona.Controls.Add(label6);
            TabNadarayaWatsona.Controls.Add(label38);
            TabNadarayaWatsona.Controls.Add(ResultTextBoxNadaraya);
            TabNadarayaWatsona.Controls.Add(label40);
            TabNadarayaWatsona.Controls.Add(label41);
            TabNadarayaWatsona.Controls.Add(progressBar5);
            TabNadarayaWatsona.Controls.Add(dataGridView5);
            TabNadarayaWatsona.Controls.Add(TrainButtonNadaraya);
            TabNadarayaWatsona.Location = new Point(4, 24);
            TabNadarayaWatsona.Margin = new Padding(3, 2, 3, 2);
            TabNadarayaWatsona.Name = "TabNadarayaWatsona";
            TabNadarayaWatsona.Padding = new Padding(3, 2, 3, 2);
            TabNadarayaWatsona.Size = new Size(1292, 808);
            TabNadarayaWatsona.TabIndex = 4;
            TabNadarayaWatsona.Text = "Nadaraya-Watson";
            TabNadarayaWatsona.UseVisualStyleBackColor = true;
            // 
            // label20
            // 
            label20.AutoSize = true;
            label20.BackColor = Color.Transparent;
            label20.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point, 204);
            label20.Location = new Point(75, 167);
            label20.Name = "label20";
            label20.Size = new Size(72, 20);
            label20.TabIndex = 73;
            label20.Text = "Тип ядра";
            // 
            // kernelTypeNadaraya
            // 
            kernelTypeNadaraya.FormattingEnabled = true;
            kernelTypeNadaraya.Location = new Point(13, 190);
            kernelTypeNadaraya.Name = "kernelTypeNadaraya";
            kernelTypeNadaraya.Size = new Size(202, 23);
            kernelTypeNadaraya.TabIndex = 72;
            // 
            // label19
            // 
            label19.AutoSize = true;
            label19.BackColor = Color.Transparent;
            label19.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point, 204);
            label19.Location = new Point(41, 113);
            label19.Name = "label19";
            label19.Size = new Size(156, 20);
            label19.TabIndex = 71;
            label19.Text = "Полоса пропускания";
            // 
            // PredictButtonNadaraya
            // 
            PredictButtonNadaraya.BackColor = Color.LightGray;
            PredictButtonNadaraya.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 204);
            PredictButtonNadaraya.Location = new Point(14, 686);
            PredictButtonNadaraya.Name = "PredictButtonNadaraya";
            PredictButtonNadaraya.Size = new Size(202, 60);
            PredictButtonNadaraya.TabIndex = 61;
            PredictButtonNadaraya.Text = "Предсказать";
            PredictButtonNadaraya.UseVisualStyleBackColor = false;
            // 
            // problemTypeComboBoxNadaraya
            // 
            problemTypeComboBoxNadaraya.FormattingEnabled = true;
            problemTypeComboBoxNadaraya.Location = new Point(13, 373);
            problemTypeComboBoxNadaraya.Name = "problemTypeComboBoxNadaraya";
            problemTypeComboBoxNadaraya.Size = new Size(202, 23);
            problemTypeComboBoxNadaraya.TabIndex = 60;
            // 
            // targetComboBoxNadaraya
            // 
            targetComboBoxNadaraya.FormattingEnabled = true;
            targetComboBoxNadaraya.Location = new Point(13, 402);
            targetComboBoxNadaraya.Name = "targetComboBoxNadaraya";
            targetComboBoxNadaraya.Size = new Size(202, 23);
            targetComboBoxNadaraya.TabIndex = 59;
            // 
            // featuresCheckedListBox4
            // 
            featuresCheckedListBox4.FormattingEnabled = true;
            featuresCheckedListBox4.Location = new Point(13, 219);
            featuresCheckedListBox4.Name = "featuresCheckedListBox4";
            featuresCheckedListBox4.Size = new Size(202, 148);
            featuresCheckedListBox4.TabIndex = 58;
            // 
            // numNadaraya
            // 
            numNadaraya.Location = new Point(13, 136);
            numNadaraya.Name = "numNadaraya";
            numNadaraya.Size = new Size(202, 23);
            numNadaraya.TabIndex = 57;
            numNadaraya.Value = new decimal(new int[] { 1, 0, 0, 0 });
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 204);
            label6.Location = new Point(14, 27);
            label6.Name = "label6";
            label6.Size = new Size(208, 21);
            label6.TabIndex = 54;
            label6.Text = "Алгоритм Nadaraya-Watson";
            // 
            // label38
            // 
            label38.AutoSize = true;
            label38.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 204);
            label38.Location = new Point(690, 528);
            label38.Name = "label38";
            label38.Size = new Size(186, 21);
            label38.TabIndex = 50;
            label38.Text = "Результат предсказания:";
            // 
            // ResultTextBoxNadaraya
            // 
            ResultTextBoxNadaraya.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point, 204);
            ResultTextBoxNadaraya.Location = new Point(238, 552);
            ResultTextBoxNadaraya.Multiline = true;
            ResultTextBoxNadaraya.Name = "ResultTextBoxNadaraya";
            ResultTextBoxNadaraya.ReadOnly = true;
            ResultTextBoxNadaraya.Size = new Size(1046, 156);
            ResultTextBoxNadaraya.TabIndex = 49;
            // 
            // label40
            // 
            label40.AutoSize = true;
            label40.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 204);
            label40.Location = new Point(470, 318);
            label40.Name = "label40";
            label40.Size = new Size(64, 21);
            label40.TabIndex = 46;
            label40.Text = "Статус: ";
            // 
            // label41
            // 
            label41.AutoSize = true;
            label41.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 204);
            label41.Location = new Point(718, 27);
            label41.Name = "label41";
            label41.Size = new Size(139, 21);
            label41.TabIndex = 45;
            label41.Text = "Просмотр данных";
            // 
            // progressBar5
            // 
            progressBar5.Location = new Point(242, 342);
            progressBar5.Name = "progressBar5";
            progressBar5.Size = new Size(1041, 156);
            progressBar5.TabIndex = 44;
            // 
            // dataGridView5
            // 
            dataGridView5.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView5.Location = new Point(238, 51);
            dataGridView5.Name = "dataGridView5";
            dataGridView5.RowHeadersWidth = 51;
            dataGridView5.Size = new Size(1046, 254);
            dataGridView5.TabIndex = 43;
            // 
            // TrainButtonNadaraya
            // 
            TrainButtonNadaraya.BackColor = Color.LightGray;
            TrainButtonNadaraya.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 204);
            TrainButtonNadaraya.Location = new Point(14, 620);
            TrainButtonNadaraya.Name = "TrainButtonNadaraya";
            TrainButtonNadaraya.Size = new Size(202, 60);
            TrainButtonNadaraya.TabIndex = 42;
            TrainButtonNadaraya.Text = "Обучить модель";
            TrainButtonNadaraya.UseVisualStyleBackColor = false;
            // 
            // normalizationComboBoxKNN
            // 
            normalizationComboBoxKNN.FormattingEnabled = true;
            normalizationComboBoxKNN.Location = new Point(12, 90);
            normalizationComboBoxKNN.Name = "normalizationComboBoxKNN";
            normalizationComboBoxKNN.Size = new Size(202, 23);
            normalizationComboBoxKNN.TabIndex = 32;
            // 
            // label21
            // 
            label21.AutoSize = true;
            label21.BackColor = Color.Transparent;
            label21.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point, 204);
            label21.Location = new Point(37, 67);
            label21.Name = "label21";
            label21.Size = new Size(146, 20);
            label21.TabIndex = 65;
            label21.Text = "Тип нормирования";
            // 
            // label22
            // 
            label22.AutoSize = true;
            label22.BackColor = Color.Transparent;
            label22.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point, 204);
            label22.Location = new Point(45, 20);
            label22.Name = "label22";
            label22.Size = new Size(146, 20);
            label22.TabIndex = 67;
            label22.Text = "Тип нормирования";
            // 
            // normalizationComboBoxWeightKNN
            // 
            normalizationComboBoxWeightKNN.FormattingEnabled = true;
            normalizationComboBoxWeightKNN.Location = new Point(20, 43);
            normalizationComboBoxWeightKNN.Name = "normalizationComboBoxWeightKNN";
            normalizationComboBoxWeightKNN.Size = new Size(202, 23);
            normalizationComboBoxWeightKNN.TabIndex = 66;
            // 
            // label23
            // 
            label23.AutoSize = true;
            label23.BackColor = Color.Transparent;
            label23.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point, 204);
            label23.Location = new Point(45, 37);
            label23.Name = "label23";
            label23.Size = new Size(146, 20);
            label23.TabIndex = 68;
            label23.Text = "Тип нормирования";
            // 
            // normalizationComboBoxSTOL
            // 
            normalizationComboBoxSTOL.FormattingEnabled = true;
            normalizationComboBoxSTOL.Location = new Point(20, 60);
            normalizationComboBoxSTOL.Name = "normalizationComboBoxSTOL";
            normalizationComboBoxSTOL.Size = new Size(202, 23);
            normalizationComboBoxSTOL.TabIndex = 67;
            // 
            // label24
            // 
            label24.AutoSize = true;
            label24.BackColor = Color.Transparent;
            label24.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point, 204);
            label24.Location = new Point(39, 67);
            label24.Name = "label24";
            label24.Size = new Size(146, 20);
            label24.TabIndex = 75;
            label24.Text = "Тип нормирования";
            // 
            // normalizationComboBoxSVM
            // 
            normalizationComboBoxSVM.FormattingEnabled = true;
            normalizationComboBoxSVM.Location = new Point(14, 90);
            normalizationComboBoxSVM.Name = "normalizationComboBoxSVM";
            normalizationComboBoxSVM.Size = new Size(202, 23);
            normalizationComboBoxSVM.TabIndex = 74;
            // 
            // label25
            // 
            label25.AutoSize = true;
            label25.BackColor = Color.Transparent;
            label25.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point, 204);
            label25.Location = new Point(39, 64);
            label25.Name = "label25";
            label25.Size = new Size(146, 20);
            label25.TabIndex = 75;
            label25.Text = "Тип нормирования";
            // 
            // normalizationComboBoxNadaraya
            // 
            normalizationComboBoxNadaraya.FormattingEnabled = true;
            normalizationComboBoxNadaraya.Location = new Point(14, 87);
            normalizationComboBoxNadaraya.Name = "normalizationComboBoxNadaraya";
            normalizationComboBoxNadaraya.Size = new Size(202, 23);
            normalizationComboBoxNadaraya.TabIndex = 74;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.PaleGreen;
            ClientSize = new Size(1298, 849);
            Controls.Add(Tab);
            Name = "MainForm";
            Text = "MainForm";
            TabKnn.ResumeLayout(false);
            TabKnn.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)numKNN).EndInit();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            Tab.ResumeLayout(false);
            WeightKnnPage.ResumeLayout(false);
            WeightKnnPage.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)numWeightKNN).EndInit();
            ((System.ComponentModel.ISupportInitialize)dataGridView2).EndInit();
            STOLPage.ResumeLayout(false);
            STOLPage.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)confidenceThresholdSTOL).EndInit();
            ((System.ComponentModel.ISupportInitialize)maxSamplesSTOL).EndInit();
            ((System.ComponentModel.ISupportInitialize)numSTOL).EndInit();
            ((System.ComponentModel.ISupportInitialize)dataGridView3).EndInit();
            SVMPage.ResumeLayout(false);
            SVMPage.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)lambdaSVM).EndInit();
            ((System.ComponentModel.ISupportInitialize)learningRateSVM).EndInit();
            ((System.ComponentModel.ISupportInitialize)cParamSVM).EndInit();
            ((System.ComponentModel.ISupportInitialize)numSVM).EndInit();
            ((System.ComponentModel.ISupportInitialize)dataGridView4).EndInit();
            TabNadarayaWatsona.ResumeLayout(false);
            TabNadarayaWatsona.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)numNadaraya).EndInit();
            ((System.ComponentModel.ISupportInitialize)dataGridView5).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private TabPage TabKnn;
        private Label ResultTextBoxLabel;
        private TextBox ResultTextBox;
        private Label progressBar1Label;
        private Label dataGridView1Label;
        private ProgressBar progressBar1;
        private DataGridView dataGridView1;
        private Button TrainButton;
        private Button chosefileButton;
        private TabControl Tab;
        private TabPage WeightKnnPage;
        private TabPage STOLPage;
        private TabPage SVMPage;
        private Label label2;
        private TextBox ResultTextBoxWeighKNN;
        private Label label4;
        private Label label5;
        private ProgressBar progressBar2;
        private DataGridView dataGridView2;
        private Button TrainButtonWeightKNN;
        private Label label14;
        private TextBox ResultTextBoxSTOL;
        private Label label16;
        private Label label17;
        private ProgressBar progressBar3;
        private DataGridView dataGridView3;
        private Button TrainButtonSTOL;
        private Label label26;
        private TextBox ResultTextBoxSVM;
        private Label label28;
        private Label label29;
        private ProgressBar progressBar4;
        private DataGridView dataGridView4;
        private Button TrainButtonSVM;
        private TabPage TabNadarayaWatsona;
        private Label label38;
        private TextBox ResultTextBoxNadaraya;
        private Label label40;
        private Label label41;
        private ProgressBar progressBar5;
        private DataGridView dataGridView5;
        private Button TrainButtonNadaraya;
        private Label label1;
        private Label label3;
        private Label label8;
        private Label label7;
        private Label label6;
        private NumericUpDown numKNN;
        private ComboBox problemTypeComboBox;
        private ComboBox targetComboBox;
        private CheckedListBox featuresCheckedListBox;
        private Button PredictButton;
        private ComboBox targetComboBoxWeightKNN;
        private ComboBox problemTypeComboBoxWeightKNN;
        private CheckedListBox featuresCheckedListBox1;
        private NumericUpDown numWeightKNN;
        private Button PredictButtonWeightKNN;
        private Button PredictButtonSTOL;
        private ComboBox problemTypeComboBoxSTOL;
        private ComboBox targetComboBoxSTOL;
        private CheckedListBox featuresCheckedListBox2;
        private NumericUpDown numSTOL;
        private Button PredictButtonSVM;
        private ComboBox problemTypeComboBoxSVM;
        private ComboBox targetComboBoxSVM;
        private CheckedListBox featuresCheckedListBox3;
        private NumericUpDown numSVM;
        private Button PredictButtonNadaraya;
        private ComboBox problemTypeComboBoxNadaraya;
        private ComboBox targetComboBoxNadaraya;
        private CheckedListBox featuresCheckedListBox4;
        private NumericUpDown numNadaraya;
        private Label label9;
        private NumericUpDown confidenceThresholdSTOL;
        private NumericUpDown maxSamplesSTOL;
        private Label label11;
        private Label label10;
        private Label label12;
        private Label label13;
        private Label label15;
        private NumericUpDown lambdaSVM;
        private NumericUpDown learningRateSVM;
        private NumericUpDown cParamSVM;
        private Label label18;
        private ComboBox kernelTypeNadaraya;
        private Label label19;
        private Label label20;
        private Button ShowPlotButton;
        private Label label21;
        private ComboBox normalizationComboBoxKNN;
        private Label label22;
        private ComboBox normalizationComboBoxWeightKNN;
        private Label label23;
        private ComboBox normalizationComboBoxSTOL;
        private Label label24;
        private ComboBox normalizationComboBoxSVM;
        private Label label25;
        private ComboBox normalizationComboBoxNadaraya;
    }
}
