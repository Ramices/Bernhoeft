using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.IO;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Bernhoeft.Entitys
{
    [Table("users")]
    public class User
    {
        public User()
        {

        }

        [Display(Name ="id")]
        [Column("id")]
        public int Id { get; set; }

        [Display(Name = "name")]
        [Column("name")]
        public string Name { get; set; }

        [Display(Name = "email")]
        [Column("email")]
        public string Email { get; set; }

        [Display(Name = "username")]
        [Column("username")]
        public string Username { get; set; }

        [Display(Name = "password")]
        [Column("password")]
        public string Password { get; set; }

    }
}
