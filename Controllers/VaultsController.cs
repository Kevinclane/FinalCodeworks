using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Keepr.Models;
using Keepr.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Keepr.Controllers
{
  [ApiController]
  [Route("api/[controller]")]
  [Authorize]
  public class VaultsController : ControllerBase
  {
    private readonly VaultsService _vs;
    private readonly VaultKeepsService _vks;
    public VaultsController(VaultsService ks, VaultKeepsService vks)
    {
      _vs = ks;
      _vks = vks;
    }

    [HttpGet("{id}/keeps")]
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

    [HttpGet("{Id}")]
    public ActionResult<Vault> GetById(int Id)
    {
      try
      {
        return Ok(_vs.GetById(Id));
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }

    [HttpGet]
    public ActionResult<IEnumerable<Vault>> GetMyVault()
    {
      try
      {
        string userId = findUserInfo();
        return Ok(_vs.GetMyVaults(userId));
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      };
    }

    [HttpPost]
    public ActionResult<Vault> Post([FromBody] Vault newVault)
    {
      try
      {
        string userId = findUserInfo();
        newVault.UserId = userId;
        return Ok(_vs.Create(newVault));
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }

    }
    [HttpDelete("{id}")]
    public ActionResult<string> Delete(int id)
    {
      try
      {
        string userId = findUserInfo();
        return Ok(_vs.Delete(id, userId));
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