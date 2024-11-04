using System.ComponentModel.DataAnnotations;
using System.Text.Json;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Primitives;

namespace HxContact.WebApp.Pages;

public class Contact
{
    public required string Id { get; set; }
    public required string Name { get; set; }
    public required string Email { get; set; }
}


public class ContactRepository
{
    private static readonly List<Contact> ContactList = JsonSerializer.Deserialize<List<Contact>>(File.ReadAllText("Data/mock_contacts.json"))!;

    public List<Contact> GetContacts(int page, int pageSize)
    {
        int skip = (page - 1) * pageSize;
        if (skip + pageSize > ContactList.Count)
        {
            return [];
        }

        return ContactList.Skip(skip).Take(pageSize).ToList();
    }

    public Contact? GetContactById(string id) => ContactList.FirstOrDefault(x => x.Id == id);

    public void AddContact(Contact contact) => ContactList.Add(contact);
}

public class AddContactFormData
{
    [Required]
    public required string Name { get; set; }

    [Required]
    [EmailAddress]
    public required string Email { get; set; }

    public List<string> Errors { get; set; } = [];
}

public class ContactListViewModel
{
    public List<Contact> Contacts { get; set; } = [];
    public int Page { get; set; } = 1;
    public int ItemsPerPage { get; set; } = 25;
}

public class Contacts(ContactRepository repository) : PageModel
{
    public ContactListViewModel ContactList { get; set; } = new();

    public Contact? Contact { get; set; }

    [BindProperty]
    public AddContactFormData FormData { get; set; } = new()
    {
        Name = "",
        Email = ""
    };

    public IActionResult OnGet()
    {
        ViewData["HX-Request"] = Request.Headers["HX-Request"];

        ContactList.Contacts = repository.GetContacts(1, ContactList.ItemsPerPage);

        return Page();
    }

    public IActionResult OnGetLoadMore(int pageNumber)
    {
        ContactList.Contacts = repository.GetContacts(pageNumber, ContactList.ItemsPerPage);
        ContactList.Page = pageNumber;

        return Partial("_ContactList", ContactList);
    }

    public IActionResult OnPost([FromForm] AddContactFormData formData)
    {
        if (!ModelState.IsValid)
        {
            return Partial("_AddContactForm" );
        }

        Contact = new Contact
        {
            Id = Guid.NewGuid().ToString(),
            Name = FormData.Name,
            Email = FormData.Email,
        };

        return Partial("_ContactAddedResult", this);
    }
}
