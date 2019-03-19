using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ComboProject
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            openFileDialog.InitialDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
        }

        private void browse_Click(object sender, EventArgs e)
        {
            DialogResult result = openFileDialog.ShowDialog();
            if (result == DialogResult.OK)
            {
                file = openFileDialog.FileName;
                movelist = LoadMoves.getMovelist(file);
                fileTextBox.Text = file;
                simulate.Enabled = true;

                List<LoadMoves.Chain> chains = LoadMoves.getChains("C:\\Users\\Taylor\\source\\repos\\ComboProject\\ComboProject\\bin\\Debug\\doublechains.csv", movelist);
                ComboGenerator gen = new ComboGenerator(movelist, chains);

                /*using (var w = new StreamWriter("C:\\Users\\Taylor\\source\\repos\\ComboProject\\ComboProject\\bin\\Debug\\test.csv"))
                {
                    for (int i = 0; i < 100000; i++)
                    {
                        var first = gen.generateRandomCombo();
                        var second = ComboSimulator.getComboDamage(first, movelist);
                        var line = string.Format("{0},{1}\n", first, second.ToString());
                        w.Write(line);
                        w.Flush();
                    }
                }*/

                GeneticCombo gc = new GeneticCombo(gen, movelist);
                gc.Start();
            }
        }

        private void simulate_Click(object sender, EventArgs e)
        {
            int damage = ComboSimulator.getComboDamage(comboTextBox.Text, movelist);
            damageTextBox.Text = damage.ToString();
        }

        private List<LoadMoves.Attack> movelist;
        private string file;
    }
}