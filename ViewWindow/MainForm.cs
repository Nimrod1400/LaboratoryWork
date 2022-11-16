using BusinessLogic;
using Ninject;
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
        private BL Logic { get; set; }
        private string[] studentInfo = new string[] {"", "", ""};
        private int selectedStudentIndex = -1;
        private HistogramForm histogram = new HistogramForm();

        public MainForm()
        {
            InitializeComponent();

            IKernel ninjectKernel = new StandardKernel(new SimpleConfigModule());
            Logic = ninjectKernel.Get<BL>();

            RefreshList();
            this.Text = "Список студентов";
            FormBorderStyle = FormBorderStyle.FixedSingle;
            listView1.MultiSelect = false;
            listView1.AllowColumnReorder = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string name = studentInfo[0];
            string spec = studentInfo[1];
            string group = studentInfo[2];
            if (name != "" && spec != "" && group != "")
            {
                Logic.AddStudent(name, spec, group);
                nameTextBox.Text = "";
                specTextBox.Text = "";
                groupTextBox.Text = "";
                studentInfo = (new string[] { "", "", "" });
                RefreshList();
                histogram.ShowHistogram(Logic);
            }
            else
            {
                ShowErrorDialogWindow("Имя студента, его группа и специальность не должны быть пустыми строками.");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (selectedStudentIndex != -1)
            {
                Logic.DeleteStudent(selectedStudentIndex);
                RefreshList();
                histogram.ShowHistogram(Logic);
            }            
        }

        private void showHistogramButton_Click(object sender, EventArgs e)
        {
            if (histogram.IsDisposed)
            {
                histogram = new HistogramForm();
            }
            histogram.ShowHistogram(Logic);
            histogram.Visible = true;
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count > 0)
            {
                selectedStudentIndex = listView1.SelectedIndices[0];
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

            for (int i = 0; i < Logic.CountStudents(); i++)
            {
                var (name, spec, group) = Logic.GetStudent(i);

                ListViewItem newitem = new ListViewItem(name);
                newitem.SubItems.Add(spec);
                newitem.SubItems.Add(group);

                listView1.Items.Add(newitem);
            }
        }
    }
}
