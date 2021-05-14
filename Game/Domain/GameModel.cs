using System;

namespace Game.Domain
{
    public class GameModel
    {
        private const double OneLoopTime = 0.5;
        private readonly LevelGenerator generator = new LevelGenerator();
        public GameStage Stage { get; private set; } = GameStage.Main;
        public double Time { get; private set; } = 100;

        public Level CurrentLevel { get; private set; }
        public int Points { get; private set; }

        public bool IsPlaying => Stage == GameStage.Playing;

        public void Start()
        {
            if (IsPlaying)
                throw new GameExceptions.GameStageMustNotBePlayingException();
            Stage = GameStage.Playing;
            Points = 0;
            ResetTime();
            CurrentLevel = generator.Generate();
        }

        public void NextRound()
        {
            if (!IsPlaying)
                throw new GameExceptions.GameStageMustBePlayingException();
            Points++;
            ResetTime();
            CurrentLevel = generator.Generate();
        }

        public void MakeMove(bool move)
        {
            if (!IsPlaying)
                throw new GameExceptions.GameStageMustBePlayingException();
            if(move == CurrentLevel.Answer)
                NextRound();
            else
                Over();
        }

        public void MakeGameLoop()
        {
            if (!IsPlaying)
                throw new GameExceptions.GameStageMustBePlayingException();
            Time -= OneLoopTime;
            if (Time <= 0)
                Over();
        }
        
        private void ResetTime()
        {
            if (!IsPlaying)
                throw new GameExceptions.GameStageMustBePlayingException();
            Time = Math.Max(Math.Min(Time + 50 / (Points + 1), 100), 45);
        }
        
        private void Over()
        {
            if (!IsPlaying)
                throw new GameExceptions.GameStageMustBePlayingException();
            Stage = GameStage.Finished;
        }
    }
}