using System;
using System.Threading.Tasks;
using Microsoft.Bot.Builder;

namespace GaryPretty.Bot.Builder.Extensions.Middleware
{
    public class HandleActivityTypeMiddleware : IMiddleware
    {
        /// <summary>
        /// The type of Activity that the middleware should check for. e.g. ConversationUpdated or Message
        /// </summary>
        private readonly string _activityType;

        /// <summary>
        /// Handler to call when a matching Activity type is received
        /// </summary>
        private readonly ActivityTypeHandler _activityTypeHandler;

        /// <summary>
        /// Middleware to call when a matching Activity type is received
        /// </summary>
        private readonly IMiddleware _nextMiddleware;

        public HandleActivityTypeMiddleware(string activityType, ActivityTypeHandler handler)
        {
            if (string.IsNullOrEmpty(activityType))
                throw new ArgumentNullException(nameof(activityType));

            // Activity types can be found in ActivityTypes enum
            _activityType = activityType;
            _activityTypeHandler = handler;
        }

        public HandleActivityTypeMiddleware(string activityType, IMiddleware nextMiddleware)
        {
            // Activity types can be found in ActivityTypes enum
            _activityType = activityType;
            _nextMiddleware = nextMiddleware ?? throw new ArgumentNullException(nameof(activityType));
        }

        public delegate Task ActivityTypeHandler(IBotContext context, MiddlewareSet.NextDelegate next);

        public async Task OnProcessRequest(IBotContext context, MiddlewareSet.NextDelegate next)
        {
            if (string.Equals(context.Request.Type, _activityType, StringComparison.InvariantCultureIgnoreCase))
            {
                // if the incoming Activity type matches the type of activity we are checking for then
                // invoke our handler or next middleware (whevever has been supplied via constructor)

                if (_activityTypeHandler != null)
                {
                    await _activityTypeHandler.Invoke(context, next).ConfigureAwait(false);
                }
                else
                {
                    await _nextMiddleware.OnProcessRequest(context, next).ConfigureAwait(false);
                }
            }
            else
            {
                // If the incoming Activity is not a match then continue routing
                await next().ConfigureAwait(false);
            }
        }
    }
}
