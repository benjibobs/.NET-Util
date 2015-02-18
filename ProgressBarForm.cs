using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EDITME
{
    public partial class ProgressBarForm : Form
    {
        public ProgressBarForm()
        {
            InitializeComponent();
        }

        private void ProgressBarForm_Load(object sender, EventArgs e)
        {

        }

        public void setProgress(int number)
        {
            progress.Value = number;
        }

        public void setMax(int max)
        {
            progress.Maximum = max;
        }

        public int getProgress()
        {
            return progress.Value;
        }

        public void addProgress(int toAdd)
        {
            progress.Value = progress.Value + toAdd;
        }

    }
}
