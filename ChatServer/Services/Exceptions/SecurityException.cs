﻿using System;

namespace Services.Exceptions
{
    public class SecurityException : Exception
    {
        public SecurityException(string message) : base(message)
        {

        }
    }
}
