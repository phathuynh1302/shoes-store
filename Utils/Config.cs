using System.IO;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Hosting;
using PRN211_ShoesStore.Utils.Interface;

namespace PRN211_ShoesStore.Utils
{
    public class Config : IConfig {
        private readonly IWebHostEnvironment _env;
        private readonly IConfiguration _config;
        public Config(IWebHostEnvironment env, IConfiguration config) {
            this._env = env;
            this._config = config;
        }

        public string GetEnvByKey(string name) {
            string currentEnv = this._env.EnvironmentName.ToLower();
            string envFileName = "env." + currentEnv + ".json";
            string envPath = Path.Combine(Directory.GetCurrentDirectory(), "config", envFileName);

            var envConfig = new ConfigurationBuilder().AddJsonFile(envPath, optional: true, reloadOnChange: true).Build();
            return envConfig[name] ?? _config[name];
        }
    }
}