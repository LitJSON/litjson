#region Header
/**
 * JsonReaderTest.cs
 *   Tests for the JsonReader class.
 *
 * The authors disclaim copyright to this source code. For more details, see
 * the COPYING file included with this distribution.
 **/
#endregion


using LitJson;
using NUnit.Framework;
using System;
using System.IO;
using System.Reflection;


namespace LitJson.Test
{
    [TestFixture]
    public class JsonReaderTest
    {
        [Test]
        public void BooleanTest ()
        {
            string json = "[ true, false ]";

            JsonReader reader = new JsonReader (json);
            reader.Read ();

            reader.Read ();
            Assert.IsTrue ((bool) reader.Value, "A1");
            reader.Read ();
            Assert.IsTrue (! ((bool) reader.Value), "A2");

            reader.Close ();
        }

        [Test]
        public void CommentsTest()
        {
            string json = @"
                {
                    // This is the first property
                    ""foo"" : ""bar"",

                    /**
                     * This is the second property
                     **/
                     ""baz"": ""blah""
                }";

            JsonReader reader = new JsonReader (json);

            reader.Read ();
            reader.Read ();
            Assert.AreEqual ("foo", (string) reader.Value, "A1");

            reader.Read ();
            reader.Read ();
            Assert.AreEqual ("baz", (string) reader.Value, "A2");

            reader.Read ();
            reader.Read ();
            Assert.IsTrue (reader.EndOfJson, "A3");
        }

        [Test]
        public void DoubleTest ()
        {
            string json = @"[ 0.0, -0.0, 3.1416, 8e-3, 7E-5, -128.000009,
                   144e+3, 0.1e2 ]";

            JsonReader reader = new JsonReader (json);
            reader.Read ();

            reader.Read ();
            Assert.AreEqual ((double) reader.Value, 0.0,
                             Double.Epsilon, "A1");
            reader.Read ();
            Assert.AreEqual ((double) reader.Value, 0.0,
                             Double.Epsilon, "A2");
            reader.Read ();
            Assert.AreEqual ((double) reader.Value, 3.1416,
                             Double.Epsilon, "A3");
            reader.Read ();
            Assert.AreEqual ((double) reader.Value, 0.008,
                             Double.Epsilon, "A4");
            reader.Read ();
            Assert.AreEqual ((double) reader.Value, 0.00007,
                             Double.Epsilon, "A5");
            reader.Read ();
            Assert.AreEqual ((double) reader.Value, -128.000009,
                             Double.Epsilon, "A6");
            reader.Read ();
            Assert.AreEqual ((double) reader.Value, 144000.0,
                             Double.Epsilon, "A7");
            reader.Read ();
            Assert.AreEqual ((double) reader.Value, 10.0,
                             Double.Epsilon, "A8");

            reader.Close ();
        }

        [Test]
        public void EmptyStringTest ()
        {
            string json = "[ \"\" ]";

            JsonReader reader = new JsonReader (json);
            reader.Read ();

            reader.Read ();
            Assert.AreEqual (reader.Value, String.Empty);

            reader.Close ();
        }

        [Test]
        public void EndOfJsonTest ()
        {
            string json = " [ 1 ] [ 2, 3 ] [ 4, 5, 6 ] ";

            JsonReader reader = new JsonReader (json);

            int i;
            for (i = 0; i < 3; i++) {
                Assert.IsFalse (reader.EndOfJson, "A1");
                reader.Read ();
            }

            Assert.IsTrue (reader.EndOfJson, "A2");
            Assert.IsFalse (reader.EndOfInput, "A3");

            reader.Read ();

            for (i = 0; i < 3; i++) {
                Assert.IsFalse (reader.EndOfJson, "A4");
                reader.Read ();
            }

            Assert.IsTrue (reader.EndOfJson, "A5");
            Assert.IsFalse (reader.EndOfInput, "A6");

            reader.Read ();

            for (i = 0; i < 4; i++) {
                Assert.IsFalse (reader.EndOfJson, "A7");
                reader.Read ();
            }

            Assert.IsTrue (reader.EndOfJson, "A8");

            reader.Read ();
            Assert.IsTrue (reader.EndOfInput, "A9");
        }

        [Test]
        public void FromFileTest ()
        {
            Assembly asmb = typeof (JsonReaderTest).Assembly;
            StreamReader stream = new StreamReader (
                asmb.GetManifestResourceStream ("json-example.txt"));

            JsonReader reader = new JsonReader (stream);

            while (reader.Read ());
        }

        [Test]
        public void IntTest ()
        {
            string json = "[ 0, -0, 123, 14400, -500 ]";

            JsonReader reader = new JsonReader (json);
            reader.Read ();

            reader.Read ();
            Assert.AreEqual ((int) reader.Value, 0, "A1");
            reader.Read ();
            Assert.AreEqual ((int) reader.Value, 0, "A2");
            reader.Read ();
            Assert.AreEqual ((int) reader.Value, 123, "A3");
            reader.Read ();
            Assert.AreEqual ((int) reader.Value, 14400, "A4");
            reader.Read ();
            Assert.AreEqual ((int) reader.Value, -500, "A5");

            reader.Close ();
        }

        [Test]
        [ExpectedException (typeof (JsonException))]
        public void LexerErrorEscapeSequenceTest ()
        {
            string json = "[ \"Hello World \\ufffg \" ]";

            JsonReader reader = new JsonReader (json);

            while (reader.Read ());
        }

        [Test]
        [ExpectedException (typeof (JsonException))]
        public void LexerErrorRealNumberTest ()
        {
            // One ore more digits have to appear after the '.'
            string json = "[ 0.e5 ]";

            JsonReader reader = new JsonReader (json);

            while (reader.Read ());
        }

        [Test]
        [ExpectedException (typeof (JsonException))]
        public void LexerErrorTrueTest ()
        {
            string json = "[ TRUE ]";

            JsonReader reader = new JsonReader (json);

            while (reader.Read ());
        }

        [Test]
        [Category ("RuntimeBug")]  // Int32.TryParse in mono 1.2.5
        public void LongTest ()
        {
            string json = "[ 2147483648, -10000000000 ]";

            JsonReader reader = new JsonReader (json);
            reader.Read ();

            reader.Read ();
            Assert.AreEqual (typeof (Int64), reader.Value.GetType (), "A1");
            Assert.AreEqual (2147483648l, (long) reader.Value, "A2");
            reader.Read ();
            Assert.AreEqual (-10000000000l, (long) reader.Value, "A3");

            reader.Close ();
        }

        [Test]
        public void NestedArrays ()
        {
            string json = "[ [ [ [ [ 1, 2, 3 ] ] ] ] ]";

            int array_count = 0;

            JsonReader reader = new JsonReader (json);

            while (reader.Read ()) {
                if (reader.Token == JsonToken.ArrayStart)
                    array_count++;
            }

            Assert.AreEqual (array_count, 5);
        }

        [Test]
        public void NestedObjects ()
        {
            string json = "{ \"obj1\": { \"obj2\": { \"obj3\": true } } }";

            int object_count = 0;
            JsonReader reader = new JsonReader (json);

            while (reader.Read ()) {
                if (reader.Token == JsonToken.ObjectStart)
                    object_count++;
            }

            Assert.AreEqual (object_count, 3);
        }

        [Test]
        [ExpectedException (typeof (ArgumentNullException))]
        public void NullReaderTest ()
        {
            TextReader text_reader = null;
            JsonReader reader = new JsonReader (text_reader);

            while (reader.Read ());
        }

        [Test]
        public void NullTest ()
        {
            string json = "[ null ]";

            JsonReader reader = new JsonReader (json);
            reader.Read ();

            reader.Read ();
            Assert.IsNull (reader.Value);

            reader.Close ();
        }

        [Test]
        [ExpectedException (typeof (JsonException))]
        public void ParserErrorArrayClosingTest ()
        {
            string json = "[ 1, 2, 3 }";

            JsonReader reader = new JsonReader (json);

            while (reader.Read ());
        }

        [Test]
        [ExpectedException (typeof (JsonException))]
        public void ParserErrorIncompleteObjectTest ()
        {
            string json = "{ \"temperature\" : 21 ";

            JsonReader reader = new JsonReader (json);

            while (reader.Read ());
        }

        [Test]
        [ExpectedException (typeof (JsonException))]
        public void ParserErrorNoArrayOrObjectTest ()
        {
            string json = "true";

            JsonReader reader = new JsonReader (json);

            while (reader.Read ());
        }

        [Test]
        [ExpectedException (typeof (JsonException))]
        public void ParserErrorObjectClosingTest ()
        {
            string json = @"{
                ""sports"": [
                    ""football"", ""baseball"", ""basketball"" ] ]";

            JsonReader reader = new JsonReader (json);

            while (reader.Read ());
        }

        [Test]
        [ExpectedException (typeof (JsonException))]
        public void ParserErrorPropertyExpectedTest ()
        {
            string json = "{ {\"foo\": bar} }";

            JsonReader reader = new JsonReader (json);

            while (reader.Read ());
        }

        [Test]
        public void QuickArrayTest ()
        {
            string json = "[ \"George\", \"John\", \"Ringo\", \"Paul\" ]";

            JsonReader reader = new JsonReader (json);

            reader.Read ();
            Assert.AreEqual (reader.Token, JsonToken.ArrayStart, "A1");
            reader.Read ();
            Assert.AreEqual (reader.Token, JsonToken.String, "A2");
            Assert.AreEqual (reader.Value, "George", "A3");
            reader.Read ();
            Assert.AreEqual (reader.Token, JsonToken.String, "A3");
            Assert.AreEqual (reader.Value, "John", "A4");
            reader.Read ();
            Assert.AreEqual (reader.Token, JsonToken.String, "A6");
            Assert.AreEqual (reader.Value, "Ringo", "A7");
            reader.Read ();
            Assert.AreEqual (reader.Token, JsonToken.String, "A8");
            Assert.AreEqual (reader.Value, "Paul", "A9");
            reader.Read ();
            Assert.AreEqual (reader.Token, JsonToken.ArrayEnd, "A10");
            reader.Read ();
            Assert.IsTrue (reader.EndOfJson, "A11");
        }

        [Test]
        public void QuickObjectTest ()
        {
            string json = @"{
                ""vehicle"": ""submarine"",
                ""color"":   ""yellow""
            }";

            JsonReader reader = new JsonReader (json);

            reader.Read ();
            Assert.AreEqual (reader.Token, JsonToken.ObjectStart, "A1");
            reader.Read ();
            Assert.AreEqual (reader.Token, JsonToken.PropertyName, "A2");
            Assert.AreEqual (reader.Value, "vehicle", "A3");
            reader.Read ();
            Assert.AreEqual (reader.Token, JsonToken.String, "A4");
            Assert.AreEqual (reader.Value, "submarine", "A5");
            reader.Read ();
            Assert.AreEqual (reader.Token, JsonToken.PropertyName, "A6");
            Assert.AreEqual (reader.Value, "color", "A7");
            reader.Read ();
            Assert.AreEqual (reader.Token, JsonToken.String, "A8");
            Assert.AreEqual (reader.Value, "yellow", "A9");
            reader.Read ();
            Assert.AreEqual (reader.Token, JsonToken.ObjectEnd, "A10");
            reader.Read ();
            Assert.IsTrue (reader.EndOfJson, "A11");
        }

        [Test]
        [ExpectedException (typeof (JsonException))]
        public void StrictCommentsTest ()
        {
            string json = @"
                [
                    // This is a comment
                    1,
                    2,
                    3
                ]";

            JsonReader reader = new JsonReader (json);
            reader.AllowComments = false;

            while (reader.Read ());
        }

        [Test]
        [ExpectedException (typeof (JsonException))]
        public void StrictStringsTest ()
        {
            string json = "[ 'Look! Single quotes' ]";

            JsonReader reader = new JsonReader (json);
            reader.AllowSingleQuotedStrings = false;

            while (reader.Read ());
        }

        [Test]
        public void StringsTest ()
        {
            string json =
                "[ \"abc 123 \\n\\f\\b\\t\\r \\\" \\\\ \\u263a \\u25CF\" ]";

            string str = "abc 123 \n\f\b\t\r \" \\ \u263a \u25cf";

            JsonReader reader = new JsonReader (json);
            reader.Read ();
            reader.Read ();

            Assert.AreEqual (str, reader.Value, "A1");

            reader.Close ();

            json = " [ '\"Hello\" \\'world\\'' ] ";
            str  = "\"Hello\" 'world'";

            reader = new JsonReader (json);
            reader.Read ();
            reader.Read ();

            Assert.AreEqual (str, reader.Value, "A2");

            reader.Close ();
        }
    }
}
