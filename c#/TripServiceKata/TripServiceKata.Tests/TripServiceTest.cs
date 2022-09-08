using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using NUnit.Framework;
using TripServiceKata.Exception;
using TripServiceKata.Trip;
using static TripServiceKata.Tests.UserBuilder;

namespace TripServiceKata.Tests
{
    [TestFixture]
    public class TripServiceTest
    {
        private TestableTripService _tripService;
        private static readonly User.User GUEST = null;
        private static readonly User.User UNUSED_USER = null;
        private static readonly User.User BILLYNOMATES = new User.User();

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
            User.User friend = oUser()
                .isFriendsWith(ANOTHER_FRIEND)
                .goingOnHolidayTo(TO_SPAIN)
                .Build();

            // Act
            List<Trip.Trip> trips = _tripService.GetTripsByUser(friend);

            // Assert
            Assert.That(trips.Count, Is.EqualTo(0));
        }

        [Test]
        public void Should_return_trips_when_users_are_friends()
        {


            // Arrange
            User.User friend = oUser()
                .isFriendsWith(ANOTHER_FRIEND, _loggedInUser)
                .goingOnHolidayTo(TO_SPAIN, TO_CANADA)
                .Build();

            // Act
            List<Trip.Trip> trips = _tripService.GetTripsByUser(friend);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(trips.Count, Is.EqualTo(2));
                Assert.That(trips[0], Is.EqualTo(TO_SPAIN));
                Assert.That(trips[1], Is.EqualTo(TO_CANADA));
            });
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
