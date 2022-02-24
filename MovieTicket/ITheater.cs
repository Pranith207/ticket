using System.Collections.Generic;

namespace MovieTicket
{
    public interface ITheater
    {
        public List<ReservationResponse> ReserveTickets(List<ReservationRequest> request);
    }
}
