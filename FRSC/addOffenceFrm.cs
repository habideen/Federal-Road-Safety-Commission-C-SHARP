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
using System.Text.RegularExpressions;

namespace FRSC
{
    public partial class addOffenceFrm : Form
    {
        public addOffenceFrm()
        {
            InitializeComponent();
        }

        private void offenceCmb_Leave(object sender, EventArgs e)
        {
            if (!offenceCmb.Items.Contains(offenceCmb.Text))
            {
                offenceCmb.Focus();
                MessageBox.Show("Please select an item from the list");
            }
        }

        private void addOffenceFrm_Load(object sender, EventArgs e)
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
        }

        private void offenceCmb_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!offenceCmb.Items.Contains(offenceCmb.Text) || offenceCmb.Text=="")
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

        private void licencePlateTxt_TextChanged(object sender, EventArgs e)
        {
            if (!Regex.IsMatch(licencePlateTxt.Text, @"^[a-zA-Z0-9]{3,20}$"))
                licencePlateTxt.BackColor = global.inputBad;
            else
                licencePlateTxt.BackColor = global.inputGood;
        }

        private void gifmisTxt_TextChanged(object sender, EventArgs e)
        {
            if (!Regex.IsMatch(gifmisTxt.Text, @"^[a-zA-Z0-9 ]{0,20}$"))
                gifmisTxt.BackColor = global.inputBad;
            else
                gifmisTxt.BackColor = global.inputGood;
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

        private void paymentTxt_TextChanged(object sender, EventArgs e)
        {
            if (!Regex.IsMatch(paymentTxt.Text, @"^[a-zA-Z0-9 \-,.)(]{3,100}$"))
                paymentTxt.BackColor = global.inputBad;
            else
                paymentTxt.BackColor = global.inputGood;
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
                || offenceCmb.Text=="")
                return;

            String code = offenceCmb.Text.Split('#')[1].Trim();

            String query = "INSERT INTO " +
                            "offence(fname, mname, lname, vehicleregno, vehiclecolor, vehicletype, vehiclemake, gifmis, licence, code, price, payment_description, registered_by) " +
                            "VALUES(?fname, ?mname, ?lname, ?vehicleregno, ?vehiclecolor, ?vehicletype, ?vehiclemake, ?gifmis, ?licence, ?code, ?price, ?payment_description, ?registered_by)";
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
            cmd.Parameters.AddWithValue("?price", offencePriceTxt.Text.Trim());
            cmd.Parameters.AddWithValue("?payment_description", paymentTxt.Text.Trim());
            cmd.Parameters.AddWithValue("?registered_by", global.loggedUsername);

            try
            {
                dbCon.Open();
                cmd.ExecuteNonQuery();
                dbCon.Close();

                MessageBox.Show("Offence registered");

                offencePriceTxt.Text = code = licencePlateTxt.Text = gifmisTxt.Text = vehicleMakeTxt.Text = vehicleTypeTxt.Text = vehicleColorTxt.Text = vehicleRegNoTxt.Text = fnameTxt.Text = mnameTxt.Text = lnameTxt.Text = offencePriceTxt.Text = paymentTxt.Text = offencePriceTxt.Text = "";
                offenceCmb.SelectedIndex = 0;
                paymentTxt.BackColor = offencePriceTxt.BackColor = licencePlateTxt.BackColor = gifmisTxt.BackColor = vehicleMakeTxt.BackColor = vehicleTypeTxt.BackColor = vehicleColorTxt.BackColor = vehicleRegNoTxt.BackColor = fnameTxt.BackColor = mnameTxt.BackColor = lnameTxt.BackColor = offencePriceTxt.BackColor = global.inputDefault;
            }
            catch (Exception ex)
            {
                dbCon.Close();
                MessageBox.Show(ex.Message);
            }
        }
    }
}
