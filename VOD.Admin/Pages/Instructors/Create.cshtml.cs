using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using VOD.Common.DTOModels;
using VOD.Database.Services;


namespace VOD.Admin.Pages.Instructors
{
    [Authorize(Roles = "Admin")]
    public class CreateModel : PageModel
    {
        #region Properties and Variables
        private readonly IAdminService _db;


        [BindProperty]
        public InstructorDTO Input { get; set; } = new InstructorDTO();

        [TempData] public string Alert { get; set; }
        #endregion
        #region Constructor

        public CreateModel(IAdminService db)
        {
            _db = db;
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
                var succeeded = (await _db.CreateAsync<InstructorDTO,
                Instructor>(Input)) > 0;
                if (succeeded)
                {
                    // Message sent back to the Index Razor Page.
                    Alert = $"Created a new Instructor: {Input.Name}.";
                    return RedirectToPage("Index");
                }
            }
            // Something failed, redisplay the form.
            return Page();
        }
        #endregion
    }
}
