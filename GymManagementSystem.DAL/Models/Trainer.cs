using GymManagementSystem.DAL.Models.Enums;

namespace GymManagementSystem.DAL.Models;

public class Trainer : GymUsers
{
    //HireDate => CreatedAt
    public Specialty Specialty { get; set; }

}
