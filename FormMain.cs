using Npgsql;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TestApp
{
    public partial class FormMain : Form
    {
        private Database db = new Database();
        private int rowIndex = -1;

        public FormMain()
        {
            InitializeComponent();

            this.FormBorderStyle = FormBorderStyle.FixedSingle;

            dgvData.AllowUserToAddRows = false;
            dgvData.AllowUserToDeleteRows = false;
            dgvData.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvData.MultiSelect = false;
            dgvData.ReadOnly = true;
            dgvData.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        }

        private void FormMain_Load(object sender, EventArgs e)
        {
            buttonDelete.Enabled = false;

            try
            {
                db.CreateConnection();
                UpdateTable();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка");
                db.CloseConnection();
            }

        }

        private void UpdateTable()
        {
            dgvData.DataSource = db.Refresh();
            dgvData.ClearSelection();
            buttonDelete.Enabled = false;
            rowIndex = -1;
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            Data data = null;
            using (FormAdd fa = new FormAdd())
            {
                if(fa.ShowDialog() == DialogResult.OK)
                {
                    data = fa.GetValue();
                }
            }
            try
            {
                if (data != null)
                {
                    db.Add(data);
                    UpdateTable();
                }
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message, "Ошибка");
            }
        }

        private void buttonDelete_Click(object sender, EventArgs e)
        {
            if(rowIndex >= 0)
            {
                try
                {
                    db.Delete(int.Parse(dgvData.Rows[rowIndex].Cells[0].Value.ToString()));
                    UpdateTable();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Ошибка");
                }
            }
        }

        private void dgvData_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if(e.RowIndex >= 0)
            {
                buttonDelete.Enabled = true;
                rowIndex = e.RowIndex;
            }
        }

        private void FormMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                db.CloseConnection();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка");
            }
        }
    }
}
