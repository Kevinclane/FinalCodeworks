using System;
using System.Collections.Generic;
using System.Data;
using Keepr.Models;
using Keepr.Repositories;

namespace Keepr.Services
{
  public class KeepsService
  {
    private readonly KeepsRepository _repo;
    public KeepsService(KeepsRepository repo)
    {
      _repo = repo;
    }
    public IEnumerable<Keep> Get()
    {
      return _repo.Get();
    }
    public Keep GetById(int id)
    {
      Keep foundKeep = _repo.GetById(id);
      if (foundKeep == null) { throw new Exception("Invalid id"); }
      return foundKeep;
    }

    public Keep Create(Keep newKeep)
    {
      return _repo.Create(newKeep);
    }

    public string Delete(int id, string userId)
    {
      Keep foundKeep = GetById(id);
      if (foundKeep.UserId != userId)
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

    public Keep GetByUserId(string userId)
    {
      Keep foundKeep = _repo.GetByUserId(userId);
      if (foundKeep == null)
      {
        throw new Exception("Invalid UserId");
      }
      return foundKeep;
    }

    public Keep Edit(Keep editKeep, string userId)
    {
      Keep original = GetById(editKeep.Id);
      if (original.UserId != userId)
      {
        throw new UnauthorizedAccessException("This is not your keep!");
      }
      original.Name = editKeep.Name.Length > 0 ? editKeep.Name : original.Name;
      original.Description = editKeep.Description.Length > 0 ? editKeep.Description : original.Description;
      original.Img = editKeep.Img.Length > 0 ? editKeep.Img : original.Img;
      if (original.IsPrivate != editKeep.IsPrivate)
      {
        original.IsPrivate = editKeep.IsPrivate;
      }
      if (original.Keeps != editKeep.Keeps)
      {
        original.Keeps = editKeep.Keeps;
      }
      if (original.Shares != editKeep.Shares)
      {
        original.Shares = editKeep.Shares;
      }
      if (original.Views != editKeep.Views)
      {
        original.Views = editKeep.Views;
      }
      return _repo.Edit(original);
    }
  }
}