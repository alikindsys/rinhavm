// See https://aka.ms/new-console-template for more information
using RinhaVM.External.AST.Lexemes;
using RinhaVM.External.AST.TopLevel;
using RinhaVM.Json;
using System.Text.Json;
using System.Text.Json.Serialization;

Console.WriteLine("Hello, World!");



var options = new JsonSerializerOptions {
    WriteIndented = true,
    Converters = { new JsonStringEnumConverter(), new LexemeSerializer() }
};

using (FileStream? fs = new FileStream("fib.json", FileMode.Open)) {
    Console.WriteLine($"Attempting to deserialize fib.json with LexemeSerializer." );
    var file = JsonSerializer.Deserialize<RinhaVM.External.AST.TopLevel.File>(fs, options);
    if(file != null) {

        Console.WriteLine("Success.");
        Console.WriteLine("Re-serializing the file into outs.json");

        var str = JsonSerializer.Serialize(file, options);

        System.IO.File.WriteAllText("outs.json", str);

        Console.WriteLine("Done. Please check for referencial transparency.");
    } 
}