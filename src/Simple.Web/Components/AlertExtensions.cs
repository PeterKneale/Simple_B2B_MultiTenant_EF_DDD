using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Newtonsoft.Json;

namespace Simple.Web.Components;

public static class AlertExtensions
{
    private const string Key = "Alerts";

    public static bool HasAlert(this ITempDataDictionary dictionary) =>
        dictionary.ContainsKey(Key);

    public static void SetAlert(this ITempDataDictionary dictionary, Alert alert)
    {
        if (dictionary.HasAlert()) return;
        var value = JsonConvert.SerializeObject(alert);
        dictionary.Add(Key, value);
    }

    public static Alert GetAlert(this ITempDataDictionary dictionary)
    {
        var json = dictionary[Key]!.ToString();
        return JsonConvert.DeserializeObject<Alert>(json!)!;
    }
}