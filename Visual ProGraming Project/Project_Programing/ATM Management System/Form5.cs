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
    public partial class Form5 : Form
    {
        private SqlConnection conn;

        public Form5()
        {
            InitializeComponent();
            InitializeDatabaseConnection();
        }

        private void InitializeDatabaseConnection()
        {
            conn = new SqlConnection();
            conn.ConnectionString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=\"D:\\project Database\\ATM Mnagement System DBMS.mdf\";Integrated Security=True;Connect Timeout=30";
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string accountNumber = textBox4.Text; // TextBox for account number
            string oldPassword = textBox1.Text;  // TextBox for old password
            string newPassword = textBox2.Text;  // TextBox for new password
            string confirmPassword = textBox3.Text;  // TextBox for confirm new password

            if (newPassword != confirmPassword)
            {
                MessageBox.Show("New password and confirm password do not match.");
                return;
            }

            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("SELECT Password FROM UserDetails WHERE AccountNumber = @AccountNumber", conn);
                cmd.Parameters.AddWithValue("@AccountNumber", accountNumber);
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    string currentPassword = reader["Password"].ToString();

                    if (oldPassword == currentPassword)
                    {
                        reader.Close(); // Close the reader before executing another command

                        SqlCommand updateCmd = new SqlCommand("UPDATE UserDetails SET Password = @NewPassword WHERE AccountNumber = @AccountNumber", conn);
                        updateCmd.Parameters.AddWithValue("@NewPassword", newPassword);
                        updateCmd.Parameters.AddWithValue("@AccountNumber", accountNumber);
                        int rowsAffected = updateCmd.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Password updated successfully!");
                            //this.Close();
                        }
                        else
                        {
                            MessageBox.Show("Error updating password.");
                        }
                    }
                    else
                    {
                        MessageBox.Show("Old password is incorrect.");
                    }
                }
                else
                {
                    MessageBox.Show("User not found.");
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

        private void linkLabel3_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {

                Form1 form1 = new Form1();
                form1.ShowDialog();
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message);
            }
        }
    }
}