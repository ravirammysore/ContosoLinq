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
    public partial class CreateWithBinding : Form
    {
        ContosoEntities db;
        public CreateWithBinding()
        {
            InitializeComponent();
            db = new ContosoEntities();

            studentBindingSource.DataSource = new Student();
        }
        //create
        private void button1_Click(object sender, EventArgs e)
        {
            Student s = studentBindingSource.Current as Student;

            db.Students.Add(s); 
            db.SaveChanges();

            MessageBox.Show("Done");
        }

        //fetch
        private void button2_Click(object sender, EventArgs e)
        {
            studentBindingSource.DataSource = db.Students.FirstOrDefault();
        }
    }
}
