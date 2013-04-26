# Quick Start

## Mapping JSON to objects and vice versa

In order to consume data in JSON format inside .Net programs, the natural
approach that comes to mind is to use JSON text to populate a new instance
of a particular class; either a custom one, built to match the structure of
the input JSON text, or a more general one which acts as a dictionary.

Conversely, in order to build new JSON strings from data stored in objects,
a simple _export_--like operation sounds like a good idea.

For this purpose, *LitJSON* includes the `JsonMapper` class,
which provides two main methods used to do JSON--to--object and
object--to--JSON conversions.  These methods are
`JsonMapper.ToObject` and `JsonMapper.ToJson`.

### Simple `JsonMapper` examples

As the following example demonstrates, the `ToObject` method has a generic
variant, `JsonMapper.ToObject<T>`, that is used to specify the type of the
object to be returned.

```cs
using LitJson;
using System;

public class Person
{
    // C# 3.0 auto-implemented properties
    public string   Name     { get; set; }
    public int      Age      { get; set; }
    public DateTime Birthday { get; set; }
}

public class JsonSample
{
    public static void Main()
    {
        PersonToJson();
        JsonToPerson();
    }

    public static void PersonToJson()
    {
        Person bill = new Person();

        bill.Name = "William Shakespeare";
        bill.Age  = 51;
        bill.Birthday = new DateTime(1564, 4, 26);

        string json_bill = JsonMapper.ToJson(bill);

        Console.WriteLine(json_bill);
    }

    public static void JsonToPerson()
    {
        string json = @"
            {
                ""Name""     : ""Thomas More"",
                ""Age""      : 57,
                ""Birthday"" : ""02/07/1478 00:00:00""
            }";

        Person thomas = JsonMapper.ToObject<Person>(json);

        Console.WriteLine("Thomas' age: {0}", thomas.Age);
    }
}
```

Output from the example:

```console
{"Name":"William Shakespeare","Age":51,"Birthday":"04/26/1564 00:00:00"}
Thomas' age: 57
```

### Using the non--generic variant of `JsonMapper.ToObject`

When JSON data is to be read and a custom class that matches a particular
data structure is not available or desired, users can use the non--generic
variant of `ToObject`, which returns a `JsonData` instance. `JsonData` is a
general purpose type that can hold any of the data types supported by JSON,
including lists and dictionaries.

```cs
using LitJson;
using System;

public class JsonSample
{
    public static void Main()
    {
        string json = @"
          {
            ""album"" : {
              ""name""   : ""The Dark Side of the Moon"",
              ""artist"" : ""Pink Floyd"",
              ""year""   : 1973,
              ""tracks"" : [
                ""Speak To Me"",
                ""Breathe"",
                ""On The Run""
              ]
            }
          }
        ";

        LoadAlbumData(json);
    }

    public static void LoadAlbumData(string json_text)
    {
        Console.WriteLine("Reading data from the following JSON string: {0}",
                          json_text);

        JsonData data = JsonMapper.ToObject(json_text);

        // Dictionaries are accessed like a hash-table
        Console.WriteLine("Album's name: {0}", data["album"]["name"]);

        // Scalar elements stored in a JsonData instance can be cast to
        // their natural types
        string artist = (string) data["album"]["artist"];
        int    year   = (int) data["album"]["year"];

        Console.WriteLine("Recorded by {0} in {1}", artist, year);

        // Arrays are accessed like regular lists as well
        Console.WriteLine("First track: {0}", data["album"]["tracks"][0]);
    }
}
```

Output from the example:

```console
Reading data from the following JSON string:
          {
            "album" : {
              "name"   : "The Dark Side of the Moon",
              "artist" : "Pink Floyd",
              "year"   : 1973,
              "tracks" : [
                "Speak To Me",
                "Breathe",
                "On The Run"
              ]
            }
          }

Album's name: The Dark Side of the Moon
Recorded by Pink Floyd in 1973
First track: Speak To Me
```

## Readers and Writers

An alternative interface to handling JSON data that might be familiar to
some developers is through classes that make it possible to read and write
data in a stream--like fashion. These classes are `JsonReader` and
`JsonWriter`.

These two types are in fact the foundation of this library, and the
`JsonMapper` type is built on top of them, so in a way, the developer can
think of the reader and writer classes as the low--level programming
interface for LitJSON.

### Using `JsonReader`

```cs
using LitJson;
using System;

public class DataReader
{
    public static void Main()
    {
        string sample = @"{
            ""name""  : ""Bill"",
            ""age""   : 32,
            ""awake"" : true,
            ""n""     : 1994.0226,
            ""note""  : [ ""life"", ""is"", ""but"", ""a"", ""dream"" ]
          }";

        PrintJson(sample);
    }

    public static void PrintJson(string json)
    {
        JsonReader reader = new JsonReader(json);

        Console.WriteLine ("{0,14} {1,10} {2,16}", "Token", "Value", "Type");
        Console.WriteLine (new String ('-', 42));

        // The Read() method returns false when there's nothing else to read
        while (reader.Read()) {
            string type = reader.Value != null ?
                reader.Value.GetType().ToString() : "";

            Console.WriteLine("{0,14} {1,10} {2,16}",
                              reader.Token, reader.Value, type);
        }
    }
}
```

This example would produce the following output:

```console
         Token      Value             Type
------------------------------------------
   ObjectStart                            
  PropertyName       name    System.String
        String       Bill    System.String
  PropertyName        age    System.String
           Int         32     System.Int32
  PropertyName      awake    System.String
       Boolean       True   System.Boolean
  PropertyName          n    System.String
        Double  1994.0226    System.Double
  PropertyName       note    System.String
    ArrayStart                            
        String       life    System.String
        String         is    System.String
        String        but    System.String
        String          a    System.String
        String      dream    System.String
      ArrayEnd                            
     ObjectEnd                            
```

### Using `JsonWriter`

The `JsonWriter` class is quite simple. Keep in mind that if you want to
convert some arbitrary object into a JSON string, you'd normally just use
`JsonMapper.ToJson`.

```cs
using LitJson;
using System;
using System.Text;

public class DataWriter
{
    public static void Main()
    {
        StringBuilder sb = new StringBuilder();
        JsonWriter writer = new JsonWriter(sb);

        writer.WriteArrayStart();
        writer.Write(1);
        writer.Write(2);
        writer.Write(3);

        writer.WriteObjectStart();
        writer.WritePropertyName("color");
        writer.Write("blue");
        writer.WriteObjectEnd();

        writer.WriteArrayEnd();

        Console.WriteLine(sb.ToString());
    }
}
```

Output from the example:

```console
[1,2,3,{"color":"blue"}]
```

## Configuring the library's behaviour

JSON is a very concise data--interchange format; nothing more, nothing less.
For this reason, handling data in JSON format inside a program may require a
deliberate decision on your part regarding some little detail that goes
beyond the scope of JSON's specs.

Consider, for example, reading data from JSON strings where
single--quotes are used to delimit strings, or Javascript--style
comments are included as a form of documentation. Those things are not part
of the JSON standard, but they are commonly used by some developers, so you
may want to be forgiving or strict depending on the situation. Or what about
if you want to convert a *.Net* object into a JSON string, but
pretty--printed (using indentation)?

To declare the behaviour you want, you may change a few properties from your
`JsonReader` and `JsonWriter` objects.

### Configuration of `JsonReader`

```cs
using LitJson;
using System;

public class JsonReaderConfigExample
{
    public static void Main()
    {
        string json;

        json = " /* these are some numbers */ [ 2, 3, 5, 7, 11 ] ";
        TestReadingArray(json);

        json = " [ \"hello\", 'world' ] ";
        TestReadingArray(json);
    }

    static void TestReadingArray(string json_array)
    {
        JsonReader defaultReader, customReader;

        defaultReader = new JsonReader(json_array);
        customReader  = new JsonReader(json_array);

        customReader.AllowComments            = false;
        customReader.AllowSingleQuotedStrings = false;

        ReadArray(defaultReader);
        ReadArray(customReader);
    }

    static void ReadArray(JsonReader reader)
    {
        Console.WriteLine("Reading an array");

        try {
            JsonData data = JsonMapper.ToObject(reader);

            foreach (JsonData elem in data)
                Console.Write("  {0}", elem);

            Console.WriteLine("  [end]");
        }
        catch (Exception e) {
            Console.WriteLine("  Exception caught: {0}", e.Message);
        }
    }
}
```

The output would be:

```console
Reading an array
  2  3  5  7  11  [end]
Reading an array
  Exception caught: Invalid character '/' in input string
Reading an array
  hello  world  [end]
Reading an array
  Exception caught: Invalid character ''' in input string
```

### Configuration of `JsonWriter`

```cs
using LitJson;
using System;

public enum AnimalType
{
    Dog,
    Cat,
    Parrot
}

public class Animal
{
    public string     Name { get; set; }
    public AnimalType Type { get; set; }
    public int        Age  { get; set; }
    public string[]   Toys { get; set; }
}

public class JsonWriterConfigExample
{
    public static void Main()
    {
        var dog = new Animal {
            Name = "Noam Chompsky",
            Type = AnimalType.Dog,
            Age  = 3,
            Toys = new string[] { "rubber bone", "tennis ball" }
        };

        var cat = new Animal {
            Name = "Colonel Meow",
            Type = AnimalType.Cat,
            Age  = 5,
            Toys = new string[] { "cardboard box" }
        };

        TestWritingAnimal(dog);
        TestWritingAnimal(cat, 2);
    }

    static void TestWritingAnimal(Animal pet, int indentLevel = 0)
    {
        Console.WriteLine("\nConverting {0}'s data into JSON..", pet.Name);
        JsonWriter writer1 = new JsonWriter(Console.Out);
        JsonWriter writer2 = new JsonWriter(Console.Out);

        writer2.PrettyPrint = true;
        if (indentLevel != 0)
            writer2.IndentValue = indentLevel;

        Console.WriteLine("Default JSON string:");
        JsonMapper.ToJson(pet, writer1);

        Console.Write("\nPretty-printed:");
        JsonMapper.ToJson(pet, writer2);
        Console.WriteLine("");
    }
}
```

The output from this example is:

```console

Converting Noam Chompsky's data into JSON..
Default JSON string:
{"Name":"Noam Chompsky","Type":0,"Age":3,"Toys":["rubber bone","tennis ball"]}
Pretty-printed:
{
    "Name" : "Noam Chompsky",
    "Type" : 0,
    "Age"  : 3,
    "Toys" : [
        "rubber bone",
        "tennis ball"
    ]
}

Converting Colonel Meow's data into JSON..
Default JSON string:
{"Name":"Colonel Meow","Type":1,"Age":5,"Toys":["cardboard box"]}
Pretty-printed:
{
  "Name" : "Colonel Meow",
  "Type" : 1,
  "Age"  : 5,
  "Toys" : [
    "cardboard box"
  ]
}
```
