using LitJson;

using System.IO;


namespace LitJson.Benchmarks
{
    public class BenchmarkLitJson
    {
        static JsonData person;


        public static void Init (string[] args)
        {
            person = JsonMapper.ToObject (Common.PersonJson);
        }

        [Benchmark]
        public static void LitJsonConversionFromGenericObject ()
        {
            for (int i = 0; i < Common.Iterations; i++) {
                string json = JsonMapper.ToJson (person);

                TextWriter.Null.Write (json);
            }
        }

        [Benchmark]
        public static void LitJsonConversionFromHashtable ()
        {
            for (int i = 0; i < Common.Iterations; i++) {
                string json = JsonMapper.ToJson (Common.HashtablePerson);

                TextWriter.Null.Write (json);
            }
        }

        [Benchmark]
        public static void LitJsonConversionFromObject ()
        {
            for (int i = 0; i < Common.Iterations; i++) {
                string json = JsonMapper.ToJson (Common.SamplePerson);

                TextWriter.Null.Write (json);
            }
        }
    }
}
