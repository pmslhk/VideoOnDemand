using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using VOD.Common.DTOModels;
using VOD.Common.DTOModels.Admin;
using VOD.Common.Entities;
using VOD.Common.Services;
using VOD.Database.Services;


namespace VOD.Admin.Pages.Courses
{
    [Authorize(Roles = "Admin")]
    public class CreateModel : PageModel
    {
        #region Properties and Variables
        private readonly IAdminService _db;


        [BindProperty]
        public CourseDTO Input { get; set; } = new CourseDTO();

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
                var succeeded = (await _db.CreateAsync<CourseDTO,
                Course>(Input)) > 0;
                if (succeeded)
                {
                    // Message sent back to the Index Razor Page.
                    Alert = $"Created a new Instructor: {Input.Description}.";
                    return RedirectToPage("Index");
                }
            }
            // Something failed, redisplay the form.
            return Page();
        }
        #endregion
    }
}
