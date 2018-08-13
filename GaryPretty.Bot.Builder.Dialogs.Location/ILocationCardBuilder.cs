using System.Collections.Generic;
using Microsoft.Bot.Schema;

namespace GaryPretty.Bot.Builder.Dialogs.Location
{
    internal interface ILocationCardBuilder
    {
        IEnumerable<HeroCard> CreateHeroCards(IList<Bing.Location> locations, bool alwaysShowNumericPrefix = false, IList<string> locationNames = null);
    }
}
