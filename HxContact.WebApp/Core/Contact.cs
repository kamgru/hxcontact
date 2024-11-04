namespace HxContact.WebApp.Pages;

public class Contact
{
    public required string Id { get; set; }
    public required string Name { get; set; }
    public required string Email { get; set; }
    public string? Phone { get; set; }
    public DateTime? Birthday { get; set; }
}