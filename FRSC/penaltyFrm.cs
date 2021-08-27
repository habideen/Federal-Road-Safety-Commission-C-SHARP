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
    public partial class penaltyFrm : Form
    {
        private MySqlConnection conn = null;
        private MySqlDataAdapter sqlDataAdapter = null;
        private MySqlCommandBuilder sqlCommandBuilder = null;
        private DataTable dataTable = null;
        private BindingSource bindingSource = null;


        public penaltyFrm()
        {
            InitializeComponent();
        }


        private void penaltyFrm_Load(object sender, EventArgs e)
        {
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.ReadOnly = true;

            conn = new MySqlConnection(global.connectionString);
            string query = "SELECT code AS 'CODE', penalty AS 'Penalty', point AS 'Point', price AS 'Price' FROM penalty";

            conn.Open();

            sqlDataAdapter = new MySqlDataAdapter(query, conn);
            sqlCommandBuilder = new MySqlCommandBuilder(sqlDataAdapter);

            dataTable = new DataTable();
            sqlDataAdapter.Fill(dataTable);
            bindingSource = new BindingSource();
            bindingSource.DataSource = dataTable;

            dataGridView1.DataSource = bindingSource;
            penaltyCountLbl.Text = dataGridView1.Rows.Count.ToString();
        }



        private void refreshGrid()
        {
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.ReadOnly = true;

            conn = new MySqlConnection(global.connectionString);
            string query = "SELECT code AS 'CODE', penalty AS 'Penalty', point AS 'Point', price AS 'Price' FROM penalty";

            conn.Open();

            sqlDataAdapter = new MySqlDataAdapter(query, conn);
            sqlCommandBuilder = new MySqlCommandBuilder(sqlDataAdapter);

            dataTable = new DataTable();
            sqlDataAdapter.Fill(dataTable);
            bindingSource = new BindingSource();
            bindingSource.DataSource = dataTable;

            dataGridView1.DataSource = bindingSource;
            penaltyCountLbl.Text = dataGridView1.Rows.Count.ToString();
        }

        private void codeTxt_TextChanged(object sender, EventArgs e)
        {
            if (!Regex.IsMatch(codeTxt.Text, @"^[a-zA-Z0-9]{3,10}$"))
                codeTxt.BackColor = global.inputBad;
            else
                codeTxt.BackColor = global.inputGood;
        }

        private void priceTxt_TextChanged(object sender, EventArgs e)
        {
            if (!Regex.IsMatch(priceTxt.Text, @"^[1-9][0-9]{2,12}$"))
                priceTxt.BackColor = global.inputBad;
            else
                priceTxt.BackColor = global.inputGood;
        }

        private void penaltyTxt_TextChanged(object sender, EventArgs e)
        {
            if (!Regex.IsMatch(penaltyTxt.Text, @"^[a-zA-Z0-9 -/-,)(]{2,100}$"))
                penaltyTxt.BackColor = global.inputBad;
            else
                penaltyTxt.BackColor = global.inputGood;
        }



        bool checkInput()
        {
            penaltyTxt_TextChanged(null, EventArgs.Empty);
            priceTxt_TextChanged(null, EventArgs.Empty);
            codeTxt_TextChanged(null, EventArgs.Empty);

            if (!Regex.IsMatch(codeTxt.Text, @"^[a-zA-Z0-9]{3,10}$")
                || !Regex.IsMatch(priceTxt.Text, @"^[1-9][0-9]{2,12}$")
                || !Regex.IsMatch(penaltyTxt.Text, @"^[a-zA-Z0-9 -/-,)(]{2,100}$")
                || pointCmb.Text == "")
            {
                if (pointCmb.Text == "")
                    MessageBox.Show("Please select point!");
                return false;
            }

            return true;
        }



        private void registerBtn_Click(object sender, EventArgs e)
        {
            if (!checkInput())
                return;

            ///check if code or penalty already exist
            ///
            String query = "SELECT code, penalty  FROM penalty WHERE code=?code OR  penalty=?penalty";
            MySqlConnection dbCon = new MySqlConnection(global.connectionString);
            MySqlCommand cmd = new MySqlCommand(query, dbCon);

            cmd.Parameters.AddWithValue("?code", codeTxt.Text.Trim());
            cmd.Parameters.AddWithValue("?penalty", penaltyTxt.Text.Trim());
            cmd.CommandTimeout = 60;

            try
            {
                dbCon.Open();
                MySqlDataReader dbReader = cmd.ExecuteReader();

                if (dbReader.HasRows)
                {
                    if (penaltyTxt.Text == dbReader["penalty"].ToString())
                        MessageBox.Show("Penalty already exist!");

                    if (codeTxt.Text == dbReader["code"].ToString())
                        MessageBox.Show("Code already exist!");

                    dbCon.Close();
                    return;
                }
                dbCon.Close();
            }
            catch (Exception ex)
            {
                dbCon.Close(); 
                MessageBox.Show(ex.Message);
            }
            ///end of check if already exist
            

            ///register penalty
            ///
            query = "INSERT INTO penalty(code, penalty, point, price) VALUES (?code, ?penalty, ?point, ?price)";

            cmd = new MySqlCommand(query, dbCon);
            cmd.Parameters.AddWithValue("?code", codeTxt.Text.Trim().ToUpper());
            cmd.Parameters.AddWithValue("?penalty", penaltyTxt.Text.Trim().ToUpper());
            cmd.Parameters.AddWithValue("?point", pointCmb.Text.Trim().ToUpper());
            cmd.Parameters.AddWithValue("?price", priceTxt.Text.Trim().ToUpper());

            cmd.CommandTimeout = 60;

            try
            {
                dbCon.Open();
                cmd.ExecuteNonQuery();
                dbCon.Close();
                
                refreshGrid();

                MessageBox.Show("Penalty registered");

                codeTxt.Text = penaltyTxt.Text = priceTxt.Text = "";
                pointCmb.SelectedIndex = 0;
                codeTxt.BackColor = penaltyTxt.BackColor = priceTxt.BackColor = global.inputDefault;
            }
            catch (Exception ex)
            {
                dbCon.Close();
                MessageBox.Show(ex.Message);
            }
            ///end of registration

        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in dataGridView1.SelectedRows)
            {
                codeTxt.Text = row.Cells[0].Value.ToString();
                editIDTxt.Text = row.Cells[0].Value.ToString();

                penaltyTxt.Text = row.Cells[1].Value.ToString();
                pointCmb.Text = row.Cells[2].Value.ToString();
                priceTxt.Text = row.Cells[3].Value.ToString();

                registerBtn.Visible = false;

                editBtn.Visible = newBtn.Visible = true;
            }
        }



        private void newBtn_Click(object sender, EventArgs e)
        {
            codeTxt.Text = penaltyTxt.Text = priceTxt.Text = "";
            pointCmb.SelectedIndex = 0;
            codeTxt.BackColor = penaltyTxt.BackColor = priceTxt.BackColor = global.inputDefault;

            registerBtn.Visible = true;

            editBtn.Visible = newBtn.Visible = false;
        }



        private void editBtn_Click(object sender, EventArgs e)
        {
            if (!checkInput())
                return;
            
            
            ///check if code or penalty already exist
            ///
            String query;
            MySqlConnection dbCon;
            MySqlCommand cmd;
            
            if (editIDTxt.Text.Trim() != codeTxt.Text.Trim())
            {
                query = "SELECT code, penalty FROM penalty WHERE code=?code";
                dbCon = new MySqlConnection(global.connectionString);
                cmd = new MySqlCommand(query, dbCon);

                cmd.Parameters.AddWithValue("?codeCheck", editIDTxt.Text.Trim());
                cmd.Parameters.AddWithValue("?code", codeTxt.Text.Trim());
                cmd.Parameters.AddWithValue("?penalty", penaltyTxt.Text.Trim());
                cmd.CommandTimeout = 60;

                try
                {
                    dbCon.Open();
                    MySqlDataReader dbReader = cmd.ExecuteReader();

                    if (dbReader.HasRows)
                    {
                        if ( dbReader.Read() )
                        {
                            if (penaltyTxt.Text == dbReader["penalty"].ToString())
                                MessageBox.Show("Penalty already exist!");

                            if (codeTxt.Text == dbReader["code"].ToString())
                                MessageBox.Show("Code already exist!");
                        }

                        dbCon.Close();
                        return;
                    }
                    dbCon.Close();
                }
                catch (Exception ex)
                {
                    dbCon.Close(); 
                    MessageBox.Show(ex.Message);
                }
                ///end of check if already exist
            }


            ///update penalty
            ///
            query = "UPDATE penalty SET code=?code, penalty=?penalty, point=?point, price=?price WHERE code=?editID";
            dbCon = new MySqlConnection(global.connectionString);
            
            cmd = new MySqlCommand(query, dbCon);
            cmd.Parameters.AddWithValue("?code", codeTxt.Text.Trim().ToUpper());
            cmd.Parameters.AddWithValue("?penalty", penaltyTxt.Text.Trim().ToUpper());
            cmd.Parameters.AddWithValue("?point", pointCmb.Text.Trim().ToUpper());
            cmd.Parameters.AddWithValue("?price", priceTxt.Text.Trim().ToUpper());
            cmd.Parameters.AddWithValue("?editID", editIDTxt.Text.Trim().ToUpper());

            cmd.CommandTimeout = 60;

            try
            {
                dbCon.Open();
                cmd.ExecuteNonQuery();
                dbCon.Close();

                refreshGrid();

                MessageBox.Show("Penalty updated");

                codeTxt.Text = penaltyTxt.Text = priceTxt.Text = "";
                pointCmb.SelectedIndex = 0;
                codeTxt.BackColor = penaltyTxt.BackColor = priceTxt.BackColor = global.inputDefault;

                registerBtn.Visible = true;

                editBtn.Visible = newBtn.Visible = false;
            }
            catch (Exception ex)
            {
                dbCon.Close();
                MessageBox.Show(ex.Message);
            }
            ///end of update
        }
    }
}
