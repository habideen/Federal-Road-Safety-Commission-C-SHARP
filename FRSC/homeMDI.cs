using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FRSC
{
    public partial class homeMDI : Form
    {
        public homeMDI()
        {
            InitializeComponent();
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            aboutFrm about = new aboutFrm();
            about.ShowDialog();
        }

        private void dashboardMenuTool_Click(object sender, EventArgs e)
        {
            if (global.activeForm == "dashboard")
                return;

            if (Application.OpenForms.OfType<dashboardFrm>().Count() == 1)
                Application.OpenForms.OfType<dashboardFrm>().First().Close();

            dashboardFrm frm = new dashboardFrm();
            frm.MdiParent = this;
            frm.FormBorderStyle = FormBorderStyle.None;
            frm.Dock = DockStyle.Fill;
            frm.ControlBox = false;
            frm.Show();

            global.activeForm = "dashboard";
        }

        private void homeMDI_Load(object sender, EventArgs e)
        {
            dashboardFrm dashboard = new dashboardFrm();
            dashboard.MdiParent = this;
            dashboard.FormBorderStyle = FormBorderStyle.None;
            dashboard.Dock = DockStyle.Fill;
            dashboard.ControlBox = false;
            dashboard.Show();

            global.activeForm = "dashboard";
        }

        private void homeMDI_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (MessageBox.Show("Quit Application? All pending operations will not be saved.", "Exit", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                e.Cancel = false;
            }
            else
            {
                e.Cancel = true;
                this.Activate();
            }
        }

        private void homeMDI_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void officeRegionMenu_Click(object sender, EventArgs e)
        {
            if (global.activeForm == "sectorFrm")
                return;

            if (Application.OpenForms.OfType<sectorFRM>().Count() == 1)
                Application.OpenForms.OfType<sectorFRM>().First().Close();

            sectorFRM dashboard = new sectorFRM();
            dashboard.MdiParent = this;
            dashboard.FormBorderStyle = FormBorderStyle.None;
            dashboard.Dock = DockStyle.Fill;
            dashboard.ControlBox = false;
            dashboard.Show();

            global.activeForm = "sectorFrm";
        }

        private void newUserToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (global.activeForm == "addUserFrm")
                return;

            if (Application.OpenForms.OfType<addUserFrm>().Count() == 1)
                Application.OpenForms.OfType<addUserFrm>().First().Close();

            addUserFrm dashboard = new addUserFrm();
            dashboard.MdiParent = this;
            dashboard.FormBorderStyle = FormBorderStyle.None;
            dashboard.Dock = DockStyle.Fill;
            dashboard.ControlBox = false;
            dashboard.Show();

            global.activeForm = "addUserFrm";
        }

        private void changePasswordToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (global.activeForm == "changePasswordFrm")
                return;

            if (Application.OpenForms.OfType<changePasswordFrm>().Count() == 1)
                Application.OpenForms.OfType<changePasswordFrm>().First().Close();

            changePasswordFrm dashboard = new changePasswordFrm();
            dashboard.MdiParent = this;
            dashboard.FormBorderStyle = FormBorderStyle.None;
            dashboard.Dock = DockStyle.Fill;
            dashboard.ControlBox = false;
            dashboard.Show();

            global.activeForm = "changePasswordFrm";
        }

        private void viewUserToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (global.activeForm == "viewUsersFrm")
                return;

            if (Application.OpenForms.OfType<viewUsersFrm>().Count() == 1)
                Application.OpenForms.OfType<viewUsersFrm>().First().Close();

            viewUsersFrm dashboard = new viewUsersFrm();
            dashboard.MdiParent = this;
            dashboard.FormBorderStyle = FormBorderStyle.None;
            dashboard.Dock = DockStyle.Fill;
            dashboard.ControlBox = false;
            dashboard.Show();

            global.activeForm = "viewUsersFrm";
        }

        private void newOffenceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (global.activeForm == "addOffenceFrm")
                return;

            if (Application.OpenForms.OfType<addOffenceFrm>().Count() == 1)
                Application.OpenForms.OfType<addOffenceFrm>().First().Close();

            addOffenceFrm dashboard = new addOffenceFrm();
            dashboard.MdiParent = this;
            dashboard.FormBorderStyle = FormBorderStyle.None;
            dashboard.Dock = DockStyle.Fill;
            dashboard.ControlBox = false;
            dashboard.Show();

            global.activeForm = "addOffenceFrm";
        }

        private void manageOffenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (global.activeForm == "penaltyFrm")
                return;

            if (Application.OpenForms.OfType<penaltyFrm>().Count() == 1)
                Application.OpenForms.OfType<penaltyFrm>().First().Close();

            penaltyFrm dashboard = new penaltyFrm();
            dashboard.MdiParent = this;
            dashboard.FormBorderStyle = FormBorderStyle.None;
            dashboard.Dock = DockStyle.Fill;
            dashboard.ControlBox = false;
            dashboard.Show();

            global.activeForm = "penaltyFrm";
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void viewOffenceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (global.activeForm == "viewOffenceFrm")
                return;

            if (Application.OpenForms.OfType<viewOffenceFrm>().Count() == 1)
                Application.OpenForms.OfType<viewOffenceFrm>().First().Close();

            viewOffenceFrm dashboard = new viewOffenceFrm();
            dashboard.MdiParent = this;
            dashboard.FormBorderStyle = FormBorderStyle.None;
            dashboard.Dock = DockStyle.Fill;
            dashboard.ControlBox = false;
            dashboard.Show();

            global.activeForm = "viewOffenceFrm";
        }
    }
}
