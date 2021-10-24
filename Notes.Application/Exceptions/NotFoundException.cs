using System;

namespace Notes.Application.Exceptions
{
    public class NotFoundException : Exception
    {
        public NotFoundException(string name, object key)
            : base($"Entity not found key:{key}\"{name}\"")
        {

        }


    }
}
