using System;

namespace Notes.Application.Interfaces
{
    public interface ICurrentUserService
    {
        public Guid UserId { get; }
    }
}
