using System;
using System.Collections.Generic;
using System.Data;
using Keepr.Models;
using Keepr.Repositories;

namespace Keepr.Services
{
  public class VaultsService
  {
    private readonly VaultsRepository _repo;
    public VaultsService(VaultsRepository repo)
    {
      _repo = repo;
    }
    public IEnumerable<Vault> GetMyVaults(string userId)
    {
      return _repo.GetMyVaults(userId);
    }
    public Vault GetById(int id, string userId)
    {
      Vault foundVault = _repo.GetById(id);
      if (foundVault == null) { throw new Exception("Invalid id"); }
      if (foundVault.UserId != userId)
      {
        throw new Exception("This is not your vault!");
      }
      return foundVault;
    }

    public Vault Create(Vault newVault)
    {
      return _repo.Create(newVault);
    }

    public string Delete(int id, string userId)
    {
      Vault foundVault = GetById(id, userId);
      if (foundVault.UserId != userId)
      {
        throw new Exception("This is not your Keep");
      }
      else
      {
        bool res = _repo.Delete(id, userId);
        if (res == true)
        {
          return "Sucessfully deleted";
        }
        else
        {
          return "Something went wrong";
        }
      }
    }
  }
}