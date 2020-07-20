using System;
using System.Collections.Generic;
using System.Security.Claims;
using Keepr.Models;
using Keepr.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Keepr.Controllers
{
  [ApiController]
  [Route("api/[controller]")]
  [Authorize]
  public class VaultKeepsController : ControllerBase
  {
    private readonly VaultKeepsService _kvs;
    public VaultKeepsController(VaultKeepsService kvs)
    {
      _kvs = kvs;
    }

    [HttpGet]
    public ActionResult<IEnumerable<VaultKeepViewModel>> Get([FromBody] int vaultId)
    {
      try
      {
        string userId = findUserInfo();
        return Ok(_kvs.GetKeepsByVaultId(userId, vaultId));
      }
      catch (System.Exception err)
      {
        return BadRequest(err.Message);
      }
    }

    //POST
    [HttpPost]


    public ActionResult<DTOVaultKeep> Create([FromBody] DTOVaultKeep newDTOVaultKeep)
    {
      try
      {
        newDTOVaultKeep.UserId = findUserInfo();
        return Ok(_kvs.Create(newDTOVaultKeep));
      }
      catch (System.Exception err)
      {
        return BadRequest(err.Message);
      }
    }

    //DEL
    [HttpDelete("{id}")]
    public ActionResult<DTOVaultKeep> Delete(int id)
    {
      try
      {
        return Ok(_kvs.Delete(id));
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }
    string findUserInfo()
    {
      return HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
    }
  }
}