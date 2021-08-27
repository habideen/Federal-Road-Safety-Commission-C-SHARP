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
    public partial class addUserFrm : Form
    {
        public addUserFrm()
        {
            InitializeComponent();
        }

        private void fnameTxt_TextChanged(object sender, EventArgs e)
        {
            if (!Regex.IsMatch(fnameTxt.Text, @"^[a-zA-Z]{3,30}$"))
                fnameTxt.BackColor = global.inputBad;
            else
                fnameTxt.BackColor = global.inputGood;
        }

        private void signinBtn_Click(object sender, EventArgs e)
        {
            if (!Regex.IsMatch(fnameTxt.Text, @"^[a-zA-Z]{3,30}$"))
                fnameTxt.BackColor = global.inputBad;

            if (!Regex.IsMatch(lnameTxt.Text, @"^[a-zA-Z]{3,30}$"))
                lnameTxt.BackColor = global.inputBad;

            if (!Regex.IsMatch(phoneTxt.Text, @"^[0][7-9][0-9]{9}$"))
                phoneTxt.BackColor = global.inputBad;

            if (!Regex.IsMatch(usernameTxt.Text, @"^[0-9a-zA-Z]{3,30}$"))
                usernameTxt.BackColor = global.inputBad;


            string query = "SELECT username FROM user WHERE username=?username";
            MySqlConnection dbCon = new MySqlConnection(global.connectionString);
            MySqlCommand cmd = new MySqlCommand(query, dbCon);

            cmd.Parameters.AddWithValue("?username", usernameTxt.Text.Trim());
            cmd.CommandTimeout = 60;

            string username = "";
            try
            {
                dbCon.Open();
                MySqlDataReader dbReader = cmd.ExecuteReader();

                if (dbReader.HasRows)
                {
                    while (dbReader.Read())
                    {
                        username = dbReader["username"].ToString();
                    }
                }
                dbCon.Close();
            }
            catch (Exception ex)
            { MessageBox.Show(ex.Message); }


            if (username == usernameTxt.Text.Trim())
            {
                usernameTxt.BackColor = global.inputBad;
                errorLbl.Text = "Username already exist!";
                errorLbl.Visible = true;
            }
            else
            {
                if (!Regex.IsMatch(fnameTxt.Text, @"^[a-zA-Z]{3,30}$")
                    || !Regex.IsMatch(lnameTxt.Text, @"^[a-zA-Z]{3,30}$")
                    || !Regex.IsMatch(phoneTxt.Text, @"^[0][7-9][0-9]{9}$")
                    || !Regex.IsMatch(usernameTxt.Text, @"^[0-9a-zA-Z]{3,30}$"))
                    return;

                //save record
                BlowFish b = new BlowFish(global.blowFishKey); //call ecryption key

                query = "INSERT INTO user VALUES (?username, ?password, ?lname, ?fname, ?phone)";

                cmd = new MySqlCommand(query, dbCon);
                cmd.Parameters.AddWithValue("?username", usernameTxt.Text.Trim());
                cmd.Parameters.AddWithValue("?password", b.Encrypt_CBC(lnameTxt.Text.Trim().ToLower()));
                cmd.Parameters.AddWithValue("?lname", global.upperFirst(lnameTxt.Text.Trim()));
                cmd.Parameters.AddWithValue("?fname", global.upperFirst(fnameTxt.Text.Trim()));
                cmd.Parameters.AddWithValue("?phone", phoneTxt.Text.Trim());

                cmd.CommandTimeout = 60;

                try
                {
                    dbCon.Open();

                    cmd.ExecuteNonQuery();
                    MessageBox.Show("User registered");

                    fnameTxt.Text = lnameTxt.Text = phoneTxt.Text = usernameTxt.Text = "";
                    fnameTxt.BackColor = lnameTxt.BackColor = phoneTxt.BackColor = usernameTxt.BackColor = global.inputDefault;

                    dbCon.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }

            }
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

        private void usernameTxt_TextChanged(object sender, EventArgs e)
        {
            errorLbl.Visible = false;
            if (!Regex.IsMatch(usernameTxt.Text, @"^[0-9a-zA-Z]{3,30}$"))
                usernameTxt.BackColor = global.inputBad;
            else
                usernameTxt.BackColor = global.inputGood;
        }
    }
}
