using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DataTable1
{
    public partial class Form1 : Form
    {
        //UPDATE...1.1
        int selectedRowIndex = -1;
        public Form1()
        {
            InitializeComponent();
        }


        // Form
        private void Form1_Load(object sender, EventArgs e)
        {
            //Id is Disable....1
            txtId.Visible = false;
            lblId.Visible = false;

            //KeyDelete.....1
            this.KeyPreview = true;

            //KeyDelete.....3
            dataGridView1.KeyDown += dataGridView1_KeyDown;

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

         
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        //InsertButton
        private void button1_Click(object sender, EventArgs e)
        {
            if (btnSave.Text == "Save")
            {

                //1.....INSERT

                int id = (dataGridView1.Rows
                            .Cast<DataGridViewRow>()
                            .Where(r => !r.IsNewRow && r.Cells["ID"].Value != null)
                            .Select(r => Convert.ToInt32(r.Cells["ID"].Value))
                            .DefaultIfEmpty(0) // returns 0 if no valid rows
                            .Max()) + 1;

                //dataGridView1.Rows.Add(id, txtName.Text, txtAge.Text, txtClass.Text);
                DataGridViewRow row = new DataGridViewRow();
                row.CreateCells(dataGridView1); // Creates empty cells equal to the number of columns

                row.Cells[dataGridView1.Columns["ID"].Index].Value = id;
                row.Cells[dataGridView1.Columns["studentname"].Index].Value = txtName.Text;
                row.Cells[dataGridView1.Columns["studentage"].Index].Value = txtAge.Text;
                row.Cells[dataGridView1.Columns["studentClass"].Index].Value = txtClass.Text;

                row.Cells[dataGridView1.Columns["Edit"].Index].Value = "Edit";
                row.Cells[dataGridView1.Columns["Delete"].Index].Value = "Delete";
                row.Cells[dataGridView1.Columns["View"].Index].Value = "View";

                string newText = id + "|" + txtName.Text + "|" + txtAge.Text + "|" + txtClass.Text + ",";

                File.AppendAllText("C:\\Users\\priya.chotani\\source\\repos\\DataTable1\\bin\\Debug\\Dynamically.txt", newText + Environment.NewLine);


                
                dataGridView1.Rows.Add(row);



                //2.....EMPTY
                //after insertion textbox value is empty
                 Empty();
            } 
            
            //UPDATE...2
            else
            {
                //if (selectedRowIndex >= 0)
                //{
                //    dataGridView1.Rows[selectedRowIndex].SetValues(txtId.Text, txtName.Text, txtAge.Text, txtClass.Text);
                //    btnSave.Text = "Save"; // Reset button text
                //    Empty(); // Clear all textbox
                //}

                if(txtId.Text != "0" && txtId.Text != "")
                {
                    var row = dataGridView1.Rows
                .Cast<DataGridViewRow>()
                .FirstOrDefault(r => !r.IsNewRow &&
                                     r.Cells["ID"].Value != null &&
                                     Convert.ToInt32(r.Cells["ID"].Value) == Convert.ToInt32(txtId.Text));

                    if (row != null)
                    {
                        row.Cells["studentname"].Value = txtName.Text;
                        row.Cells["studentage"].Value = txtAge.Text;
                        row.Cells["studentClass"].Value = txtClass.Text;
                     
                        btnSave.Text = "Save";
                        txtId.Text = "0";
                        Empty();
                    }
                }
                
            }
        }

        //Clear                                                                                                                                                                
        private void button2_Click(object sender, EventArgs e)
        {   
            //When click on clear button the textbox value is clear
            txtId.Text = "";
            txtName.Text = "";
            txtAge.Text = "";
            txtClass.Text = "";
            txtDescription.Text = "";
            btnSave.Text = "Save"; //After Update when click on clear button further showing Save button
            dataGridView1.Rows.Clear();   //DataGridView Content Clear

            //Id is Disable...2
            // Hide the ID again
            txtId.Visible = false;
            lblId.Visible = false;
        }

        //1......EMPTY
        public void Empty()
        {
            txtId.Text = "";
            txtName.Text = "";
            txtAge.Text = "";
            txtClass.Text = "";
            txtDescription.Text = "";
        }

        //Reset
        private void button3_Click(object sender, EventArgs e)
        {
            btnSave.Enabled = true;
            //dataGridView1.Columns["Edit"].Visible = true;
            //dataGridView1.Columns["Delete"].Visible = true;
            Empty();

            btnSave.Text = "Save";

            txtId.Visible = false;
            lblId.Visible = false;
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
        }       

        private void dataGridView1_CellBorderStyleChanged(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            //UPDATE.......1
            //Fill Data
                     
            if (dataGridView1.Columns[e.ColumnIndex].HeaderText=="Edit")
            {
                //Id is Disable...3
                // Show ID field
              
                selectedRowIndex = e.RowIndex; 

                btnSave.Text = "Edit";  // When click on edit button Save to change Edit 

                txtId.Visible = false;
                lblId.Visible = false;

                //txtId.Text = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString(); 
                txtId.Text = dataGridView1.Rows[e.RowIndex].Cells["Id"].Value.ToString(); //=>Design(Name) = Id
                txtName.Text = dataGridView1.Rows[e.RowIndex].Cells["studentname"].Value.ToString();
                txtAge.Text = dataGridView1.Rows[e.RowIndex].Cells["studentage"].Value.ToString();
                txtClass.Text = dataGridView1.Rows[e.RowIndex].Cells["studentClass"].Value.ToString();

                btnSave.Enabled = true; // when click on view the button is disable but click on edit (Save to edit) therefor edit button is visible                
            }

            //DELETE......1
            else if(dataGridView1.Columns[e.ColumnIndex].HeaderText=="Delete")
            {              
                dataGridView1.Rows.RemoveAt(e.RowIndex);

                //Id is Disable...4
                txtId.Visible = false;
                lblId.Visible = false;

                Empty();
            }



            else if (dataGridView1.Columns[e.ColumnIndex].HeaderText == "View")
            {
                btnSave.Enabled = false;

                //dataGridView1.Columns["Edit"].Visible = false;
                //dataGridView1.Columns["Delete"].Visible = false;
                txtId.Visible = false;
                lblId.Visible = false;

                //txtId.Text = dataGridView1.Rows[e.RowIndex].Cells["Id"].Value.ToString(); //=>Design(Name) = Id
                txtName.Text = dataGridView1.Rows[e.RowIndex].Cells["studentname"].Value.ToString();
                txtAge.Text = dataGridView1.Rows[e.RowIndex].Cells["studentage"].Value.ToString();
                txtClass.Text = dataGridView1.Rows[e.RowIndex].Cells["studentClass"].Value.ToString();

            
                //Also showing data in Description Textbox
               
                txtDescription.Text = "Student Name : " + dataGridView1.Rows[e.RowIndex].Cells["studentname"].Value.ToString() + Environment.NewLine +
                      "Student Age : " + dataGridView1.Rows[e.RowIndex].Cells["studentage"].Value.ToString() + Environment.NewLine +
                      "Student Class : " + dataGridView1.Rows[e.RowIndex].Cells["studentClass"].Value.ToString();
            }
        }

        private void label5_Click(object sender, EventArgs e)
        {
        }

        private void txtDescription_TextChanged(object sender, EventArgs e)
        {
        }

        private void txtName_TextChanged(object sender, EventArgs e)
        {           
        }

        ////KeyDelete.....2
        private void dataGridView1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                if (dataGridView1.SelectedCells.Count > 0)
                {
                    int rowIndex = dataGridView1.SelectedCells[0].RowIndex;
                    if (rowIndex >= 0 && rowIndex < dataGridView1.Rows.Count && !dataGridView1.Rows[rowIndex].IsNewRow)
                    {
                        DialogResult result = MessageBox.Show("Are you sure you want to delete this row?", "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                        if (result == DialogResult.Yes)
                        {
                            dataGridView1.Rows.RemoveAt(rowIndex);
                        }
                    }
                }
            }
            Empty();
        }
    }
}
