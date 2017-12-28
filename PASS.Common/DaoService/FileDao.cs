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
    public class FileDao
    {
        static string dbPath = ConfigurationManager.AppSettings["DbPath"];
        static string cnStr = "data source=" + dbPath;

        public FileDao()
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
        public Int64 Insert(byte[] fileStream)
        {
            using (var cn = GetOpenConnection())
            {
                var sql = string.Format(@"INSERT INTO [File] (FileStream) 
                                            VALUES(@FileStream);
                                           SELECT last_insert_rowid();");
                var sqlCmd = new SQLiteCommand(cn);
                sqlCmd.CommandText = sql;
                sqlCmd.Parameters.Add("@FileStream", System.Data.DbType.Binary).Value = fileStream;
                                
                var fileNo = Convert.ToInt64(sqlCmd.ExecuteScalar());
                cn.Close();
                return fileNo;
            }
        }

        public bool Delete(Int64 fileNo)
        {
            using (var cn = GetOpenConnection())
            {
                var sql = string.Format(@"Delete FROM File WHERE FileNo = @FileNo");
                var sqlCmd = new SQLiteCommand(cn);
                sqlCmd.CommandText = sql;
                sqlCmd.Parameters.Add(new SQLiteParameter("@FileNo", fileNo));
                sqlCmd.ExecuteNonQuery();

                return true;
            }
        }

        public bool Delete(Int64 userNo, Int64 assignmentNo)
        {
            using (var cn = GetOpenConnection())
            {
                var sql = @"delete
                            from File
                            where exists( select 1 from SubmissionDetail as sub where sub.UserNo = @UserNo and sub.AssignmentNo = @AssignmentNo and sub.FileNo = File.FileNo)";
                var sqlCmd = new SQLiteCommand(cn);
                sqlCmd.CommandText = sql;
                sqlCmd.Parameters.Add(new SQLiteParameter("@UserNo", userNo));
                sqlCmd.Parameters.Add(new SQLiteParameter("@AssignmentNo", assignmentNo));
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
               

        protected SQLiteConnection GetOpenConnection()
        {
            var connection = new SQLiteConnection(cnStr);
            connection.Open();
            return connection;
        }
    }
}
