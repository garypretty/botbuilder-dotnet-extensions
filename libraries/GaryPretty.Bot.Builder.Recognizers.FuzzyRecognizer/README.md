## Fuzzy Matching Recognizer

This is part of the [Bot Builder v4 Extensions](../../) project which contains various pieces pf middleware, recognizers and other components for use with the Bot Builder .NET SDK v4.

The Fuzzy Recognizer allows you to compare a specified string against a list of 1 or more other strings.  The result is a list of string that are close enough to match with the comparison string (above a given threshold which you can set).  This can be useful when taking input from the user where spelling mistakes may be common, e.g. names or people, companies or other entities etc.

The Fuzzy Recognizer is available via [NuGet](https://www.nuget.org/packages/GaryPretty.Bot.Builder.Recognizers.FuzzyRecognizer/).

Install it into your project using the following command in the package manager;
```
    PM> Install-Package GaryPretty.Bot.Builder.Recognizers.FuzzyRecognizer
```
