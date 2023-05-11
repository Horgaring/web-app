


using System.ComponentModel.DataAnnotations;

public class Account{
    
    public int Id { get; set; }
    [MaxLength(20)]
    public string Name { get; set; } = null!;
    public string Email { get; set; } = null!;
    [MinLength(8)]
    public string Password { get; set; } = null!;
    public string? PathImage { get; set; } = null!;
    public int Wallet { get; set; }
    public List<Product>? Products { get; set; }
    public List<ChatModel>?  Groups { get; set; }
    public static Account Create(AccountDTO acc)=> new Account{
        Name = acc.Name,
        Email = acc.Email,
        Password = acc.Password,
        PathImage = acc.PathImage,
    };
}