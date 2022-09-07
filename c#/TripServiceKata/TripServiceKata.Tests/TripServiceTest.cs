using System;
using System.Collections.Generic;
using NUnit.Framework;
using TripServiceKata.Exception;
using TripServiceKata.Trip;

namespace TripServiceKata.Tests
{
    [TestFixture]
    public class TripServiceTest
    {
        private TestableTripService _tripService;
        private static readonly User.User GUEST = null;
        private static readonly User.User UNUSED_USER = null;
        private static  readonly  User.User BILLYNOMATES = new User.User();

        private static User.User _loggedInUser;
        private readonly User.User ANOTHER_FRIEND = new User.User();
        private readonly Trip.Trip TO_SPAIN = new Trip.Trip();
        private readonly Trip.Trip TO_CANCUN = new Trip.Trip();
        private readonly Trip.Trip TO_CANADA = new Trip.Trip();

        [SetUp]
        public void SetUp()
        {
            _tripService = new TestableTripService();
            _loggedInUser = BILLYNOMATES;
        }

        [Test]
        public void ShouldThrowExceptionIfUserNotLoggedIn()
        {
            _loggedInUser = GUEST;
            Assert.Throws<UserNotLoggedInException>(() =>
                _tripService.GetTripsByUser(UNUSED_USER)
            );
        }

        [Test]
        public void Should_return_no_trips_when_users_not_friends()
        {
            // Arrange
            User.User friend = new User.User();
            friend.AddFriend(ANOTHER_FRIEND);
            friend.AddTrip(TO_SPAIN);

            // Act
            List<Trip.Trip> trips = _tripService.GetTripsByUser(friend);

            // Assert
            Assert.That(trips.Count, Is.EqualTo(0));
        }

        [Test]
        public void Should_return_trips_when_users_are_friends()
        {
            // Arrange
            User.User friend = new User.User();
            friend.AddFriend(ANOTHER_FRIEND);
            friend.AddFriend(_loggedInUser);
            friend.AddTrip(TO_SPAIN);

            // Act
            List<Trip.Trip> trips = _tripService.GetTripsByUser(friend);

            // Assert
            Assert.That(trips.Count, Is.EqualTo(1));
            Assert.That(trips[0], Is.EqualTo(TO_SPAIN));
        }




        private class TestableTripService : TripService
        {
            protected override User.User GetLoggedInUser()
            {
                return _loggedInUser;
            }

            protected override List<Trip.Trip> FindTripsByUser(User.User user)
            {
                return user.Trips();
            }
        }
    }
}
