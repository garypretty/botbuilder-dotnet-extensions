# Bot Builder Extensions for Bot Builder v4 SDK

A collection of extensions for use when building bots with the Bot Builder v4 SDK.

## Installation

Each extension, such as middleware or recognizers, is available individually from NuGet. See each individual component description for installation details and links.

## Middleware

The following pieces of middleware are currently available;

* [Handle Activity Type Middleware](/tree/master/libraries/GaryPretty.Bot.Builder.Middleware.HandleActivityType) - Middleware component which allows you to respond to different types of incoming activities, e.g. send a greeting, or even filter out activities you do not care about altogether.


## Recognizers

* [Fuzzy Matching Recognizer](/tree/master/libraries/GaryPretty.Bot.Builder.Recognizers/FuzzyRecognizer) - A recognizer that allows you to use fuzzy matching to compare strings.  Useful in situations such as when a user make a spelling mistake etc. When the recognizer is used a list of matches, along with confidence scores, are returned.
