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
  public class VaultsController : ControllerBase
  {
    private readonly VaultsService _vs;
    public VaultsController(VaultsService ks)
    {
      _vs = ks;
    }
    [HttpGet]
    public ActionResult<IEnumerable<Keep>> Get()
    {
      try
      {
        return Ok(_vs.Get());
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      };
    }
    [HttpGet("{Id}")]
    public ActionResult<Keep> Get(int Id)
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

    [HttpPost]
    [Authorize]
    public ActionResult<Keep> Post([FromBody] Keep newKeep)
    {
      try
      {
        string userId = findUserInfo();
        newKeep.UserId = userId;
        return Ok(_vs.Create(newKeep));
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }

    }
    [HttpDelete("{id}")]
    [Authorize]
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