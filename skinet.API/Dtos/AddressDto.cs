﻿using System.ComponentModel.DataAnnotations;

namespace skinet.API.Dtos;

public class AddressDto
{
    [Required]
    public string FirsName { get; set; }
    [Required]
    public string LastName { get; set; }
    [Required]
    public string Street { get; set; }
    [Required]
    public string City { get; set; }
    [Required]
    public string State { get; set; }
    [Required]
    public string ZipCode { get; set; }
}