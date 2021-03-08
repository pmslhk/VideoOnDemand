using System;
using System.Collections.Generic;
using System.Text;

namespace VOD.Common.DTOModels_UI
{
    class VideoDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string TiDescriptiontle { get; set; }
        public string Duration { get; set; }
        public string Thumbnail { get; set; }
        public string Url { get; set; }
    }
}
