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

namespace FRSC
{
    public partial class dashboardFrm : Form
    {
        private MySqlConnection conn = null;
        private MySqlDataAdapter sqlDataAdapter = null;
        private MySqlCommandBuilder sqlCommandBuilder = null;
        private DataTable dataTable = null;
        private BindingSource bindingSource = null;


        public dashboardFrm()
        {
            InitializeComponent();
        }

        private void addCaseFrm_Click(object sender, EventArgs e)
        {
            if (global.activeForm == "addOffenceFrm")
                return;

            if (Application.OpenForms.OfType<addOffenceFrm>().Count() == 1)
                Application.OpenForms.OfType<addOffenceFrm>().First().Close();

            addOffenceFrm dashboard = new addOffenceFrm();
            dashboard.MdiParent = this.MdiParent;
            dashboard.FormBorderStyle = FormBorderStyle.None;
            dashboard.Dock = DockStyle.Fill;
            dashboard.ControlBox = false;
            dashboard.Show();

            global.activeForm = "addOffenceFrm";
        }

        private void dashboardFrm_Load(object sender, EventArgs e)
        {
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.ReadOnly = true;

            conn = new MySqlConnection(global.connectionString);
            String query = "SELECT offence.id AS 'ID', offence.fname AS 'First name', offence.mname AS 'Middle name', " +
                            "offence.lname AS 'Last name', offence.vehicleregno AS 'Vehicle no.', offence.vehiclecolor AS 'Vehicle colour', " +
                            "offence.vehicletype AS 'Vehicle type', offence.vehiclemake AS 'Vehicle make', offence.gifmis AS 'GIFMIS', " +
                            "offence.licence AS 'Licence' , offence.code AS 'Offence code', penalty.penalty AS 'Penalty', offence.price AS 'Price', " +
                            "offence.payment_description AS 'Payment', offence.registered_by AS 'Created by', " +
                            "offence.created_on AS 'Reg. date' FROM offence " +
                            "INNER JOIN penalty ON offence.code=penalty.code " +
                            "WHERE MONTH(created_on) = MONTH(CURRENT_DATE()) ORDER BY created_on DESC";

            conn.Open();

            sqlDataAdapter = new MySqlDataAdapter(query, conn);
            sqlCommandBuilder = new MySqlCommandBuilder(sqlDataAdapter);

            dataTable = new DataTable();
            sqlDataAdapter.Fill(dataTable);
            bindingSource = new BindingSource();
            bindingSource.DataSource = dataTable;

            dataGridView1.DataSource = bindingSource;
            offenceCountLbl.Text = dataGridView1.Rows.Count.ToString();


            ///get this month amount
            ///
            query = "SELECT SUM(price) AS price, COUNT(id) AS fig FROM offence WHERE MONTH(created_on) = MONTH(CURRENT_DATE())";
            MySqlConnection dbCon = new MySqlConnection(global.connectionString);
            MySqlCommand cmd = new MySqlCommand(query, dbCon);

            try
            {
                dbCon.Open();
                MySqlDataReader dbReader = cmd.ExecuteReader();

                if (dbReader.HasRows)
                {//Amount: 486,000 NGN
                    if (dbReader.Read())
                    {
                        thisMonthAmountLbl.Text = "Amount: " + String.Format("{0:n0}", int.Parse(dbReader["price"].ToString())) + " NGN";
                        monthCountLbl.Text = "Number: " + dbReader["fig"].ToString();
                    }
                    dbCon.Close();
                }
                dbCon.Close();
            }
            catch (Exception ex)
            {
                dbCon.Close();
                MessageBox.Show(ex.Message);
            }
            ///end of get this month amount

            DateTime dt = DateTime.UtcNow.Date;
            String start = dt.ToString("yyyy-MM-dd") + " 23:59:59";
            String end = dt.ToString("yyyy-MM-dd") + " 00:00:00";
            ///get today amount
            ///
            query = "SELECT SUM(price) AS price, COUNT(id) AS fig FROM offence WHERE created_on>=?start AND created_on<=?end";
            dbCon = new MySqlConnection(global.connectionString);
            cmd = new MySqlCommand(query, dbCon);

            cmd.Parameters.AddWithValue("?start", dt.ToString("yyyy-MM-dd") + " 00:00:00");
            cmd.Parameters.AddWithValue("?end", dt.ToString("yyyy-MM-dd") + " 23:59:59");

            try
            {
                dbCon.Open();
                MySqlDataReader dbReader = cmd.ExecuteReader();

                if (dbReader.HasRows)
                {//Amount: 486,000 NGN
                    if (dbReader.Read())
                    {
                        todayAmountLbl.Text = "Amount: " + String.Format("{0:n0}", int.Parse(dbReader["price"].ToString())) + " NGN";
                        todayCountLbl.Text = "Number: " + dbReader["fig"].ToString();
                    }
                    dbCon.Close();
                }
                dbCon.Close();
            }
            catch (Exception ex)
            {
                dbCon.Close();
                MessageBox.Show(ex.Message);
            }
            ///end of get today amount

            
            ///get all amount
            ///
            query = "SELECT SUM(price) AS price, COUNT(id) AS fig FROM offence";
            dbCon = new MySqlConnection(global.connectionString);
            cmd = new MySqlCommand(query, dbCon);

            try
            {
                dbCon.Open();
                MySqlDataReader dbReader = cmd.ExecuteReader();

                if (dbReader.HasRows)
                {//Amount: 486,000 NGN
                    if (dbReader.Read())
                    {
                        allAmountLbl.Text = "Amount: " + String.Format("{0:n0}", int.Parse(dbReader["price"].ToString())) + " NGN";
                        allCountLbl.Text = "Number: " + dbReader["fig"].ToString();
                    }
                    dbCon.Close();
                }
                dbCon.Close();
            }
            catch (Exception ex)
            {
                dbCon.Close();
                MessageBox.Show(ex.Message);
            }
            ///end of get all amount
        }
    }
}
