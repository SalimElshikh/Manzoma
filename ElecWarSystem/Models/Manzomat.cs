using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ElecWarSystem.Models
{
    public class Manzomat
    {
        public int Id { get; set; } // معرف البطاقة
        public string ImageUrl { get; set; } // رابط الصورة
        public string Title { get; set; } // عنوان البطاقة
        public string Description { get; set; } // وصف البطاقة
        public string ButtonText { get; set; } // نص الزر
        public string LinkUrl { get; set; }
    }
}