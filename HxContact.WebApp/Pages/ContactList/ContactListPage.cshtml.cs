using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace HxContact.WebApp.Pages.ContactList;

public class ContactListPage(ContactRepository repository) : PageModel
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
