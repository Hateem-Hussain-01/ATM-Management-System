using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace ATM_Management_System
{
    public partial class Form4 : Form
    {
        private SqlConnection conn;
        public Form4()
        {
            InitializeComponent();
            InitializeDatabaseConnection();
        }
        private void InitializeDatabaseConnection()
        {
            conn = new SqlConnection();
            conn.ConnectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=D:\project Database\ATM Mnagement System DBMS.mdf;Integrated Security=True;Connect Timeout=30";
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand("SELECT Amount FROM UserDetails WHERE AccountNumber = @AccountNumber", conn);
                cmd.Parameters.AddWithValue("@AccountNumber", textBox1.Text);

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                dataGridView1.DataSource = dt;

                if (dt.Rows.Count == 0)
                {
                    MessageBox.Show("No account found with the given account number.");
                }
            }
            catch (SqlException sqlEx)
            {
                // Handle SQL-specific exceptions
                MessageBox.Show("A database error occurred: " + sqlEx.Message);
            }
            catch (InvalidOperationException invOpEx)
            {
                // Handle invalid operation exceptions, such as issues with the connection state
                MessageBox.Show("An invalid operation occurred: " + invOpEx.Message);
            }
            catch (Exception ex)
            {
                // Handle general exceptions
                MessageBox.Show("An error occurred: " + ex.Message);
            }
            finally
            {
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void linkLabel3_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {

                Form3 form3 = new Form3();
                form3.ShowDialog();
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message);
            }
        }
    }
}
