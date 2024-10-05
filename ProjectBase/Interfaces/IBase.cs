namespace ProjectBase.Interfaces;

public interface IBase: ITimestamps
{
    public Guid Id { get; set; }
    public bool? IsActive { get; set; }
}