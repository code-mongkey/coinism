using System;
using System.Data;
using System.Data.SQLite;

namespace coinism.Data
{
    /// <summary>
    /// SQLite 연결 및 커맨드 생성을 위한 공통 유틸
    /// </summary>
    public class SqliteContext : IDisposable
    {
        private readonly SQLiteConnection _connection;

        public SqliteContext(string dbPath)
        {
            _connection = new SQLiteConnection($"Data Source={dbPath};Version=3;");
            _connection.Open();
        }

        public SQLiteCommand CreateCommand(string sql)
        {
            return new SQLiteCommand(sql, _connection);
        }

        public IDbConnection Connection => _connection;

        public void Dispose()
        {
            _connection?.Dispose();
        }
    }
}
