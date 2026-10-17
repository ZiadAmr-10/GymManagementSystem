namespace GymManagementSystem.DAL.Models;

public class HealthRecord : BaseEntity
{
    public string Height { get; set; } = default!;
    public string Weight { get; set; } = default!;
    public string BloodType { get; set; } = default!;
    public string? Note { get; set; }
    #region Relationships
    public Member Member { get; set; } = default!; // Healthrecord has one member(Mandatory)
    public int MemberId { get; set; } // Fk -> Member

    #endregion
}
