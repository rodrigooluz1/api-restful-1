using API.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.Domain.ViewModels
{
    public class VideoViewModel
    {
        public string Id { get; set; }
        public string Hat { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public string UrlVideo { get; set; }
        public string Thumbnail { get; set; }
        public string Slug { get; set; }
        public Status Status { get; set; }
    }
}
