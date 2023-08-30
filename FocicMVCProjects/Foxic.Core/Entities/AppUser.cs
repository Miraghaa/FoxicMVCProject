using Microsoft.AspNetCore.Identity;

namespace Foxic.Core.Entities;

public class AppUser:IdentityUser
{
	public string Fullname { get; set; } = null!;
}
