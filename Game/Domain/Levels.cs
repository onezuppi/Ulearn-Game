using System.Collections.Generic;

namespace Game.Domain
{
    public static class Levels
    {
        public static readonly Dictionary<LevelType, int> Rewards = new Dictionary<LevelType, int>
        {
            {LevelType.SimpleLevel, 1},
            {LevelType.ShiverLevel, 1},
            {LevelType.MirroredLevel, 2},
            {LevelType.OpacityLevel, 3},
            {LevelType.ReversedLevel, 2},
            {LevelType.ExtinguishedLevel, 3},
            {LevelType.FadedLevel, 2},
            {LevelType.StrikethroughLevel, 1},
            {LevelType.TimeBarLevel, 2},
            {LevelType.PointsLevel, 2},
            {LevelType.JumpingLevel, 4},
            {LevelType.WithoutSeveralLettersLevel, 1},
            {LevelType.VerticalLevel, 1},
        };

        public enum LevelType
        {
            SimpleLevel,
            ShiverLevel,
            MirroredLevel,
            OpacityLevel,
            ReversedLevel,
            ExtinguishedLevel,
            FadedLevel,
            StrikethroughLevel,
            TimeBarLevel,
            PointsLevel,
            JumpingLevel,
            WithoutSeveralLettersLevel,
            VerticalLevel
        }
    }
}