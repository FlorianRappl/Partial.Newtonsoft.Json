namespace Newtonsoft.Json.Partial.Tests
{
    using Newtonsoft.Json.Partial.Tests.Models;
    using NUnit.Framework;

    [TestFixture]
    public class WritePartialTests
    {
        [Test]
        public void WriteModelBackShouldWorkFine()
        {
            var originalJson = "{\"id\":\"abc\",\"name\":\"Test\"}";
            var obj = JsonConvert.DeserializeObject<Part<Employee>>(originalJson);
            var json = JsonConvert.SerializeObject(obj);

            Assert.AreEqual(originalJson, json);
        }
    }
}
