namespace TripServiceKata.Tests
{
    public class UserBuilder
    {
        private User.User[] friends = new User.User[] { };
        private Trip.Trip[] trips = new Trip.Trip[] { };

        public static UserBuilder oUser()
        {
            return new UserBuilder();
        }

        public UserBuilder isFriendsWith(params User.User[] friends)
        {
            this.friends = friends;
            return this;
        }

        public UserBuilder goingOnHolidayTo(params Trip.Trip[] trips)
        {
            this.trips = trips;
            return this;
        }

        public User.User Build()
        {
            User.User _user = new User.User();
            AddFriendsTo(_user);
            AddTripsTo(_user);

            return _user;
        }

        private void AddFriendsTo(User.User user)
        {
            foreach (var friend in friends)
            {
                user.AddFriend(friend);
            }
        }

        private void AddTripsTo(User.User user)
        {
            foreach (var trip in trips)
            {
                user.AddTrip(trip);
            }
        }

    }
}