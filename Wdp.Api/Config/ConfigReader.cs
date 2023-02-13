using Microsoft.Extensions.Options;

namespace Wdp.Api.Config
{
    public class ConfigReader
    {
        private readonly IOptionsSnapshot<DbSettings> dbSettings;

        public ConfigReader(IOptionsSnapshot<DbSettings> dbSettings)
        {
            this.dbSettings = dbSettings;
        }

        public void Test()
        {
            var db = dbSettings.Value;
            Console.WriteLine($"数据库:{db.DbType},连接串：{db.ConnectionString}");
        }
    }
}
