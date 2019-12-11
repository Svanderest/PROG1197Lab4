using PROG1197Lab4Objects;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PROG1197Lab4Interface
{
    public partial class Form1 : Form
    {
        MovieTree Tree;
        public Form1(MovieTree tree)
        {
            Tree = tree;
            InitializeComponent();            
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            lstMovies.Items.AddRange(Tree.Display.ToArray());
            dtpRelease.MaxDate = DateTime.Today;
            nudYear.Maximum = Convert.ToDecimal(DateTime.Today.Year);
        }

        private void BtnAdd_Click(object sender, EventArgs e)
        {
            /*Title of moviecannot be empty
             * b.Release dateshould be a valid date that is not in the future.
             * c.Runtime is greater than zero
             * d.Rating is between 0.0 and 10.*/
            if (txtTitle.Text.Length == 0)
                MessageBox.Show("Please enter the title");
            else if (dtpRelease.Value > DateTime.Today)
                MessageBox.Show("Release date cannot be in the future");
            else if (nudRuntime.Value <= 0m)
                MessageBox.Show("Runtime cannot be 0");
            else if (nudRating.Value < 0m || nudRating.Value > 10m)
                MessageBox.Show("Rating must be between 0 and 10");
            else
            {
                Tree.Add(new MovieNode
                {
                    Title = txtTitle.Text,
                    Director = txtDirector.Text,
                    ReleaseDate = dtpRelease.Value,
                    Runtime = TimeSpan.FromMinutes(Convert.ToDouble(nudRuntime.Value)),
                    Rating = Convert.ToDouble(nudRating.Value)
                });
                if(lstMovies.Items.Count != Tree.Count)
                {
                    lstMovies.Items.Clear();
                    lstMovies.Items.AddRange(Tree.Display.ToArray());
                }
                txtDirector.Clear();
                txtTitle.Clear();
                dtpRelease.Value = DateTime.Today;
                nudRating.Value = 0m;
                nudRuntime.Value = 0m;
            }
        }

        private void BtnFind_Click(object sender, EventArgs e)
        {
            if (txtFindTitle.Text.Length == 0)
                MessageBox.Show("Please enter a title");
            else
            {
                MovieNode partial = new MovieNode
                {
                    Title = txtFindTitle.Text,
                    ReleaseDate = new DateTime(Convert.ToInt32(nudYear.Value), 1, 1)
                };
                MessageBox.Show(Tree.Find(partial) != null ? "You own this move" : "You don't own this move");
            }
        }
    }
}
