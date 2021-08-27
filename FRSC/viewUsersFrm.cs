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
    public partial class viewUsersFrm : Form
    {
        private MySqlConnection conn = null;
        private MySqlDataAdapter sqlDataAdapter = null;
        private MySqlCommandBuilder sqlCommandBuilder = null;
        private DataTable dataTable = null;
        private BindingSource bindingSource = null;


        public viewUsersFrm()
        {
            InitializeComponent();
        }

        private void viewUsersFrm_Load(object sender, EventArgs e)
        {
            this.MaximizeBox = false;

            dataGridView1.AllowUserToAddRows = false;

            conn = new MySqlConnection(global.connectionString);
            string query = "Select username AS Username, lname AS 'Last name', fname AS 'First name', phone AS Phone From user";

            conn.Open();

            sqlDataAdapter = new MySqlDataAdapter(query, conn);
            sqlCommandBuilder = new MySqlCommandBuilder(sqlDataAdapter);

            dataTable = new DataTable();
            sqlDataAdapter.Fill(dataTable);
            bindingSource = new BindingSource();
            bindingSource.DataSource = dataTable;

            dataGridView1.DataSource = bindingSource;
        }


        private void refreshGrid()
        {
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.ReadOnly = true;

            conn = new MySqlConnection(global.connectionString);
            string query = "Select username AS Username, lname AS 'Last name', fname AS 'First name', phone AS Phone From user";

            conn.Open();

            sqlDataAdapter = new MySqlDataAdapter(query, conn);
            sqlCommandBuilder = new MySqlCommandBuilder(sqlDataAdapter);

            dataTable = new DataTable();
            sqlDataAdapter.Fill(dataTable);
            bindingSource = new BindingSource();
            bindingSource.DataSource = dataTable;

            dataGridView1.DataSource = bindingSource;
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in dataGridView1.SelectedRows)
            {
                usernameTxt.Text = row.Cells[0].Value.ToString();

                lnameTxt.Text = row.Cells[1].Value.ToString();
                fnameTxt.Text = row.Cells[2].Value.ToString();
                phoneTxt.Text = row.Cells[3].Value.ToString();
            }
        }

        private void fnameTxt_TextChanged(object sender, EventArgs e)
        {
            if (!Regex.IsMatch(fnameTxt.Text, @"^[a-zA-Z]{3,30}$"))
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

        private void phoneTxt_TextChanged(object sender, EventArgs e)
        {
            if (!Regex.IsMatch(phoneTxt.Text, @"^[0][7-9][0-9]{9}$"))
                phoneTxt.BackColor = global.inputBad;
            else
                phoneTxt.BackColor = global.inputGood;
        }

        private void updateBtn_Click(object sender, EventArgs e)
        {
            if (!Regex.IsMatch(fnameTxt.Text, @"^[a-zA-Z]{3,30}$"))
                fnameTxt.BackColor = global.inputBad;

            if (!Regex.IsMatch(lnameTxt.Text, @"^[a-zA-Z]{3,30}$"))
                lnameTxt.BackColor = global.inputBad;

            if (!Regex.IsMatch(phoneTxt.Text, @"^[0][7-9][0-9]{9}$"))
                phoneTxt.BackColor = global.inputBad;

            if (!Regex.IsMatch(fnameTxt.Text, @"^[a-zA-Z]{3,30}$")
                || !Regex.IsMatch(lnameTxt.Text, @"^[a-zA-Z]{3,30}$")
                || !Regex.IsMatch(phoneTxt.Text, @"^[0][7-9][0-9]{9}$"))
                return;

            string query = "UPDATE user SET fname=?fname, lname=?lname, phone=?phone WHERE username=?username";

            MySqlConnection dbCon = new MySqlConnection(global.connectionString);
            MySqlCommand cmd = new MySqlCommand(query, dbCon);

            cmd.Parameters.AddWithValue("?lname", global.upperFirst(lnameTxt.Text.Trim()));
            cmd.Parameters.AddWithValue("?fname", global.upperFirst(fnameTxt.Text.Trim()));
            cmd.Parameters.AddWithValue("?phone", phoneTxt.Text.Trim());
            cmd.Parameters.AddWithValue("?username", usernameTxt.Text.Trim());

            cmd.CommandTimeout = 60;

            try
            {
                dbCon.Open();

                cmd.ExecuteNonQuery();
                MessageBox.Show("Record updated");

                usernameTxt.Text = fnameTxt.Text = lnameTxt.Text = phoneTxt.Text = "";
                fnameTxt.BackColor = lnameTxt.BackColor = phoneTxt.BackColor = global.inputDefault;

                refreshGrid();
                dbCon.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
