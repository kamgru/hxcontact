using System.ComponentModel.DataAnnotations;

namespace HxContact.WebApp.Pages.ContactList;

public class AddContactFormData
{
    [Required]
    public required string Name { get; set; }

    [Required]
    [EmailAddress]
    public required string Email { get; set; }
}
