using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace HxContact.WebApp.Pages;

public class ContactDetails(ContactRepository repository) : PageModel
{
    public IActionResult OnGet(string id)
    {
        Contact? contact = repository.GetContactById(id);
        if (contact is null)
        {
            return NotFound();
        }

        return Page();
    }
}
