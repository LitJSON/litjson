using Jayrock.Json;
using Jayrock.Json.Conversion;

using System;
using System.IO;
using System.Text;


namespace LitJson.Benchmarks
{
    public class BenchmarkJayrock
    {
        [Benchmark]
        public static void JayrockOutputFile ()
        {
            using (FileStream fs = new FileStream ("jayrock_out.txt",
                                                   FileMode.Create)) {
                StreamWriter out_stream = new StreamWriter (fs);

                StringReader sr = new StringReader (Common.JsonText);

                JsonReader reader = new JsonTextReader (sr);


                out_stream.WriteLine (
                    "*** Reading with Jayrock.Json.JsonReader");

                while (reader.Read ()) {
                    out_stream.Write ("Token: {0}", reader.TokenClass);

                    if (reader.Text != null)
                        out_stream.WriteLine (" Value: {0}", reader.Text);
                    else
                        out_stream.WriteLine ("");

                }


                out_stream.WriteLine (
                    "\n*** Writing with Jayrock.Json.JsonWriter");

                JsonWriter writer = new JsonTextWriter (out_stream);
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
                        writer.WriteMember (
                            (string) Common.SampleObject[i + 1]);
                        break;

                    case 'I':
                        writer.WriteNumber (
                            (int) Common.SampleObject[i + 1]);
                        break;

                    case 'D':
                        writer.WriteNumber (
                            (double) Common.SampleObject[i + 1]);
                        break;

                    case 'S':
                        writer.WriteString (
                            (string) Common.SampleObject[i + 1]);
                        break;

                    case 'B':
                        writer.WriteBoolean (
                            (bool) Common.SampleObject[i + 1]);
                        break;

                    case 'N':
                        writer.WriteNull ();
                        break;
                    }
                }


                out_stream.WriteLine (
                    "\n\n*** Data imported with " +
                    "Jayrock.Json.Conversion.JsonConvert\n");

                Person art = (Person) JsonConvert.Import (typeof (Person),
                                                          Common.PersonJson);

                out_stream.Write (art.ToString ());


                out_stream.WriteLine (
                    "\n\n*** Object exported with " +
                    "Jayrock.Json.Conversion.JsonConvert\n");

                out_stream.Write (JsonConvert.ExportToString (
                        Common.SamplePerson));

                out_stream.WriteLine (
                    "\n\n*** Generic object exported with " +
                    "Jayrock.Json.Conversion.JsonConvert\n");
                JsonObject person = (JsonObject) JsonConvert.Import (
                    Common.PersonJson);

                out_stream.Write (JsonConvert.ExportToString (person));


                out_stream.WriteLine (
                    "\n\n*** Hashtable exported with " +
                    "Jayrock.Json.Conversion.JsonConvert\n");

                out_stream.Write (JsonConvert.ExportToString (
                        Common.HashtablePerson));

                out_stream.Close ();
            }
        }
    }
}
