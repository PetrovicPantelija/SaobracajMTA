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
    public partial class Info : Form
    {
        private readonly Action<Form> _open;
        public Info(Action<Form> open)
        {
            InitializeComponent();
            _open = open;
        }
    }
}
