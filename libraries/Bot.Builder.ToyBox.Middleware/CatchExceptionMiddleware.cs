using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Bot.Builder;
using Microsoft.Bot.Builder.Middleware;
using Microsoft.Bot.Schema;

namespace Bot.Builder.ToyBox.Middleware
{
    public class CatchExceptionMiddleware : IReceiveActivity, IContextCreated, ISendActivity
    {
        private readonly CatchExceptionHandler _handler;
        private readonly string _id = new Guid().ToString();
        private readonly string _stateKey;

        public CatchExceptionMiddleware(CatchExceptionHandler handler)
        {
            _handler = handler;
            _stateKey = $"catchErrorCalled{_id}";
        }

        public async Task ReceiveActivity(IBotContext context, MiddlewareSet.NextDelegate next)
        {
            await CatchError(context, "receiveActivity", next);
        }

        public async Task ContextCreated(IBotContext context, MiddlewareSet.NextDelegate next)
        {
            await CatchError(context, "contextCreated", next);
        }

        public async Task SendActivity(IBotContext context, IList<IActivity> activities, MiddlewareSet.NextDelegate next)
        {
            await CatchError(context, "sendActivity", next);
        }

        private async Task CatchError(IBotContext context, string phase, MiddlewareSet.NextDelegate next)
        {
            try
            {
                await next().ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                if (!context.State.ConversationProperties.ContainsKey(_stateKey) 
                    || context.State.ConversationProperties[_stateKey] == false)
                {
                    context.State.ConversationProperties.Add(_stateKey, true);
                    await _handler.Invoke(context, phase, ex);
                }
                else
                {
                    throw;
                }
            }
        }

        public delegate Task CatchExceptionHandler(IBotContext context, string phase, Exception exception);
    }
}
