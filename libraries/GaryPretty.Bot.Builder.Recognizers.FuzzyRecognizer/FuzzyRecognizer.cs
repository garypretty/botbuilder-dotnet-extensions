using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Chronic;

namespace GaryPretty.Bot.Builder.Recognizers.FuzzyRecognizer
{
    public class FuzzyRecognizer
    {
        private readonly FuzzyRecognizerOptions _fuzzyRecognizerOptions;

        public FuzzyRecognizer(FuzzyRecognizerOptions fuzzyRecognizerOptions = null)
        {
            _fuzzyRecognizerOptions = fuzzyRecognizerOptions ?? new FuzzyRecognizerOptions();
        }

        public async Task<FuzzyRecognizerResult> Recognize(IEnumerable<string> choices, string utterance)
        {
            if (string.IsNullOrEmpty(utterance))
                throw new ArgumentNullException(nameof(utterance));

            if (choices == null)
                throw new ArgumentNullException(nameof(choices));

            return await FindAllMatches(choices, utterance, _fuzzyRecognizerOptions);
        }

        private async static Task<FuzzyRecognizerResult> FindAllMatches(IEnumerable<string> choices, string utterance, FuzzyRecognizerOptions options)
        {
            var result = new FuzzyRecognizerResult()
            {
                Matches = new List<FuzzyMatch>()
            };

            var choicesList = choices as IList<string> ?? choices.ToList();

            if (!choicesList.Any())
                return result;

            var utteranceToCheck = options.IgnoreNonAlphanumeric
                ? utterance.ReplaceAll(@"[^A-Za-z0-9 ]", string.Empty).Trim()
                : utterance;

            var tokens = utterance.Split(' ');

            foreach (var choice in choicesList)
            {
                double score = 0;

                var choiceValue = choice.Trim();

                if (options.IgnoreNonAlphanumeric)
                    choiceValue.ReplaceAll(@"[^A-Za-z0-9 ]", string.Empty);

                if (choiceValue.IndexOf(utteranceToCheck, options.IgnoreCase ? StringComparison.OrdinalIgnoreCase : StringComparison.Ordinal) >= 0)
                {
                    score = (double)decimal.Divide((decimal)utteranceToCheck.Length, (decimal)choiceValue.Length);
                }
                else if (utteranceToCheck.IndexOf(choiceValue, StringComparison.Ordinal) >= 0)
                {
                    score = Math.Min(0.5 + (choiceValue.Length / utteranceToCheck.Length), 0.9);
                }
                else
                {
                    foreach (var token in tokens)
                    {
                        var matched = string.Empty;

                        if (choiceValue.IndexOf(token, options.IgnoreCase ? StringComparison.OrdinalIgnoreCase : StringComparison.Ordinal) >= 0)
                        {
                            matched += token;
                        }

                        score = (double)decimal.Divide((decimal)matched.Length, (decimal)choiceValue.Length);
                    }
                }

                if (score >= options.Threshold)
                {
                    result.Matches.Add(new FuzzyMatch { Choice = choiceValue, Score = score });
                }
            }

            return result;
        }
    }

    public class FuzzyRecognizerResult
    {
        public FuzzyRecognizerResult()
        {
            Matches = new List<FuzzyMatch>();
        }

        public List<FuzzyMatch> Matches { get; set; }
    }

    public class FuzzyRecognizerOptions
    {
        public FuzzyRecognizerOptions(double threshold = 0.6, bool ignoreCase = true, bool ignoreNonAlphanumeric = true)
        {
            Threshold = threshold;
            IgnoreCase = ignoreCase;
            IgnoreNonAlphanumeric = ignoreNonAlphanumeric;
        }

        public bool IgnoreNonAlphanumeric { get; set; }

        public bool IgnoreCase { get; set; }

        public double Threshold { get; set; }
    }

    public class FuzzyMatch
    {
        public string Choice { get; set; }
        public double Score { get; set; }
    }
}
