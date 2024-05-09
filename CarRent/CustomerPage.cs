using MySql.Data.MySqlClient;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using System.Configuration;
using System.Data;
using System;

namespace CarRent
{
    public partial class customerPage : Form
    {

        private MySqlConnection connection;
        private int pageSize = 10;
        private int currentPage = 1;
        private int limit = 10;
        public customerPage()
        {
            InitializeComponent();
            DBConnection();
            FetchingData();
        }

        private void DBConnection()
        {

            connection = new MySqlConnection(ConfigurationManager.ConnectionStrings["MyConnectionString"].ConnectionString);

        }



        //Fetchin Data of Customers
        private void FetchingData()
        {
            try
            {



                //Fetching the data with pagination 
                page_count.Text = currentPage.ToString();
                connection.Open();
                MySqlCommand command = new MySqlCommand($"SELECT * FROM customer ORDER BY added_at DESC LIMIT {pageSize} OFFSET {(currentPage - 1) * pageSize}", connection);
                MySqlDataAdapter adapter = new MySqlDataAdapter(command);
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);

                int rowCount = dataTable.Rows.Count;
                //iterate sa lahat ng data and pass sa props 
                if (rowCount != 0)
                {
                    flowLayoutPanel1.Controls.Clear();
                    customerControl[] customer_data = new customerControl[rowCount];
                    limit = rowCount;
                    for (int i = 0; i < rowCount; i++)
                    {

                        customer_data[i] = new customerControl();
                        customer_data[i].first_name_method= dataTable.Rows[i]["first_name"].ToString().ToUpper();
                        customer_data[i].id_method = (int)dataTable.Rows[i]["id"];
                        customer_data[i].last_name_method = dataTable.Rows[i]["last_name"].ToString().ToUpper();
                        customer_data[i].contact_no_method = dataTable.Rows[i]["contact_no"].ToString().ToUpper();
                        customer_data[i].email_method = dataTable.Rows[i]["email"].ToString().ToUpper();
                        customer_data[i].no_rent_method = (int)dataTable.Rows[i]["rent_count"];
                        customer_data[i].added_at_method = dataTable.Rows[i]["added_at"].ToString();
                        flowLayoutPanel1.Controls.Add(customer_data[i]);
                    }

                }
            }
            catch (Exception ex)
            {

                MessageBox.Show("Database Connection Error: Fetching Customer Data" + ex.Message);
            }
            finally
            {
                connection.Close();
            }

        }
    }
}
