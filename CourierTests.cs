using Coursework;
using Microsoft.VisualStudio.TestTools.UnitTesting;



namespace CourseworkTest
{
    [TestClass]
    public class CourseworkTests
    {
        [TestMethod]
        public void testAddToPostcode()
        {
            Parcel p = new Parcel("EH1 5DT", "hello");
            p.addToPostcode();
            string actual = "EH1";

            Assert.AreEqual(actual, p.Postcode, true, "this does not work");
        }
    }
}
