namespace GymManagementSystem.BLL.Services.Plans.ViewModels
{
    public class PlanDetailsViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; } = default!;
        public decimal Price { get; set; }
        public int DurationDays { get; set; }
        public string Description { get; set; } = default!;
        public bool IsActive { get; set; }
    }
}
