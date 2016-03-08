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
    public partial class MergedWP : Form
    {
        public MergedWP()
        {
            InitializeComponent();
        }

        private void MergedWP_Load(object sender, EventArgs e)
        {
            Levels.SelectedIndex = 1;
            Courses.SelectedIndex = 1;
            Teachers.SetItemChecked(1, true);
            Teachers.SetItemChecked(2, true);
        }
        
    }
}
