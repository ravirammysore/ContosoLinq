using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using EDM;
namespace ContosoForms
{
    public partial class CreateNoBinding : Form
    {
        ContosoEntities db;
        public CreateNoBinding()
        {
            InitializeComponent();
            db = new ContosoEntities();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Student student = new Student();

            student.FirstName = textBox1.Text;
            student.LastName = textBox2.Text;
            student.EnrollmentDate = dateTimePicker1.Value;

            db.Students.Add(student);
            db.SaveChanges();

            MessageBox.Show("Done");
        }
    }
}
