using GymManagementSystem.DAL.Models.Enums;

namespace GymManagementSystem.BLL.Services.Trainers.ViewModels
{
    public class TrainerToUpdateViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; } = default!; //DisplayOnly
        public string Email { get; set; } = default!;
        public string Phone { get; set; } = default!;
        public Specialty Specialty { get; set; }
        public int BuildingNumber { get; set; }
        public string Street { get; set; } = default!;
        public string City { get; set; } = default!;
    }
}
