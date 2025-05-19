using System;
using System.Collections.Generic;
using System.Data.SQLite;
using coinism.Core.Models;

namespace coinism.Data
{
    public class CandleRepository
    {
        private readonly SqliteContext _context;

        public CandleRepository(SqliteContext context)
        {
            _context = context;
        }

        public IEnumerable<Candle> GetCandles(string market, string interval, DateTime? from = null, DateTime? to = null)
        {
            var sql = "SELECT * FROM Candles WHERE Market = @market AND Interval = @interval";
            if (from.HasValue)
                sql += " AND Timestamp >= @from";
            if (to.HasValue)
                sql += " AND Timestamp <= @to";
            sql += " ORDER BY Timestamp ASC";

            using var cmd = _context.CreateCommand(sql);
            cmd.Parameters.AddWithValue("@market", market);
            cmd.Parameters.AddWithValue("@interval", interval);
            if (from.HasValue) cmd.Parameters.AddWithValue("@from", from.Value);
            if (to.HasValue) cmd.Parameters.AddWithValue("@to", to.Value);

            using var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                yield return new Candle
                {
                    Timestamp = DateTime.Parse(reader["Timestamp"].ToString()),
                    Open = Convert.ToDecimal(reader["Open"]),
                    High = Convert.ToDecimal(reader["High"]),
                    Low = Convert.ToDecimal(reader["Low"]),
                    Close = Convert.ToDecimal(reader["Close"]),
                    Volume = Convert.ToDecimal(reader["Volume"]),
                    Market = reader["Market"].ToString(),
                    Interval = reader["Interval"].ToString()
                };
            }
        }
    }
}
