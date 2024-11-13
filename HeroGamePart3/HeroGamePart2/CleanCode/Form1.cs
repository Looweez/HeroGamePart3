using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CleanCode
{
    public partial class Form1 : Form
    {
        private GameEngine gameEngine;
        public Form1()
        {
            InitializeComponent();

            gameEngine = new GameEngine(10);
            UpdateDisplay();
        }

        public void UpdateDisplay()
        {

            lblDisplay.Text = gameEngine.ToString(); // display game

            lblHeroStats.Text = "X: " + gameEngine.level.Hero.Position.X.ToString() +
                " || Y: " + gameEngine.level.Hero.Position.Y.ToString() +
                "\n" + gameEngine.HeroStats + "\n\nLevel: " + gameEngine.CurrentLevel;
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        //attacking ------------------------------------------------------------
        private void btnAtkUp_Click(object sender, EventArgs e)
        {

            gameEngine.TriggerAttack(Direction.Up);
            UpdateDisplay();
        }

        private void btnAtkRight_Click(object sender, EventArgs e)
        {
            gameEngine.TriggerAttack(Direction.Right);
            UpdateDisplay();
        }

        private void btnAtkDown_Click(object sender, EventArgs e)
        {
            gameEngine.TriggerAttack(Direction.Down);
            UpdateDisplay();
        }

        private void btnAtkLeft_Click(object sender, EventArgs e)
        {
            gameEngine.TriggerAttack(Direction.Left);
            UpdateDisplay();
        }

        //movement ------------------------------------------------------------
        private void btnUp_Click(object sender, EventArgs e)
        {
            gameEngine.TriggerMovement(Direction.Up);
            UpdateDisplay();
        }

        private void btnRight_Click(object sender, EventArgs e)
        {
            gameEngine.TriggerMovement(Direction.Right);
            UpdateDisplay();
        }

        private void btnDown_Click(object sender, EventArgs e)
        {
            gameEngine.TriggerMovement(Direction.Down);
            UpdateDisplay();
        }

        private void btnLeft_Click(object sender, EventArgs e)
        {
            gameEngine.TriggerMovement(Direction.Left);
            UpdateDisplay();
        }

        private void lblHeroStats_Click(object sender, EventArgs e)
        {

        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            gameEngine.SaveGame();
        }

        private void btnLoad_Click(object sender, EventArgs e)
        {
            gameEngine.LoadGame();
        }
    }
}
