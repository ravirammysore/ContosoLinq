using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using EDM;

namespace ContosoForms
{
    public partial class Set1Form : Form
    {
        public Set1Form()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            using(ContosoEntities db = new ContosoEntities())
            {
                var qry = from s in db.Students
                          select s;

                dataGridView1.DataSource = qry.ToList();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            using (ContosoEntities db = new ContosoEntities())
            {
                var qry = from s in db.Students
                          where s.ID < 5
                          select new
                          {
                              StudentID = s.ID,
                              SirName = s.LastName
                          };

                dataGridView1.DataSource = qry.ToList();
            }            
        }

        private void button3_Click(object sender, EventArgs e)
        {
            using (ContosoEntities db = new ContosoEntities())
            {
                IQueryable<Student> studs = from student in db.Students
                                            where student.ID < 5
                                            select student;

                dataGridView1.DataSource = studs.ToList();


                IQueryable<string> names = from student in studs
                                           select student.FirstName;
                
                listBox1.DataSource = names.ToList();
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            using (ContosoEntities db = new ContosoEntities())
            {
                //navigating from the 'many' to 'one' side 
                var qry = from course in db.Courses
                          where course.Department.Name.ToLower().Equals("mathematics")
                          select course.Title;

                listBox1.DataSource = qry.ToList();

                //Also:
                //this is the qry to find the HOD responsible for "Calculus"
                //we can recursively navigate from course entity
                //see the sql equivalent of this in linqpad!
                var qry2 = from course in db.Courses
                          where course.Title.Equals("Calculus")
                          select course.Department.Instructor.FirstName;

            }
        }
    }
}
