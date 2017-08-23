using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Security.Claims;
using Hello.Data;

namespace Hello.Controllers
{
    [Produces("application/json")]
    [Route("api/Account")]
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ILogger _logger;

        public AccountController(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            ILoggerFactory loggerFactory)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = loggerFactory.CreateLogger<AccountController>();
        }

        // TODO: move UserModel
        public class UserModel
        {
            public string UserName { get; set; }
            public string Email { get; set; }

            public Dictionary<string, string> Claims { get; set; }
        }

        private async Task<UserModel> GetUser(string userName)
        {
            var user = await _userManager.FindByNameAsync(userName);
            var claims = await _userManager.GetClaimsAsync(user);
            var vm = new UserModel
            {
                Email = user.Email,
                UserName = user.UserName,
                Claims = claims.ToDictionary(c => c.Type, c => c.Value)
            };
            return vm;
        }

        // TODO: move LoginModel
        public class LoginModel
        {
            [Required]

            //[EmailAddress]
            //public string Email { get; set; }
           

            public string Username { get; set; }

            [Required]
            [DataType(DataType.Password)]
            public string Password { get; set; }

            [Display(Name = "Remember me?")]
            public bool RememberMe { get; set; }
        }

        // POST: /Account/Login
        [HttpPost("login")]
        //[AllowAnonymous]
        public async Task<IActionResult> Login([FromBody]LoginModel model, string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;

            if (ModelState.IsValid)
            {
                // This doesn't count login failures towards account lockout
                // To enable password failures to trigger account lockout, set lockoutOnFailure: true
                var result = await _signInManager.PasswordSignInAsync(model.Username, model.Password, model.RememberMe, lockoutOnFailure: false);
                if (result.Succeeded)
                {
                    _logger.LogInformation(1, "User logged in.");
                    var user = await GetUser(model.Username);
                    return Ok(user);
                }
                if (result.RequiresTwoFactor)
                {
                    // TODO: need to handle 2 factor?
                    //return RedirectToAction(nameof(SendCode), new { ReturnUrl = returnUrl, RememberMe = model.RememberMe });
                }
                if (result.IsLockedOut)
                {
                    _logger.LogWarning(2, "User account locked out.");
                    this.ModelState.AddModelError("", "User account locked out.");
                    return BadRequest(this.ModelState);
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                    return BadRequest(this.ModelState);
                }
            }

            // If we got this far, something failed, redisplay form
            return BadRequest(this.ModelState);
        }

        // POST: /Account/LogOff
        [HttpPost("logout")]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            _logger.LogInformation(4, "User logged out.");
            return Ok();
        }

        // TODO: move RegisterModel
        public class RegisterModel
        {
            public string UserName { get; set; }
            public string FirstName { get; set; }
            public string LastName { get; set; }

            public DateTime DateCreated { get; set; }


            [Required]
            [EmailAddress]
            [Display(Name = "Email")]
            public string Email { get; set; }

            [Required]
            [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
            [DataType(DataType.Password)]
            [Display(Name = "Password")]
            public string Password { get; set; }

            [DataType(DataType.Password)]
            [Display(Name = "Confirm password")]
            [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
            public string ConfirmPassword { get; set; }
        }

        // POST: /Account/Register
        [HttpPost("Register")]
        //[AllowAnonymous]
        public async Task<IActionResult> Register([FromBody]RegisterModel model, string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            DateTime now = DateTime.Now;
            
            if (ModelState.IsValid)
            {
                
                var user = new ApplicationUser { UserName = model.UserName , Email = model.Email , FirstName = model.FirstName ,LastName = model.LastName, DateCreated = now };
                var result = await _userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    await _signInManager.SignInAsync(user, isPersistent: false);
                    _logger.LogInformation("User created a new account with password.");
                    var userViewModel = await GetUser(user.UserName);
                    return Ok(userViewModel);
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            // If we got this far, something failed
            return BadRequest(this.ModelState);
        }

        // TODO: move UserModel
        public class ExternalLoginModel
        {
            public string AuthenticationScheme { get; set; }

            public string DisplayName { get; set; }
        }

        // GET api/Account/ExternalLogins?returnUrl=%2F&generateState=true
        //[AllowAnonymous]
        [HttpGet("GetExternalLogins")]
        public async Task<IEnumerable<ExternalLoginModel>> GetExternalLogins()
        {
            return (await _signInManager.GetExternalAuthenticationSchemesAsync()).Select(a => new ExternalLoginModel
            {
                DisplayName = a.DisplayName,
                AuthenticationScheme = a.Name   //a.AuthenticationScheme
            });
        }

        // POST: /Account/ExternalLogin
        [HttpGet("ExternalLogin")]
        //[AllowAnonymous]
        public IActionResult ExternalLogin(string provider, string returnUrl = null)
        {
            // Request a redirect to the external login provider.
            var redirectUrl = Url.Action("ExternalLoginCallback", "Account", new { ReturnUrl = returnUrl });
            var properties = _signInManager.ConfigureExternalAuthenticationProperties(provider, redirectUrl);
            return new ChallengeResult(provider, properties);
        }

        // GET: /Account/ExternalLoginCallback
        [HttpGet("ExternalLoginCallback")]
        //[AllowAnonymous]
        public async Task<IActionResult> ExternalLoginCallback(string returnUrl = null, string remoteError = null)
        {
            // TODO: what to do on error?
            if (remoteError != null)
            {
                _logger.LogError($"Error from external provider: {remoteError}");
                //return RedirectToPage("./Login");
                return LocalRedirect("/");
            }

            var info = await _signInManager.GetExternalLoginInfoAsync();
            if (info == null)
            {
                //return RedirectToAction(nameof(Login));
                return LocalRedirect("/");
            }

            // Sign in the user with this external login provider if the user already has a login.
            var result = await _signInManager.ExternalLoginSignInAsync(info.LoginProvider, info.ProviderKey, isPersistent: false);
            if (result.Succeeded)
            {
                _logger.LogInformation(5, "User logged in with {Name} provider.", info.LoginProvider);
                return LocalRedirect("/");
                //return RedirectToLocal(returnUrl);
            }

            // TODO: refactor and handle ExternalLoginCallback logic
            //if (result.RequiresTwoFactor)
            //{
            //    return RedirectToAction(nameof(SendCode), new { ReturnUrl = returnUrl });
            //}
            if (result.IsLockedOut)
            {
                return View("Lockout");
                // TODO: refactor and handle lockout logic
                throw new Exception("lockout");
            }
            else
            {
                // If the user does not have an account, then ask the user to create an account.
                ViewData["ReturnUrl"] = returnUrl;
                ViewData["LoginProvider"] = info.LoginProvider;
                var email = info.Principal.FindFirstValue(ClaimTypes.Email);

                //return LocalRedirect("/externalRegister");
                //return RedirectToLocal("/externalRegister");
                //return View("ExternalLoginConfirmation", new ExternalLoginConfirmationModel { Email = email });

                #region development code
                var user = new ApplicationUser { UserName = email, Email = email };
                var createUserResult = await _userManager.CreateAsync(user);
                if (createUserResult.Succeeded)
                {
                    createUserResult = await _userManager.AddLoginAsync(user, info);
                    if (createUserResult.Succeeded)
                    {
                        await _signInManager.SignInAsync(user, isPersistent: false);
                        _logger.LogInformation("User created an account using {Name} provider.", info.LoginProvider);
                        //return LocalRedirect(Url.GetLocalUrl(returnUrl));
                        return LocalRedirect("/");
                    }
                }
                foreach (var error in createUserResult.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
                return LocalRedirect("/");
                #endregion development code
            }
        }

        // TODO: move ExternalLoginConfirmationModel
        public class ExternalLoginConfirmationModel
        {
            [Required]
            [EmailAddress]
            public string Email { get; set; }
        }

        // POST: /Account/ExternalLoginConfirmation
        [HttpPost("ExternalLoginConfirmation")]
        //[AllowAnonymous]
        public async Task<IActionResult> ExternalLoginConfirmation([FromBody]ExternalLoginConfirmationModel model, string returnUrl = null)
        {
            if (ModelState.IsValid)
            {
                // Get the information about the user from the external login provider
                var info = await _signInManager.GetExternalLoginInfoAsync();
                if (info == null)
                {
                    ModelState.AddModelError("", "ExternalLoginFailure");
                    return BadRequest(this.ModelState);
                    //return View("ExternalLoginFailure");
                }
                var user = new ApplicationUser { UserName = model.Email, Email = model.Email };
                var result = await _userManager.CreateAsync(user);
                if (result.Succeeded)
                {
                    result = await _userManager.AddLoginAsync(user, info);
                    if (result.Succeeded)
                    {
                        await _signInManager.SignInAsync(user, isPersistent: false);
                        _logger.LogInformation(6, "User created an account using {Name} provider.", info.LoginProvider);
                        var userViewModel = await GetUser(user.UserName);
                        return Ok(userViewModel);
                    }
                }
                AddErrors(result);
            }

            // ViewData["ReturnUrl"] = returnUrl;
            return BadRequest(this.ModelState);
        }

        // TODO: refactor this, is the user supposed to see this?  Is the error log enough info?
        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                _logger.LogError(error.Description);
            }
        }

        [HttpGet("checkAuthentication")]
        //[AllowAnonymous]
        public async Task<IActionResult> CheckAuthentication()
        {
            if (this._signInManager.IsSignedIn(this.User))
            {
                var userViewModel = await GetUser(this.User.Identity.Name);
                return Ok(userViewModel);
            }
            return Ok();
        }
    }
}