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
    public partial class loginFrm : Form
    {
        public loginFrm()
        {
            InitializeComponent();
        }

        private void signinBtn_Click(object sender, EventArgs e)
        {
            string query = "SELECT username, password, lname, fname FROM user WHERE username=?username";
            MySqlConnection dbCon = new MySqlConnection(global.connectionString);
            MySqlCommand cmd = new MySqlCommand(query, dbCon);

            BlowFish b = new BlowFish(global.blowFishKey); //call ecryption key

            cmd.Parameters.AddWithValue("?username", usernameTxt.Text.Trim());
            cmd.CommandTimeout = 60;

            try
            {
                dbCon.Open();
                MySqlDataReader dbReader = cmd.ExecuteReader();

                if (dbReader.HasRows)
                {
                    string password = ""; string fname = ""; string lname = ""; string username = "";

                    while (dbReader.Read())
                    {
                        password = b.Decrypt_CBC(dbReader["password"].ToString());
                        lname = dbReader["lname"].ToString();
                        fname = dbReader["fname"].ToString();
                        username = dbReader["username"].ToString();
                    }

                    if (password != "" && password == passwordTxt.Text)
                    {
                        global.loggedFname = fname;
                        global.loggedLname = lname;
                        global.loggedUsername = username;

                        new homeMDI().Show();
                        this.Hide();
                        return;
                    }
                    else
                        errorLbl.Visible = true;
                }
                else
                    errorLbl.Visible = true;

                dbCon.Close();


            }
            catch (Exception ex)
            { MessageBox.Show(ex.Message); }
        }

        private void loginFrm_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }
    }
}
