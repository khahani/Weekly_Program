using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WP_Windows
{
    public partial class TeacherWP : Form
    {
        public TeacherWP()
        {
            InitializeComponent();
        }
        private void MathChangeColor(Color c)
        {
            Math1.BackColor = c;
            Math2.BackColor = c;
            Math3.BackColor = c;
            Math4.BackColor = c;
        }
        private void SelectMath()
        {
            MathChangeColor(Color.FromArgb(128, 255, 128));
        }

        private void BackMath()
        {
            MathChangeColor(Color.FromName("Info"));
        }

        private void Courses_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Courses.SelectedIndex == 1)
            {
                SelectMath();

                return;
            }

            ResetAll();
        }

        private void ResetAll()
        {
            BackMath();
        }
    }
}
