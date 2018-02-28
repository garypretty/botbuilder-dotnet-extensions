using System.Threading.Tasks;
using Microsoft.Bot.Builder;
using Microsoft.Bot.Builder.Middleware;
using Microsoft.Bot.Schema;

namespace Bot.Builder.ToyBox.Middleware
{
    public class ShowTypingMiddleware : IReceiveActivity
    {
        /// <summary>
        /// (Optional) initial delay before sending first typing indicator. Defaults to 500ms.
        /// </summary>
        private readonly int _delay;

        /// <summary>
        /// (Optional) rate at which additional typing indicators will be sent. Defaults to every 2000ms.
        /// </summary>
        private readonly int _freqency;

        public ShowTypingMiddleware(int delay = 500, int frequency = 2000)
        {
            _delay = delay;
            _freqency = frequency;
        }

        public async Task ReceiveActivity(IBotContext context, MiddlewareSet.NextDelegate next)
        {
            if (context.Request.Type == ActivityTypes.Message)
            {
                var typingActivity = new Activity
                {
                    Type = ActivityTypes.Typing,
                    RelatesTo = context.ConversationReference
                };
                context.Reply(typingActivity);
            }

            // dont await this
            next().ConfigureAwait(false);

            // check the conv state and send typing activity if the response has not been sent yet.
        }
    }
}
