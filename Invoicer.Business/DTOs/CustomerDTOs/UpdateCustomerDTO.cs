﻿namespace Invoicer.Business.DTOs.CustomerDTOs;

public record UpdateCustomerDTO
{
	public string? Name { get; set; }
	public string? Email { get; set; }
	public string? PhoneNumber { get; set; }
}
