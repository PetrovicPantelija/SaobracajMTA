using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Saobracaj.MainLeget
{
    public partial class UvozMain : Form
    {
        private readonly Action<Form> _open;
        public UvozMain(Action<Form> open)
        {
            InitializeComponent();
            _open = open;
        }
    }
}
