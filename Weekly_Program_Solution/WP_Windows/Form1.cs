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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Courses.SelectedIndex = 0;
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

        private void PhysicChangeColor(Color c)
        {
            Physic1.BackColor = c;
            Physic2.BackColor = c;
        }
        private void SelectPhysic()
        {
            PhysicChangeColor(Color.FromArgb(128, 255, 128));
        }

        private void BackPhysic()
        {
            PhysicChangeColor(Color.FromName("Info"));
        }

        private void Courses_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Courses.SelectedIndex == 0)
            {
                ResetAll();
            }
            if (Courses.SelectedIndex == 1)
            {
                BackPhysic();
                SelectMath();
            }
            if (Courses.SelectedIndex == 2)
            {
                SelectPhysic();
                BackMath();
            }
        }

        private void ResetAll()
        {
            BackMath();
            BackPhysic();
        }
    }
}
