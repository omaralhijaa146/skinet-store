﻿using System.ComponentModel.DataAnnotations;

namespace skinet.Core.Entities.Identity;

public class Address

{
    public int Id { get; set; }
    public string FirsName { get; set; }
    public string LastName { get; set; }
    public string Street { get; set; }
    public string City { get; set; }
    public string State { get; set; }
    public string ZipCode { get; set; }
    [Required]
    public string AppUserId { get; set; }
    public AppUser AppUser { get; set; }
}