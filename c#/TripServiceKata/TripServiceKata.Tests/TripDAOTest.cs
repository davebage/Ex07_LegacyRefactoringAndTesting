using System;
using NUnit.Framework;
using TripServiceKata.Exception;
using TripServiceKata.Trip;

namespace TripServiceKata.Tests
{
    [TestFixture]
    public class TripDAOTest
    {
        TripDAO _tripDAO;
        public readonly User.User REGISTEREDUSER = new User.User();

        [SetUp]
        public void SetUp()
        {
            _tripDAO = new TripDAO();
        }

        [Test]
        public void ShouldThrowExceptionWhenRetrievingUserTrips()
        {
            Assert.Throws<DependendClassCallDuringUnitTestException>(() => _tripDAO.FindTripsFor(REGISTEREDUSER));
        }
    }
}