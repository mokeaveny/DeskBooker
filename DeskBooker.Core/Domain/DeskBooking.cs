using System.Text.Json.Serialization;

namespace DeskBooker.Core.Domain
{
    public class DeskBooking : DeskBookingBase
    {
        [JsonIgnore]
        public int Id { get; set; }
        public int DeskId { get; set; }
    }
}
