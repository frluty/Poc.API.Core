using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Tool.hbm2ddl;
using Poc.API.Core.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
namespace Poc.API.Core.Data
{
    public class FluentNHibernateHelper
    {
        string _dbFile = string.Empty;
        bool _overwriteExisting = false;
       public static ISessionFactory sessionFactory;

        public FluentNHibernateHelper(string dbFile, bool overwriteExisting)
        {
            _dbFile = dbFile;
            _overwriteExisting = overwriteExisting;
            sessionFactory = CreateSessionFactory();
        }

        public ISessionFactory CreateSessionFactory()
        {
            return Fluently.Configure()
                .Database(
                    SQLiteConfiguration.Standard
                        .Dialect("NHibernate.Dialect.SQLiteDialect")
                        .Driver("NHibernate.Driver.SQLite20Driver")
                        .UsingFile(_dbFile)
                        .ShowSql()
                )
                .Mappings(m => m.FluentMappings.AddFromAssemblyOf<Produit>())
                .ExposeConfiguration(BuildSchema)
                .BuildSessionFactory();
        }

        private void BuildSchema(Configuration config)
        {
            if (_overwriteExisting)
            {
                if (File.Exists( _dbFile))
                    File.Delete(_dbFile);
                //else
                //    File.Create(_dbFile);

                var se = new SchemaExport(config);
                se.Create(true, true);
            }
            
        }
    }
}
