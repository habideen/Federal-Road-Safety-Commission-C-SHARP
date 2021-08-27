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
    public partial class changePasswordFrm : Form
    {
        public changePasswordFrm()
        {
            InitializeComponent();
        }

        private void currentPasswordTxt_TextChanged(object sender, EventArgs e)
        {
            errorLbl.Visible = false;
        }

        private void newPasswordTxt_TextChanged(object sender, EventArgs e)
        {
            errorLbl.Visible = false;

            confirmPasswordTxt.Text = "";
            confirmPasswordTxt.BackColor = global.inputBad;

            if (newPasswordTxt.Text.Length < 8)
                newPasswordTxt.BackColor = global.inputBad;
            else
                newPasswordTxt.BackColor = global.inputGood;
        }

        private void confirmPasswordTxt_TextChanged(object sender, EventArgs e)
        {
            errorLbl.Visible = false;

            if (confirmPasswordTxt.Text != newPasswordTxt.Text)
                confirmPasswordTxt.BackColor = global.inputBad;
            else
                confirmPasswordTxt.BackColor = global.inputGood;
        }

        private void updateBtn_Click(object sender, EventArgs e)
        {
            if (currentPasswordTxt.Text == "")
                currentPasswordTxt.BackColor = global.inputBad;

            if (newPasswordTxt.Text.Length < 8)
                newPasswordTxt.BackColor = global.inputBad;

            if (confirmPasswordTxt.Text != newPasswordTxt.Text)
                confirmPasswordTxt.BackColor = global.inputBad;


            if (currentPasswordTxt.Text == ""
                || newPasswordTxt.Text.Length < 8
                || confirmPasswordTxt.Text != newPasswordTxt.Text)
                return;


            ///
            string query = "SELECT username, password, lname, fname FROM user WHERE username=?username";
            MySqlConnection dbCon = new MySqlConnection(global.connectionString);
            MySqlCommand cmd = new MySqlCommand(query, dbCon);

            BlowFish b = new BlowFish(global.blowFishKey); //call ecryption key

            cmd.Parameters.AddWithValue("?username", global.loggedUsername);
            cmd.CommandTimeout = 60;

            try
            {
                dbCon.Open();
                MySqlDataReader dbReader = cmd.ExecuteReader();

                if (dbReader.HasRows)
                {
                    string password = "";

                    while (dbReader.Read())
                        password = b.Decrypt_CBC(dbReader["password"].ToString());

                    if (password != currentPasswordTxt.Text)
                    {
                        errorLbl.Text = "Current password is incorrect!";
                        errorLbl.Visible = true;
                        currentPasswordTxt.BackColor = global.inputBad;

                        dbCon.Close();
                    }
                    else
                    {
                        if (currentPasswordTxt.Text == newPasswordTxt.Text)
                        {
                            MessageBox.Show("New password is same as old password! Please use another");
                            return;
                        }

                        query = "UPDATE user SET password=?password WHERE username=?username";
                        dbCon = new MySqlConnection(global.connectionString);
                        cmd = new MySqlCommand(query, dbCon);

                        cmd.Parameters.AddWithValue("?username", global.loggedUsername);
                        cmd.Parameters.AddWithValue("?password", b.Encrypt_CBC(newPasswordTxt.Text.ToLower()));
                        cmd.CommandTimeout = 60;

                        try
                        {
                            dbCon.Open();

                            cmd.ExecuteNonQuery();
                            MessageBox.Show("Password updated.");

                            dbCon.Close();

                            currentPasswordTxt.Text = newPasswordTxt.Text = confirmPasswordTxt.Text = "";
                            currentPasswordTxt.BackColor = newPasswordTxt.BackColor = confirmPasswordTxt.BackColor = global.inputDefault;
                            errorLbl.Visible = false;
                        }
                        catch (Exception ex)
                        { MessageBox.Show(ex.Message); }


                    }
                }
            }
            catch (Exception ex)
            { MessageBox.Show(ex.Message); }
        }
    }
}
