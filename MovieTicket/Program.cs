using System;
using System.Collections.Generic;
using System.IO;

namespace MovieTicket
{
    public class Program
    {
        public static void Main(string[] args)
        {
            int rows = 10, seatsInRow = 20, bufferSeats = 3;
            var theater = new Theater(rows, seatsInRow, bufferSeats);

            List<string> inputLines = new(File.ReadAllLines(@"C:\Users\PRADEEP\Desktop\MovieTicket\MovieTicket\input.txt"));
            List<string> outputLines = new();

            List<ReservationRequest> request = new();
            foreach (string line in inputLines)
            {
                string[] words = line.Split(' ');
                string reservationId = words[0];

                if (!int.TryParse(words[1], out int seatsRequested))
                    Console.WriteLine("Incorrect input format");
                else
                    request.Add(new ReservationRequest(reservationId, seatsRequested));
            }

            var response = theater.ReserveTickets(request);
            foreach (ReservationResponse currentResponse in response)
                outputLines.Add(currentResponse.ReservationId + " " +
                    (currentResponse.Seats.Count == 0 ? currentResponse.Status : String.Join(", ", currentResponse.Seats.ToArray())));

            File.WriteAllLines(@"C:\Users\PRADEEP\Desktop\MovieTicket\MovieTicket\output.txt", outputLines.ToArray());
            Console.WriteLine("Press any key to exit.");
            Console.ReadKey();
        }
    }
}
