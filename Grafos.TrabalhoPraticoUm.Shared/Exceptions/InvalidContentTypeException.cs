using System;

namespace Grafos.TrabalhoPraticoUm.Shared.Exceptions
{
    public class InvalidContentTypeException : Exception
    {
        public InvalidContentTypeException()
        {
        }

        public InvalidContentTypeException(string message) : base(message)
        {
        }

        public InvalidContentTypeException(string message, Exception inner) : base(message, inner)
        {
        }
    }
}
