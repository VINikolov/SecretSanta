using System.Configuration;

namespace DataAccess
{
    public static class Settings
    {
        public static string DbConnectionString = ConfigurationManager.AppSettings["DbConnectionString"];
    }
}
