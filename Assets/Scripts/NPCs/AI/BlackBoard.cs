using System.Collections.Generic;

public class BlackBoard
{
    public Dictionary<string, object> values = new Dictionary<string, object>();

    public T GetValue<T>(string key)
    {
        return values.ContainsKey(key) ? (T)values[key] : default(T); 
    }

    public void SetValue<T>(string key, T value)
    {
        if (values.ContainsKey(key))
        {
            values[key] = value;
        }
        else
        {
            values.Add(key, value);
        }
    }
}
