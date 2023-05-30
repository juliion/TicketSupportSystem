namespace TicketSupportSystem.DTOs.Responses
{
    public class FilteredTicketsDTO
    {
        public int Take { get; set; }
        public int Skip { get; set; }
        public long Total { get; set; }
        public List<TicketDTO> Tickets { get; set; } = new List<TicketDTO>();
    }
}
