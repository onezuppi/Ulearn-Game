using System;

namespace Game.Domain
{
    public class GameModel
    {
        private const double OneLoopTime = 0.5;
        private const int MaxComplexity = 6;

        private readonly LevelGenerator generator = new LevelGenerator();

        private readonly RecordSaver recordSaver;
        public GameStage Stage { get; private set; } = GameStage.Main;
        public double Time { get; private set; }
        public LevelBase CurrentLevel { get; private set; }
        public int Points { get; private set; }

        public bool IsPlaying => Stage == GameStage.Playing;
        public int Record => recordSaver.Record;

        public GameModel(bool deleteSavedRecord = false)
        {
            recordSaver = new RecordSaver(deleteSavedRecord);
        }


        public void Start()
        {
            if (IsPlaying)
                throw new GameExceptions.GameStageMustNotBePlayingException();
            Stage = GameStage.Playing;
            Points = 0;
            Time = 100;
            CurrentLevel = generator.Generate(MaxComplexity);
        }

        public void NextRound()
        {
            if (!IsPlaying)
                throw new GameExceptions.GameStageMustBePlayingException();
            Points += CurrentLevel.Reward;
            UpdateTime();
            CurrentLevel = generator.Generate(MaxComplexity);
        }

        public void MakeMove(bool move)
        {
            if (!IsPlaying)
                throw new GameExceptions.GameStageMustBePlayingException();
            if (move == CurrentLevel.Answer)
                NextRound();
            else
                Over();
            recordSaver.UpdateRecord(Points);
        }

        public void MakeGameLoop()
        {
            if (!IsPlaying)
                throw new GameExceptions.GameStageMustBePlayingException();
            Time -= OneLoopTime;
            if (Time <= 0)
                Over();
        }

        private void UpdateTime()
        {
            if (!IsPlaying)
                throw new GameExceptions.GameStageMustBePlayingException();
            Time = Math.Max(Math.Min(Time + 50 / (Points + 1), 100), 60);
        }

        private void Over()
        {
            if (!IsPlaying)
                throw new GameExceptions.GameStageMustBePlayingException();
            Stage = GameStage.Finished;
        }
    }
}