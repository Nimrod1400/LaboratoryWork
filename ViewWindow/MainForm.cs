using BusinessLogic;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;


namespace ViewWindow
{
    public partial class MainForm : Form
    {
        private BL logic = new BL();
        private string[] studentInfo = new string[] {"", "", ""};
        private int selectedStudentIndex = -1;
        private HistogramForm histogram = new HistogramForm();

        public MainForm()
        {
            InitializeComponent();
            RefreshList();
            this.Text = "Список студентов";
            FormBorderStyle = FormBorderStyle.FixedSingle;
            listView1.MultiSelect = false;
            listView1.AllowColumnReorder = false;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (selectedStudentIndex != -1)
            {
                logic.DeleteStudent(selectedStudentIndex);
                RefreshList();
                histogram.ShowHistogram(logic);
            }            
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count > 0)
            {
                selectedStudentIndex = listView1.SelectedIndices[0];
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string name = studentInfo[0];
            string spec = studentInfo[1];
            string group = studentInfo[2];
            if (name != "" && spec != "" && group != "")
            {
                logic.AddStudent(name, spec, group);
                nameTextBox.Text = "";
                specTextBox.Text = "";
                groupTextBox.Text = "";
                studentInfo = (new string[] { "", "", "" });
                RefreshList();
                histogram.ShowHistogram(logic);
            }
            else
            {
                ShowErrorDialogWindow("Имя студента, его группа и специальность не должны быть пустыми строками."); 
            }            
        }

        public void ShowErrorDialogWindow(string message)
        {
            MessageBox.Show(message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        public void RefreshList()
        {
            listView1.Clear();

            listView1.View = View.Details;

            listView1.Columns.Add("Имя", 85);
            listView1.Columns.Add("Специальность", 190);
            listView1.Columns.Add("Группа", 70);

            for (int i = 0; i < logic.CountStudents(); i++)
            {
                var (name, spec, group) = logic.GetStudent(i);

                ListViewItem newitem = new ListViewItem(name);
                newitem.SubItems.Add(spec);
                newitem.SubItems.Add(group);

                listView1.Items.Add(newitem);
            }
        }

        private void nameTextBox_TextChanged(object sender, EventArgs e)
        {            
            studentInfo[0] = nameTextBox.Text.Trim();
        }

        private void specTextBox_TextChanged(object sender, EventArgs e)
        {
            studentInfo[1] = specTextBox.Text.Trim();
        }

        private void groupTextBox_TextChanged(object sender, EventArgs e)
        {
            studentInfo[2] = groupTextBox.Text.Trim();
        }

        private void showHistogramButton_Click(object sender, EventArgs e)
        {
            if (histogram.IsDisposed)
            {
                histogram = new HistogramForm();
            }
            histogram.ShowHistogram(logic);
            histogram.Visible = true;            
        }
    }
}
