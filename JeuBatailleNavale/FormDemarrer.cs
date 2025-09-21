using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace JeuBatailleNavale
{
    public partial class FormDemarrer : Form
    {
        public FormDemarrer()
        {
            InitializeComponent();
        }

        private void btnCommencer_Click(object sender, EventArgs e)
        {
            FormJeu jeu = new FormJeu();
            jeu.Show();
            this.Hide();
        }
    }
}
