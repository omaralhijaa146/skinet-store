using System.Security.Claims;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using skinet.API.Dtos;
using skinet.API.Errors;
using skinet.API.Extensions;
using skinet.Core.Entities.Identity;
using skinet.Core.Interfaces;

namespace skinet.API.Controllers;

public class AccountController:BaseApiController
{
    private readonly UserManager<AppUser> _userManager;
    private readonly SignInManager<AppUser> _signInManager;
    private readonly ITokenService _tokenService;
    private readonly IMapper _mapper;

    public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager,ITokenService tokenService,IMapper mapper)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _tokenService = tokenService;
        _mapper = mapper;
    }

    [HttpGet]
    [Authorize]
    public async Task<ActionResult<UserDto>> GetCurrentUser()
    {
        
        var user = await _userManager.FindByEmailFromClaimsPrinciple(User);
        
        return Ok(new UserDto
        {
            Email = user.Email,
            Token=_tokenService.CreateToken(user),
            DisplayName = user.DisplayName
        });
    }

    [HttpGet("emailexists")]
    public async Task<ActionResult<bool>> CheckEmailExists([FromQuery] string email)
    {
        return await _userManager.FindByEmailAsync(email) != null;
    }

    [HttpGet("address")]
    [Authorize]
    public async Task<ActionResult<AddressDto>> GetUserAddress()
    {
        var user = await _userManager.FindUserByClaimsPrincipleWithAddress(User);
        return _mapper.Map<Address,AddressDto>(user.Address);
    }

    [HttpPut("address")]
    [Authorize]
    public async Task<ActionResult<AddressDto>> UpdateUserAddress(AddressDto address)
    {
        var user = await _userManager.FindUserByClaimsPrincipleWithAddress(User);

        user.Address = _mapper.Map<AddressDto, Address>(address);
        var result = await _userManager.UpdateAsync(user);
        if(result.Succeeded)
            return Ok(_mapper.Map<Address,AddressDto>(user.Address));
        return BadRequest(new ApiResponse(400,"Problem updating the user"));

    }
    

    [HttpPost("register")]
    public async Task<ActionResult<UserDto>> Register(RegisterDto register)
    {
        if ((await CheckEmailExists(register.Email)).Value)
        {
            return new BadRequestObjectResult(new ApiValidationErrorResponse
            {
                Errors = new []{"Email address is in use"}
            });
        }
        
        var user = new AppUser
        {
            DisplayName = register.DisplayName,
            Email = register.Email,
            UserName = register.Email
        };

        var result = await _userManager.CreateAsync(user, register.Password);

        if (!result.Succeeded)
        {
            return BadRequest(new ApiResponse(400));
        }
        return Ok(new UserDto
        {
            Email = user.Email,
            DisplayName = user.DisplayName,
            Token = _tokenService.CreateToken(user)
        });
    }

    [HttpPost("login")]
    public async Task<ActionResult<UserDto>> Login(LoginDto login)
    {
        var user = await _userManager.FindByEmailAsync(login.Email);
        if (user is null)
        {
            return Unauthorized(new ApiResponse(401));
        }

        var result = await _signInManager.CheckPasswordSignInAsync(user, login.Password, false);
        
        if(!result.Succeeded)
        {
            return Unauthorized(new ApiResponse(401));
        }

        return Ok(new UserDto
        {
            Email = user.Email,
            Token=_tokenService.CreateToken(user),
            DisplayName = user.DisplayName
        });
    }
}