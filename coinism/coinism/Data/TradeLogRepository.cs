using System;
using System.Data.SQLite;
using coinism.Core.Models;

namespace coinism.Data
{
    public class TradeLogRepository
    {
        private readonly SqliteContext _context;

        public TradeLogRepository(SqliteContext context)
        {
            _context = context;
        }

        public void InsertTrade(TradeAction action, string strategyName)
        {
            var sql = @"INSERT INTO TradeLog (Timestamp, Type, Price, Volume, Reason, Strategy)
                        VALUES (@timestamp, @type, @price, @volume, @reason, @strategy)";

            using var cmd = _context.CreateCommand(sql);
            cmd.Parameters.AddWithValue("@timestamp", DateTime.UtcNow);
            cmd.Parameters.AddWithValue("@type", action.Type.ToString());
            cmd.Parameters.AddWithValue("@price", action.Price);
            cmd.Parameters.AddWithValue("@volume", action.Volume);
            cmd.Parameters.AddWithValue("@reason", action.Reason);
            cmd.Parameters.AddWithValue("@strategy", strategyName);

            cmd.ExecuteNonQuery();
        }
    }
}
