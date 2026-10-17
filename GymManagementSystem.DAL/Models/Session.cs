namespace GymManagementSystem.DAL.Models
{
    public class Session : BaseEntity
    {
        public string Description { get; set; } = default!;
        public int Capacity { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate {  get; set; }

        #region Relationships
        public Trainer Trainer { get; set; } = default!; //Session has one Trainer (Mandatory)
        public int TrainerId { get; set; }//Fk -> Trainer
        public Category Category { get; set; } = default!; // Session has one category(Mandatory)
        public int CategoryId { get; set; } // Fk -> Category
        public ICollection<MemberSession> MemberSessions { get; set; } = default!;// Realtion (M:M) Members -> Session

        #endregion

    }
}
