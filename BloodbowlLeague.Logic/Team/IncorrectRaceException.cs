using System;
using System.Runtime.Serialization;

namespace BloodbowlLeague.Logic.Team
{
    [Serializable]
    public class IncorrectRaceException : Exception
    {
        public IncorrectRaceException()
        {
        }

        public IncorrectRaceException(string message) : base(message)
        {
        }

        public IncorrectRaceException(string message, Exception inner) : base(message, inner)
        {
        }

        protected IncorrectRaceException(
            SerializationInfo info,
            StreamingContext context) : base(info, context)
        {
        }
    }
}