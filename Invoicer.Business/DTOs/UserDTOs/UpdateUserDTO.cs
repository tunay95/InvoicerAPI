namespace Invoicer.Business.DTOs.UserDTOs;

public record UpdateUserDTO
{
	public string? FirstName { get; set; }
	public string? LastName { get; set; }
	public string? Email { get; set; }
	//public string? Password { get; set; }
	public string? Address { get; set; }
	public string? PhoneNumber { get; set; }
}
