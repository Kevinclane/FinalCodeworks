using System;
using System.Collections.Generic;
using System.Data;
using Dapper;
using Keepr.Models;

namespace Keepr.Repositories
{
  public class VaultKeepRepository
  {
    private readonly IDbConnection _db;
    public VaultKeepRepository(IDbConnection db)
    {
      _db = db;
    }


    internal DTOVaultKeep GetById(int Id)
    {
      string sql = "SELECT * FROM vaultkeeps WHERE id = @Id";
      return _db.QueryFirstOrDefault<DTOVaultKeep>(sql, new { Id });
    }

    internal int Create(DTOVaultKeep newDTOVaultKeep)
    {
      string sql = @"
        INSERT INTO vaultkeeps
        (kitId, legoId)
        VALUES
        (@KitId, @LegoId);
        SELECT LAST_INSERT_ID();";
      return _db.ExecuteScalar<int>(sql, newDTOVaultKeep);
    }

    internal void Delete(int Id)
    {
      string sql = "DELETE FROM vaultkeeps WHERE id = @Id";
      _db.Execute(sql, new { Id });
    }

    internal IEnumerable<VaultKeepViewModel> GetKeepsByVaultId(int id)
    {
      string sql = @"
        SELECT 
        k.*,
        vk.id as vaultKeepId
        FROM vaultkeeps vk
        INNER JOIN keeps k ON k.id = vk.keepId 
        WHERE (vaultId = @vaultId AND vk.userId = @userId) 
        ";
      return _db.Query<VaultKeepViewModel>(sql, new { id });
    }
  }
}