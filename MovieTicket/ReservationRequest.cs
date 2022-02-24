using System;
using System.Collections.Generic;
using System.Text;

namespace MovieTicket
{
    public class ReservationRequest
    {
        public string reservationId { get; }

        public int ticketCount { get; set; }

        public ReservationRequest(string reservationId, int ticketCount) {
            this.reservationId = reservationId;
            this.ticketCount = ticketCount;
        }
    }
}
