using System;
using System.ComponentModel.DataAnnotations;

namespace MyHiveService.Models.DB
{
    public class User
    {
        [Key]
        public Guid? id { get; set; }

        [Required]
        [EmailAddress]
        public string username { get; set; }

        [Required]
        public string password { get; set; }
    }
}