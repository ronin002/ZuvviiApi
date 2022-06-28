using NUnit.Framework;
using ZuvviiAPI.Services;

namespace ZuvviiUnitTest
{
    public class UtilsServiceTest
    {

        [SetUp]
        public void Setup()
        {

        }

        [Test]
        public void ValidMail_Works()
        {
            //Arrange
            string[] emails = { "edson.marcio7@gmail.com", "debora@gmail.com", "felipe@gmail.com","mark@zuvvii.com" };
            var count = 0;

            //Act
            foreach (string email in emails)
            {
                var emailOk = Utils.ValidMail(email);
                if (!emailOk) break;
                count++;
            }

            //Assert
            Assert.That(count, Is.EqualTo(emails.Length));
        }

        [Test]
        public void ValidMail_Failure()
        {
            //Arrange
            string[] emails = { "edsgmail.com", "debora@gmail", "@gmail.com", "mark@.zuvvii.com" };
            var emailOk = true;
            var count = 0;
            //Act
            foreach (string email in emails)
            {
                emailOk = Utils.ValidMail(email);
                if (emailOk) break;
                count++;
            }

            //Assert
            Assert.That(count, Is.EqualTo(emails.Length));
        }

    }
}