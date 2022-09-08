using System.Collections.Generic;
using Moq;
using NUnit.Framework;
using TripServiceKata.Exception;
using TripServiceKata.Trip;
using static TripServiceKata.Tests.UserBuilder;

namespace TripServiceKata.Tests
{
    [TestFixture]
    public class TripServiceTest
    {
        private static readonly User.User GUEST = null;
        private static readonly User.User UNUSED_USER = null;
        private static readonly User.User BILLYNOMATES = new User.User();

        private readonly User.User ANOTHER_FRIEND = new User.User();
        private readonly Trip.Trip TO_SPAIN = new Trip.Trip();
        private readonly Trip.Trip TO_CANADA = new Trip.Trip();

        private TripService _tripService = new TripService();


        [SetUp]
        public void SetUp()
        {
            _tripService = new TripService();
        }

        [Test]
        public void ShouldThrowExceptionIfUserNotLoggedIn()
        {
            Assert.Throws<UserNotLoggedInException>(() =>
                _tripService.GetFriendTrips(UNUSED_USER, GUEST)
            );
        }

        [Test]
        public void Should_return_no_trips_when_users_not_friends()
        {
            // Arrange
            User.User friend = oUser()
                .isFriendsWith(ANOTHER_FRIEND)
                .goingOnHolidayTo(TO_SPAIN)
                .Build();

            // Act
            List<Trip.Trip> trips = _tripService.GetFriendTrips(friend, BILLYNOMATES);

            // Assert
            Assert.That(trips.Count, Is.EqualTo(0));
        }

        [Test]
        public void Should_return_trips_when_users_are_friends()
        {
            // Arrange
            User.User friend = oUser()
                .isFriendsWith(ANOTHER_FRIEND)
                .goingOnHolidayTo(TO_SPAIN, TO_CANADA)
                .Build();
            Mock<ITripDAO> _tripDAOMock = new Mock<ITripDAO>();

            _tripDAOMock.Setup(x => x.FindTripsFor(friend)).Returns(friend.Trips());
            _tripService = new TripService(_tripDAOMock.Object);


            // Act
            List<Trip.Trip> trips = _tripService.GetFriendTrips(friend, ANOTHER_FRIEND);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(trips.Count, Is.EqualTo(2));
                Assert.That(trips[0], Is.EqualTo(TO_SPAIN));
                Assert.That(trips[1], Is.EqualTo(TO_CANADA));
            });
        }

    }
}
