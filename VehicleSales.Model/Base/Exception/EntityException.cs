using System;

namespace VehicleSales.Model.Base.Exception
{
    public class EntityException : System.Exception
    {
        public EntityException()
        {
        }

        public EntityException(string message) : base(message)
        {
        }

        public EntityException(string message, System.Exception inner)
            : base(message, inner)
        {
        }
    }
}
