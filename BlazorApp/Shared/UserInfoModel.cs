namespace BlazorApp.Shared;

public class UserInfoModel
{
    public string? UserName { get; set; }
    public IEnumerable<string> Roles { get; set; }
}