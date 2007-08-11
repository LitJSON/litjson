using Jayrock.Json;

using System;
using System.IO;
using System.Text;


namespace LitJson.Benchmarks
{
    public class BenchmarkJayrock
    {
        [Benchmark]
        public static void JayrockWriterNumbers ()
        {
            for (int i = 0; i < Common.Iterations; i++) {
                StringBuilder output = new StringBuilder ();
                JsonWriter writer = new JsonTextWriter (new StringWriter (output));

                writer.WriteStartArray ();

                foreach (int n in Common.SampleInts)
                    writer.WriteNumber (n);

                foreach (double n in Common.SampleDoubles)
                    writer.WriteNumber (n);

                writer.WriteEndArray ();
            }
        }

        [Benchmark]
        public static void JayrockWriterObjects ()
        {
            for (int j = 0; j < Common.Iterations; j++) {
                StringBuilder output = new StringBuilder ();
                JsonWriter writer = new JsonTextWriter (new StringWriter (output));

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
            }
        }
    }
}
