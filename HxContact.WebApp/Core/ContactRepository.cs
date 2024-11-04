using System.Text.Json;

namespace HxContact.WebApp.Pages;

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