using Newtonsoft.Json;

namespace UI.Helpers;

public static class ObjectCopier
{
    public static T Clone<T>(this T source)
    {
        var serialized = JsonConvert.SerializeObject(source);
        return JsonConvert.DeserializeObject<T>(serialized)!;
    }
}