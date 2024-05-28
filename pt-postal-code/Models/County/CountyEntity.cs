using PChouse.PTPostalCode.Models.Address;

namespace PChouse.PTPostalCode.Models.County;

public partial class CountyEntity
{
    public virtual string Dd { get; set; } = String.Empty;

    public virtual string Cc { get; set; } = String.Empty;

    public virtual string CountyName { get; set; } = String.Empty;

    public override bool Equals(object? obj)
    {
        if (obj == null || GetType() != obj.GetType())
            return false;

        var other = (AddressEntity)obj;
        return Dd == other.Dd && Cc == other.Cc;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Cc, Dd);
    }

}
