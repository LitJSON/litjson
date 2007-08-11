using Jayrock.Json;

using System;
using System.IO;


namespace LitJson.Benchmarks
{
    public class BenchmarkJayrock
    {
        [Benchmark]
        public static void JayrockReaderNumbers ()
        {
            for (int i = 0; i < Common.Iterations; i++) {
                StringReader sr = new StringReader (Common.JsonNumbers);
                JsonReader reader = new JsonTextReader (sr);

                while (reader.Read ());
            }
        }

        [Benchmark]
        public static void JayrockReaderStrings ()
        {
            for (int i = 0; i < Common.Iterations; i++) {
                StringReader sr = new StringReader (Common.JsonStrings);
                JsonReader reader = new JsonTextReader (sr);

                while (reader.Read ());
            }
        }

        [Benchmark]
        public static void JayrockReaderFirstProperty ()
        {
            for (int i = 0; i < Common.Iterations; i++) {
                bool found = false;

                StringReader sr = new StringReader (Common.JsonText);

                JsonReader reader = new JsonTextReader (sr);

                while (reader.Read ()) {
                    if (reader.TokenClass == JsonTokenClass.Member &&
                        reader.Text == "FirstProperty") {
                        found = true;
                        break;
                    }
                }

                if (! found)
                    Console.WriteLine ("FirstProperty not found!");
            }
        }

        [Benchmark]
        public static void JayrockReaderLastProperty ()
        {
            for (int i = 0; i < Common.Iterations; i++) {
                bool found = false;

                StringReader sr = new StringReader (Common.JsonText);

                JsonReader reader = new JsonTextReader (sr);

                while (reader.Read ()) {
                    if (reader.TokenClass == JsonTokenClass.Member &&
                        reader.Text == "LastProperty") {
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
