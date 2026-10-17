namespace GymManagementSystem.DAL.Models;

public class Member : GymUsers
{
    //JoinDate => CreatedAt
    public string? Photo { get; set; }

    #region Relationships
    public HealthRecord HealthRecord { get; set; } = default!; //Member has one healthrecord(Mandatory)
    public ICollection<MemberShip> MemberShips { get; set; } = default!;// Realtion (M:M) Members -> Plans  
    public ICollection<MemberSession> MemberSessions { get; set; } = default!;// Realtion (M:M) Members -> Session
    #endregion

}
