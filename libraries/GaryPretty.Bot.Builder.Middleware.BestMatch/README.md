## Best Match Middleware

This is part of the [Bot Builder v4 Extensions](https://github.com/garypretty/botbuilder-dotnet-extensions/) project which contains various pieces pf middleware, recognizers and other components for use with the Bot Builder .NET SDK v4.

This piece of middleware will allow you to match a message receieved from a bot user against a list of strings and then carry out an appropriate action. Matching does not have to be exact and you can set the threshold as to how closely the message should match with an item in the list.

Available via [NuGet](https://www.nuget.org/packages/GaryPretty.Bot.Builder.Middleware.BestMatch/).

Install into your project using the following command in the package manager;
```
    PM> Install-Package GaryPretty.Bot.Builder.Middleware.BestMatch
```

BestMatchMiddleware is an abstract class, so you need to implement it yourself to use it, before adding it to your middleware pipeline.
For example, you might use it to respond to common 'manners', such as greetings and salutations. e.g.

```cs
    public class CommonResponsesBestMatchMiddleware : BestMatchMiddleware
    {
		// On your middleware you add a method for each group of phrases that you wish
		// to respond to. Here we are responding to common greetings.  This also shows your
		// the various optional parameters available;
		//  - threshold (defaults to 0.6 and used to determine how close the incoming phrase should be to the phrases within the list)
		//  - ignoreCase (defaults to true)
		//  - ignoreAlphanumericCharacters (defaults to true by removing alphanumeric characters before matching)
        [BestMatch(new string[] { "Hi", "Hi There", "Hello there", "Hey", "Hello",
                "Hey there", "Greetings", "Good morning", "Good afternoon", "Good evening", "Good day" },
            threshold: 0.5, ignoreCase: false, ignoreNonAlphaNumericCharacters: false)]
        public async Task HandleGreeting(IBotContext context, string messageText, MiddlewareSet.NextDelegate next)
        {
            await context.SendActivity("Well hello there. What can I do for you today?");

			// If you wish to call the next piece of middleware and eventually call 
			// your bot you need to call next() by uncommenting the line below. If you
			// do not call next using the line below then execution will stop here.
			// await next();
        }

        [BestMatch(new string[] { "how goes it", "how do", "hows it going", "how are you",
            "how do you feel", "whats up", "sup", "hows things" })]
        public async Task HandleStatusRequest(IBotContext context, string messageText, MiddlewareSet.NextDelegate next)
        {
            await context.SendActivity("I am great.");
        }

        [BestMatch(new string[] { "bye", "bye bye", "got to go",
            "see you later", "laters", "adios" })]
        public async Task HandleGoodbye(IBotContext context, string messageText, MiddlewareSet.NextDelegate next)
        {
            await context.SendActivity("Bye");   
        }

		// If this method is not overridden the default behaviour is to await next() to call 
		// the next component in the pipeline, but you can override as shown below if you wanted 
		// to send an additional message or do anything else
        public override async Task NoMatchHandler(IBotContext context, string messageText, MiddlewareSet.NextDelegate next)
        {
			await context.SendActivities("I am not sure what you wanted...");
            await next();
        }
    }
```

Once you have implemented your middleware class you can add it to the pipeline as shown below.

```cs
	middleware.Add(new CommonResponsesBestMatchMiddleware());
```