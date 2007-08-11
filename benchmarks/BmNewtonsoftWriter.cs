using Newtonsoft.Json;

using System;
using System.IO;
using System.Text;


namespace LitJson.Benchmarks
{
    public class BenchmarkNewtonsoft
    {
        [Benchmark]
        public static void NewtonsoftWriterNumbers ()
        {
            for (int i = 0; i < Common.Iterations; i++) {
                StringBuilder output = new StringBuilder ();
                JsonWriter writer = new JsonWriter (new StringWriter (output));

                writer.WriteStartArray ();

                foreach (int n in Common.SampleInts)
                    writer.WriteValue (n);

                foreach (double n in Common.SampleDoubles)
                    writer.WriteValue (n);

                writer.WriteEndArray ();
            }
        }

        [Benchmark]
        public static void NewtonsoftWriterObjects ()
        {
            for (int j = 0; j < Common.Iterations; j++) {
                StringBuilder output = new StringBuilder ();
                JsonWriter writer = new JsonWriter (new StringWriter (output));

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

            }
        }
    }
}
