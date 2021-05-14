using System;

namespace Game.Domain
{
    public static class GameExceptions
    {
        public class GameStageMustBePlayingException : Exception
        {
            public GameStageMustBePlayingException() : base("Game stage must be playing!")
            {
            }
        }

        public class GameStageMustNotBePlayingException : Exception
        {
            public GameStageMustNotBePlayingException() : base("Game stage must not be playing!")
            {
            }
        }
    }
}