using Microsoft.AspNetCore.Identity;

namespace MyUserCenter.Domain;

public class MyUser : IdentityUser {
    public string? DisplayName { get; set; }
}
