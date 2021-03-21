using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace Task_Manager
{
    public partial class mainForm : Form
    {
        TaskAdapter taskAdapter = new TaskAdapter();

        public mainForm()
        {
            InitializeComponent();
        }

        private void mainForm_Load(object sender, EventArgs e)
        {

        }

        private void FillDataGrid()
        {
            dataGridView1.Rows.Clear();
            foreach(var task in taskAdapter.GetTasks())
            {
                dataGridView1.Rows.Add(task._name, task._description, task._date, task._priority, task._isDone);
            }
        }

        private void dataGridView1_UserDeletingRow(object sender, DataGridViewRowCancelEventArgs e)
        {
            taskAdapter.RemoveTask(e.Row.Index);
        }

        private void загрузитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            taskAdapter.Load();
            FillDataGrid();
        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            taskAdapter.Save();
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrWhiteSpace(textBoxName.Text))
            {
                MessageBox.Show(this, "Внимание!\nВведите название задачи!", "Внимание!",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else if (String.IsNullOrWhiteSpace(textBoxDesc.Text))
            {
                MessageBox.Show(this, "Внимание!\nВведите описание задачи!", "Внимание!",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                taskAdapter.AddTask(textBoxName.Text, textBoxDesc.Text, dateTimePicker.Value, (int)numericUpDownPriority.Value);

                textBoxName.Text = String.Empty;
                textBoxDesc.Text = String.Empty;
                dateTimePicker.Value = DateTime.Now;
                numericUpDownPriority.Value = 0;

                FillDataGrid();
            }
        }

        private void dataGridView1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            int col_ind = e.ColumnIndex;
            int row_ind = e.RowIndex;
            switch (col_ind)
            {
                case 0:
                    taskAdapter.editName(dataGridView1.Rows[row_ind].Cells[col_ind].Value.ToString(), row_ind);
                    break;
                case 1:
                    taskAdapter.editDescription(dataGridView1.Rows[row_ind].Cells[col_ind].Value.ToString(), row_ind);
                    break;
                case 2:
                    taskAdapter.editDate(DateTime.Parse(dataGridView1.Rows[row_ind].Cells[col_ind].Value.ToString()), row_ind);
                    break;
                case 3:
                    taskAdapter.editPriority(Int32.Parse(dataGridView1.Rows[row_ind].Cells[col_ind].Value.ToString()), row_ind);
                    break;
                case 4:
                    taskAdapter.editStatus(bool.Parse(dataGridView1.Rows[row_ind].Cells[col_ind].Value.ToString()), row_ind);
                    break;
            }
        }

        private void buttonClear_Click(object sender, EventArgs e)
        {
            taskAdapter.Clear();
            dataGridView1.Rows.Clear();
        }
    }
}
