using System;
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

    //POST
    [HttpPost]
    public ActionResult<DTOVaultKeep> Post([FromBody] DTOVaultKeep newDTOVaultKeep)
    {
      try
      {
        return Ok(_kvs.Create(newDTOVaultKeep));
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
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
  }
}