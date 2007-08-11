using Newtonsoft.Json;

using System.Collections;
using System.IO;


namespace LitJson.Benchmarks
{
    public class BenchmarkNewtonsoft
    {
        static JavaScriptObject person;


        public static void Init (string[] args)
        {
            person = (JavaScriptObject)
                JavaScriptConvert.DeserializeObject (
                    Common.PersonJson, typeof (JavaScriptObject));
        }

        [Benchmark]
        public static void NewtonsoftConversionFromGenericObject ()
        {
            for (int i = 0; i < Common.Iterations; i++) {
                string json = JavaScriptConvert.SerializeObject (person);

                TextWriter.Null.Write (json);
            }
        }

        [Benchmark]
        public static void NewtonsoftConversionFromHashtable ()
        {
            for (int i = 0; i < Common.Iterations; i++) {
                string json = JavaScriptConvert.SerializeObject (
                    Common.HashtablePerson);

                TextWriter.Null.Write (json);
            }
        }

        [Benchmark]
        public static void NewtonsoftConversionFromObject ()
        {
            for (int i = 0; i < Common.Iterations; i++) {
                string json = JavaScriptConvert.SerializeObject (
                    Common.SamplePerson);

                TextWriter.Null.Write (json);
            }
        }
    }
}
