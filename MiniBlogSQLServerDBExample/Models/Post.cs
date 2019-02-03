using System;
using System.ComponentModel.DataAnnotations;

namespace MiniBlogSQLServerDBExample.Models
{
    public class Post
    {
        public int Id { get; set; }
        public string Title { get; set; }

        [Display(Name = "Date of Post")]
        [DataType(DataType.Date)]
        public DateTime PostDate { get; set; }
        public string AuthName { get; set; }
        public string PostContent { get; set; }
    }
}
