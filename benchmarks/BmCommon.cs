using System;
using System.Collections;


namespace LitJson.Benchmarks
{
    public class Job
    {
        public string Title;
        public string Description;
    }

    public class Person
    {
        private int      age;
        private double   height;
        private Job      job;
        private string   name;
        private bool     retired;
        private string[] urls;


        public int Age {
            get { return age; }
            set { age = value; }
        }

        public double Height {
            get { return height; }
            set { height = value; }
        }

        public Job Job {
            get { return job; }
            set { job = value; }
        }

        public string Name {
            get { return name; }
            set { name = value; }
        }

        public bool Retired {
            get { return retired; }
            set { retired = value; }
        }

        public string[] Urls {
            get { return urls; }
            set { urls = value; }
        }


        public override string ToString ()
        {
            string url_list = String.Empty;

            foreach (string url in urls)
                url_list += "\n  " + url;

            return String.Format (
                "Hi, I'm {0}, {1} years old and {2} tall.\n" +
                "I'm {3}. I work as '{4}', where I {5}.\n" +
                "You can visit my websites at:{6}",
                name, age, height,
                retired ? "retired" : "not retired yet",
                job.Title, job.Description, url_list);
        }
    }


    public class Common
    {
        public static readonly int Iterations = 10000;

        // Originally the last couple of numbers didn't have the '.0'
        // portion, but Newtonsoft's library throws an Exception otherwise
        public static readonly string JsonNumbers = @"
[
    42,
    1,
    1,
    2,
    3,
    5,
    8,
    -50,
    -678.56,
    3.1415,
    1.4e10,
    4.0e5,
    8.0e-3
]
";

        public static readonly string JsonStrings = @"
[
    ""Hello World!"",
    ""The quick brown fox jumps over the lazy dog"",
    ""Lorem ipsum dolor sit amet, consectetur adipisicing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum."",
    ""$%&'()*+,-./0123456789:;<=>?@ABCDEFGHIJKLMNOPQRSTUVWXYZ[\\]^""
]
";

        public static readonly string JsonText = @"

{
    ""Image"": {
        ""FirstProperty"": true,
        ""Width"":  800,
        ""Height"": 600,
        ""Title"":  ""View from 15th Floor"",
        ""Comment"": ""Sample text:\t\""abcdef\""\nSecond Line"",
        ""Comment2"": ""\u03c0\u03c1\u03cc\u03b3\u03c1\u03b1\u03bc\u03bc\u03b1"",
        ""Default"": true,
        ""Active"": false,
        ""Resource"": null,
        ""Thumbnail"": {
            ""Url"":    ""http://www.example.com/image/481989943"",
            ""Height"": 125,
            ""Width"":  ""100"" },
        ""IDs"": [ 116, 943, 234, 38793 ],
        ""Score"": 9.40,
        ""Scale"": 1.0e-1,
        ""Views"": 3000000000,
        ""LastProperty"": true
    }
}

";

        public static readonly double[] SampleDoubles = new double[] {
            0.0,
            10.0,
            3.1416,
            0.0000001,
            -789.123,
            0.00056,
            50000000000.0
        };

        public static readonly int[] SampleInts = new int[] {
            0,
            42,
            100000,
            -1,
            -123,
            7777,
            25
        };

        /* This is to be iterated by pairs. The first item is a char
         * indicating the type of the second item (if any):
         *
         *   { - Object start
         *   } - Object end
         *   [ - Array start
         *   ] - Array end
         *   P - Property name
         *   I - Int
         *   D - Double
         *   S - String
         *   B - Boolean
         *   N - Null
         */
        public static readonly object[] SampleObject = new object[] {
            '[', null,
            '{', null,
            'P', "precision",
            'S', "zip",
            'P', "Latitude",
            'D', 37.7668,
            'P', "Longitude",
            'D', -122.3959,
            'P', "Address",
            'S', "",
            'P', "City",
            'S', "SAN FRANCISCO",
            'P', "State",
            'S', "CA",
            'P', "Zip",
            'S', "94107",
            'P', "Country",
            'S', "US",
            'P', "Visited",
            'B', true,
            'P', "Ref",
            'N', null,
            'P', "Comment",
            'S', "This is a \"comment\"\tColumn2\nLine2. " +
                "\u00c6nema is a good album.",
            '}', null,

            '{', null,
            'P', "precision",
            'S', "zip",
            'P', "Latitude",
            'D', 37.371991,
            'P', "Longitude",
            'D', -122.026020,
            'P', "Address",
            'S', "",
            'P', "City",
            'S', "SUNNYVALE",
            'P', "State",
            'S', "CA",
            'P', "Zip",
            'S', "94085",
            'P', "Country",
            'S', "US",
            'P', "Visited",
            'B', false,
            'P', "Ref",
            'N', null,
            '}', null,
            ']', null
        };


        private static Person sample_person;

        public static Person SamplePerson {
            get {
                if (sample_person != null)
                    return sample_person;

                sample_person = new Person ();

                sample_person.Name    = "Art Vandelay";
                sample_person.Age     = 30;
                sample_person.Height  = 1.65;
                sample_person.Retired = false;
                sample_person.Urls    = new string[] {
                    "http://example.com/artvandelay",
                    "http://artvandelay.org/"
                };

                sample_person.Job = new Job ();
                sample_person.Job.Title = "Importer/Exporter";
                sample_person.Job.Description = "import matches... long matches";

                return sample_person;
            }
        }

        public static readonly string PersonJson = @"

{
    ""Name""    : ""Art Vandelay"",
    ""Age""     : 30,
    ""Height""  : 1.65,
    ""Retired"" : false,
    ""Urls""    : [
        ""http://example.com/artvandelay"",
        ""http://artvandelay.org/"" ],

    ""Job"" : {
        ""Title""       : ""Importer/Exporter"",
        ""Description"" : ""import matches... long matches""
    }
}

";

        private static Hashtable hashtable_person;

        public static Hashtable HashtablePerson {
            get {
                if (hashtable_person != null)
                    return hashtable_person;

                hashtable_person = new Hashtable ();
                hashtable_person.Add ("Name", "Art Vandelay");
                hashtable_person.Add ("Age", 30);
                hashtable_person.Add ("Height", 1.65);
                hashtable_person.Add ("Retired", false);

                string[] urls = new string[] {
                    "http://example.com/artvandelay",
                    "http://artvandelay.org/"
                };

                hashtable_person.Add ("Urls", urls);

                Hashtable job = new Hashtable ();
                job.Add ("Title", "Importer/Exporter");
                job.Add ("Description", "import matches... long matches");

                hashtable_person.Add ("Job", job);

                return hashtable_person;
            }
        }
    }
}
