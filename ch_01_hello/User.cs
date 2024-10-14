public class User
{
    public string Name { get; set; }
    public string Lastname { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public DateTime CreationDate { get; private set; } 

    public User(string name, string lastname, string email, string password)
    {
        Name = name;
        Lastname = lastname;
        Email = email;
        Password = password;
        CreationDate = DateTime.Now; 
    }

    public int GetCreationYear() => CreationDate.Year; 
}
