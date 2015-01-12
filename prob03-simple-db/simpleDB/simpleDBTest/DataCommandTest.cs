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
            List<object> arg1 = new List<object>(){
                "z",
                23
            };
            List<object> arg2 = new List<object>(){
                "z",
                9
            };

            set.Operation(arg1);
            set.Operation(arg2);
            int? actual = get.Operation(arg1);

            Assert.AreEqual(arg2[1], actual);
        }

        [Test]
        public void TestSetAndGet()
        {
            List<object> args = new List<object>(){
                "a",
                10
            };

            set.Operation(args);
            int? actual = get.Operation(args);

            Assert.AreEqual(args[1], actual.Value);
        }
        
        [Test]
        public void TestGetEmpty()
        {
            int? actual = get.Operation(new List<object>() { "taco" });

            Assert.IsFalse(actual.HasValue);
        }
        [Test]
        public void TestNumEqualTo()
        {
            List<object> args1 = new List<object>(){
                "b",
                7
            };
            List<object> args2 = new List<object>(){
                "c",
                7
            };
            List<object> args3 = new List<object>(){
                "a",
                20
            };

            set.Operation(args1);
            set.Operation(args2);
            set.Operation(args3);
            int? actual = numEqualTo.Operation(new List<object>() { 7 });

            Assert.AreEqual(2, actual);
        }

        [Test]
        public void TestUnset()
        {
            List<object> arg = new List<object>(){
                "utopic",
                93
            };

            set.Operation(arg);
            unSet.Operation(arg);
            int? actual = get.Operation(arg);

            Assert.IsFalse(actual.HasValue);
        }
    }
}
