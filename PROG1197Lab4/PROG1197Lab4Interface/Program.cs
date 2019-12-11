using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using PROG1197Lab4Objects;

namespace PROG1197Lab4Interface
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {            
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            MovieTree tree;
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                var result = ofd.ShowDialog();
                if (result == DialogResult.Cancel)
                    return;
                using (FileStream fs = new FileStream(ofd.FileName, FileMode.Open))
                using (StreamReader sr = new StreamReader(fs))
                {
                    List<string> lines = new List<string>();
                    while (!sr.EndOfStream)
                    {
                        lines.Add(sr.ReadLine());
                    }
                    var nodes = lines.Select(l => l.Split(';')).Select(line =>
                    new MovieNode
                    {
                        Title = line[0],
                        ReleaseDate = Convert.ToDateTime(line[1]),
                        Runtime = TimeSpan.FromMinutes(Convert.ToInt32(line[2])),
                        Director = line[3],
                        Rating = Convert.ToDouble(line[4])
                    });
                    tree = new MovieTree(nodes);
                }
            }
            Application.Run(new Form1(tree));
        }
    }
}
