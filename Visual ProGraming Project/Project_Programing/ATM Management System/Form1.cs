
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
    public partial class Form1 : Form
    {
        SqlConnection conn;

        public Form1()
        {
            InitializeComponent();
            InitializeDatabaseConnection(); // Call to establish database connection
        }

        private void InitializeDatabaseConnection()
        {
            conn = new SqlConnection();
            conn.ConnectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=D:\project Database\ATM Mnagement System DBMS.mdf;Integrated Security=True;Connect Timeout=30";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void linkLabel3_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                Form2 form2 = new Form2();
                form2.ShowDialog();
                this.Hide();
                //this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("SELECT * FROM UserDetails WHERE AccountNumber = @AccountNumber AND Password = @Password", conn);

                // Assuming TextBox1 is for AccountNumber and TextBox2 is for Password
                cmd.Parameters.AddWithValue("@AccountNumber", textBox2.Text);
                cmd.Parameters.AddWithValue("@Password", textBox3.Text);

                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    // Credentials are correct, open Form3
                    Form3 form3 = new Form3();
                    form3.Show();
                    this.Hide();
                    //this.Close();
                }
                else
                {
                    // Credentials are incorrect
                    MessageBox.Show("Incorrect username or password. Please try again.");
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

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                Form5 form5 = new Form5();
                form5.ShowDialog();
                this.Hide();

            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message);
            }
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Application.Exit();
        }
    }
}
