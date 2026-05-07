using static DMSWebPortal.Controllers.appControllers;

namespace DMSWebPortal.DTOs
{
    public class OrderRequestDto
    {
        public HeaderDto Header { get; set; }

        public List<DetailsOneDto>? DetailsOne { get; set; }
        public List<DetailsTwoDto>? DetailsTwo { get; set; }
    }
}
