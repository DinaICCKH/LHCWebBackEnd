using DMSWebPortal.Models;

namespace DMSWebPortal.DTOs
{
    public class OrderDto
    {
        public SO Header { get; set; }
        public List<SO1> Details { get; set; }
    }
}
