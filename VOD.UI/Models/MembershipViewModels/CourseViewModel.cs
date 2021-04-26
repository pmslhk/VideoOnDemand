using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VOD.Common.DTOModels.UI;

                         //20210426 제거 
namespace VOD.UI.Models  //.MembershipViewModels
{
    public class CourseViewModel
    {
        public CourseDTO Course { get; set; }
        public InstructorDTO Instructor { get; set; }
        public  IEnumerable<ModuleDTO> Modules { get; set; }
    }
}
