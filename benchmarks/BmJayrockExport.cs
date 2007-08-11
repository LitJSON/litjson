using Jayrock.Json;
using Jayrock.Json.Conversion;

using System.IO;


namespace LitJson.Benchmarks
{
    public class BenchmarkJayrock
    {
        static JsonObject person;


        public static void Init (string[] args)
        {
            person = (JsonObject) JsonConvert.Import (Common.PersonJson);
        }

        [Benchmark]
        public static void JayrockConversionFromGenericObject ()
        {
            for (int i = 0; i < Common.Iterations; i++) {
                string json = JsonConvert.ExportToString (person);

                TextWriter.Null.Write (json);
            }
        }

        [Benchmark]
        public static void JayrockConversionFromHashtable ()
        {
            for (int i = 0; i < Common.Iterations; i++) {
                string json = JsonConvert.ExportToString (
                    Common.HashtablePerson);

                TextWriter.Null.Write (json);
            }
        }

        [Benchmark]
        public static void JayrockConversionFromObject ()
        {
            for (int i = 0; i < Common.Iterations; i++) {
                string json = JsonConvert.ExportToString (
                    Common.SamplePerson);

                TextWriter.Null.Write (json);
            }
        }
    }
}
