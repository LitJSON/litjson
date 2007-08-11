using Jayrock.Json;
using Jayrock.Json.Conversion;

using System.IO;


namespace LitJson.Benchmarks
{
    public class BenchmarkJayrock
    {
        [Benchmark]
        public static void JayrockConversionToGenericObject ()
        {
            for (int i = 0; i < Common.Iterations; i++) {
                JsonObject art = (JsonObject) JsonConvert.Import (
                    Common.PersonJson);

                TextWriter.Null.Write (art["Name"]);
            }
        }

        [Benchmark]
        public static void JayrockConversionToObject ()
        {
            for (int i = 0; i < Common.Iterations; i++) {
                Person art = (Person) JsonConvert.Import (typeof (Person),
                                                          Common.PersonJson);

                TextWriter.Null.Write (art.ToString ());
            }
        }
    }
}
