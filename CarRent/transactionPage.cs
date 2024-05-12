using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using System.Configuration;



namespace CarRent
{
    public partial class transactionPage : Form
    {
        private MySqlConnection connection;
        private int pageSize = 10;
        private int currentPage = 1;
        private int limit = 10;
        public transactionPage()
        {
            InitializeComponent();
            DBConnection();
            FetchingData();
        }

        private void DBConnection()
        {

            connection = new MySqlConnection(ConfigurationManager.ConnectionStrings["MyConnectionString"].ConnectionString);

        }

        private void FetchingData()
        {
            try
            {



                //Fetching the data with pagination 
                page_count.Text = currentPage.ToString();
                connection.Open();
                MySqlCommand command = new MySqlCommand($"SELECT tt.invoice_no,tt.total_amount,tt.status,c.car_name,c.plate_no,cu.first_name,cu.last_name,cu.contact_no FROM transaction_table as tt JOIN cars as c ON tt.car_id = c.id JOIN customer as cu ON tt.client_id=cu.id ORDER BY tt.added_at DESC LIMIT {pageSize} OFFSET {(currentPage - 1) * pageSize}", connection);
                MySqlDataAdapter adapter = new MySqlDataAdapter(command);
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);

                int rowCount = dataTable.Rows.Count;
                //iterate sa lahat ng data and pass sa props 
                if (rowCount != 0)
                {
                    flowLayoutPanel1.Controls.Clear();
                    transactionControl[] transaction= new transactionControl[rowCount];
                    limit = rowCount;
                    for (int i = 0; i < rowCount; i++)
                    {

                        transaction[i] = new transactionControl();
                        transaction[i].full_name_method = dataTable.Rows[i]["first_name"].ToString().ToUpper() + " "+ dataTable.Rows[i]["last_name"].ToString().ToUpper();
                        transaction[i].car_name_method = dataTable.Rows[i]["car_name"].ToString().ToUpper();
                        transaction[i].plate_no_method = dataTable.Rows[i]["plate_no"].ToString().ToUpper();
                        transaction[i].price_method = (Double)dataTable.Rows[i]["total_amount"];
                        transaction[i].contact_no_method = dataTable.Rows[i]["contact_no"].ToString();
                        transaction[i].status_method = (int)dataTable.Rows[i]["status"];
                        flowLayoutPanel1.Controls.Add(transaction[i]);
                    }

                }
            }
            catch (Exception ex)
            {

                MessageBox.Show("Database Connection Error: Fetching Cars Data"+ex.Message);
            }
            finally
            {
                connection.Close();
            }

        }
    }
}
