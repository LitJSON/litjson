using LitJson;

using System.Collections;
using System.IO;


namespace LitJson.Benchmarks
{
    public class BenchmarkLitJson
    {
        [Benchmark]
        public static void LitJsonConversionToGenericObject ()
        {
            for (int i = 0; i < Common.Iterations; i++) {
                JsonData art = JsonMapper.ToObject (Common.PersonJson);

                TextWriter.Null.Write (art["Name"]);
            }
        }

        [Benchmark]
        public static void LitJsonConversionToHashtable ()
        {
            for (int i = 0; i < Common.Iterations; i++) {
                Hashtable art = JsonMapper.ToObject<Hashtable> (
                    Common.PersonJson);

                TextWriter.Null.Write (art["Name"]);
            }
        }

        [Benchmark]
        public static void LitJsonConversionToObject ()
        {
            for (int i = 0; i < Common.Iterations; i++) {
                Person art = JsonMapper.ToObject<Person> (Common.PersonJson);

                TextWriter.Null.Write (art.ToString ());
            }
        }
    }
}
