# Bot Builder Extensions for Bot Builder v4 SDK

A collection of extensions for use when building bots with the Bot Builder v4 SDK.

## Middleware

### Handle Activity Type Middleware

This piece of middleware will allow you you to handle incoming activities of specific types, such as 'conversationUpdate' or 'contactRelationUpdate'.

To use the middleware, add it to the pipeline:

```cs
middleware.Add(new HandleActivityTypeMiddleware(ActivityTypes.ConversationUpdate, async (context, next) =>
                    {
                        // here you can do whatever you want to respond to the activity
                        await context.SendActivity("Hi! Welcome. I am the bot :)");

                        // If you want to continue routing through the pipeline to additional
                        // middleware and to the bot itself then call the following line.
                        await next().CongifureAwait(false);

                        // If you want routing to stop here and for no further processing to happen
                        // then call the following line
                        // return Task.CompletedTask;
                    }));
```
