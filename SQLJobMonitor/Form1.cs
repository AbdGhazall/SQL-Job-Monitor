using System;
using System.Data;
using Microsoft.Data.SqlClient; // modern SqlClient
using System.Windows.Forms;
using System.Drawing; // for colors

namespace SQLJobMonitor
{
    public partial class Form1 : Form
    {
        // ?? Your working connection string
        private string connectionString = "Data Source=.;Database=msdb;User Id=sa;Password=123456;Integrated Security=true;TrustServerCertificate=True;";

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            LoadJobData();

            // Left padding for the first label
            toolStripStatusLabel1.Padding = new Padding(28, 0, 0, 0);
            toolStripStatusLabel1.Spring = false; // stay left

            // The summary label expands and pushes itself to the right
            toolStripStatusLabelSummary.Spring = true;
            toolStripStatusLabelSummary.TextAlign = ContentAlignment.MiddleRight;

            // Center all cells text
            dataGridView1.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            // Already centered column headers
            dataGridView1.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;


            // Hook row coloring event
            dataGridView1.RowPrePaint += dataGridView1_RowPrePaint;

            // Make column headers bold
            //dataGridView1.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Bold);

            // Style column headers
            dataGridView1.EnableHeadersVisualStyles = false; // allow custom styling
            dataGridView1.ColumnHeadersDefaultCellStyle.BackColor = Color.Yellow; // yellow/golden background
            dataGridView1.ColumnHeadersDefaultCellStyle.ForeColor = Color.Black; // black text
            dataGridView1.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            //dataGridView1.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;


        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            LoadJobData();
        }

        private void LoadJobData()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    string query = @"
    SELECT  
        j.name AS JobName,
        msdb.dbo.agent_datetime(h.run_date, h.run_time) AS LastRunDate,
        CASE 
            WHEN h.run_status = 2 THEN 'Retry'
            WHEN h.run_status = 3 THEN 'Canceled'
            WHEN ja.start_execution_date IS NOT NULL AND ja.stop_execution_date IS NULL THEN 'Running'
            WHEN h.run_status = 0 THEN 'Failed'
            WHEN h.run_status = 1 THEN 'Succeeded'
            ELSE 'Idle'
        END AS Status,
        js.next_run_date AS NextRunDate,
        h.run_duration AS Duration,
        h.message AS Message
    FROM msdb.dbo.sysjobs j
    LEFT JOIN msdb.dbo.sysjobhistory h 
        ON j.job_id = h.job_id
        AND h.instance_id = (
            SELECT MAX(h2.instance_id) 
            FROM msdb.dbo.sysjobhistory h2 
            WHERE h2.job_id = j.job_id
        )
    LEFT JOIN msdb.dbo.sysjobschedules js 
        ON j.job_id = js.job_id
    LEFT JOIN msdb.dbo.sysjobactivity ja
        ON j.job_id = ja.job_id
        AND ja.session_id = (SELECT MAX(session_id) FROM msdb.dbo.syssessions)
    WHERE j.name <> 'syspolicy_purge_history'   -- ?? Exclude system job
    ORDER BY j.name;";


                    SqlDataAdapter da = new SqlDataAdapter(query, conn);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    dataGridView1.DataSource = dt;
                    dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

                    // ? Clear auto-selected first row
                    dataGridView1.ClearSelection();

                    // ? Update status strip
                    toolStripStatusLabel1.Text = "Last Refreshed: " + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                    toolStripStatusLabelCount.Text = "Jobs Loaded: " + dt.Rows.Count;

                    // ? Count breakdown
                    int success = dt.Select("Status = 'Succeeded'").Length;
                    int fail = dt.Select("Status = 'Failed'").Length;
                    int running = dt.Select("Status = 'Running'").Length;
                    int retry = dt.Select("Status = 'Retry'").Length;
                    int canceled = dt.Select("Status = 'Canceled'").Length;

                    toolStripStatusLabel1.Text = "Last Refreshed: " + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

                    toolStripStatusLabelSummary.Text =
                        $"Jobs: {dt.Rows.Count} | " +
                        $"Succeeded: {success} | Failed: {fail} | Running: {running} | Retry: {retry} | Canceled: {canceled}";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading data: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dataGridView1_RowPrePaint(object sender, DataGridViewRowPrePaintEventArgs e)
        {
            if (dataGridView1.Rows[e.RowIndex].Cells["Status"].Value != null)
            {
                string status = dataGridView1.Rows[e.RowIndex].Cells["Status"].Value.ToString();

                // Reset background first (so previous colors don’t stay)
                dataGridView1.Rows[e.RowIndex].Cells["Status"].Style.BackColor = Color.White;

                switch (status)
                {
                    case "Succeeded":
                        dataGridView1.Rows[e.RowIndex].Cells["Status"].Style.BackColor = Color.LightGreen;
                        break;
                    case "Failed":
                        dataGridView1.Rows[e.RowIndex].Cells["Status"].Style.BackColor = Color.LightCoral;
                        break;
                    case "Running":
                        dataGridView1.Rows[e.RowIndex].Cells["Status"].Style.BackColor = Color.Khaki;
                        break;
                    case "Retry":
                        dataGridView1.Rows[e.RowIndex].Cells["Status"].Style.BackColor = Color.LightBlue;
                        break;
                    case "Canceled":
                        dataGridView1.Rows[e.RowIndex].Cells["Status"].Style.BackColor = Color.LightGray;
                        break;
                    default:
                        dataGridView1.Rows[e.RowIndex].Cells["Status"].Style.BackColor = Color.White;
                        break;
                }
            }
        }

    }
}
