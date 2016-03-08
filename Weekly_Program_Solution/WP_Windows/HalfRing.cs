using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WP_Windows
{
    public partial class HalfRing : UserControl
    {
        public string FirstHalfRing
        {
            get
            {
                return FirstHR.Text;
            }
            set
            {
                FirstHR.Text = value;
                FirstHR.Visible = value.Trim() != string.Empty;
            }
        }

        public string SecondHalfRing
        {
            get
            {
                return SecondHR.Text;
            }
            set
            {
                SecondHR.Text = value;
                SecondHR.Visible = value.Trim() != string.Empty;
            }
        }
        
        public Color FirstHalfRingColor
        {
            get
            {
                return FirstHR.BackColor;
            }
            set
            {
                FirstHR.BackColor = value;
            }
        }

        public Color SecondHalfRingColor
        {
            get
            {
                return SecondHR.BackColor;
            }
            set
            {
                SecondHR.BackColor = value;
            }
        }

        public HalfRing()
        {
            InitializeComponent();
            FirstHR.Visible = false;
            SecondHR.Visible = false;
        }
    }
}
