using System;
using System.Drawing;
using System.Linq;
using Game.Domain;

namespace Game.Views
{
    public class LevelProperties
    {
        private const int UpdateForEveryIteration = 3;

        public readonly LevelBase Level;
        public string Task { get; private set; }
        public Color TaskColor { get; private set; }

        public (double X, double Y) ShiftRatio { get; private set; } = (0, 0);
        public FontStyle TaskFontStyle { get; private set; } = FontStyle.Regular;
        public Color Background { get; private set; } = Colors.Background;
        public Brush PointsBrush { get; private set; } = Domain.Brushes.PointsBrush;
        public Brush TimeBarBrush { get; private set; } = Domain.Brushes.TimeBarBrush;

        public StringFormat TaskStringFormat { get; private set; } = new StringFormat
            {LineAlignment = StringAlignment.Center, Alignment = StringAlignment.Center};

        private const double MaxJumpRatio = 0.2;
        private readonly Random generator = new Random();

        private (bool IsIncreasing, double Ratio, double Min, double Max, double Delta) size = (false, 1, 0.95, 1.05,
            0.01);

        private int opacity = 100;
        private int iteration = 0;

        public double SizeRatio => size.Ratio;

        public LevelProperties(LevelBase level)
        {
            Level = level;
            if (level != null)
            {
                (Task, TaskColor) = (level.ColorName, level.Color);
                ConfigureLevel();
            }
        }

        public void CalculateDynamicLevels()
        {
            iteration++;

            if (Level.Is(Levels.LevelType.ShiverLevel))
                CalculateParamsForShiverLevel();

            if (Level.Is(Levels.LevelType.ExtinguishedLevel))
                CalculateParamsForExtinguishedLevel();

            if (Level.Is(Levels.LevelType.FadedLevel))
                CalculateParamsForFadedLevel();

            if (Level.Is(Levels.LevelType.JumpingLevel) && iteration % UpdateForEveryIteration == 0)
                CalculateParamsForJumpingLevel();
        }

        private void ConfigureLevel()
        {
            if (Level.Is(Levels.LevelType.MirroredLevel))
                Task = new string(Task.Reverse().ToArray());

            if (Level.Is(Levels.LevelType.OpacityLevel))
                TaskColor = Color.FromArgb(generator.Next(80, 100), TaskColor.R, TaskColor.G, TaskColor.B);

            if (Level.Is(Levels.LevelType.StrikethroughLevel))
                TaskFontStyle = FontStyle.Strikeout;

            if (Level.Is(Levels.LevelType.WithoutSeveralLettersLevel))
                Task = string.Join("", Task.Select(x => generator.Next(0, 100) > 70 ? '_' : x));

            if (Level.Is(Levels.LevelType.VerticalLevel))
                (TaskStringFormat.FormatFlags, size.Ratio) = (StringFormatFlags.DirectionVertical, 0.5);

            if (Level.Is(Levels.LevelType.TimeBarLevel))
                (TimeBarBrush, TaskColor, Task) = (new SolidBrush(TaskColor), Colors.TimeBar, $"Таймбар:\n\t{Task}");

            else if (Level.Is(Levels.LevelType.PointsLevel))
                (PointsBrush, TaskColor, Task) = (new SolidBrush(TaskColor), Colors.Information, $"Счёт:\n\t{Task}");
            
            else if (Level.Is(Levels.LevelType.ReversedLevel))
                (Task, Background, TaskColor, TimeBarBrush) =
                    ($"Фон: {Task}", TaskColor, Background, new SolidBrush(Background));
        }

        private void CalculateParamsForShiverLevel()
        {
            if (size.IsIncreasing)
                size.Ratio += size.Delta;
            else
                size.Ratio -= size.Delta;
            if (size.Ratio > size.Max || size.Ratio < size.Min)
                size.IsIncreasing = !size.IsIncreasing;
        }

        private void CalculateParamsForExtinguishedLevel()
        {
            opacity = Math.Max(opacity - 2, 0);
            TaskColor = Color.FromArgb(opacity, TaskColor.R, TaskColor.G, TaskColor.B);
        }

        private void CalculateParamsForFadedLevel()
        {
            opacity = opacity < 30 ? 100 : opacity - 2;
            TaskColor = Color.FromArgb(opacity, TaskColor.R, TaskColor.G, TaskColor.B);
        }

        private void CalculateParamsForJumpingLevel()
        {
            ShiftRatio = (generator.NextDouble() * MaxJumpRatio * RandomSign,
                generator.NextDouble() * MaxJumpRatio * RandomSign);
        }

        private int RandomSign => generator.Next(100) > 50 ? 1 : -1;
    }
}