namespace Graphics
{
    partial class Form1
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

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea3 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend3 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series7 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series8 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series9 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.tabControl_graphic = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.chart_sinus = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.checkBox_rectangle = new System.Windows.Forms.CheckBox();
            this.checkBox_triangle = new System.Windows.Forms.CheckBox();
            this.checkBox_sinus = new System.Windows.Forms.CheckBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.textBox_N = new System.Windows.Forms.TextBox();
            this.trackBar_d = new System.Windows.Forms.TrackBar();
            this.label6 = new System.Windows.Forms.Label();
            this.trackBar_F = new System.Windows.Forms.TrackBar();
            this.label1 = new System.Windows.Forms.Label();
            this.trackBar_fo = new System.Windows.Forms.TrackBar();
            this.trackBar_A = new System.Windows.Forms.TrackBar();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.tabControl_graphic.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chart_sinus)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar_d)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar_F)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar_fo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar_A)).BeginInit();
            this.SuspendLayout();
            // 
            // tabControl_graphic
            // 
            this.tabControl_graphic.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl_graphic.Controls.Add(this.tabPage1);
            this.tabControl_graphic.Controls.Add(this.tabPage2);
            this.tabControl_graphic.Location = new System.Drawing.Point(0, 1);
            this.tabControl_graphic.Name = "tabControl_graphic";
            this.tabControl_graphic.SelectedIndex = 0;
            this.tabControl_graphic.Size = new System.Drawing.Size(1122, 552);
            this.tabControl_graphic.TabIndex = 4;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.groupBox3);
            this.tabPage1.Controls.Add(this.groupBox2);
            this.tabPage1.Controls.Add(this.groupBox1);
            this.tabPage1.Location = new System.Drawing.Point(4, 25);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(1114, 523);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Harmonic";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // groupBox3
            // 
            this.groupBox3.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox3.Controls.Add(this.chart_sinus);
            this.groupBox3.Location = new System.Drawing.Point(8, 3);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(781, 523);
            this.groupBox3.TabIndex = 6;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Graphic image";
            // 
            // chart_sinus
            // 
            this.chart_sinus.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.chart_sinus.BackImageAlignment = System.Windows.Forms.DataVisualization.Charting.ChartImageAlignmentStyle.Center;
            chartArea3.Name = "ChartArea1";
            this.chart_sinus.ChartAreas.Add(chartArea3);
            legend3.Name = "Legend1";
            this.chart_sinus.Legends.Add(legend3);
            this.chart_sinus.Location = new System.Drawing.Point(0, 21);
            this.chart_sinus.Name = "chart_sinus";
            series7.BorderWidth = 3;
            series7.ChartArea = "ChartArea1";
            series7.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Spline;
            series7.Legend = "Legend1";
            series7.LegendText = "sinus";
            series7.Name = "Series1";
            series8.BorderWidth = 3;
            series8.ChartArea = "ChartArea1";
            series8.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series8.Legend = "Legend1";
            series8.LegendText = "triangle";
            series8.Name = "Series2";
            series9.BorderWidth = 3;
            series9.ChartArea = "ChartArea1";
            series9.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series9.Legend = "Legend1";
            series9.LegendText = "rectangle";
            series9.Name = "Series3";
            this.chart_sinus.Series.Add(series7);
            this.chart_sinus.Series.Add(series8);
            this.chart_sinus.Series.Add(series9);
            this.chart_sinus.Size = new System.Drawing.Size(775, 496);
            this.chart_sinus.TabIndex = 0;
            this.chart_sinus.Text = "chart1";
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.checkBox_rectangle);
            this.groupBox2.Controls.Add(this.checkBox_triangle);
            this.groupBox2.Controls.Add(this.checkBox_sinus);
            this.groupBox2.Location = new System.Drawing.Point(795, 384);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(288, 142);
            this.groupBox2.TabIndex = 5;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Graphics";
            // 
            // checkBox_rectangle
            // 
            this.checkBox_rectangle.AutoSize = true;
            this.checkBox_rectangle.Location = new System.Drawing.Point(26, 105);
            this.checkBox_rectangle.Name = "checkBox_rectangle";
            this.checkBox_rectangle.Size = new System.Drawing.Size(85, 20);
            this.checkBox_rectangle.TabIndex = 2;
            this.checkBox_rectangle.Text = "rectangle";
            this.checkBox_rectangle.UseVisualStyleBackColor = true;
            this.checkBox_rectangle.CheckedChanged += new System.EventHandler(this.checkBox_rectangle_CheckedChanged);
            // 
            // checkBox_triangle
            // 
            this.checkBox_triangle.AutoSize = true;
            this.checkBox_triangle.Location = new System.Drawing.Point(26, 69);
            this.checkBox_triangle.Name = "checkBox_triangle";
            this.checkBox_triangle.Size = new System.Drawing.Size(73, 20);
            this.checkBox_triangle.TabIndex = 1;
            this.checkBox_triangle.Text = "triangle";
            this.checkBox_triangle.UseVisualStyleBackColor = true;
            this.checkBox_triangle.CheckedChanged += new System.EventHandler(this.checkBox_triangle_CheckedChanged);
            // 
            // checkBox_sinus
            // 
            this.checkBox_sinus.AutoSize = true;
            this.checkBox_sinus.Checked = true;
            this.checkBox_sinus.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox_sinus.Location = new System.Drawing.Point(26, 32);
            this.checkBox_sinus.Name = "checkBox_sinus";
            this.checkBox_sinus.Size = new System.Drawing.Size(60, 20);
            this.checkBox_sinus.TabIndex = 0;
            this.checkBox_sinus.Text = "sinus";
            this.checkBox_sinus.UseVisualStyleBackColor = true;
            this.checkBox_sinus.CheckedChanged += new System.EventHandler(this.checkBox_sinus_CheckedChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.textBox_N);
            this.groupBox1.Controls.Add(this.trackBar_d);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.trackBar_F);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.trackBar_fo);
            this.groupBox1.Controls.Add(this.trackBar_A);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Location = new System.Drawing.Point(795, 31);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(288, 347);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Parametrs";
            // 
            // textBox_N
            // 
            this.textBox_N.Location = new System.Drawing.Point(35, 280);
            this.textBox_N.Name = "textBox_N";
            this.textBox_N.Size = new System.Drawing.Size(86, 22);
            this.textBox_N.TabIndex = 9;
            this.textBox_N.Text = "512";
            this.textBox_N.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBox_N_KeyPress);
            this.textBox_N.KeyUp += new System.Windows.Forms.KeyEventHandler(this.textBox_N_KeyUp);
            // 
            // trackBar_d
            // 
            this.trackBar_d.Enabled = false;
            this.trackBar_d.LargeChange = 2;
            this.trackBar_d.Location = new System.Drawing.Point(26, 218);
            this.trackBar_d.Maximum = 20;
            this.trackBar_d.Name = "trackBar_d";
            this.trackBar_d.Size = new System.Drawing.Size(256, 56);
            this.trackBar_d.TabIndex = 11;
            this.trackBar_d.TickStyle = System.Windows.Forms.TickStyle.Both;
            this.trackBar_d.Value = 10;
            this.trackBar_d.ValueChanged += new System.EventHandler(this.trackBar_d_ValueChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(1, 215);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(28, 16);
            this.label6.TabIndex = 10;
            this.label6.Text = "d = ";
            // 
            // trackBar_F
            // 
            this.trackBar_F.LargeChange = 2;
            this.trackBar_F.Location = new System.Drawing.Point(26, 94);
            this.trackBar_F.Maximum = 40;
            this.trackBar_F.Name = "trackBar_F";
            this.trackBar_F.Size = new System.Drawing.Size(256, 56);
            this.trackBar_F.TabIndex = 7;
            this.trackBar_F.TickStyle = System.Windows.Forms.TickStyle.Both;
            this.trackBar_F.Value = 5;
            this.trackBar_F.ValueChanged += new System.EventHandler(this.trackBar_F_ValueChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(8, 283);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(30, 16);
            this.label1.TabIndex = 1;
            this.label1.Text = "N = ";
            // 
            // trackBar_fo
            // 
            this.trackBar_fo.LargeChange = 2;
            this.trackBar_fo.Location = new System.Drawing.Point(26, 156);
            this.trackBar_fo.Maximum = 20;
            this.trackBar_fo.Name = "trackBar_fo";
            this.trackBar_fo.Size = new System.Drawing.Size(256, 56);
            this.trackBar_fo.TabIndex = 6;
            this.trackBar_fo.TickStyle = System.Windows.Forms.TickStyle.Both;
            this.trackBar_fo.Value = 5;
            this.trackBar_fo.ValueChanged += new System.EventHandler(this.trackBar_fo_ValueChanged);
            // 
            // trackBar_A
            // 
            this.trackBar_A.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.trackBar_A.LargeChange = 2;
            this.trackBar_A.Location = new System.Drawing.Point(26, 37);
            this.trackBar_A.Name = "trackBar_A";
            this.trackBar_A.Size = new System.Drawing.Size(256, 56);
            this.trackBar_A.TabIndex = 0;
            this.trackBar_A.TickStyle = System.Windows.Forms.TickStyle.Both;
            this.trackBar_A.Value = 5;
            this.trackBar_A.ValueChanged += new System.EventHandler(this.trackBar_A_ValueChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(1, 156);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(23, 16);
            this.label5.TabIndex = 4;
            this.label5.Text = "f = ";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(1, 96);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(28, 16);
            this.label4.TabIndex = 3;
            this.label4.Text = "F = ";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(-3, 37);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(29, 16);
            this.label2.TabIndex = 0;
            this.label2.Text = "A = ";
            // 
            // tabPage2
            // 
            this.tabPage2.Location = new System.Drawing.Point(4, 25);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(1114, 523);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Polyharmonic";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1125, 554);
            this.Controls.Add(this.tabControl_graphic);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "ЦОС1";
            this.tabControl_graphic.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.chart_sinus)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar_d)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar_F)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar_fo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar_A)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl_graphic;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.DataVisualization.Charting.Chart chart_sinus;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.CheckBox checkBox_rectangle;
        private System.Windows.Forms.CheckBox checkBox_triangle;
        private System.Windows.Forms.CheckBox checkBox_sinus;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox textBox_N;
        private System.Windows.Forms.TrackBar trackBar_d;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TrackBar trackBar_F;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TrackBar trackBar_fo;
        private System.Windows.Forms.TrackBar trackBar_A;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TabPage tabPage2;
    }
}

