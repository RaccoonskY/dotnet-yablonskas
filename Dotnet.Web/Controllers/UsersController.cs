using Dotnet.Web.Data;
using Dotnet.Web.Dto;
using Dotnet.Web.Interfaces;
using Dotnet.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.ComponentModel.DataAnnotations;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Dotnet.Web.Controllers;


public class UserController: DotnetControllerBase
{

    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;      
    }

    [Authorize("Bearer")]
    [HttpGet("/Users")]
    [ProducesResponseType(typeof(UserDto), 200)]
    [Produces("application/json", "text/plain", "text/json")]
    public IActionResult GetUser() {
        var user = _userService.GetUser();
        return Ok(user);
    }

    [HttpPost("/Users/Login")]
    [Consumes("application/json", "text/plain", "text/json")]
    [ProducesResponseType(typeof(LoginResponseDto), 200)]
    [Produces("application/json", "text/plain", "text/json")]
    public async Task<IActionResult> LoginUser([FromBody] LoginDto login)  {
        try
        {
            return Ok(await _userService.Login(login));
        }
        catch (UnauthorizedAccessException ex)
        {
            return Unauthorized(ex.Message);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPost("/Users/Register")]
    [Consumes("application/json", "application/*+json", "text/json")]
    [ProducesResponseType(typeof(bool), 200)]
    [Produces("application/json", "text/plain", "text/json")]
    public async Task<IActionResult> RegisterUser(RegisterDto register) {
        try {

            if (await _userService.Register(register)) 
            { return Ok(true); } 
            else 
            { return BadRequest(false); }
        }
        catch(ArgumentNullException)
        {
            return BadRequest("No login or password entered");
        }
        catch(Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [Authorize("Bearer", Roles ="Admin")]
    [HttpGet("/Users/GetByEmail")]
    [ProducesResponseType(typeof(UserDto), 200)]
    [Produces("application/json", "text/plain", "text/json")]
    public async Task<IActionResult> GetByEmail([Required] string email) {
        try 
        { 
            return Ok(await _userService.GetUserByEmail(email));
        }
        catch(UnauthorizedAccessException ex) { 
            return Unauthorized(ex.Message);
        }

    }
}