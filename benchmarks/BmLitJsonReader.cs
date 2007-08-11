using LitJson;

using System;


namespace LitJson.Benchmarks
{
    public class BenchmarkLitJson
    {
        [Benchmark]
        public static void LitJsonReaderNumbers ()
        {
            for (int i = 0; i < Common.Iterations; i++) {
                JsonReader reader = new JsonReader (Common.JsonNumbers);

                while (reader.Read ());
            }
        }

        [Benchmark]
        public static void LitJsonReaderStrings ()
        {
            for (int i = 0; i < Common.Iterations; i++) {
                JsonReader reader = new JsonReader (Common.JsonStrings);

                while (reader.Read ());
            }
        }

        [Benchmark]
        public static void LitJsonReaderFirstProperty ()
        {
            for (int i = 0; i < Common.Iterations; i++) {
                bool found = false;

                JsonReader reader = new JsonReader (Common.JsonText);

                while (reader.Read ()) {
                    if (reader.Token == JsonToken.PropertyName &&
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
        public static void LitJsonReaderLastProperty ()
        {
            for (int i = 0; i < Common.Iterations; i++) {
                bool found = false;

                JsonReader reader = new JsonReader (Common.JsonText);

                while (reader.Read ()) {
                    if (reader.Token == JsonToken.PropertyName &&
                        (string) reader.Value == "LastProperty") {
                        found = true;

                        break;
                    }
                }

                if (! found)
                    Console.WriteLine ("FirstProperty not found!");
            }
        }
    }
}
