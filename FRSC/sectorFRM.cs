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
    public partial class sectorFRM : Form
    {
        public sectorFRM()
        {
            InitializeComponent();
        }

        private void stateTxt_TextChanged(object sender, EventArgs e)
        {
            if (!Regex.IsMatch(stateTxt.Text, @"^[a-zA-Z ]{0,30}$"))
                stateTxt.BackColor = global.inputBad;
            else
                stateTxt.BackColor = global.inputGood;
        }

        private void lgtTxt_TextChanged(object sender, EventArgs e)
        {
            if (!Regex.IsMatch(lgtTxt.Text, @"^[a-zA-Z ]{0,30}$"))
                lgtTxt.BackColor = global.inputBad;
            else
                lgtTxt.BackColor = global.inputGood;
        }

        private void updateSectorBtn_Click(object sender, EventArgs e)
        {
            stateTxt_TextChanged(null, EventArgs.Empty);
            lgtTxt_TextChanged(null, EventArgs.Empty);

            if (!Regex.IsMatch(stateTxt.Text, @"^[a-zA-Z ]{0,30}$")
                || !Regex.IsMatch(lgtTxt.Text, @"^[a-zA-Z ]{0,30}$"))
                return;

            String query = "UPDATE sector SET state=?state, lgt=?lgt WHERE id='1'";
            MySqlConnection dbCon = new MySqlConnection(global.connectionString);
            MySqlCommand cmd = new MySqlCommand(query, dbCon);

            cmd.Parameters.AddWithValue("?state", global.upperFirst(stateTxt.Text.Trim()));
            cmd.Parameters.AddWithValue("?lgt", global.upperFirst(lgtTxt.Text.Trim()));

            try
            {
                dbCon.Open();
                cmd.ExecuteNonQuery();
                dbCon.Close();

                MessageBox.Show("Record updated");

                stateTxt.Text = global.upperFirst(stateTxt.Text.Trim());
                lgtTxt.Text = global.upperFirst(lgtTxt.Text.Trim());

                stateTxt.BackColor = lgtTxt.BackColor = global.inputDefault;
            }
            catch (Exception ex)
            {
                dbCon.Close();
                MessageBox.Show(ex.Message);
            }
        }

        private void sectorFRM_Load(object sender, EventArgs e)
        {
            String query = "SELECT state, lgt FROM sector WHERE id='1'";
            MySqlConnection dbCon = new MySqlConnection(global.connectionString);
            MySqlCommand cmd = new MySqlCommand(query, dbCon);

            try
            {
                dbCon.Open();
                MySqlDataReader dbReader = cmd.ExecuteReader();

                if (dbReader.HasRows)
                {
                    if (dbReader.Read())
                    {
                        stateTxt.Text = dbReader["state"].ToString();
                        lgtTxt.Text = dbReader["lgt"].ToString();
                    }
                }
                dbCon.Close();

                stateTxt.Text = global.upperFirst(stateTxt.Text.Trim());
                lgtTxt.Text = global.upperFirst(lgtTxt.Text.Trim());

                stateTxt.BackColor = lgtTxt.BackColor = global.inputDefault;
            }
            catch (Exception ex)
            {
                dbCon.Close();
                MessageBox.Show(ex.Message);
            }
        }
    }
}
