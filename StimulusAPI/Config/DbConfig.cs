using Microsoft.Data.SqlClient;

namespace StimulusAPI.Config
{
    public class DbConfig
    {
        public DbConfig()
        {
            SqlConnStringBuilder = new SqlConnectionStringBuilder()
            {
                /*
                  DataSource = builder.Configuration["BDServeur"],
                  InitialCatalog = builder.Configuration["BDNom"],
                  UserID = builder.Configuration["BDUser"],
                  Password = builder.Configuration["BDPassword"]
                */
                DataSource = "dicjwin01",
                InitialCatalog = "TestStimulusProjet",
                UserID = "P2022-Dev",
                Password = "9jj96wqwoFYSj6Dxw26w"
            };

            SqlLoginConnStringBuilder = new SqlConnectionStringBuilder()
            {
                /*
                  DataSource = builder.Configuration["BDServeur"],
                  InitialCatalog = builder.Configuration["BDLoginNom"],
                  UserID = builder.Configuration["BDUser"],
                  Password = builder.Configuration["BDPassword"]
                */
                DataSource = "dicjwin01",
                InitialCatalog = "TestStimulusProjet",
                UserID = "P2022-Dev",
                Password = "9jj96wqwoFYSj6Dxw26w"
            };
        }

        public SqlConnectionStringBuilder SqlConnStringBuilder { get; private set; }
        public SqlConnectionStringBuilder SqlLoginConnStringBuilder { get; private set; }
    }
}
