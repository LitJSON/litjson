using Newtonsoft.Json;

using System;
using System.IO;


namespace LitJson.Benchmarks
{
    public class BenchmarkNewtonsoft
    {
        [Benchmark]
        public static void NewtonsoftOutputFile ()
        {
            using (FileStream fs = new FileStream ("newtonsoft_out.txt",
                                                   FileMode.Create)) {
                StreamWriter out_stream = new StreamWriter (fs);

                StringReader sr = new StringReader (Common.JsonText);

                JsonReader reader = new JsonReader (sr);

                out_stream.WriteLine (
                    "*** Reading with Newtonsoft.Json.JsonReader");

                int count = 0;

                while (reader.Read ()) {
                    out_stream.Write ("Token: {0}", reader.TokenType);

                    if (reader.Value != null)
                        out_stream.WriteLine (" Value: {0}", reader.Value);
                    else
                        out_stream.WriteLine ("");


                    // reader.Read() loops instead of returning false at the end
                    if (reader.TokenType == JsonToken.EndObject)
                        count++;

                    if (count == 3)
                        break;
                }


                out_stream.WriteLine (
                    "\n*** Writing with Newtonsoft.Json.JsonWriter");

                JsonWriter writer = new JsonWriter (out_stream);
                int n = Common.SampleObject.Length;
                for (int i = 0; i < n; i += 2) {
                    switch ((char) Common.SampleObject[i]) {
                    case '{':
                        writer.WriteStartObject ();
                        break;

                    case '}':
                        writer.WriteEndObject ();
                        break;

                    case '[':
                        writer.WriteStartArray ();
                        break;

                    case ']':
                        writer.WriteEndArray ();
                        break;

                    case 'P':
                        writer.WritePropertyName (
                            (string) Common.SampleObject[i + 1]);
                        break;

                    case 'I':
                        writer.WriteValue (
                            (int) Common.SampleObject[i + 1]);
                        break;

                    case 'D':
                        writer.WriteValue (
                            (double) Common.SampleObject[i + 1]);
                        break;

                    case 'S':
                        writer.WriteValue (
                            (string) Common.SampleObject[i + 1]);
                        break;

                    case 'B':
                        writer.WriteValue (
                            (bool) Common.SampleObject[i + 1]);
                        break;

                    case 'N':
                        writer.WriteNull ();
                        break;
                    }
                }


                out_stream.WriteLine (
                    "\n\n*** Data imported with " +
                    "Newtonsoft.Json.JavaScriptConvert\n");

                Person art = (Person)
                    JavaScriptConvert.DeserializeObject (
                        Common.PersonJson, typeof (Person));

                out_stream.Write (art.ToString ());


                out_stream.WriteLine (
                    "\n\n*** Object exported with " +
                    "Newtonsoft.Json.JavaScriptConvert\n");

                out_stream.Write (JavaScriptConvert.SerializeObject (
                        Common.SamplePerson));


                out_stream.WriteLine (
                    "\n\n*** Generic object exported with " +
                    "Newtonsoft.Json.JavaScriptConvert\n");

                JavaScriptObject person = (JavaScriptObject)
                    JavaScriptConvert.DeserializeObject (
                        Common.PersonJson, typeof (JavaScriptObject));

                out_stream.Write (JavaScriptConvert.SerializeObject (person));


                out_stream.WriteLine (
                    "\n\n*** Hashtable exported with " +
                    "Newtonsoft.Json.JavaScriptConvert\n");

                out_stream.Write (JavaScriptConvert.SerializeObject (
                        Common.HashtablePerson));

                out_stream.Close ();
            }
        }

    }
}
