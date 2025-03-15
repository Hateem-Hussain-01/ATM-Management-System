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
    public partial class Form2 : Form
    {
        private SqlConnection conn;

        public Form2()
        {
            InitializeComponent();
            InitializeDatabaseConnection(); // Call to establish database connection
        }

        private void InitializeDatabaseConnection()
        {
            conn = new SqlConnection();
            conn.ConnectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=D:\project Database\ATM Mnagement System DBMS.mdf;Integrated Security=True;Connect Timeout=30";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("SELECT AdminPassword FROM admin", conn);
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    string adminPassword = reader["AdminPassword"].ToString();

                    if (textBox2.Text == adminPassword)
                    {
                        Form8 form8 = new Form8();
                        form8.Show();
                        this.Hide();
                        //this.Close();
                    }
                    else
                    {
                        MessageBox.Show("Incorrect password. Please try again.");
                    }
                }
                else
                {
                    MessageBox.Show("Admin password not found.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message);
            }
            finally
            {
                if (conn.State == System.Data.ConnectionState.Open)
                {
                    conn.Close();
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {

            this.Close();
        }
    }
}