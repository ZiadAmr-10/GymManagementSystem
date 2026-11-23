namespace GymManagementSystem.BLL.Services.Sessions.ViewModels
{
    public class SessionDetailsViewModel
    {
        public string CategoryName { get; set; } = default!;
        public string TrainerName { get; set; } = default!;
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Description { get; set; } = default!;
        public int Capacity { get; set; }
        public int BookedSlots { get; set; }


        //ComputedProperty
        public string Status
        {
            get
            {
                if (StartDate > DateTime.Now)
                    return "Upcoming";
                else if (StartDate <= DateTime.Now && EndDate >= DateTime.Now)
                    return "Ongoing";
                else
                    return "Completed";
            } 
        }
        public TimeSpan Duration => EndDate - StartDate;
        public string CapacityDisplay => $"{BookedSlots} / {Capacity} slots";

    }
}
