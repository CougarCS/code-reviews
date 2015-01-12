using System;
using NUnit.Framework;
using simpleDB;
using System.Collections.Generic;

namespace simpleDBTest
{
    [TestFixture]
    public class DataCommandTest
    {
        //should never need to reinstantiate the data commands
        Set set = new Set();
        Get get = new Get();
        NumEqualTo numEqualTo = new NumEqualTo();
        Unset unSet = new Unset();
        ResetAll reset = new ResetAll();

        [SetUp]
        public void SetUp()
        {
            reset.Operation(null);
        }

        [Test]
        public void Canary()
        {
            Assert.IsTrue(true);
        }

        [Test]
        public void TestSetExisting()
        {
            List<string> arg1 = new List<string>(){
                "z",
                "23"
            };
            List<string> arg2 = new List<string>(){
                "z",
                "9"
            };

            set.Operation(arg1);
            set.Operation(arg2);
            int? actual = get.Operation(arg1);

            Assert.AreEqual(9, actual);
        }

        [Test]
        public void TestSetAndGet()
        {
            List<string> args = new List<string>(){
                "a",
                "10"
            };

            set.Operation(args);
            int? actual = get.Operation(args);

            Assert.AreEqual(10, actual.Value);
        }
        
        [Test]
        public void TestGetEmpty()
        {
            int? actual = get.Operation(new List<string>() { "taco" });

            Assert.IsFalse(actual.HasValue);
            Assert.AreEqual(Messages.NULL, get.Message);
        }

        [Test]
        public void TestSetBadArguments()
        {
            List<string> args = new List<string>(){
                "b",
                "s"
            };

            int? actual = set.Operation(args);

            Assert.IsFalse(actual.HasValue);
            Assert.AreEqual(Messages.INVALID_ARGUMENT, set.Message);
        }
        [Test]
        public void TestNumEqualTo()
        {
            List<string> args1 = new List<string>(){
                "b",
                "7"
            };
            List<string> args2 = new List<string>(){
                "c",
                "7"
            };
            List<string> args3 = new List<string>(){
                "a",
                "20"
            };

            set.Operation(args1);
            set.Operation(args2);
            set.Operation(args3);
            int? actual = numEqualTo.Operation(new List<string>() { "7" });

            Assert.AreEqual(2, actual);
        }

        [Test]
        public void TestNumEqualToBadArguments()
        {
            int? actual = numEqualTo.Operation(null);

            Assert.IsFalse(actual.HasValue);
            Assert.AreEqual(Messages.INVALID_ARGUMENT, numEqualTo.Message);
        }

        [Test]
        public void TestUnset()
        {
            List<string> arg = new List<string>(){
                "utopic",
                "93"
            };

            set.Operation(arg);
            unSet.Operation(arg);
            int? actual = get.Operation(arg);

            Assert.IsFalse(actual.HasValue);
        }
    }
}
