using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VOD.Common.DTOModels;
using VOD.Common.DTOModels.UI;
//using VOD.UI.Models.MembershipViewModels;

namespace VOD.UI.Models
{
    public class DashboardViewModel
    {
        public List<List<CourseDTO>> Courses { get; set; }

    }
}
