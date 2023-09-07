// A gente tá trabalhando com JSONs de tamanhos gargantuãs.
// JSONs polimórficos de tamanhos estratosféricos.

// O fib vai de 106B para 4.77KB (um aumento de 45x!).
// Nota: Não confiar nas meninas. Fazer algo mais eficiente.

// Pra lidar com isso temos que streamar o json ao invéz de
// carregar ele na memória.

using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;
using RinhaVM.External.AST;
using RinhaVM.External.AST.Lexemes;

namespace RinhaVM.Json {
    // Agora que a gente tem a AST, tá na hora do JSON-rank polymorphism.
    public class LexemeSerializer : JsonConverter<Lexeme>
    {
        public override Lexeme? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            // Algoritmo:
            // 1) Tenta obter a field `kind` do objeto
            // 1.1) Se não existir, não é um Lexema.
            // 1.2) Se existir, verifica se é um lexema válido
            // 2) Caso for válido, passa isso para o parser do tipo.
            JsonElement? elem; 
            JsonNode? node;

            //Console.WriteLine("[LexemeSerializer] Started with a fresh reader.");

            JsonElement.TryParseValue(ref reader, out elem);

            //Console.WriteLine("[LexemeSerializer] Read into elem.");
            

            if (elem == null) {
                return null;
            }

            //Console.WriteLine("[LexemeSerializer] elem is not null.");

// Se fosse kotlin, eu não precisaria disso. Olha o null check lá em cima. pqp.
#pragma warning disable CS8600
            JsonObject obj = JsonObject.Create(elem.Value);
#pragma warning restore CS8600

            //Console.WriteLine("[LexemeSerializer] Created JsonObject from elem.");

#pragma warning disable CS8602
            if(!obj.TryGetPropertyValue("kind", out node)) {
#pragma warning restore CS8602
                return null;
            }

            //Console.WriteLine("[LexemeSerializer] Elem has key \"kind\".");
            //Console.WriteLine($"    kind: {node}");

            //Console.WriteLine("[LexemeSerializer] Deserializing to LexemeKind");

            var kind = node.Deserialize<LexemeKind>(options);

            //Console.WriteLine($"[LexemeSerializer] LexemeKind= {kind}. Deferring to Type-Specific Implementation.");


            return kind switch
            {
                LexemeKind.Int => obj.Deserialize<Int>(options),
                LexemeKind.Str => obj.Deserialize<Str>(options),
                LexemeKind.Call => obj.Deserialize<Call>(options),
                LexemeKind.Binary => obj.Deserialize<Binary>(options),
                LexemeKind.Function => obj.Deserialize<Function>(options),
                LexemeKind.Let => obj.Deserialize<Let>(options),
                LexemeKind.If => obj.Deserialize<If>(options),
                LexemeKind.Print => obj.Deserialize<Print>(options),
                LexemeKind.First => obj.Deserialize<First>(options),
                LexemeKind.Second => obj.Deserialize<Second>(options),
                LexemeKind.Bool => obj.Deserialize<Bool>(options),
                LexemeKind.Tuple => obj.Deserialize<External.AST.Lexemes.Tuple>(options),
                LexemeKind.Var => obj.Deserialize<Var>(options),
                _ => null
            };
        }

        public override void Write(Utf8JsonWriter writer, Lexeme value, JsonSerializerOptions options)
        {
            switch(value.kind) {
                case LexemeKind.Int:
                    JsonSerializer.Serialize<Int>(writer, (Int) value, options);
                    return;

                case LexemeKind.Str:
                    JsonSerializer.Serialize<Str>(writer, (Str) value, options);
                    return;

                case LexemeKind.Call:
                    JsonSerializer.Serialize<Call>(writer, (Call) value, options);
                    return;

                case LexemeKind.Binary:
                    JsonSerializer.Serialize<Binary>(writer, (Binary) value, options);
                    return;

                case LexemeKind.Function:
                    JsonSerializer.Serialize<Function>(writer, (Function) value, options);
                    return;
                
                case LexemeKind.Let:
                    JsonSerializer.Serialize<Let>(writer, (Let) value, options);
                    return;

                case LexemeKind.If:
                    JsonSerializer.Serialize<If>(writer, (If) value, options);
                    return;

                case LexemeKind.Print:
                    JsonSerializer.Serialize<Print>(writer, (Print) value, options);
                    return;

                case LexemeKind.First:
                    JsonSerializer.Serialize<First>(writer, (First) value, options);
                    return;

                case LexemeKind.Second:
                    JsonSerializer.Serialize<Second>(writer, (Second) value, options);
                    return;
                
                case LexemeKind.Bool:
                    JsonSerializer.Serialize<Bool>(writer, (Bool) value, options);
                    return;

                case LexemeKind.Tuple:
                    JsonSerializer.Serialize<External.AST.Lexemes.Tuple>(writer, (External.AST.Lexemes.Tuple) value, options);
                    return;

                case LexemeKind.Var:
                    JsonSerializer.Serialize<Var>(writer, (Var) value, options);
                    return;

                default:
                    throw new InvalidOperationException();
            }
        }
    }
}