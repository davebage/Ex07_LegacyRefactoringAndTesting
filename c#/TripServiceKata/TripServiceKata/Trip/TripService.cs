using System.Collections.Generic;
using TripServiceKata.Exception;
using TripServiceKata.User;

namespace TripServiceKata.Trip
{
    public class TripService
    {
        public TripService()
        {
            
        }
        private readonly ITripDAO _tripDAO;

        public TripService(ITripDAO tripDAO)
        {
            this._tripDAO = tripDAO;
        }

        public List<Trip> GetFriendTrips(User.User friend, User.User loggedInUser)
        {
            if(loggedInUser == null)
                throw new UserNotLoggedInException();

            // To this shorthand
            return friend.IsFriendsWith(loggedInUser) ? 
                FindTripsByUser(friend) : 
                NoTrips();
        }

        private List<Trip> NoTrips()
        {
            return new List<Trip>();
        }

        private List<Trip> FindTripsByUser(User.User user)
        {
            return _tripDAO.FindTripsFor(user);
        }
    }

}
