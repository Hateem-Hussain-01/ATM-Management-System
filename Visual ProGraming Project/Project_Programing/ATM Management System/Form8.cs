using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ATM_Management_System
{
    public partial class Form8 : Form
    {
        private SqlConnection conn;

        public Form8()
        {
            InitializeComponent();
            InitializeDatabaseConnection();
        }

        private void InitializeDatabaseConnection()
        {
            conn = new SqlConnection();
            conn.ConnectionString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=\"D:\\project Database\\ATM Mnagement System DBMS.mdf\";Integrated Security=True;Connect Timeout=30";
        }

        private void button13_Click(object sender, EventArgs e)
        {
            try
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand("INSERT INTO UserDetails (AccountNumber, UserName, Password, Amount, AccountType) VALUES (@AccountNumber, @UserName, @Password, @Amount, @AccountType)", conn))
                {
                    cmd.Parameters.AddWithValue("@AccountNumber", textBox7.Text);
                    cmd.Parameters.AddWithValue("@UserName", textBox6.Text); // Corrected parameter name
                    cmd.Parameters.AddWithValue("@Password", textBox5.Text);
                    cmd.Parameters.AddWithValue("@Amount", int.Parse(textBox8.Text));
                    cmd.Parameters.AddWithValue("@AccountType", comboBox3.Text);

                    cmd.ExecuteNonQuery();
                }
                MessageBox.Show("Information Saved Successfully");
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

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
        }

        private void button11_Click(object sender, EventArgs e)
        {
            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("Select * From UserDetails", conn);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                dataGridView1.DataSource = dt;
            }
            catch (Exception ex)
            {
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

        private void button14_Click(object sender, EventArgs e)
        {
            try
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand("SELECT * FROM UserDetails WHERE AccountNumber = @AccountNumber", conn);
                cmd.Parameters.AddWithValue("@AccountNumber", textBox7.Text);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                dataGridView1.DataSource = dt;

                // Optional: Check if any rows were returned
                if (dt.Rows.Count == 0)
                {
                    MessageBox.Show("No records found for the provided Information.");
                }
            }
            catch (Exception ex)
            {
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

        private void button10_Click(object sender, EventArgs e)
        {
            try
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand("Delete UserDetails  where AccountNumber=@AccountNumber", conn);
                cmd.Parameters.AddWithValue("@AccountNumber", textBox7.Text);


                cmd.ExecuteNonQuery();

                //conn.Close();
                MessageBox.Show("Information Deleted Successfully");
            }
            catch (Exception ex)
            {
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

        private void button15_Click(object sender, EventArgs e)
        {
            try
            {
                conn.Open();
                string updateQuery = "UPDATE UserDetails set UserName = @UserName,password= @password, Amount = @Amount WHERE AccountNumber = @AccountNumber";

                SqlCommand cmd = new SqlCommand(updateQuery, conn);

                cmd.Parameters.AddWithValue("@UserName", textBox6.Text);
                cmd.Parameters.AddWithValue("@Amount", int.Parse(textBox8.Text));
                cmd.Parameters.AddWithValue("@AccountNumber", textBox7.Text);
                cmd.Parameters.AddWithValue("Password", textBox5.Text);

                int rowsAffected = cmd.ExecuteNonQuery();

                if (rowsAffected > 0)
                {
                    MessageBox.Show("Update successful!");
                }
                else
                {
                    MessageBox.Show("No rows were updated.");
                }
            }
            catch (Exception ex)
            {
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

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
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
