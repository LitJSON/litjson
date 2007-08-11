using LitJson;

using System.IO;
using System.Text;


namespace LitJson.Benchmarks
{
    public class BenchmarkLitJson
    {
        [Benchmark]
        public static void LitJsonWriterNumbers ()
        {
            for (int i = 0; i < Common.Iterations; i++) {
                StringBuilder output = new StringBuilder ();
                JsonWriter writer = new JsonWriter (new StringWriter (output));

                writer.WriteArrayStart ();

                foreach (int n in Common.SampleInts)
                    writer.Write (n);

                foreach (double n in Common.SampleDoubles)
                    writer.Write (n);

                writer.WriteArrayEnd ();
            }
        }

        [Benchmark]
        public static void LitJsonWriterObjects ()
        {
            for (int j = 0; j < Common.Iterations; j++) {
                StringBuilder output = new StringBuilder ();
                JsonWriter writer = new JsonWriter (new StringWriter (output));

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

            }
        }
    }
}
