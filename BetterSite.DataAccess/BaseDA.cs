using IBatisNet.DataMapper;
using System.Collections;
using System.Collections.Generic;

namespace BetterSite.DataAccess
{
    public  class BaseDA
    {
       // public BaseDA(){
        static ISqlMapper iSqlMapper = MapperHelper.Instance();
      //  }
        #region Init iSqlMapper (单例)
        //public static volatile ISqlMapper iSqlMapper = null;
        ////protected static void Configure(object obj)
        ////{
        ////    mapper = null;
        ////}
        //protected static void InitMapper()
        //{
        //    iSqlMapper = Mapper.Instance();
        //   // ConfigureHandler handler = new ConfigureHandler(Configure);
        //   // DomSqlMapBuilder builder = new DomSqlMapBuilder();
        //   // mapper = builder.ConfigureAndWatch(@"../SportsStore.Domain/SqlMap.config", handler);
        //}
        //public static ISqlMapper Instance()
        //{
        //    //保证只有一个实例
        //    if (iSqlMapper == null)
        //    {
        //        lock (typeof(SqlMapper))
        //        {
        //            if (iSqlMapper == null)
        //            {
        //                InitMapper();
        //            }
        //        }
        //    }
        //    return iSqlMapper;
        //}
        //public static ISqlMapper Get()
        //{
        //    return Instance();
        //}
        #endregion
       /// <summary>
       /// 
       /// </summary>
       /// <typeparam name="T"></typeparam>
       /// <param name="statementName"></param>
       /// <param name="t"></param>
        /// <returns>失败 0</returns>
        public static object Insert<T>(string statementName, T t)
        {
            //ISqlMapper iSqlMapper = Mapper.Instance();
            if (iSqlMapper != null)
            {
                return iSqlMapper.Insert(statementName, t);
            }
            return 0; 
        }

        public static int Update<T>(string statementName, T t)
        {
           // ISqlMapper iSqlMapper = Mapper.Instance();
            if (iSqlMapper != null)
            {
                return iSqlMapper.Update(statementName, t);
            }
            return 0;
        }

        public static int Delete(string statementName, string primaryKeyId)
        {
          // ISqlMapper iSqlMapper = Mapper.Instance();
            if (iSqlMapper != null)
            {
                return iSqlMapper.Delete(statementName, primaryKeyId);
            }
            return 0;
        }

        public static T Get<T>(string statementName, string primaryKeyId) where T : class
        {
        // ISqlMapper iSqlMapper = Mapper.Instance();
            if (iSqlMapper != null)
            {
                return iSqlMapper.QueryForObject<T>(statementName, primaryKeyId);
            }
            return null;
        }

        //public static IList<T> QueryForList<T>(string statementName, object parameterObject = null)
        //{
        //  // ISqlMapper iSqlMapper = Mapper.Instance();
        //    if (iSqlMapper != null)
        //    {
        //        return iSqlMapper.QueryForList<T>(statementName, parameterObject);
        //    }
        //    return null;
        //}
        public static IList QueryForList(string statementName, object parameterObject = null)
        {
          //  ISqlMapper iSqlMapper = Mapper.Instance();
            if (iSqlMapper != null)
            {
                return iSqlMapper.QueryForList(statementName, parameterObject);
            }
            return null;
        }
    }
}