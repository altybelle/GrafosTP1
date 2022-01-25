using System;

namespace Grafos.TrabalhoPraticoUm.Shared.Exceptions
{
    public class NoFileWasLoadedException : Exception
    {
        public NoFileWasLoadedException() : base()
        {
        }

        public NoFileWasLoadedException(string message) : base(message)
        {
        }

        public NoFileWasLoadedException(string message, Exception inner) : base(message, inner)
        {
        }
    }
}
