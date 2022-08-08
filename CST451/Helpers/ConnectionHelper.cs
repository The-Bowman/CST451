using Microsoft.Extensions.Configuration;

namespace CST451.Helpers
{
    public class ConnectionHelper
    {

        private readonly IConfiguration Configuration;

        public string dbConnection
        {
            get { return Configuration.GetConnectionString("dbPCParts");}
        }
    }
}
