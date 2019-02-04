using System;
using System.ComponentModel.DataAnnotations;

namespace MiniBlogSQLServerDBExample.Models
{
    // Post är den ända model som jag har använd överallt. Uppe på varje .cshtml sida finns en pipline IIS module som 
    // gör request till backend, och sätter in data till module variabler 

    public class Post
    {
        public int Id { get; set; }
        public string Title { get; set; }

        [Display(Name = "Post Date")]
        [DataType(DataType.Date)]
        public DateTime PostDate { get; set; }
        public string AuthName { get; set; }
        public string PostContent { get; set; }
    }
}
