using System;
using System.Windows.Forms;
using Game.Domain;
using Game.Views;

namespace Game
{
    public class Controller
    {
        public readonly GameModel Game;
        public Form MainForm { get; }

        public Controller()
        {
            Game = new GameModel();
            MainForm = new MainForm(this);
            var timer = new Timer {Interval = 20};
            timer.Tick += Update;
            timer.Start();
        }
        
        private void Update(object sender, EventArgs e)
        {
            if (Game.IsPlaying)
                Game.MakeGameLoop();
            MainForm.Invalidate();
        }
        
        public void OnKeyDown(KeyEventArgs e)
        {
            if(Game.IsPlaying) 
                switch (e.KeyCode)
                {
                    case Keys.Left:
                        Game.MakeMove(false);
                        break;
                    case Keys.Right:
                        Game.MakeMove(true);
                        break;
                }
            else if (e.KeyCode == Keys.Enter)
                Game.Start();
        }
    }
}