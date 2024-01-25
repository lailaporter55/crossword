using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace Crossword
{
    public partial class Form1 : Form
    {
        Form2 clueWindow = new Form2();
        List<idCells> idc = new List<idCells>();
        public String puzzle = Application.StartupPath + "C:\\Users\\Laila Porter\\OneDrive - rit.edu\\Desktop\\Personal Projects\\crossword\\Crossword\\puzzle1.pzl";
        public Form1()
        {
            InitializeComponent();
            BuildWordList();
        }
        private void exitToolStripMenuItem__Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        private void aboutToolStripMenuItem__Click(object sender, EventArgs e)
        {
            MessageBox.Show("Help About", "By Laila");
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            InitializeBoard();
            clueWindow.SetDesktopLocation(this.Location.X + this.Width + 1, this.Location.Y);
            clueWindow.StartPosition = FormStartPosition.Manual;

            clueWindow.Show();
            clueWindow.clueTable.AutoResizeColumns();
        }
        private void InitializeBoard()
        {
            dataGridView1.BackgroundColor = Color.AliceBlue;
            dataGridView1.DefaultCellStyle.BackColor = Color.AliceBlue;
            for (int i = 0; i < 22; i++)
            {
                dataGridView1.Rows.Add();
            }
            //set width of each column
            foreach (DataGridViewColumn c in dataGridView1.Columns)
            {
                c.Width = dataGridView1.Width / dataGridView1.Columns.Count;

            }
            //set height for rows 
            foreach (DataGridViewRow r in dataGridView1.Rows)
            {
                r.Height = dataGridView1.Height / dataGridView1.Rows.Count;

            }
            //read only 
            for (int row = 0; dataGridView1.Rows.Count > row; row++)
            {
                for (int col = 0; dataGridView1.Columns.Count > col; col++)
                {
                    dataGridView1[col, row].ReadOnly = true;
                }
            }
        }

        private void FormatCell(int row, int col, String letter)
        {
            DataGridViewCell c = dataGridView1[col, row];
            c.Style.BackColor = Color.White;
            c.ReadOnly = false;
            c.Style.SelectionBackColor = Color.LightCyan;
            c.Tag = letter;
        }

        private void Form1_LocationChanged(object sender, EventArgs e)
        {
            clueWindow.SetDesktopLocation(this.Location.X + this.Width + 1, this.Location.Y);
        }

        private void BuildWordList()
        {
            String line = "";
            using (StreamReader s = new StreamReader(puzzle))
            {
                line = s.ReadLine(); //ignore 1st line 
                while ((line = s.ReadLine()) != null)
                {
                    String[] l = line.Split('|');
                    idc.Add(new idCells(Int32.Parse(l[0]), Int32.Parse(l[1]), (l[2]), (l[3]), (l[4]), (l[5])));
                    clueWindow.clueTable.Rows.Add(new String[] { l[3], l[2], l[5] });

                }
            }
        }

        public class idCells
        {
            //creating an object to store info abt a clue

            public int X;
            public int Y;
            public String direction;
            public String number;
            public String word;
            public String clue;

            public idCells(int x, int y, String direction, String number, String word, String clue)
            {
                this.X = x;
                this.Y = y;
                this.direction = direction;
                this.number = number;
                this.word = word;
                this.clue = clue;
            }
        }//end class
    }

}
