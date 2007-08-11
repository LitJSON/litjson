using Newtonsoft.Json;

using System;
using System.IO;


namespace LitJson.Benchmarks
{
    public class BenchmarkNewtonsoft
    {
        [Benchmark]
        public static void NewtonsoftReaderNumbers ()
        {
            for (int i = 0; i < Common.Iterations; i++) {
                StringReader sr = new StringReader (Common.JsonNumbers);
                JsonReader reader = new JsonReader (sr);

                while (reader.Read () &&
                       reader.TokenType != JsonToken.EndArray);
            }
        }

        [Benchmark]
        public static void NewtonsoftReaderStrings ()
        {
            for (int i = 0; i < Common.Iterations; i++) {
                StringReader sr = new StringReader (Common.JsonStrings);
                JsonReader reader = new JsonReader (sr);

                while (reader.Read () &&
                       reader.TokenType != JsonToken.EndArray);
            }
        }

        [Benchmark]
        public static void NewtonsoftReaderFirstProperty ()
        {
            for (int i = 0; i < Common.Iterations; i++) {
                bool found = false;

                StringReader sr = new StringReader (Common.JsonText);

                JsonReader reader = new JsonReader (sr);

                while (reader.Read ()) {
                    if (reader.TokenType == JsonToken.PropertyName &&
                        (string) reader.Value == "FirstProperty") {
                        found = true;
                        break;
                    }
                }

                if (! found)
                    Console.WriteLine ("FirstProperty not found!");
            }
        }

        [Benchmark]
        public static void NewtonsoftReaderLastProperty ()
        {
            for (int i = 0; i < Common.Iterations; i++) {
                bool found = false;

                StringReader sr = new StringReader (Common.JsonText);

                JsonReader reader = new JsonReader (sr);

                while (reader.Read ()) {
                    if (reader.TokenType == JsonToken.PropertyName &&
                        (string) reader.Value == "LastProperty") {
                        found = true;
                        break;
                    }
                }

                if (! found)
                    Console.WriteLine ("LastProperty not found!");
            }
        }
    }
}
