namespace Newtonsoft.Json.Partial.Tests
{
    using Newtonsoft.Json.Partial.Tests.Models;
    using NUnit.Framework;

    [TestFixture]
    public class ReadPartialTests
    {
        [Test]
        public void StillDeserializesStandardObject()
        {
            var json = "{\"id\": \"abc\", \"name\": \"Test\"}";
            var obj = JsonConvert.DeserializeObject<Employee>(json);

            Assert.IsNotNull(obj);
            Assert.AreEqual("Test", obj.Name);
            Assert.AreEqual("abc", obj.Id);
            Assert.AreEqual(0, obj.Age);
        }

        [Test]
        public void StillDeserializesDespiteTypeMismatch()
        {
            var json = "{\"id\": 17, \"name\": \"Test\"}";
            var obj = JsonConvert.DeserializeObject<Employee>(json);

            Assert.IsNotNull(obj);
            Assert.AreEqual("Test", obj.Name);
            Assert.AreEqual("17", obj.Id);
            Assert.AreEqual(0, obj.Age);
        }

        [Test]
        public void StillDeserializesPartObject()
        {
            var json = "{\"id\": \"abc\", \"name\": \"Test\"}";
            var obj = JsonConvert.DeserializeObject<Part<Employee>>(json);

            Assert.IsNotNull(obj);
            Assert.IsNotNull(obj.Data);
            CollectionAssert.AreEquivalent(new[] { "id", "name" }, obj.Keys);
            Assert.AreEqual("Test", obj.Data.Name);
            Assert.AreEqual("abc", obj.Data.Id);
            Assert.AreEqual(0, obj.Data.Age);
        }

        [Test]
        public void StillDeserializesPartDespiteTypeMismatch()
        {
            var json = "{\"id\": 17, \"name\": \"Test\"}";
            var obj = JsonConvert.DeserializeObject<Part<Employee>>(json);

            Assert.IsNotNull(obj);
            Assert.IsNotNull(obj.Data);
            CollectionAssert.AreEquivalent(new[] { "id", "name" }, obj.Keys);
            Assert.AreEqual("Test", obj.Data.Name);
            Assert.AreEqual("17", obj.Data.Id);
            Assert.AreEqual(0, obj.Data.Age);
        }
    }
}