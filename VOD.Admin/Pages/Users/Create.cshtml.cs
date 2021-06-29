using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using VOD.Common.DTOModels;
using VOD.Database.Services;

namespace VOD.Admin.Pages.Users
{
    [Authorize(Roles = "Admin")]
    public class CreateModel : PageModel
    {
        #region Properties and Variables
        private readonly IUserService _userService;
        [BindProperty]
        public RegisterUserDTO Input { get; set; } =
        new RegisterUserDTO();
        [TempData] public string Alert { get; set; }
        #endregion
        #region Constructor
        public CreateModel(IUserService userService)
        {
            _userService = userService;
        }
        #endregion
        #region Actions
        public async Task OnGetAsync()
        {
        }
        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                var result = await _userService.AddUserAsync(Input);
                if (result.Succeeded)
                {
                    // Message sent back to the Index Razor Page.
                    Alert = $"Created a new account for {Input.Email}.";

                    return RedirectToPage("Index");
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty,
                    error.Description);
                }
            }
            // Something failed, redisplay the form.
            return Page();
        }
        #endregion

    }
}
