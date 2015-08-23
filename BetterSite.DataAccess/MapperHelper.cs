using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IBatisNet.DataMapper;
using IBatisNet.DataMapper.Configuration;
using IBatisNet.Common.Utilities;
using System.Configuration;
using System.Collections.Specialized;
using System.Reflection;
using IBatisNet.DataMapper.SessionStore;

namespace BetterSite.DataAccess
{
    public class MapperHelper
    {

        public static volatile ISqlMapper mapper = null;
        protected static void Configure(object obj)
        {
            mapper = null;
        }
        //protected static void InitMapper()
        //{
        //    ConfigureHandler handler = new ConfigureHandler(Configure);
        //    DomSqlMapBuilder builder = new DomSqlMapBuilder();
        //    string connString = ConfigurationManager.ConnectionStrings["conn"].ConnectionString;
        //    NameValueCollection prop = new NameValueCollection();
        //    prop.Add("ConnectionString", connString);
        //    builder.Properties = prop;
        //   mapper = builder.ConfigureAndWatch(@"../BetterSite.DataAccess/SqlMap.config", handler);
        //  //  mapper = builder.ConfigureAndWatch(@"F:\NavSite\SRC\BetterSite\BetterSite.DataAccess\SqlMap.config", handler);


        //   // Assembly assembly = typeof(MapperHelper).Assembly;
        //   // System.IO.Stream streamSmall = assembly.GetManifestResourceStream("BetterSite.DataAccess.SqlMap.config");
        //   //mapper = builder.Configure(streamSmall);    //.ConfigureAndWatch(  "bin\\SQLMapConfig\\DEVBZZB.config",  handler);
        //   // mapper.SessionStore = new HybridWebThreadSessionStore(mapper.Id);
        //   // //mapper.DataSource.DbProvider.DbCommandTimeout = 900;
        //}
        /// <summary>
        /// 初始化SqlMapper
        /// </summary>
        protected static void InitMapper()
        {
            ConfigureHandler handler = new ConfigureHandler(Configure);
            DomSqlMapBuilder builder = new DomSqlMapBuilder();
            string connection;
            NameValueCollection properties;

            connection = ConfigurationManager.ConnectionStrings["conn"].ConnectionString;
            //connection = ConfigurationManager.AppSettings["conn"];
            // Put the string in collection to pass to the iBATIS configurator
            properties = new NameValueCollection();
            properties.Add("conn", connection);
            // Build the iBATIS configurator
            builder = new DomSqlMapBuilder();
            builder.Properties = properties;
            Assembly assembly = typeof(MapperHelper).Assembly;
            System.IO.Stream streamSmall = assembly.GetManifestResourceStream(@"BetterSite.DataAccess.SqlMap.config");
            mapper = builder.Configure(streamSmall);    //.ConfigureAndWatch(  "bin\\SQLMapConfig\\DEVBZZB.config",  handler);
            mapper.SessionStore = new HybridWebThreadSessionStore(mapper.Id);
           // mapper.DataSource.DbProvider.DbCommandTimeout = 900;
        }
        public static ISqlMapper Instance()
        {
            if (mapper == null)
            {
                lock (typeof(SqlMapper))
                {
                    if (mapper == null)
                    {
                        InitMapper();
                    }
                }
            }
            return mapper;
        }
        public static ISqlMapper Get()
        {
            return Instance();
        }
    }
}
