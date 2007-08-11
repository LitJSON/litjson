using Newtonsoft.Json;

using System.Collections;
using System.IO;


namespace LitJson.Benchmarks
{
    public class BenchmarkNewtonsoft
    {
        [Benchmark]
        public static void NewtonsoftConversionToGenericObject ()
        {
            for (int i = 0; i < Common.Iterations; i++) {
                JavaScriptObject art = (JavaScriptObject)
                    JavaScriptConvert.DeserializeObject (
                        Common.PersonJson, typeof (JavaScriptObject));

                TextWriter.Null.Write (art["Name"]);
            }
        }

        [Benchmark]
        public static void NewtonsoftConversionToHashtable ()
        {
            for (int i = 0; i < Common.Iterations; i++) {
                Hashtable art = (Hashtable)
                    JavaScriptConvert.DeserializeObject (
                        Common.PersonJson, typeof (Hashtable));

                TextWriter.Null.Write (art["Name"]);
            }
        }

        [Benchmark]
        public static void NewtonsoftConversionToObject ()
        {
            for (int i = 0; i < Common.Iterations; i++) {
                Person art = (Person)
                    JavaScriptConvert.DeserializeObject (
                        Common.PersonJson, typeof (Person));

                TextWriter.Null.Write (art.ToString ());
            }
        }
    }
}
