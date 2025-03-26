using CommonApi.Model.Repositorys;

using Microsoft.Data.SqlClient;

using Npgsql;

using System.Data;

namespace Comm.WebUtil
{
    public class DbContext : IDisposable
    {
        private IDbConnection _connection;
        static IConfigurationRoot Configuration { get; set; }
        private MyAppConfig config;
        public DbContext()
        {
            var builder = new ConfigurationBuilder()
           .SetBasePath(Directory.GetCurrentDirectory())  // 設定根目錄
           .AddJsonFile("appsettings.json", true);   // 讀取appsettings.json檔案
            Configuration = builder.Build();
            config = Configuration.Get<MyAppConfig>();
        }

        /// <summary>
        /// SQLConnection
        /// </summary>
        public IDbConnection NGConnection
        {
            get
            {

                return new NpgsqlConnection(config.ConnectionStrings.Connsql);
            }
            private set
            {
                _connection = value;
            }
        }
        private RecipeRepository _RecipeRepository;

        public RecipeRepository RecipeRepository
        {
            get
            {
                if (_RecipeRepository == null)
                {
                    _RecipeRepository = new RecipeRepository();
                }

                return _RecipeRepository;
            }
        }
        private RecipeDetailsRepository _RecipeDetailsRepository;

        public RecipeDetailsRepository RecipeDetailsRepository
        {
            get
            {
                if (_RecipeDetailsRepository == null)
                {
                    _RecipeDetailsRepository = new RecipeDetailsRepository();
                }

                return _RecipeDetailsRepository;
            }
        }

        private MemberRepository _MemberRepository;
        public MemberRepository MemberRepository
        {
            get
            {
                if (_MemberRepository == null)
                {
                    _MemberRepository = new MemberRepository();
                  
                }
                return _MemberRepository;
            }
        }
        private MemberFavoriteRepository _MemberFavoriteRepository;
        public MemberFavoriteRepository MemberFavoriteRepository
        {
            get
            {
                if (_MemberFavoriteRepository == null)
                {
                    _MemberFavoriteRepository = new MemberFavoriteRepository();

                }
                return _MemberFavoriteRepository;
            }
        }


















        public void Dispose()
        {
            if (_connection?.State != ConnectionState.Closed)
            {
                _connection?.Close();
            }
        }
    }
}
