namespace GymManagementSystem.DAL.Models;

public class Category : BaseEntity
{
    public string CategoryName { get; set; } = default!;

    #region Relationships
    public ICollection<Session> Sessions { get; set; } = default!; //One category has many sessions(optional)

    #endregion
}
