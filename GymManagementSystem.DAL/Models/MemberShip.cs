namespace GymManagementSystem.DAL.Models;

public class MemberShip : BaseEntity
{
    //Class MemberShip -> Relation between Members -> Plans 
    public Member Member { get; set; } = default!;
    public int MemberId { get; set; } //Fk -> Member 
    public Plan Plan { get; set; } = default!;
    public int PlanId { get; set; } // Fk -> Plan

    //StartDate => Createdat
    public DateTime EndDate { get; set; }
    public string Status => IsActive ? "Active" : "Expired" ; //Derived attribute -> ComputedProperty
    public bool IsActive => EndDate > DateTime.Now && CreatedAt<=DateTime.Now; // ComputedProperty


}
