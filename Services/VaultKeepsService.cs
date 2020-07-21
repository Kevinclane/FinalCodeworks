using System;
using System.Collections.Generic;
using Keepr.Models;
using Keepr.Repositories;


namespace Keepr.Services
{
  public class VaultKeepsService
  {
    private readonly VaultKeepRepository _repo;
    public VaultKeepsService(VaultKeepRepository repo)
    {
      _repo = repo;
    }

    public DTOVaultKeep Get(int Id)
    {
      DTOVaultKeep exists = _repo.GetById(Id);
      if (exists == null) { throw new Exception("Invalid Vault Keep"); }
      return exists;
    }
    public IEnumerable<VaultKeepViewModel> GetKeepsByVaultId(string userId, int vaultId)
    {
      return _repo.GetKeepsByVaultId(userId, vaultId);
    }
    public DTOVaultKeep Create(DTOVaultKeep newVaultKeep)
    {
      if (_repo.hasRelationship(newVaultKeep))
      {
        throw new Exception("This Vault already exists");
      }
      return _repo.Create(newVaultKeep);
    }

    public DTOVaultKeep Delete(int id)
    {
      DTOVaultKeep exists = Get(id);
      _repo.Delete(id);
      return exists;
    }


  }
}