using System;
using System.IO;
using System.Data.SQLite;

namespace coinism.Data
{
    public static class DbInitializer
    {
        public static void EnsureDatabase(string dbPath)
        {
            bool dbExists = File.Exists(dbPath);

            if (!dbExists)
            {
                SQLiteConnection.CreateFile(dbPath);
                Console.WriteLine($"[INFO] DB 파일 생성됨: {dbPath}");
            }

            using var connection = new SQLiteConnection($"Data Source={dbPath};Version=3;");
            connection.Open();

            CreateCandlesTable(connection);
            CreateTradeLogTable(connection);
        }

        private static void CreateCandlesTable(SQLiteConnection connection)
        {
            string sql = @"
                CREATE TABLE IF NOT EXISTS Candles (
                    Timestamp DATETIME,
                    Open REAL,
                    High REAL,
                    Low REAL,
                    Close REAL,
                    Volume REAL,
                    Market TEXT,
                    Interval TEXT
                );
            ";
            using var cmd = new SQLiteCommand(sql, connection);
            cmd.ExecuteNonQuery();
        }

        private static void CreateTradeLogTable(SQLiteConnection connection)
        {
            string sql = @"
                CREATE TABLE IF NOT EXISTS TradeLog (
                    Id INTEGER PRIMARY KEY AUTOINCREMENT,
                    Timestamp DATETIME,
                    Type TEXT,
                    Price REAL,
                    Volume REAL,
                    Reason TEXT,
                    Strategy TEXT
                );
            ";
            using var cmd = new SQLiteCommand(sql, connection);
            cmd.ExecuteNonQuery();
        }
    }
}
