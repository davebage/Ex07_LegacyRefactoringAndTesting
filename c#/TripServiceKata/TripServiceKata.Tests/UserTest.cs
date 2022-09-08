using NUnit.Framework;

namespace TripServiceKata.Tests
{
    [TestFixture]
    public class UserTest
    {
        private User.User _user;
        private readonly User.User PAUL = new User.User();
        private readonly User.User DAVE = new User.User();

        [SetUp]
        public void SetUp()
        {
            _user = UserBuilder.oUser().isFriendsWith(DAVE).Build();
        }

        [Test]
        public void should_inform_when_not_friends()
        {
            Assert.That(_user.IsFriendsWith(PAUL), Is.False);
        }

        [Test]
        public void should_inform_when_are_friends()
        {
            Assert.That(_user.IsFriendsWith(DAVE), Is.EqualTo(true));
        }
    }
}