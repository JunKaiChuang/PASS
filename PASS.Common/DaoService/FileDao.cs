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

namespace PASS.Common.DaoService
{
    public class FileDao
    {
        static string dbPath = @"E:\DB\PASS.db";
        static string cnStr = "data source=" + dbPath;

        public FileDao()
        {
            if (!File.Exists(dbPath))
            {
                throw new ArgumentNullException("DB路徑錯誤，請修改PASS.Common.DaoService.FileDao的[dbPath]");
            }
        }

        /// <summary>
        /// Insert new data to table.
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public Int64 Insert(MemoryStream fileStream)
        {
            using (var cn = GetOpenConnection())
            {
                var sql = string.Format(@"INSERT INTO [File] (FileStream) 
                                            VALUES(@FileStream)");
                var sqlCmd = new SQLiteCommand(cn);
                sqlCmd.CommandText = sql;
                sqlCmd.Parameters.Add("@FileStream", System.Data.DbType.Binary).Value = StreamToByte(fileStream);

                var fileNo = sqlCmd.ExecuteNonQuery();
                cn.Close();
                return fileNo;
            }
        }

        public bool Delete(Int64 fileNo)
        {
            using (var cn = GetOpenConnection())
            {
                var sql = string.Format(@"Delete FROM FILE WHERE FileNo = @FileNo");
                var sqlCmd = new SQLiteCommand(cn);
                sqlCmd.CommandText = sql;
                sqlCmd.Parameters.Add(new SQLiteParameter("@FileNo", fileNo));
                sqlCmd.ExecuteNonQuery();

                return true;
            }
        }

        public MemoryStream GetFile(Int64 fileNo)
        {
            using (var cn = GetOpenConnection())
            {
                var sql = string.Format(@"SELECT FileStream FROM File WHERE FileNo = @FileNo");
                var sqlCmd = new SQLiteCommand(cn);
                sqlCmd.CommandText = sql;
                sqlCmd.Parameters.Add(new SQLiteParameter("@FileNo", fileNo));

                using (var reader = sqlCmd.ExecuteReader())
                {
                    reader.Read();
                    return GetStream(reader);
                }
            }
        }

        static MemoryStream GetStream(SQLiteDataReader reader)
        {
            const int CHUNK_SIZE = 2 * 1024;
            byte[] buffer = new byte[CHUNK_SIZE];
            long bytesRead;
            long fieldOffset = 0;
            using (MemoryStream stream = new MemoryStream())
            {
                while ((bytesRead = reader.GetBytes(0, fieldOffset, buffer, 0, buffer.Length)) > 0)
                {
                    stream.Write(buffer, 0, (int)bytesRead);
                    fieldOffset += bytesRead;
                }
                return stream;
            }
        }

        public static byte[] StreamToByte(MemoryStream input)
        {
            return input.ToArray();
        }

        protected SQLiteConnection GetOpenConnection()
        {
            var connection = new SQLiteConnection(cnStr);
            connection.Open();
            return connection;
        }
    }
}
