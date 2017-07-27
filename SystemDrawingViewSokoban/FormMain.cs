using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Filer;
using GameNS;

namespace SystemDrawingViewSokoban
{
    public partial class FormMain : Form, IView
    {
        public Controller Con;
        public FormMain()
        {
            InitializeComponent();
        }
        int TimeCount = 0;
        public void CreateLevel(List<List<Parts>> grid)
        {
            int rows = grid.Count;
            int columns = grid[0].Count;
            for (int row = 0; row < rows; row++)
            {
                for (int column = 0; column < columns; column++)
                {
                    Graphics g = CreateGraphics();
                    int formWidth = Size.Width;
                    int formHeight = Size.Height;
                    int gridWidth = grid[0].Count;
                    int gridHeight = grid.Count;
                    int iconSize = 50;
                    int xStartPos = (formWidth - (gridWidth * iconSize)) / 2;
                    int yStartPos = (formHeight - (gridHeight * iconSize)) / 2;
                    float xLoc = (column * iconSize) + xStartPos;
                    float yLoc = (row * iconSize) + yStartPos;
                    float xSize = iconSize;
                    float ySize = iconSize;

                    char part = (char)grid[row][column];
                    switch (part)
                    {
                        case (char)Parts.Wall:
                            Image Wall = Properties.Resources.wall;
                            g.DrawImage(Wall, xLoc, yLoc, xSize, ySize);
                            break;
                        case (char)Parts.Empty:
                            Image Empty = Properties.Resources.empty;
                            g.DrawImage(Empty, xLoc, yLoc, xSize, ySize);
                            break;
                        case (char)Parts.Player:
                            Image Player = Properties.Resources.player;
                            g.DrawImage(Player, xLoc, yLoc, xSize, ySize);
                            break;
                        case (char)Parts.Goal:
                            Image Goal = Properties.Resources.goal;
                            g.DrawImage(Goal, xLoc, yLoc, xSize, ySize);
                            break;
                        case (char)Parts.Block:
                            Image Block = Properties.Resources.block;
                            g.DrawImage(Block, xLoc, yLoc, xSize, ySize);
                            break;
                        case (char)Parts.BlockOnGoal:
                            Image BlockOnGoal = Properties.Resources.blockongoal;
                            g.DrawImage(BlockOnGoal, xLoc, yLoc, xSize, ySize);
                            break;
                        case (char)Parts.PlayerOnGoal:
                            Image PlayerOnGoal = Properties.Resources.playerongoal;
                            g.DrawImage(PlayerOnGoal, xLoc, yLoc, xSize, ySize);
                            break;
                    }
                }
            }
        }

        public void RebuildLevel(List<List<Parts>> updatedGrid)
        {
            int rows = updatedGrid.Count;
            int columns = updatedGrid.Count;

            for (int row = 0; row < rows; row++)
            {
                for (int column = 0; column < columns; column++)
                {
                    char part = (char)updatedGrid[row][column];

                    Graphics g = CreateGraphics();
                    int formWidth = Size.Width;
                    int formHeight = Size.Height;
                    int gridWidth = updatedGrid[0].Count;
                    int gridHeight = updatedGrid.Count;
                    int iconSize = 50;
                    int xStartPos = (formWidth - (gridWidth * iconSize)) / 2;
                    int yStartPos = (formHeight - (gridHeight * iconSize)) / 2;
                    float xLoc = (column * iconSize) + xStartPos;
                    float yLoc = (row * iconSize) + yStartPos;
                    float xSize = iconSize;
                    float ySize = iconSize;

                    switch (part)
                    {
                        case (char)Parts.Wall:
                            Image Wall = Properties.Resources.wall;
                            g.DrawImage(Wall, xLoc, yLoc, xSize, ySize);
                            break;
                        case (char)Parts.Empty:
                            Image Empty = Properties.Resources.empty;
                            g.DrawImage(Empty, xLoc, yLoc, xSize, ySize);
                            break;
                        case (char)Parts.Player:
                            Image Player = Properties.Resources.player;
                            g.DrawImage(Player, xLoc, yLoc, xSize, ySize);
                            break;
                        case (char)Parts.Goal:
                            Image Goal = Properties.Resources.goal;
                            g.DrawImage(Goal, xLoc, yLoc, xSize, ySize);
                            break;
                        case (char)Parts.Block:
                            Image Block = Properties.Resources.block;
                            g.DrawImage(Block, xLoc, yLoc, xSize, ySize);
                            break;
                        case (char)Parts.BlockOnGoal:
                            Image BlockOnGoal = Properties.Resources.blockongoal;
                            g.DrawImage(BlockOnGoal, xLoc, yLoc, xSize, ySize);
                            break;
                        case (char)Parts.PlayerOnGoal:
                            Image PlayerOnGoal = Properties.Resources.playerongoal;
                            g.DrawImage(PlayerOnGoal, xLoc, yLoc, xSize, ySize);
                            break;
                    }
                }
            }
        }

        public void SetController(Controller controller)
        {
            Con = controller;
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            timer1.Start();
            switch (keyData)
            {
                case Keys.Up:
                    Con.KeyPressed(Direction.Up);
                    break;
                case Keys.Down:
                    Con.KeyPressed(Direction.Down);
                    break;
                case Keys.Left:
                    Con.KeyPressed(Direction.Left);
                    break;
                case Keys.Right:
                    Con.KeyPressed(Direction.Right);
                    break;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        public void MoveCount(int moveCount)
        {
            moveCounter.Text = moveCount.ToString();
        }

        public void Start()
        {
            Con.Go();
        }

        public void Stop()
        {
            timer1.Stop();
            /* When the player wins, show them a message box to notify
             that they have won.*/
            Show("You won with only " + moveCounter.Text + " moves\n" + "In the time of " + timeLabel.Text + " seconds");
            /* Reset level */
            Con.Reset();
            /* Set the moveCounter text back to zero */
            moveCounter.Text = "0";
            timeLabel.Text = "0";
            TimeCount = 0;

        }

        public void Show<T>(T input)
        {
            /* Setup the use of a message box */
            MessageBox.Show(input.ToString());
        }

        private void Reset_Click(object sender, EventArgs e)
        {
            timer1.Stop();
            /* Reset the view to show orignal level */
            Con.Reset();
            /* Set the moveCounter text back to zero */
            moveCounter.Text = "0";
            timeLabel.Text = "0";
            TimeCount = 0;
        }

        private void Save()
        {
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();

            saveFileDialog1.Title = "Save File";
            saveFileDialog1.FileName = "Sokoban Level 1";
            saveFileDialog1.InitialDirectory = "";
            saveFileDialog1.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";

            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                Con.Save(saveFileDialog1.FileName);
            }
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Save();
        }

        private void loadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Con.Go();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            TimeCount++;
            timeLabel.Text = TimeCount.ToString();
        }
    }
}


