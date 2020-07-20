using System;
using System.Collections.Generic;
using System.Data;
using Keepr.Models;
using Dapper;

namespace Keepr.Repositories
{
  public class VaultsRepository
  {
    private readonly IDbConnection _db;

    public VaultsRepository(IDbConnection db)
    {
      _db = db;
    }

    internal IEnumerable<Vault> GetMyVaults(string userId)
    {
      string sql = "SELECT * FROM Vaults WHERE userId = @userId;";
      return _db.Query<Vault>(sql, new { userId });
    }

    internal Vault GetById(int id)
    {
      string sql = "SELECT * FROM vaults WHERE id = @id";
      return _db.QueryFirstOrDefault<Vault>(sql, new { id });
    }

    internal Vault Create(Vault newKeep)
    {
      string sql = @"
            INSERT INTO vaults
            (userId, name, description)
            VALUES
            (@UserId, @Name, @Description);
            SELECT LAST_INSERT_ID()";
      newKeep.Id = _db.ExecuteScalar<int>(sql, newKeep);
      return newKeep;
    }

    internal bool Delete(int id, string userId)
    {
      string sql = "DELETE FROM vaults WHERE id = @id AND userId = @userId LIMIT 1";
      int affectedRows = _db.Execute(sql, new { id, userId });
      return affectedRows == 1;
    }
  }
}