using System;

namespace Game.Domain
{
    public static class GameInscriptions
    {
        public static readonly string GameRules = string.Join(Environment.NewLine, 
            "На Экране будет появлятся слово",
            "nокрашенное в какой-то цвет, если",
            "цвет и слово совпадают, то",
            "жми ->, иначе <-.",
            "\n",
            "Жми Enter чтобы начать играть!");

        public const string GameOver = "Игра окончена!";
        public const string YourPoints = "Твои очки: {0}!";
        public const string Restart = "Нажми Enter чтобы начать сначала!";
    }
}