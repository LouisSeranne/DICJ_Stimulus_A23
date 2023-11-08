using Microsoft.Data.SqlClient;

namespace StimulusAPI.Config
{
    public class DbConfig
    {
        public DbConfig()
        {

            // Projet : ProjetStimulus
            // Test : TestStimulusProjet
            string db = "ProjetStimulus";
          
            var builder = WebApplication.CreateBuilder();

            SqlConnStringBuilder = new SqlConnectionStringBuilder()
            {                
                DataSource = builder.Configuration[$"{db}:BDServeur"],
                InitialCatalog = builder.Configuration[$"{db}:BDNom"],
                UserID = builder.Configuration[$"{db}:BDUser"],
                Password = builder.Configuration[$"{db}:BDPassword"]
            };

            SqlLoginConnStringBuilder = new SqlConnectionStringBuilder()
            {               
                DataSource = builder.Configuration[$"{db}:BDServeur"],
                InitialCatalog = builder.Configuration[$"{db}:BDNom"],
                UserID = builder.Configuration[$"{db}:BDUser"],
                Password = builder.Configuration[$"{db}:BDPassword"]
            };
        }

        public SqlConnectionStringBuilder SqlConnStringBuilder { get; private set; }
        public SqlConnectionStringBuilder SqlLoginConnStringBuilder { get; private set; }
    }
}
