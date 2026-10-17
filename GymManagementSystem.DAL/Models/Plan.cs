namespace GymManagementSystem.DAL.Models;

public class Plan : BaseEntity
{

    public string Name { get; set; } = null!;
    public string Description { get; set; } = null!;
    public int DurationDays { get; set; }
    public decimal Price { get; set; }

    #region Relationships
    public bool IsActive { get; set; }   // soft-delete flag
    public ICollection<MemberShip> MemberShips { get; set; } = default!;// Realtion (M:M) Plans -> Members  

    #endregion

}
