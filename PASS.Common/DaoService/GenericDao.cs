using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using PASS.Models.Dao;
using PASS.Models.Attribute;
using System.Configuration;

namespace PASS.Common.DaoService
{

    public class GenericDao<T>
    {
        static string dbPath = ConfigurationManager.AppSettings["DbPath"];
        static string cnStr = "data source=" + dbPath;

        public GenericDao()
        {
            if (!File.Exists(dbPath))
            {
                throw new ArgumentNullException("DB路徑錯誤，請修改PASS.Common.DaoService中App.config的[DbPath]");
            }
        }

        /// <summary>
        /// Insert new data to table.
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public bool Insert(T obj)
        {
            if (File.Exists(dbPath))
            {
                using (var cn = GetOpenConnection())
                {
                    var propertyContainer = ParseProperties(obj);
                    var valueNames = propertyContainer.ValueNames;
                    var sql = string.Format(@"INSERT INTO [{0}] ({1}) 
                                            VALUES(@{2})"
                                            , typeof(T).Name
                                            , string.Join(", ", valueNames)
                                            , string.Join(", @", valueNames));
                    var sqlCmd = new SQLiteCommand(cn);
                    sqlCmd.CommandText = sql;

                    foreach (var param in propertyContainer.ValuePairs)
                    {
                        sqlCmd.Parameters.Add(new SQLiteParameter("@" + param.Key, param.Value));
                    }

                    sqlCmd.ExecuteNonQuery();
                    cn.Close();
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Update data.
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public bool Update(T obj)
        {
            if (File.Exists(dbPath))
            {
                using (var cn = GetOpenConnection())
                {
                    var propertyContainer = ParseProperties(obj);
                    var sqlIdPairs = GetSqlPairs(propertyContainer.IdNames, " AND ");
                    var sqlValuePairs = GetSqlPairs(propertyContainer.ValueNames);
                    var sql = string.Format(@"
                                        UPDATE [{0}] 
                                        SET {1}
                                        WHERE {2}
                                        ", typeof(T).Name, sqlValuePairs, sqlIdPairs);

                    var sqlCmd = new SQLiteCommand(cn);
                    sqlCmd.CommandText = sql;

                    foreach (var param in propertyContainer.ValuePairs)
                    {
                        sqlCmd.Parameters.Add(new SQLiteParameter("@" + param.Key, param.Value));
                    }

                    sqlCmd.ExecuteNonQuery();
                    cn.Close();
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Delete exist data in table.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <returns></returns>
        public bool Delete(T obj)
        {
            if (File.Exists(dbPath))
            {
                var propertyContainer = ParseProperties(obj);
                var sqlIdPairs = GetSqlPairs(propertyContainer.IdNames , " AND ");

                using (var cn = GetOpenConnection())
                {
                    var sql = string.Format(@"DELETE FROM [{0}] 
                                    WHERE {1}
                                    ", typeof(T).Name, sqlIdPairs);
                    var sqlCmd = new SQLiteCommand(cn);
                    sqlCmd.CommandText = sql;

                    foreach (var param in propertyContainer.ValuePairs)
                    {
                        sqlCmd.Parameters.Add(new SQLiteParameter("@" + param.Key, param.Value));
                    }

                    sqlCmd.ExecuteNonQuery();
                    cn.Close();
                    return true;
                }
            }
                return false;
        }


        /// <summary>
        /// Create a commaseparated list of value pairs on 
        /// the form: "key1=@value1, key2=@value2, ..."
        /// </summary>
        protected static string GetSqlPairs
        (IEnumerable<string> keys, string separator = ", ")
        {
            var pairs = keys.Select(key => string.Format("{0}=@{0}", key)).ToList();
            return string.Join(separator, pairs);
        }

        protected SQLiteConnection GetOpenConnection()
        {
            var connection = new SQLiteConnection(cnStr);
            connection.Open();
            return connection;
        }

        /// <summary>
        /// Retrieves a Dictionary with name and value 
        /// for all object properties matching the given criteria.
        /// </summary>
        protected static PropertyContainer ParseProperties(T obj)
        {
            var propertyContainer = new PropertyContainer();

            var typeName = typeof(T).Name;
            var validKeyNames = new[] { "Id",
            string.Format("{0}Id", typeName), string.Format("{0}_Id", typeName) };

            var properties = typeof(T).GetProperties();
            foreach (var property in properties)
            {
                // Skip reference types (but still include string!)
                if (property.PropertyType.IsClass && property.PropertyType != typeof(string))
                    continue;

                // Skip methods without a public setter
                if (property.GetSetMethod() == null)
                    continue;

                // Skip methods specifically ignored
                if (property.IsDefined(typeof(DapperIgnore), false))
                    continue;

                // Skip property with attribute "PrimaryKey"
                if (property.GetCustomAttribute(typeof(PrimaryKey)) != null)
                    continue;

                var name = property.Name;
                var value = typeof(T).GetProperty(property.Name).GetValue(obj, null);

                if (property.IsDefined(typeof(DapperKey), false) || validKeyNames.Contains(name))
                {
                    propertyContainer.AddId(name, value);
                    propertyContainer.AddValue(name, value);
                }
                else
                {
                    propertyContainer.AddValue(name, value);
                }
            }

            return propertyContainer;
        }
    }
}
