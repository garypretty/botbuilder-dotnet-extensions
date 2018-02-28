using System;
using System.Threading.Tasks;
using Microsoft.Bot.Builder;
using Microsoft.Bot.Builder.Middleware;

namespace Bot.Builder.ToyBox.Middleware
{
    public class ActivityFilterMiddleware : IReceiveActivity
    {
        private readonly string _activityType;
        private readonly ActivityFilterHandler _activityFilterHandler;
        
        public ActivityFilterMiddleware(string activityType, ActivityFilterHandler handler)
        {
            if(string.IsNullOrEmpty(activityType))
                throw new ArgumentNullException(nameof(activityType));

            _activityType = activityType;
            _activityFilterHandler = handler;
        }

        public async Task ReceiveActivity(IBotContext context, MiddlewareSet.NextDelegate next)
        {
            if (string.Equals(context.Request.Type, _activityType, StringComparison.InvariantCultureIgnoreCase))
            {
                await _activityFilterHandler.Invoke(context, next);
            }
        }

        public delegate Task ActivityFilterHandler(IBotContext context, MiddlewareSet.NextDelegate next);
    }
}
