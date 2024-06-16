using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CRUD_Application_Using_LINQ_To_SQL_In_C_
{
    public partial class Form1 : Form
    {
        StudentDBDataContext db;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            BrindGridview();
        }

        private void BrindGridview()
        {
            db = new StudentDBDataContext();
            dataGridView1.DataSource = db.students;
        }

        private void ClearTextBoxes()
        {
            foreach (Control ctr in this.Controls)
            {
                if (ctr is TextBox)
                {
                    TextBox txt = ctr as TextBox;
                    txt.Clear();
                }
            }
            NAMEtextBox.Focus();
        }

        private void INSERTbutton_Click(object sender, EventArgs e)
        {
            if (NAMEtextBox.Text == "" || GENDERtextBox.Text == "" || AGEtextBox.Text == "" || CLASStextBox.Text == "")
            {
                MessageBox.Show("All Fields are required", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                db = new StudentDBDataContext();
                student std = new student();
                std.name = NAMEtextBox.Text;
                std.gender = GENDERtextBox.Text;
                std.age = int.Parse(AGEtextBox.Text);
                std.standard = int.Parse(CLASStextBox.Text);
                db.students.InsertOnSubmit(std);
                db.SubmitChanges();
                MessageBox.Show("Data has been inserted Successfully", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ClearTextBoxes();
                BrindGridview();
            }
        }

        private void CLEARbutton_Click(object sender, EventArgs e)
        {
            ClearTextBoxes();
        }

        private void dataGridView1_MouseClick(object sender, MouseEventArgs e)
        {
            NAMEtextBox.Text = dataGridView1.SelectedRows[0].Cells[1].Value.ToString();
            GENDERtextBox.Text = dataGridView1.SelectedRows[0].Cells[2].Value.ToString();
            AGEtextBox.Text = dataGridView1.SelectedRows[0].Cells[3].Value.ToString();
            CLASStextBox.Text = dataGridView1.SelectedRows[0].Cells[4].Value.ToString();
        }

        private void UPDATEbutton_Click(object sender, EventArgs e)
        {
            try
            {
                if (dataGridView1.SelectedRows.Count > 0)
                {
                    if (dataGridView1.SelectedRows[0].Cells[0].Value != null)
                    {
                        db = new StudentDBDataContext();
                        int id = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells[0].Value); // Assuming the ID is in the first column (Cells[0])
                        student std = db.students.FirstOrDefault(s => s.Id == id);

                        if (std != null)
                        {
                            std.name = NAMEtextBox.Text;
                            std.gender = GENDERtextBox.Text;

                            if (int.TryParse(AGEtextBox.Text, out int age))
                            {
                                std.age = age;
                            }
                            else
                            {
                                MessageBox.Show("Please enter a valid age", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                return;
                            }

                            if (int.TryParse(CLASStextBox.Text, out int standard))
                            {
                                std.standard = standard;
                            }
                            else
                            {
                                MessageBox.Show("Please enter a valid class", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                return;
                            }

                            db.SubmitChanges();
                            MessageBox.Show("Data has been Updated Successfully", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            ClearTextBoxes();
                            BrindGridview();
                        }
                        else
                        {
                            MessageBox.Show("Student not found", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    else
                    {
                        MessageBox.Show("Invalid ID value", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    MessageBox.Show("Please Select A Row", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void DELETEbutton_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                DialogResult confirm = MessageBox.Show("Are you sure to delete Data??", "Question", MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);
                if (confirm == DialogResult.Yes)
                {
                    db = new StudentDBDataContext();
                    int id = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells[0].Value);
                    student std = db.students.FirstOrDefault(s => s.Id == id);
                    db.students.DeleteOnSubmit(std);
                    db.SubmitChanges();
                    MessageBox.Show("Data has been Deleted Successfully", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ClearTextBoxes();
                    BrindGridview();
                }
            }
            else
            {
                MessageBox.Show("Please Select A Row", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}

