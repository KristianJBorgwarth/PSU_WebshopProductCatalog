using Webshop.Domain.Common;

namespace Webshop.BookStore.Domain.AggregateRoots;

public enum Role
{
    Seller,
    Buyer
}
public class BookstoreCustomer : AggregateRoot
{
    public int Id { get; set; }
    public bool IsSeller { get; set; }
    public bool IsBuyer { get; set; }

    public void AddRole(Role customerRole)
    {
        switch (customerRole)
        {
            case Role.Buyer:
                IsBuyer = true;
                break;
            case Role.Seller:
                IsSeller = true;
                break;
            default:
                throw new ArgumentException("Invalid role.", nameof(Role));
        }
    }

    public void RemoveRole(Role role)
    {
        switch (role)
        {
            case Role.Buyer:
                IsBuyer = false;
                break;
            case Role.Seller:
                IsSeller = false;
                break;
            default:
                throw new ArgumentException("Invalid role.", nameof(Role));
        }
    }

    public bool HasRole(string roleName)
    {
        return roleName switch
        {
            "Buyer" => IsBuyer,
            "Seller" => IsSeller,
            _ => false
        };
    }
}