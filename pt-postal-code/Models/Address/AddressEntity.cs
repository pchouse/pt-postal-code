
namespace PChouse.PTPostalCode.Models.Address;

public partial class AddressEntity
{
    public virtual string Dd { get; set; } = string.Empty;

    public virtual string Cc { get; set; } = string.Empty;

    public virtual string Llll { get; set; } = string.Empty;

    public virtual string City { get; set; } = string.Empty;

    public virtual string ArtCod { get; set; } = string.Empty;

    public virtual string Street { get; set; } = string.Empty;

    public virtual string ArtLocal { get; set; } = string.Empty;

    public virtual string Chunk { get; set; } = string.Empty;

    public virtual string Dor { get; set; } = string.Empty;

    public virtual string Client { get; set; } = string.Empty;

    public virtual string Pc4 { get; set; } = string.Empty;

    public virtual string Pc3 { get; set; } = string.Empty;

    public virtual int Cpalf { get; set; }

    public override bool Equals(object? obj)
    {
        if (obj == null || GetType() != obj.GetType())
            return false;

        var other = (AddressEntity)obj;
        return Pc4 == other.Pc4 && Pc3 == other.Pc3;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Pc4, Pc3);
    }

}
