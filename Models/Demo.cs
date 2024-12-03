using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace BulkyWeb.Models;

public class Demo
{

     [Key] 
    public int UserId { get; set; } // Primary Key and Auto Increment
    public string? Firstname { get; set; }
    public string? Lastname { get; set; }
    public string? Mobilenumber { get; set; }
}
