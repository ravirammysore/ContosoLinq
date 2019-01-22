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
using static ContosoForms.AppConstants;

namespace ContosoForms
{
    public partial class Set2Form : Form
    {
        ContosoEntities db;
        public Set2Form()
        {
            InitializeComponent();
            db = new ContosoEntities();
        }

        /*From … From … (SelectMany)
            use this when we query on many to many entities
         */
        private void button1_Click(object sender, EventArgs ea)
        {
            //get (name and course) of all students who have got c grade 
            var qry = from s in db.Students
                     //navigation property s.Enrollments will yield us all enrollments of s, hence we need from..from
                      from e in db.Enrollments 
                      where s.ID == e.StudentID  && e.Grade == 2
                      select new
                      {
                          Student_Name = s.FirstName + s.LastName,
                          Course_Name = e.Course.Title,
                          Grade_Obtained = e.Grade
                      };

            dataGridView1.DataSource = qry.ToList();            
        }

        private void button2_Click(object sender, EventArgs ea)
        {
            //tried to get (name and course) of all sudents who have got c grade, but cannot apply where clause!
            //achieved that in next query
            var qry1 = from s in db.Students
                      join e in db.Enrollments     
                      on s.ID equals e.StudentID 
                      into stud_enroll                       
                      select new
                      {
                          Student_Name = s.FirstName,
                          No_Of_Courses =  stud_enroll.Count()
                      };

            dataGridView1.DataSource = qry1.ToList();

            //where condition can be used when we omit the into 'stud_enroll' clause
                //and notice that we are still able to get the No_Of_Courses the student has enrolled!! 

            //The output of this query and From...From.. query is the same!

            var qry2 = from s in db.Students
                       join e in db.Enrollments
                       on s.ID equals e.StudentID
                       where e.Grade == 2
                       select new
                       {
                           Student_Name = s.FirstName + s.LastName,                           
                           Course_Name = e.Course.Title,
                           Grade_Obtained = e.Grade,
                           No_Of_Courses = s.Enrollments.Count()
                       };

            dataGridView1.DataSource = qry2.ToList();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            //get the (student_name, course_name) of students with student ids 1 or 2 or 3
            int[] stud_Ids = { 1, 2, 3};

            var qry = from enrollment in db.Enrollments
                      where stud_Ids.Contains(enrollment.StudentID)
                      select new
                      {
                          Student_Name = enrollment.Student.FirstName,
                          Course_Name = enrollment.Course.Title
                      };

            dataGridView1.DataSource = qry.ToList();
        }

        //Group
        private void button4_Click(object sender, EventArgs e)
        {
            var qry = from stud in db.Students
                      group stud by stud.EnrollmentDate.Year into stud_group
                      select new
                      {
                          Year_Enrolled = stud_group.Key,
                          No_Of_Students = stud_group.Count()
                      };

            //Also 
            var qry2 = from stud in db.Students
                       group stud by stud.EnrollmentDate.Year into stud_group 
                       select new
                       {
                           Year_Enrolled = stud_group.Key,
                           Students = stud_group
                       };

            foreach (var item in qry2)
            {
                display(item.Year_Enrolled.ToString());
                foreach (var stud in item.Students)
                {
                    display(stud.FirstName);
                }
                display();
            }                        
        }

        private void display(string msg = "")
        {           
            if(!string.IsNullOrEmpty(msg))textBox1.Text += msg;
            textBox1.Text += Environment.NewLine;
        }
    }
}
