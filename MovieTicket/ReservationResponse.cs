using System.Collections.Generic;

namespace MovieTicket
{
    public class ReservationResponse
    {
        public string ReservationId { get; }

        public List<string> Seats { get; set; }

        public string Status { get; set; }

        public ReservationResponse(string reservationId)
        {
            ReservationId = reservationId;
            Seats = new List<string>();
            Status = string.Empty;
        }

        public ReservationResponse(string reservationId, List<string> seats, string status)
        {
            ReservationId = reservationId;
            Seats = seats;
            Status = status;
        }
    }
}
