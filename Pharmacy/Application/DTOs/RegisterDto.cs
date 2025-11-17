using System.ComponentModel.DataAnnotations;

namespace Pharmacy.Application.DTOs;

public class RegisterDto
{
    [Required]
    [StringLength(100)]
    public string Username { get; set; } = string.Empty;

    [Required]
    [EmailAddress]
    public string Email { get; set; } = string.Empty;

    [Required]
    [StringLength(100, MinimumLength = 6)]
    public string Password { get; set; } = string.Empty;

    // Employee Information
    [Required]
    [StringLength(100)]
    public string EmployeeName { get; set; } = string.Empty;

    [Required]
    [StringLength(50)]
    public string Position { get; set; } = string.Empty;

    [Phone]
    public string? PhoneNumber { get; set; }

    [Range(0, 999999)]
    public decimal Salary { get; set; }
}
