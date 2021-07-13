using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using VOD.Common.DTOModels;
using VOD.Common.DTOModels.Admin;
using VOD.Database.Services;

namespace VOD.Admin.Pages.Videos

{
    [Authorize(Roles = "Admin")]
    public class DeleteModel : PageModel
    {
        #region Properties
        private readonly IAdminService _db;
        [BindProperty] public VideoDTO Input { get; set; } = new VideoDTO();
        [TempData] public string Alert { get; set; }
        #endregion
        #region Constructor
        public DeleteModel(IAdminService db)
        {
            _db = db;
        }
        #endregion
        #region Actions
        public async Task<IActionResult> OnGet(int id, int courseId,
        int moduleId)
        {
            try
            {
                Input = await _db.SingleAsync<Video, VideoDTO>(s =>
                s.Id.Equals(id) && s.ModuleId.Equals(moduleId) &&
                s.CourseId.Equals(courseId), true);
                return Page();
            }
            catch
            {
                return RedirectToPage("/Index", new
                {
                    alert = "You do not have access to this page."
                });
            }
        }
        public async Task<IActionResult> OnPostAsync()
        {
            int id = Input.Id, moduleId = Input.ModuleId,
            courseId = Input.CourseId;
            if (ModelState.IsValid)
            {
                var succeeded = await _db.DeleteAsync<Video>(s =>
                s.Id.Equals(id) && s.ModuleId.Equals(moduleId) &&
                s.CourseId.Equals(courseId));

                if (succeeded)
                {
                    // Message sent back to the Index Razor Page.
                    Alert = $"Deleted Video: {Input.Title}.";
                    return RedirectToPage("Index");
                }
            }
            // Something failed, redisplay the form.
            Input = await _db.SingleAsync<Video, VideoDTO>(s =>
            s.Id.Equals(id) && s.ModuleId.Equals(moduleId) &&
            s.CourseId.Equals(courseId), true);
            return Page();
        }
        #endregion
    }
}
