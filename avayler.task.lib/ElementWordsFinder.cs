using System.Text.Json;

namespace avayler.task.lib
{
    public class ElementWordsFinder
    {
        private static List<List<string>> FindForms(string word, Dictionary<string, List<List<string>>> cacheData)
        {
            if (cacheData.TryGetValue(word, out var forms))
                return forms;

            var results = new List<List<string>>();

            if (string.IsNullOrEmpty(word))
            {
                results.Add(new List<string>());
                return results;
            }
            foreach (var element in PeriodictableData.elements)
            {
                var symbol = element.Key;
                var name = element.Value;

                if (word.StartsWith(symbol.ToLower()))
                {
                    var suffix = word.Substring(symbol.Length);
                    var suffixForms = FindForms(suffix, cacheData);

                    foreach (var form in suffixForms)
                    {
                        var newForm = new List<string> { $"{name} ({symbol})" };
                        newForm.AddRange(form);
                        results.Add(newForm);
                    }
                }
            }
            cacheData[word] = results;
            return results;
        }

        public static string ElementalForms(string word)
        {
            word = word.ToLower();
            var cacheData = new Dictionary<string, List<List<string>>>();
            var resultData = FindForms(word, cacheData);
            return JsonSerializer.Serialize(resultData);
        }
    }
}
