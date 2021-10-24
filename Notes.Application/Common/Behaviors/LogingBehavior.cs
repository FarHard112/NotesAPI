using MediatR;
using Notes.Application.Interfaces;
using Serilog;
using System.Threading;
using System.Threading.Tasks;

namespace Notes.Application.Common.Behaviors
{
    public class LogingBehavior<TRequest, TResponse> :
        IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
    {
        ICurrentUserService CurrentUserService;

        public LogingBehavior(ICurrentUserService currentUserService)
        {
            CurrentUserService = currentUserService;
        }

        public async Task<TResponse> Handle(TRequest
            request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            var requestName = typeof(TRequest).Name;
            var userId = CurrentUserService.UserId;
            Log.Information
                ("Notes Request:{@Name} {@UserId} {@Request}", requestName, userId, request);
            var response = await next();
            return response;
        }
    }
}
