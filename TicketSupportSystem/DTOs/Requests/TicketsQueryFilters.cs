using TicketSupportSystem.Data.Enums;

namespace TicketSupportSystem.DTOs.Requests
{
    public class TicketsQueryFilters
    {
        public int Take { get; set; } = 10;
        public int Skip { get; set; } = 0;
        public string SortColumn { get; set; } = "CreatedAt";
        public string Order { get; set; } = "desc";
        public Priority? Priority { get; set; }
        public Status? Status { get; set; }
        public Guid? AsignedToId { get; set; }
        public Guid? UserId { get; set; }
    }
}
