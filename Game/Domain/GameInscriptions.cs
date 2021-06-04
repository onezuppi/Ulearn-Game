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
        public const string PlayMusic = @"Нажмите английскую кнопку M, чтобы включить\выключить музыку.";
        public const string YourPoints = "Твой счёт: {0}, рекорд: {1}!";
        public const string Restart = "Нажми Enter чтобы начать сначала!";
        public const string TextPart = "Текст:";
        public const string BackgroundPart = "Фон:";
        public const string PointsPart = "Счёт:";
        public const string TimeBarPart = "Таймбар:";
    }
}