using System;

namespace CodiblyTest.Mailer.Core.Exceptions
{
    public class EntityNotFoundException : ApplicationException
    {
        public EntityNotFoundException(int entityId)
            : base($"Entity with id = {entityId} not found.")
        {
        }
    }
}
