using System.Collections.Generic;
using TripServiceKata.Exception;

namespace TripServiceKata.Trip
{
    public class TripDAO : ITripDAO
    {
        public static List<Trip> FindTripsByUser(User.User user)
        {
            throw new DependendClassCallDuringUnitTestException(
                        "TripDAO should not be invoked on an unit test.");
        }

        public List<Trip> FindTripsFor(User.User registereduser)
        {
            return TripDAO.FindTripsByUser(registereduser);
        }
    }

    public interface ITripDAO
    {
        List<Trip> FindTripsFor(User.User registereduser);
    }
}
