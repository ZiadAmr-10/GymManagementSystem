using GymManagementSystem.DAL.Models.Enums;

namespace GymManagementSystem.DAL.Models;

public class Trainer : GymUsers
{
    //HireDate => CreatedAt
    public Specialty Specialty { get; set; }

    #region Relationships
    public ICollection<Session> Sessions { get; set; } = default!; //Trainer has many sessions (Optional)   

    #endregion

}
