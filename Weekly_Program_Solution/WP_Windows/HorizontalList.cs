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
    public partial class HorizontalList : UserControl
    {
        public string C_FirstItemText
        {
            get { return FirstItem.Text; }
            set
            {
                FirstItem.Text = value;
                FirstItem.Visible = value.Trim() != string.Empty;
            }
        }
        public string C_SecondItemText
        {
            get { return SecondItem.Text; }
            set
            {
                SecondItem.Text = value;
                SecondItem.Visible = value.Trim() != string.Empty;
            }
        }

        public Color C_FirstItemColor
        {
            get { return FirstItem.BackColor; }
            set { FirstItem.BackColor = value; }
        }

        public Color C_SecondItemColor
        {
            get { return SecondItem.BackColor; }
            set { SecondItem.BackColor = value; }
        }

        public Font C_FirstItemStrickout
        {
            get { return FirstItem.Font; }
            set { FirstItem.Font = value; }
        }

        public Font C_SecondItemStrickout
        {
            get { return SecondItem.Font; }
            set { SecondItem.Font = value; }
        }
        public HorizontalList()
        {
            InitializeComponent();
            FirstItem.Visible = false;
            SecondItem.Visible = false;
        }
    }
}
