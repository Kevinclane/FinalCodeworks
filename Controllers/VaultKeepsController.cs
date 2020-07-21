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
    private readonly VaultKeepsService _vks;
    public VaultKeepsController(VaultKeepsService vks)
    {
      _vks = vks;
    }

    [HttpGet]
    public ActionResult<IEnumerable<VaultKeepViewModel>> Get([FromBody] int vaultId)
    {
      try
      {
        string userId = findUserInfo();
        return Ok(_vks.GetKeepsByVaultId(userId, vaultId));
      }
      catch (System.Exception err)
      {
        return BadRequest(err.Message);
      }
    }

    //POST
    [HttpPost]


    public ActionResult<DTOVaultKeep> Create([FromBody] DTOVaultKeep newVaultKeep)
    {
      try
      {
        newVaultKeep.UserId = findUserInfo();
        return Ok(_vks.Create(newVaultKeep));
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
        return Ok(_vks.Delete(id));
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