namespace SQLJobMonitor
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
            dataGridView1 = new DataGridView();
            btnRefresh = new Button();
            statusStrip1 = new StatusStrip();
            toolStripStatusLabel1 = new ToolStripStatusLabel();
            toolStripStatusLabelCount = new System.Windows.Forms.ToolStripStatusLabel();
            toolStripStatusLabelSummary = new System.Windows.Forms.ToolStripStatusLabel();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            statusStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // dataGridView1
            // 
            dataGridView1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Location = new Point(18, 99);
            dataGridView1.MultiSelect = false;
            dataGridView1.Name = "dataGridView1";
            dataGridView1.RowHeadersVisible = false;
            dataGridView1.RowHeadersWidth = 51;
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView1.Size = new Size(1108, 472);
            dataGridView1.TabIndex = 0;
            // 
            // btnRefresh
            // 
            btnRefresh.Location = new Point(18, 64);
            btnRefresh.Name = "btnRefresh";
            btnRefresh.Size = new Size(94, 29);
            btnRefresh.TabIndex = 1;
            btnRefresh.Text = "Refresh";
            btnRefresh.UseVisualStyleBackColor = true;
            btnRefresh.Click += btnRefresh_Click;
            // 
            // statusStrip1
            statusStrip1.ImageScalingSize = new Size(20, 20);
            statusStrip1.Items.AddRange(new ToolStripItem[] {
    toolStripStatusLabel1,
    //toolStripStatusLabelCount,
    toolStripStatusLabelSummary
});
            statusStrip1.Location = new Point(0, 586);
            statusStrip1.Name = "statusStrip1";
            statusStrip1.Size = new Size(1138, 26);
            statusStrip1.TabIndex = 2;
            statusStrip1.Text = "statusStrip1";

            // 
            // toolStripStatusLabel1
            // 
            toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            toolStripStatusLabel1.Size = new Size(151, 20);
            toolStripStatusLabel1.Text = "toolStripStatusLabel1";
            toolStripStatusLabel1.Spring = false;
            toolStripStatusLabelCount.Spring = true;
            toolStripStatusLabelSummary.Spring = false; // fills remaining space

            // 

            // toolStripStatusLabelCount
            toolStripStatusLabelCount.Text = "Jobs Loaded: 0";

            // toolStripStatusLabelSummary
            toolStripStatusLabelSummary.Text = "Succeeded: 0 | Failed: 0 | Running: 0 | Retry: 0 | Canceled: 0";


            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1138, 612);
            Controls.Add(statusStrip1);
            Controls.Add(btnRefresh);
            Controls.Add(dataGridView1);
            Name = "Form1";
            Text = "SQL Job Monitor";
            Load += Form1_Load;
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            statusStrip1.ResumeLayout(false);
            statusStrip1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();


            typeof(DataGridView).InvokeMember("DoubleBuffered",
    System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.SetProperty,
    null, dataGridView1, new object[] { true });


        }



        #endregion


        private DataGridView dataGridView1;
        private Button btnRefresh;
        private StatusStrip statusStrip1;
        private ToolStripStatusLabel toolStripStatusLabel1;
        private ToolStripStatusLabel toolStripStatusLabelCount;
        private ToolStripStatusLabel toolStripStatusLabelSummary;
    }
}
