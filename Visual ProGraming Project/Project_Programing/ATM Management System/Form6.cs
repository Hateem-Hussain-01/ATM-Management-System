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
    public partial class Form6 : Form
    {
        private SqlConnection conn;

        public Form6()
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
            if (!decimal.TryParse(textBox2.Text, out decimal amountToDeduct))
            {
                MessageBox.Show("Please enter a valid amount.");
                return;
            }

            string accountNumber = textBox1.Text;

            if (string.IsNullOrWhiteSpace(accountNumber))
            {
                MessageBox.Show("Please enter a valid account number.");
                return;
            }

            try
            {
                conn.Open();

                // Retrieve current amount
                SqlCommand getAmountCmd = new SqlCommand("SELECT Amount FROM UserDetails WHERE AccountNumber = @AccountNumber", conn);
                getAmountCmd.Parameters.AddWithValue("@AccountNumber", accountNumber);
                object result = getAmountCmd.ExecuteScalar();

                if (result == null)
                {
                    MessageBox.Show("User not found.");
                    return;
                }

                decimal currentAmount = Convert.ToDecimal(result);

                if (currentAmount < amountToDeduct)
                {
                    MessageBox.Show("Insufficient funds.");
                    return;
                }

                // Deduct amount
                SqlCommand updateAmountCmd = new SqlCommand("UPDATE UserDetails SET Amount = Amount - @AmountToDeduct WHERE AccountNumber = @AccountNumber", conn);
                updateAmountCmd.Parameters.AddWithValue("@AmountToDeduct", amountToDeduct);
                updateAmountCmd.Parameters.AddWithValue("@AccountNumber", accountNumber);

                int rowsAffected = updateAmountCmd.ExecuteNonQuery();

                if (rowsAffected > 0)
                {
                    MessageBox.Show("Amount deducted successfully!");
                }
                else
                {
                    MessageBox.Show("Error deducting amount.");
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

        private void linkLabel3_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {

                Form3 form3 = new Form3();
                form3.ShowDialog();
                this.Hide();
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}