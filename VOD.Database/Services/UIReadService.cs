using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VOD.Common.Entities;

namespace VOD.Database.Services
{
    public class UIReadService : IUIReadService
    {
        private readonly IDbReadService _db;
        public UIReadService(IDbReadService db)
        {
            _db = db;
           
        }

        public async Task<Course> GetCourse(string userId, int courseId)
        {
            _db.Include<Course, Module>();
            var userCourse = await _db.SingleAsync<UserCourse>(c =>
                c.UserId.Equals(userId) && c.CourseId.Equals(courseId));

            if (userCourse == null) return default;
            return userCourse.Course;

            //throw new NotImplementedException();
        }

        public async Task<IEnumerable<Course>> GetCourses(string userId)
        {
            _db.Include<UserCourse>();
            var userCourses = await _db.GetAsync<UserCourse>(uc =>
            uc.UserId.Equals(userId));
            return userCourses.Select(c => c.Course);


            //throw new NotImplementedException();
        }

        public async Task<Video> GetVideo(string userId, int videoId)
        {
            _db.Include<Course>();
            var video = await _db.SingleAsync<Video>(v => v.Id.Equals(videoId));
            var userCourse = await _db.SingleAsync<UserCourse>(c =>c.UserId.Equals(userId) 
                                                                   && c.CourseId.Equals(video.CourseId));

            if (video == null) return default;
            return video;

            //throw new NotImplementedException();
        }

        public async Task<IEnumerable<Video>> GetVideos(string userId, int moduleId = 0)
        {
            _db.Include<Video>();
            var module = await _db.SingleAsync<Module>(m => m.Id.Equals(moduleId));
            if (module == null) return default(List<Video>);

            var userCourse = await _db.SingleAsync<UserCourse>(uc => uc.UserId.Equals(userId) 
                                                                  && uc.CourseId.Equals(module.CourseId));
            if (userCourse == null) return default(List<Video>);
            return module.Videos;


            //throw new NotImplementedException();
        }
    }
}
