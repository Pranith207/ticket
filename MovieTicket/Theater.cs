using System;
using System.Collections.Generic;
using System.Linq;

namespace MovieTicket
{
    public class Theater : ITheater
    {
        public int Rows { get; set; }

        public int SeatsInRow { get; set; }

        public int BufferSeats { get; set; }

        public int TotalAvailableSeats { get; set; }

        private PriorityQueue<Tuple<char, List<int>>, int> PriorityQ { get; set; }

        public Theater(int rows = 10, int seatsInRow = 20, int bufferSeats = 3)
        {
            Rows = rows;
            SeatsInRow = seatsInRow;
            BufferSeats = bufferSeats;
            TotalAvailableSeats = rows * seatsInRow;
            PriorityQ = new();
            for (int i = 0; i < rows; i++) 
            {
                var seats = Enumerable.Range(1, seatsInRow).ToList();
                PriorityQ.Enqueue(new Tuple<char, List<int>>(Convert.ToChar(65+i), seats), seats.Count);
            }
        }

        public List<ReservationResponse> ReserveTickets(List<ReservationRequest> request)
        {
            List<ReservationResponse> result = new();
            foreach (ReservationRequest currentRequest in request) {
                try
                {
                    if (currentRequest.ticketCount > TotalAvailableSeats)
                        throw new Exception("Reservation unsuccessful due to seat unavailability");

                    var dequeueTuples = new List<Tuple<char, List<int>>>();
                    ReservationResponse currentResponse = new(currentRequest.reservationId);
                    bool reserved = false;

                    while (PriorityQ.Count > 0)
                    {
                        var topTuple = PriorityQ.Dequeue();
                        var availableSeats = topTuple.Item2.Count;

                        if (availableSeats >= currentRequest.ticketCount) 
                        {
                            char row = topTuple.Item1;
                            var reservedSeats = topTuple.Item2.Take(currentRequest.ticketCount).ToList();
                            var closedSeats = (availableSeats > currentRequest.ticketCount + BufferSeats) ?
                                                            currentRequest.ticketCount + BufferSeats :
                                                            availableSeats;
                            topTuple.Item2.RemoveRange(0, closedSeats);
                            TotalAvailableSeats -= closedSeats;
                            
                            if(topTuple.Item2.Count > 0)
                                dequeueTuples.Add(topTuple);
                            
                            foreach (var seat in reservedSeats) 
                                currentResponse.Seats.Add(row.ToString() + seat.ToString());
                            reserved = true;
                            break;
                        }
                        else
                            dequeueTuples.Add(topTuple);
                    }

                    foreach (var dequeTuple in dequeueTuples)
                        PriorityQ.Enqueue(dequeTuple, dequeTuple.Item2.Count);

                    if (!reserved)
                        currentResponse.Status = "Reservation unsuccessful due to unavailability of seats";
                    result.Add(currentResponse);
                }
                catch (Exception e) 
                {
                    result.Add(new ReservationResponse(currentRequest.reservationId, new List<string>(), e.Message));
                    continue;
                }
            }
            return result;
        }
    }
}
