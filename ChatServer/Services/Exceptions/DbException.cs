using System;

namespace Services.Exceptions
{
    public class DbException : Exception
    {
        public DbException(string message) : base(message)
        {

        }
    }
}
