using RecognizeService.Domain.TrainSet;

namespace RecognizeService.Application.ML
{
    internal static class CountrySearchML
    {
        public static string GetNationality(string[] texts, IList<Country> countries)
        {
            if (texts == null || countries == null) return "";

            List<string> allCountriesKeywords = countries?.SelectMany(s => s.KeyWords).ToList() ??  new List<string>();

            var matchingKeywords = allCountriesKeywords.Where(keyword => texts.Any(text => text.ToLower().Contains(keyword.ToLower()))).ToList();

            if (matchingKeywords.Count > 0)
            {
                return countries.FirstOrDefault(c => c.KeyWords.Any(key => matchingKeywords.Contains(key))).Nationality;
            }

            return "";
        }
    }
}
