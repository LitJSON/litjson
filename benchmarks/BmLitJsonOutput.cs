using LitJson;

using System.IO;


namespace LitJson.Benchmarks
{
    public class BenchmarkLitJson
    {
        [Benchmark]
        public static void LitJsonOutputFile ()
        {
            using (FileStream fs = new FileStream ("litjson_out.txt",
                                                   FileMode.Create)) {
                StreamWriter out_stream = new StreamWriter (fs);

                JsonReader reader = new JsonReader (Common.JsonText);

                out_stream.WriteLine ("*** Reading with LitJson.JsonReader");

                while (reader.Read ()) {
                    out_stream.Write ("Token: {0}", reader.Token);

                    if (reader.Value != null)
                        out_stream.WriteLine (" Value: {0}", reader.Value);
                    else
                        out_stream.WriteLine ("");
                }


                out_stream.WriteLine (
                    "\n*** Writing with LitJson.JsonWriter");

                JsonWriter writer = new JsonWriter (out_stream);
                int n = Common.SampleObject.Length;
                for (int i = 0; i < n; i += 2) {
                    switch ((char) Common.SampleObject[i]) {
                    case '{':
                        writer.WriteObjectStart ();
                        break;

                    case '}':
                        writer.WriteObjectEnd ();
                        break;

                    case '[':
                        writer.WriteArrayStart ();
                        break;

                    case ']':
                        writer.WriteArrayEnd ();
                        break;

                    case 'P':
                        writer.WritePropertyName (
                            (string) Common.SampleObject[i + 1]);
                        break;

                    case 'I':
                        writer.Write (
                            (int) Common.SampleObject[i + 1]);
                        break;

                    case 'D':
                        writer.Write (
                            (double) Common.SampleObject[i + 1]);
                        break;

                    case 'S':
                        writer.Write (
                            (string) Common.SampleObject[i + 1]);
                        break;

                    case 'B':
                        writer.Write (
                            (bool) Common.SampleObject[i + 1]);
                        break;

                    case 'N':
                        writer.Write (null);
                        break;
                    }
                }


                out_stream.WriteLine (
                    "\n\n*** Data imported with " +
                    "LitJson.JsonMapper\n");

                Person art = JsonMapper.ToObject<Person> (Common.PersonJson);

                out_stream.Write (art.ToString ());


                out_stream.WriteLine (
                    "\n\n*** Object exported with " +
                    "LitJson.JsonMapper\n");

                out_stream.Write (JsonMapper.ToJson (Common.SamplePerson));


                out_stream.WriteLine (
                    "\n\n*** Generic object exported with " +
                    "LitJson.JsonMapper\n");

                JsonData person = JsonMapper.ToObject (Common.PersonJson);

                out_stream.Write (JsonMapper.ToJson (person));


                out_stream.WriteLine (
                    "\n\n*** Hashtable exported with " +
                    "LitJson.JsonMapper\n");

                out_stream.Write (JsonMapper.ToJson (Common.HashtablePerson));

                out_stream.Close ();
            }
        }

    }
}
