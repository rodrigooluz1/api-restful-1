using API.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.Domain.ViewModels
{
    public class NewsViewModel
    {
        public int MyProperty { get; set; }
        public string Hat { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }
        public string Author { get; set; }
        public string Img { get; set; }
        public string Link { get; set; }
        public Status Status { get; set; }
    }
}
