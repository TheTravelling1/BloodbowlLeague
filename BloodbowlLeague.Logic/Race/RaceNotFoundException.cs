using System;
using System.Runtime.Serialization;

namespace BloodbowlLeague.Logic
{
    [Serializable]
    public class RaceNotFoundException : Exception
    {
        public RaceNotFoundException()
        {
        }

        public RaceNotFoundException(string message) : base(message)
        {
        }

        public RaceNotFoundException(string message, Exception inner) : base(message, inner)
        {
        }

        protected RaceNotFoundException(
            SerializationInfo info,
            StreamingContext context) : base(info, context)
        {
        }
    }
}
