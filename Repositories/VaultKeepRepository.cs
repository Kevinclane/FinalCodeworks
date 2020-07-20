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

    internal DTOVaultKeep Create(DTOVaultKeep newVaultKeep)
    {
      string sql = @"
        INSERT INTO vaultkeeps
        (keepId, userId, vaultId)
        VALUES
        (@keepId, @UserId, @VaultId);
        SELECT LAST_INSERT_ID();";
      newVaultKeep.Id = _db.ExecuteScalar<int>(sql, newVaultKeep);
      return newVaultKeep;
    }

    internal void Delete(int Id)
    {
      string sql = "DELETE FROM vaultkeeps WHERE id = @Id";
      _db.Execute(sql, new { Id });
    }

    internal IEnumerable<VaultKeepViewModel> GetKeepsByVaultId(string userId, int vaultId)
    {
      string sql = @"
        SELECT 
        k.*,
        vk.id as vaultKeepId
        FROM vaultkeeps vk
        INNER JOIN keeps k ON k.id = vk.keepId 
        WHERE (vaultId = @vaultId AND vk.userId = @userId) 
        ";
      return _db.Query<VaultKeepViewModel>(sql, new { userId, vaultId });
    }

    internal bool hasRelationship(DTOVaultKeep newVaultKeep)
    {
      string sql = "SELECT * FROM vaultkeeps WHERE vaultId = @VaultId AND userId = @UserId";
      var found = _db.QueryFirstOrDefault<DTOVaultKeep>(sql, newVaultKeep);
      return found != null;
    }
  }
}