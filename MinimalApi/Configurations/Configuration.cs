using DotNetEnv;

namespace MinimalApi.Configurations
{
    public class Configuration
    {
        public Configuration()
        {
            Env.Load();
        }

        public string GetValue(string key)
        {
            var value = Env.GetString(key);
            if (string.IsNullOrEmpty(value))
            {
                throw new ArgumentException($"A variável de ambiente '{key}' não está definida.");
            }

            return value;
        }

        public int GetIntValue(string key)
        {
            var value = GetValue(key);
            return int.Parse(value);
        }

        public bool GetBoolValue(string key)
        {
            var value = GetValue(key);
            return bool.Parse(value);
        } 
    }
}
