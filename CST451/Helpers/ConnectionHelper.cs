using Microsoft.Extensions.Configuration;

namespace CST451.Helpers
{
    public class ConnectionHelper
    {

        private IConfiguration Configuration;
        
        /// <summary>
        /// Gets Connection string from appsettings.json
        /// </summary>
        /// <returns>connection string</returns>
        public string GetConnection()
        {
            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: false);
            Configuration = builder.Build();
            string con = Configuration.GetValue<string>("ConnectionStrings:dbPCParts");
            return con; 
        }
    }
}
