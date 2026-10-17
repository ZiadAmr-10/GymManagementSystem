namespace GymManagementSystem.DAL.Models;

public class MemberSession : BaseEntity
{
    //Class MemberSession -> Between Member -> Session
    public Member Member { get; set; } = default!;
    public int MemberId; //Fk -> Member
    public Session Session { get; set; } = default!;
    public int SessionId { get; set; } //Fk->Session
    public int BookingDays { get; set; }
    public bool IsAttended { get; set; }

}
