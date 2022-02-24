using NUnit.Framework;
using Moq;
using MovieTicket;

namespace MovieTicketTests
{
    public class TheaterTests
    {
        public ITheater theater;
        public TheaterTests() 
        {
            theater = new Mock<ITheater>().Object;
        }

        [SetUp]
        public void Setup()
        {
            var reservationRequest = new Mock<ReservationRequest>().Object;
        }

        [Test]
        public void Test1()
        {
            //theater.ReserveTickets()
        }
    }
}