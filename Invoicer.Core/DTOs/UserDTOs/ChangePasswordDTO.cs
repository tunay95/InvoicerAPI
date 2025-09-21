namespace Invoicer.Core.DTOs.UserDTOs;

public record ChangePasswordDTO
{
	public string PreviousPassword { get; set; }
	public string NewPassword { get; set; }
}
