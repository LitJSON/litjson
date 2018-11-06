#region Header
/**
 * JsonDataTest.cs
 *   Tests for the JsonData class.
 *
 * The authors disclaim copyright to this source code. For more details, see
 * the COPYING file included with this distribution.
 **/
#endregion


using LitJson;
using NUnit.Framework;
using System;
using System.Collections.Generic;


namespace LitJson.Test
{
    [TestFixture]
    public class JsonDataTest
    {
        [Test]
        public void AsArrayTest ()
        {
            JsonData data = new JsonData ();

            data.Add (1);
            data.Add (2);
            data.Add (3);
            data.Add ("Launch!");

            Assert.IsTrue (data.IsArray, "A1");
            Assert.AreEqual ("[1,2,3,\"Launch!\"]", data.ToJson (), "A2");
        }

        [Test]
        public void AsBooleanTest ()
        {
            JsonData data;

            data = true;
            Assert.IsTrue (data.IsBoolean, "A1");
            Assert.IsTrue ((bool) data, "A2");
            Assert.AreEqual ("true", data.ToJson (), "A3");

            data = false;
            bool f = false;

            Assert.AreEqual (f, (bool) data, "A4");
        }

        [Test]
        public void AsDoubleTest ()
        {
            JsonData data;

            data = 3e6;
            Assert.IsTrue (data.IsDouble, "A1");
            Assert.AreEqual (3e6, (double) data, "A2");
            Assert.AreEqual ("3000000.0", data.ToJson (), "A3");

            data = 3.14;
            Assert.IsTrue (data.IsDouble, "A4");
            Assert.AreEqual (3.14, (double) data, "A5");
            Assert.AreEqual ("3.14", data.ToJson (), "A6");

            data = 0.123;
            double n = 0.123;

            Assert.AreEqual (n, (double) data, "A7");
        }

        [Test]
        public void AsIntTest ()
        {
            JsonData data;

            data = 13;
            Assert.IsTrue (data.IsInt, "A1");
            Assert.AreEqual ((int) data, 13, "A2");
            Assert.AreEqual (data.ToJson (), "13", "A3");

            data = -00500;

            Assert.IsTrue (data.IsInt, "A4");
            Assert.AreEqual ((int) data, -500, "A5");
            Assert.AreEqual (data.ToJson (), "-500", "A6");

            data = 1024;
            int n = 1024;

            Assert.AreEqual ((int) data, n, "A7");
        }

        [Test]
        public void AsObjectTest ()
        {
            JsonData data = new JsonData ();

            data["alignment"] = "left";
            data["font"] = new JsonData ();
            data["font"]["name"] = "Arial";
            data["font"]["style"]  = "italic";
            data["font"]["size"]  = 10;
            data["font"]["color"]  = "#fff";

            Assert.IsTrue (data.IsObject, "A1");

            string json = "{\"alignment\":\"left\",\"font\":{" +
                "\"name\":\"Arial\",\"style\":\"italic\",\"size\":10," +
                "\"color\":\"#fff\"}}";

            Assert.AreEqual (json, data.ToJson (), "A2");
        }

        [Test]
        public void AsStringTest ()
        {
            JsonData data;

            data = "All you need is love";
            Assert.IsTrue (data.IsString, "A1");
            Assert.AreEqual ("All you need is love", (string) data, "A2");
            Assert.AreEqual ("\"All you need is love\"", data.ToJson (),
                             "A3");
        }

        [Test]
        public void EqualsTest ()
        {
            JsonData a;
            JsonData b;

            // Compare ints
            a = 7;
            b = 7;
            Assert.IsTrue (a.Equals (b), "A1");

            Assert.IsFalse (a.Equals (null), "A2");

            b = 8;
            Assert.IsFalse (a.Equals (b), "A3");

            // Compare longs
            a = 10L;
            b = 10L;
            Assert.IsTrue (a.Equals (b), "A4");

            // Int now comparable to long
            b = 10;
            Assert.IsTrue (a.Equals (b), "A5");
            b = 11L;
            Assert.IsFalse (a.Equals (b), "A6");

            // Compare doubles
            a = 78.9;
            b = 78.9;
            Assert.IsTrue (a.Equals (b), "A7");

            b = 78.899999;
            Assert.IsFalse (a.Equals (b), "A8");

            // Compare booleans
            a = true;
            b = true;
            Assert.IsTrue (a.Equals (b), "A9");

            b = false;
            Assert.IsFalse (a.Equals (b), "A10");

            // Compare strings
            a = "walrus";
            b = "walrus";
            Assert.IsTrue (a.Equals (b), "A11");

            b = "Walrus";
            Assert.IsFalse (a.Equals (b), "A12");
        }

        [Test]
        public void GetKeysTest ()
        {
            JsonData data = new JsonData ();

            data["first"]  = "one";
            data["second"] = "two";
            data["third"]  = "three";
            data["fourth"] = "four";

            Assert.AreEqual (4, data.Keys.Count, "A1");

            foreach (string k in data.Keys)
                Assert.IsNotNull (data[k], "A2");
        }

        [Test]
        public void GetKeysInvalidTypeTest ()
        {
            JsonData data = new JsonData ();
            data.Add (42);  // turns it into an array

            // .. but an array doesn't have keys
            Assert.Throws<InvalidOperationException>(() => {
                ICollection<string> keys = data.Keys;
                Assert.IsNotNull (keys);
            });
        }

        [Test]
        public void InvalidCastTest ()
        {
            JsonData data = 35;

            Assert.Throws<InvalidCastException>(() => {
                string str = (string) data;

                if (str != (string) data)
                    str = (string) data;
            });
        }

        [Test]
        public void NullValue ()
        {
            string json = "{\"test\":null}";

            JsonData data = new JsonData ();
            data["test"] = null;

            Assert.AreEqual (json, data.ToJson ());
        }

        [Test]
        public void RemoveValueFromObject()
        {
            string json = "{\"test1\":1}";

            JsonData data = new JsonData();
            data["test1"] = 1;
            data["test2"] = 2;
            data.Remove("test2");

            Assert.AreEqual(json, data.ToJson());
        }

        [Test]
        public void RemoveValueFromNestedObject()
        {
            string json = "{\"test1\":{\"test3\":3}}";

            JsonData data = new JsonData();
            data["test1"] = new JsonData();
            data["test1"]["test2"] = 2;
            data["test1"]["test3"] = 3;
            data["test1"].Remove("test2");

            Assert.AreEqual(json, data.ToJson());
        }

        [Test]
        public void RemoveValueFromArray()
        {
            string json = "[\"test1\",2.0]";

            JsonData data = new JsonData();
            data.Add("test1");
            data.Add("test2");
            data.Remove("test2");
            data.Add(1);
            data.Add(2.0);
            data.Remove(1);

            Assert.AreEqual(json, data.ToJson());
        }

        [Test]
        public void PropertiesOrderTest ()
        {
            JsonData data = new JsonData ();

            string json = "{\"first\":\"one\",\"second\":\"two\"," +
                "\"third\":\"three\",\"fourth\":\"four\"}";

            for (int i = 0; i < 10; i++) {
                data.Clear ();

                data["first"]  = "one";
                data["second"] = "two";
                data["third"]  = "three";
                data["fourth"] = "four";

                Assert.AreEqual (json, data.ToJson ());
            }
        }
    }
}
