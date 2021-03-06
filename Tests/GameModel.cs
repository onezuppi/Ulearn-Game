using System;
using Game.Domain;
using NUnit.Framework;

namespace Tests
{
    public class GameModel
    {
        private const int MoveCount = 1000;

        [Test]
        public void Create_Test()
        {
            var game = new Game.Domain.GameModel();
            Assert.Zero(game.Points);
            Assert.AreEqual(GameStage.Main, game.Stage);
            Assert.AreEqual(0, game.Time);
            Assert.IsFalse(game.IsPlaying);
            Assert.IsNull(game.CurrentLevel);
        }

        [Test]
        public void Start_Test() => TestStartTesting(GetStartedGame());

        [Test]
        public void Start_ShouldThrowException_WhenGameAlreadyStarted()
        {
            var game = GetStartedGame();
            Assert.Catch<GameExceptions.GameStageMustNotBePlayingException>(game.Start);
        }

        [Test]
        public void PublicMethods_ShouldThrowException_WhenGameNotStarted()
        {
            var game = new Game.Domain.GameModel();
            Assert.Catch<GameExceptions.GameStageMustBePlayingException>(game.NextRound);
            Assert.Catch<GameExceptions.GameStageMustBePlayingException>(game.MakeGameLoop);
            Assert.Catch<GameExceptions.GameStageMustBePlayingException>(() => { game.MakeMove(false); });
            Assert.Catch<GameExceptions.GameStageMustBePlayingException>(() => { game.MakeMove(true); });
        }

        [TestCase(true)]
        [TestCase(false)]
        public void MakeMove_ShouldNotThrowException_WhenGameStarted(bool move)
        {
            var game = GetStartedGame();
            Assert.DoesNotThrow(() => { game.MakeMove(move); });
        }

        [Test]
        public void MakeMove_ShouldGetNextRound_WhenCorrectAnswers() =>
            TestMakeMove_ShouldGetNextRound_WhenCorrectAnswers(GetStartedGame(), MoveCount);


        [Test]
        public void MakeMove_ShouldGetGameOver_WhenIncorrectAnswers() =>
            TestMakeMove_ShouldGetGameOver_WhenIncorrectAnswers(GetStartedGame());

        [Test]
        public void Start_ShouldRestartGame_AfterLose()
        {
            var game = GetStartedGame();
            game.MakeMove(!game.CurrentLevel.Answer);
            game.Start();
            TestStartTesting(game);
        }

        [Test]
        public void MakeGameLoop_ShouldGetGameOver_AfterTimeLimit()
        {
            var game = GetStartedGame();
            while (game.Time > 0)
                game.MakeGameLoop();
            Assert.AreEqual(GameStage.Finished, game.Stage);
        }


        [Test]
        public void MakeGameLoop_ShouldDecreaseTime()
        {
            var game = GetStartedGame();
            var time = game.Time;
            game.MakeGameLoop();
            Assert.IsTrue(time > game.Time);
        }

        [Test]
        public void Time_ShouldBeIncreased_AfterCorrectAnswer()
        {
            var game = GetStartedGame();
            game.MakeGameLoop();
            var time = game.Time;
            game.MakeMove(game.CurrentLevel.Answer);
            Assert.IsTrue(game.Time > time);
        }

        [Test]
        public void Record_ShouldBeDeleted() => GetStartedGameWithoutRecord();


        [Test]
        public void Record_ShouldBeSaved() => GetStartedGameWithRecord();

        [Test]
        public void Record_ShouldBeDeletedAfterSaving()
        {
            GetStartedGameWithRecord();
            GetStartedGameWithoutRecord();
        }

        [Test]
        public void Game_Test()
        {
            var generator = new Random();
            var game = GetStartedGame();
            for (var i = 0; i < MoveCount * MoveCount; i++)
            {
                game.MakeGameLoop();
                if (game.Time <= 0)
                {
                    Assert.AreEqual(GameStage.Finished, game.Stage);
                    game.Start();
                }

                if (generator.Next(0, 100) <= 80) continue;

                if (generator.Next(0, 100) > 95)
                {
                    TestMakeMove_ShouldGetGameOver_WhenIncorrectAnswers(game);
                    game.Start();
                }
                else
                    TestMakeMove_ShouldGetNextRound_WhenCorrectAnswers(game);
            }
        }

        private static Game.Domain.GameModel GetStartedGameWithoutRecord()
        {
            var game = GetStartedGame(true);
            Assert.Zero(game.Record);
            return game;
        }

        private static void GetStartedGameWithRecord()
        {
            var game = GetStartedGameWithoutRecord();
            TestMakeMove_ShouldGetNextRound_WhenCorrectAnswers(game);
            var record = game.Points;
            game = GetStartedGame();
            Assert.AreEqual(record, game.Record);
        }


        private static Game.Domain.GameModel GetStartedGame(bool deleteSavedRecord = false)
        {
            var game = new Game.Domain.GameModel(deleteSavedRecord);
            game.Start();
            return game;
        }

        private static void TestStartTesting(Game.Domain.GameModel game)
        {
            Assert.NotZero(game.Time);
            Assert.Zero(game.Points);
            Assert.AreEqual(GameStage.Playing, game.Stage);
            Assert.IsNotNull(game.CurrentLevel);
        }

        private static void TestMakeMove_ShouldGetNextRound_WhenCorrectAnswers(Game.Domain.GameModel game,
            int iterationCount = 1)
        {
            var points = game.Points;
            for (var i = 0; i < iterationCount; i++)
            {
                var level = game.CurrentLevel;
                game.MakeMove(game.CurrentLevel.Answer);
                points += level.Reward;
                Assert.AreEqual(points, game.Points);
                Assert.AreNotEqual(level, game.CurrentLevel);
            }
        }

        private void TestMakeMove_ShouldGetGameOver_WhenIncorrectAnswers(Game.Domain.GameModel game)
        {
            game.MakeMove(!game.CurrentLevel.Answer);
            Assert.AreEqual(GameStage.Finished, game.Stage);
        }
    }
}