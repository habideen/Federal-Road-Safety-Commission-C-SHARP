using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using MySql.Data.MySqlClient;

namespace FRSC
{
    public partial class viewOffenceFrm : Form
    {
        private MySqlConnection conn = null;
        private MySqlDataAdapter sqlDataAdapter = null;
        private MySqlCommandBuilder sqlCommandBuilder = null;
        private DataTable dataTable = null;
        private BindingSource bindingSource = null;


        public viewOffenceFrm()
        {
            InitializeComponent();
        }

        private void viewOffenceFrm_Load(object sender, EventArgs e)
        {
            ///Load offence cmb
            ///
            String query = "SELECT code, penalty  FROM penalty ORDER BY penalty ASC";
            MySqlConnection dbCon = new MySqlConnection(global.connectionString);
            MySqlCommand cmd = new MySqlCommand(query, dbCon);

            offenceCmb.Items.Add("");
            try
            {
                dbCon.Open();
                MySqlDataReader dbReader = cmd.ExecuteReader();

                if (dbReader.HasRows)
                {
                    while (dbReader.Read())
                    {
                        offenceCmb.Items.Add(dbReader["penalty"].ToString() + "   #   " + dbReader["code"].ToString());
                    }
                }
                dbCon.Close();
            }
            catch (Exception ex)
            {
                dbCon.Close();
                MessageBox.Show(ex.Message);
            }
            ///end of load offence
            

            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.ReadOnly = true;

            conn = new MySqlConnection(global.connectionString);
            query = "SELECT offence.id AS 'ID', offence.fname AS 'First name', offence.mname AS 'Middle name', " +
                    "offence.lname AS 'Last name', offence.vehicleregno AS 'Vehicle no.', offence.vehiclecolor AS 'Vehicle colour', " +
                    "offence.vehicletype AS 'Vehicle type', offence.vehiclemake AS 'Vehicle make', offence.gifmis AS 'GIFMIS', " +
                    "offence.licence AS 'Licence' , offence.code AS 'Offence code', penalty.penalty AS 'Penalty', offence.price AS 'Price', " + 
                    "offence.payment_description AS 'Payment', offence.registered_by AS 'Created by', " +
                    "offence.created_on AS 'Reg. date' FROM offence " +
                    "INNER JOIN penalty ON offence.code=penalty.code " +
                    "ORDER BY created_on DESC";

            conn.Open();

            sqlDataAdapter = new MySqlDataAdapter(query, conn);
            sqlCommandBuilder = new MySqlCommandBuilder(sqlDataAdapter);

            dataTable = new DataTable();
            sqlDataAdapter.Fill(dataTable);
            bindingSource = new BindingSource();
            bindingSource.DataSource = dataTable;

            dataGridView1.DataSource = bindingSource;
            offenceCountLbl.Text = dataGridView1.Rows.Count.ToString();
        }


        private void refreshGrid()
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
                            "ORDER BY created_on DESC";

            conn.Open();

            sqlDataAdapter = new MySqlDataAdapter(query, conn);
            sqlCommandBuilder = new MySqlCommandBuilder(sqlDataAdapter);

            dataTable = new DataTable();
            sqlDataAdapter.Fill(dataTable);
            bindingSource = new BindingSource();
            bindingSource.DataSource = dataTable;

            dataGridView1.DataSource = bindingSource;
            //penaltyCountLbl.Text = dataGridView1.Rows.Count.ToString();
        }

        private void fnameTxt_TextChanged(object sender, EventArgs e)
        {
            if (!Regex.IsMatch(fnameTxt.Text, @"^[a-zA-Z]{3,30}$"))
                fnameTxt.BackColor = global.inputBad;
            else
                fnameTxt.BackColor = global.inputGood;
        }

        private void mnameTxt_TextChanged(object sender, EventArgs e)
        {
            if (!Regex.IsMatch(fnameTxt.Text, @"^[a-zA-Z]{0,30}$"))
                fnameTxt.BackColor = global.inputBad;
            else
                fnameTxt.BackColor = global.inputGood;
        }

        private void lnameTxt_TextChanged(object sender, EventArgs e)
        {
            if (!Regex.IsMatch(lnameTxt.Text, @"^[a-zA-Z]{3,30}$"))
                lnameTxt.BackColor = global.inputBad;
            else
                lnameTxt.BackColor = global.inputGood;
        }

        private void gifmisTxt_TextChanged(object sender, EventArgs e)
        {
            if (!Regex.IsMatch(gifmisTxt.Text, @"^[a-zA-Z0-9 ]{0,20}$"))
                gifmisTxt.BackColor = global.inputBad;
            else
                gifmisTxt.BackColor = global.inputGood;
        }

        private void licencePlateTxt_TextChanged(object sender, EventArgs e)
        {
            if (!Regex.IsMatch(licencePlateTxt.Text, @"^[a-zA-Z0-9]{3,20}$"))
                licencePlateTxt.BackColor = global.inputBad;
            else
                licencePlateTxt.BackColor = global.inputGood;
        }

        private void vehicleRegNoTxt_TextChanged(object sender, EventArgs e)
        {
            if (!Regex.IsMatch(vehicleRegNoTxt.Text, @"^[a-zA-Z0-9]{3,20}$"))
                vehicleRegNoTxt.BackColor = global.inputBad;
            else
                vehicleRegNoTxt.BackColor = global.inputGood;
        }

        private void vehicleColorTxt_TextChanged(object sender, EventArgs e)
        {
            if (!Regex.IsMatch(vehicleColorTxt.Text, @"^[a-zA-Z \-]{3,20}$"))
                vehicleColorTxt.BackColor = global.inputBad;
            else
                vehicleColorTxt.BackColor = global.inputGood;
        }

        private void vehicleTypeTxt_TextChanged(object sender, EventArgs e)
        {
            if (!Regex.IsMatch(vehicleTypeTxt.Text, @"^[a-zA-Z 0-9]{3,30}$"))
                vehicleTypeTxt.BackColor = global.inputBad;
            else
                vehicleTypeTxt.BackColor = global.inputGood;
        }

        private void vehicleMakeTxt_TextChanged(object sender, EventArgs e)
        {
            if (!Regex.IsMatch(vehicleMakeTxt.Text, @"^[a-zA-Z 0-9]{3,30}$"))
                vehicleMakeTxt.BackColor = global.inputBad;
            else
                vehicleMakeTxt.BackColor = global.inputGood;
        }

        private void offenceCmb_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!offenceCmb.Items.Contains(offenceCmb.Text) || offenceCmb.Text == "")
                return;

            String code = offenceCmb.Text.Split('#')[1].Trim();


            ///get offence price
            ///
            String query = "SELECT price  FROM penalty WHERE code=?code";
            MySqlConnection dbCon = new MySqlConnection(global.connectionString);
            MySqlCommand cmd = new MySqlCommand(query, dbCon);

            cmd.Parameters.AddWithValue("?code", code);

            try
            {
                dbCon.Open();
                MySqlDataReader dbReader = cmd.ExecuteReader();

                if (dbReader.HasRows)
                {
                    if (dbReader.Read())
                        offencePriceTxt.Text = dbReader["price"].ToString();
                }
                dbCon.Close();
            }
            catch (Exception ex)
            {
                dbCon.Close();
                MessageBox.Show(ex.Message);
            }
            ///end of load offence
        }

        private void paymentTxt_TextChanged(object sender, EventArgs e)
        {
            if (!Regex.IsMatch(paymentTxt.Text, @"^[a-zA-Z0-9 \-,.)(]{3,100}$"))
                paymentTxt.BackColor = global.inputBad;
            else
                paymentTxt.BackColor = global.inputGood;
        }

        private void saveRecordBtn_Click(object sender, EventArgs e)
        {
            licencePlateTxt_TextChanged(null, EventArgs.Empty);
            gifmisTxt_TextChanged(null, EventArgs.Empty);
            vehicleRegNoTxt_TextChanged(null, EventArgs.Empty);
            vehicleColorTxt_TextChanged(null, EventArgs.Empty);
            vehicleTypeTxt_TextChanged(null, EventArgs.Empty);
            vehicleMakeTxt_TextChanged(null, EventArgs.Empty);
            paymentTxt_TextChanged(null, EventArgs.Empty);
            fnameTxt_TextChanged(null, EventArgs.Empty);
            mnameTxt_TextChanged(null, EventArgs.Empty);
            lnameTxt_TextChanged(null, EventArgs.Empty);

            if (!Regex.IsMatch(fnameTxt.Text, @"^[a-zA-Z]{3,30}$")
                || !Regex.IsMatch(mnameTxt.Text, @"^[a-zA-Z]{0,30}$")
                || !Regex.IsMatch(lnameTxt.Text, @"^[a-zA-Z]{3,30}$")
                || !Regex.IsMatch(licencePlateTxt.Text, @"^[a-zA-Z0-9]{3,20}$")
                || !Regex.IsMatch(gifmisTxt.Text, @"^[a-zA-Z0-9 ]{0,20}$")
                || !Regex.IsMatch(vehicleRegNoTxt.Text, @"^[a-zA-Z0-9]{3,20}$")
                || !Regex.IsMatch(vehicleColorTxt.Text, @"^[a-zA-Z \-]{3,20}$")
                || !Regex.IsMatch(vehicleTypeTxt.Text, @"^[a-zA-Z 0-9]{3,30}$")
                || !Regex.IsMatch(vehicleMakeTxt.Text, @"^[a-zA-Z 0-9]{3,30}$")
                || !Regex.IsMatch(paymentTxt.Text, @"^[a-zA-Z0-9 \-,.)(]{3,100}$")
                || offenceCmb.Text == "")
                return;

            String code = offenceCmb.Text.Split('#')[1].Trim();

            String query = "UPDATE offence SET " +
                            "fname=?fname, mname=?mname, lname=?lname, vehicleregno=?vehicleregno, " +
                            "vehiclecolor=?vehiclecolor, vehicletype=?vehicletype, vehiclemake=?vehiclemake, " +
                            "gifmis=?gifmis, licence=?licence, code=?code, price=?price, payment_description=?payment_description " +
                            "WHERE id=?editID";
            MySqlConnection dbCon = new MySqlConnection(global.connectionString);
            MySqlCommand cmd = new MySqlCommand(query, dbCon);

            cmd.Parameters.AddWithValue("?fname", global.upperFirst(fnameTxt.Text.Trim()));
            cmd.Parameters.AddWithValue("?mname", global.upperFirst(mnameTxt.Text.Trim()));
            cmd.Parameters.AddWithValue("?lname", global.upperFirst(lnameTxt.Text.Trim()));
            cmd.Parameters.AddWithValue("?vehicleregno", vehicleRegNoTxt.Text.Trim().ToUpper());
            cmd.Parameters.AddWithValue("?vehiclecolor", vehicleColorTxt.Text.Trim().ToUpper());
            cmd.Parameters.AddWithValue("?vehicletype", vehicleTypeTxt.Text.Trim().ToUpper());
            cmd.Parameters.AddWithValue("?vehiclemake", vehicleMakeTxt.Text.Trim().ToUpper());
            cmd.Parameters.AddWithValue("?gifmis", gifmisTxt.Text.Trim().ToUpper());
            cmd.Parameters.AddWithValue("?licence", licencePlateTxt.Text.Trim().ToUpper());
            cmd.Parameters.AddWithValue("?code", code);
            cmd.Parameters.AddWithValue("?price", offencePriceTxt.Text.Trim().ToUpper());
            cmd.Parameters.AddWithValue("?payment_description", paymentTxt.Text.Trim());
            cmd.Parameters.AddWithValue("?editID", offenceIDTxt.Text);

            try
            {
                dbCon.Open();
                cmd.ExecuteNonQuery();
                dbCon.Close();

                refreshGrid();

                MessageBox.Show("Offence updated");

                offenceIDTxt.Text = offenceCmb.Text = offencePriceTxt.Text = code = licencePlateTxt.Text = gifmisTxt.Text = vehicleMakeTxt.Text = vehicleTypeTxt.Text = vehicleColorTxt.Text = vehicleRegNoTxt.Text = fnameTxt.Text = mnameTxt.Text = lnameTxt.Text = offencePriceTxt.Text = paymentTxt.Text = offencePriceTxt.Text = "";
                offenceCmb.SelectedIndex = 0;
                paymentTxt.BackColor = offencePriceTxt.BackColor = licencePlateTxt.BackColor = gifmisTxt.BackColor = vehicleMakeTxt.BackColor = vehicleTypeTxt.BackColor = vehicleColorTxt.BackColor = vehicleRegNoTxt.BackColor = fnameTxt.BackColor = mnameTxt.BackColor = lnameTxt.BackColor = offencePriceTxt.BackColor = global.inputDefault;
            }
            catch (Exception ex)
            {
                dbCon.Close();
                MessageBox.Show(ex.Message);
            }
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in dataGridView1.SelectedRows)
            {
                offenceIDTxt.Text = row.Cells[0].Value.ToString();

                fnameTxt.Text = row.Cells[1].Value.ToString();
                mnameTxt.Text = row.Cells[2].Value.ToString();
                lnameTxt.Text = row.Cells[3].Value.ToString();
                vehicleRegNoTxt.Text = row.Cells[4].Value.ToString();
                vehicleColorTxt.Text = row.Cells[5].Value.ToString();
                vehicleTypeTxt.Text = row.Cells[6].Value.ToString();
                vehicleMakeTxt.Text = row.Cells[7].Value.ToString();
                gifmisTxt.Text = row.Cells[8].Value.ToString();
                licencePlateTxt.Text = row.Cells[9].Value.ToString();
                offenceCmb.Text = row.Cells[11].Value.ToString() + "  #  " + row.Cells[10].Value.ToString();
                offencePriceTxt.Text = row.Cells[12].Value.ToString();
                paymentTxt.Text = row.Cells[13].Value.ToString();

            }
        }

        private void licenceSearchBtn_Click(object sender, EventArgs e)
        {
            if (!Regex.IsMatch(licenceSearchTxt.Text, @"^[a-zA-Z0-9]{3,20}$"))
            {
                MessageBox.Show("Invalid entry");
                return;
            }


            conn = new MySqlConnection(global.connectionString);
            String query = "SELECT offence.id AS 'ID', offence.fname AS 'First name', offence.mname AS 'Middle name', " +
                            "offence.lname AS 'Last name', offence.vehicleregno AS 'Vehicle no.', offence.vehiclecolor AS 'Vehicle colour', " +
                            "offence.vehicletype AS 'Vehicle type', offence.vehiclemake AS 'Vehicle make', offence.gifmis AS 'GIFMIS', " +
                            "offence.licence AS 'Licence' , offence.code AS 'Offence code', penalty.penalty AS 'Penalty', offence.price AS 'Price', " +
                            "offence.payment_description AS 'Payment', offence.registered_by AS 'Created by', " +
                            "offence.created_on AS 'Reg. date' FROM offence " +
                            "INNER JOIN penalty ON offence.code=penalty.code " +
                            "WHERE offence.licence=?licence ORDER BY created_on DESC";

            conn.Open();

            sqlDataAdapter = new MySqlDataAdapter(query, conn);
            sqlDataAdapter.SelectCommand.Parameters.AddWithValue("?licence", licenceSearchTxt.Text.Trim());
            sqlCommandBuilder = new MySqlCommandBuilder(sqlDataAdapter);

            dataTable = new DataTable();
            sqlDataAdapter.Fill(dataTable);
            bindingSource = new BindingSource();
            bindingSource.DataSource = dataTable;

            dataGridView1.DataSource = bindingSource;
            offenceCountLbl.Text = dataGridView1.Rows.Count.ToString();
        }

        private void vehicleRegNoSearchBtn_Click(object sender, EventArgs e)
        {
            if (!Regex.IsMatch(vehicleRegNoSearchTxt.Text, @"^[a-zA-Z0-9]{3,20}$"))
            {
                MessageBox.Show("Invalid entry");
                return;
            }


            conn = new MySqlConnection(global.connectionString);
            String query = "SELECT offence.id AS 'ID', offence.fname AS 'First name', offence.mname AS 'Middle name', " +
                            "offence.lname AS 'Last name', offence.vehicleregno AS 'Vehicle no.', offence.vehiclecolor AS 'Vehicle colour', " +
                            "offence.vehicletype AS 'Vehicle type', offence.vehiclemake AS 'Vehicle make', offence.gifmis AS 'GIFMIS', " +
                            "offence.licence AS 'Licence' , offence.code AS 'Offence code', penalty.penalty AS 'Penalty', offence.price AS 'Price', " +
                            "offence.payment_description AS 'Payment', offence.registered_by AS 'Created by', " +
                            "offence.created_on AS 'Reg. date' FROM offence " +
                            "INNER JOIN penalty ON offence.code=penalty.code " +
                            "WHERE offence.vehicleregno=?vehicleregno ORDER BY created_on DESC";

            conn.Open();

            sqlDataAdapter = new MySqlDataAdapter(query, conn);
            sqlDataAdapter.SelectCommand.Parameters.AddWithValue("?vehicleregno", vehicleRegNoSearchTxt.Text.Trim());
            sqlCommandBuilder = new MySqlCommandBuilder(sqlDataAdapter);

            dataTable = new DataTable();
            sqlDataAdapter.Fill(dataTable);
            bindingSource = new BindingSource();
            bindingSource.DataSource = dataTable;

            dataGridView1.DataSource = bindingSource;
            offenceCountLbl.Text = dataGridView1.Rows.Count.ToString();
        }

        private void dateSearchBtn_Click(object sender, EventArgs e)
        {
            DateTime _from = DateTime.Parse(fromDtp.Text);
            string from = _from.ToString("yyyy-MM-dd") + " 00:00:01";

            DateTime _to = DateTime.Parse(toDtp.Text);
            string to = _to.ToString("yyyy-MM-dd") + " 23:59:59";


            conn = new MySqlConnection(global.connectionString);
            String query = "SELECT offence.id AS 'ID', offence.fname AS 'First name', offence.mname AS 'Middle name', " +
                            "offence.lname AS 'Last name', offence.vehicleregno AS 'Vehicle no.', offence.vehiclecolor AS 'Vehicle colour', " +
                            "offence.vehicletype AS 'Vehicle type', offence.vehiclemake AS 'Vehicle make', offence.gifmis AS 'GIFMIS', " +
                            "offence.licence AS 'Licence' , offence.code AS 'Offence code', penalty.penalty AS 'Penalty', offence.price AS 'Price', " +
                            "offence.payment_description AS 'Payment', offence.registered_by AS 'Created by', " +
                            "offence.created_on AS 'Reg. date' FROM offence " +
                            "INNER JOIN penalty ON offence.code=penalty.code " +
                            "WHERE offence.created_on>=?from AND offence.created_on<=?to ORDER BY created_on DESC";

            conn.Open();

            sqlDataAdapter = new MySqlDataAdapter(query, conn);
            sqlDataAdapter.SelectCommand.Parameters.AddWithValue("?from", from);
            sqlDataAdapter.SelectCommand.Parameters.AddWithValue("?to", to);
            sqlCommandBuilder = new MySqlCommandBuilder(sqlDataAdapter);

            dataTable = new DataTable();
            sqlDataAdapter.Fill(dataTable);
            bindingSource = new BindingSource();
            bindingSource.DataSource = dataTable;

            dataGridView1.DataSource = bindingSource;
            offenceCountLbl.Text = dataGridView1.Rows.Count.ToString();
        }

        private void viewAllBtn_Click(object sender, EventArgs e)
        {
            viewOffenceFrm_Load(null, EventArgs.Empty);
        }
    }
}
