using System;

namespace Grafos.TrabalhoPraticoUm.Shared.Exceptions
{
    public class FileIsNullOrEmptyException: Exception
    {
        public FileIsNullOrEmptyException()
        {
        }

        public FileIsNullOrEmptyException(string message): base(message)
        {
        }

        public FileIsNullOrEmptyException(string message, Exception inner): base(message, inner)
        {
        }
    }
}
