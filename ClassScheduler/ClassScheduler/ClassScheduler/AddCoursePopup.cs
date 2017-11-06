using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ClassScheduler
{
    public partial class AddCourseForm : Form
    {
        public AddCourseForm()
        {
            InitializeComponent();
        }

        //[FUNCTION - addButton_Click]
        //Adds selected course to user's courses when button is clicked
        private void addButton_Click(object sender, EventArgs e)
        {
        }

        //[FUNCTION - addButton_Click]
        //Cancels additional course selection (closes pop-up)
        private void cancelAddButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
