namespace HxContact.WebApp.Pages.ContactList;

public class ContactListViewModel
{
    public List<Contact> Contacts { get; set; } = [];
    public int Page { get; set; } = 1;
    public int ItemsPerPage { get; set; } = 25;
}
