using System;
using System.Drawing;
using System.Windows.Forms;
using Game.Domain;

namespace Game.Views
{
    public class MainForm : Form
    {
        private readonly Controller controller;
        private readonly StageDrawer stageDrawer;

        public MainForm(Controller controller)
        {
            this.controller = controller;
            stageDrawer = new StageDrawer(Size, controller.Game);
            SetCommonSettings();
        }

        protected override void OnKeyDown(KeyEventArgs e) => controller.OnKeyDown(e);

        protected override void OnResize(EventArgs e) => stageDrawer.SetSize(Size);

        protected override void OnPaint(PaintEventArgs e)
        {
            switch (controller.Game.Stage)
            {
                case GameStage.Main:
                    stageDrawer.DrawStartScreen(e);
                    break;
                case GameStage.Playing:
                    stageDrawer.DrawPlayingScreen(e);
                    break;
                case GameStage.Finished:
                    stageDrawer.DrawFinishScreen(e);
                    break;
            }
        }

        private void SetCommonSettings()
        {
            SetStyle(
                ControlStyles.AllPaintingInWmPaint |
                ControlStyles.UserPaint |
                ControlStyles.DoubleBuffer,
                true);
            WindowState = FormWindowState.Maximized;
            MinimumSize = new Size(888, 500);
        }
    }
}