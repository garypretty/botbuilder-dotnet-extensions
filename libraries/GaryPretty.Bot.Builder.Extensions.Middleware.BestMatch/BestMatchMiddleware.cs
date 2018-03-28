using System;
using System.Threading.Tasks;
using Microsoft.Bot.Builder;

namespace GaryPretty.Bot.Builder.Extensions.Middleware.BestMatch
{
    public class BestMatchMiddleware : IMiddleware
    {
        public Task OnProcessRequest(IBotContext context, MiddlewareSet.NextDelegate next)
        {
            throw new NotImplementedException();
        }
    }
}
