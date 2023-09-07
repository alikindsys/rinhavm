// Tudo dentro de External faz interface com o rolê das menina
// o que libera a gente de fazer coisas mto loucas.

// A Spec vai ser implementada em External, pq a gente n tem controle
// sobre a spec.

// Mesma coisa com a AST delas.
using System.Text.Json.Serialization;
using RinhaVM.External.AST.TopLevel;

namespace RinhaVM.External.AST {
    // Nós sabemos que a serialização é polimórfica em `kind`.
    
    // Dados não polimórficos em `kind`.
    namespace TopLevel {
        // Esse loc é lindão, mta vontade de dar skip nele.
        //TODO: Não usar isso.

        public record File {
            public required string name {get; set;} 
            public required Lexeme expression {get; set;}
        }


        public class Parameter {
            public required  string text {get; set;}
        }

        public enum BinaryOp {
            Add,
            Sub,
            Mul,
            Div,
            Rem,
            Eq,
            Neq,
            Lt,
            Gt,
            Lte,
            Gte,
            And,
            Or
        }
    }

    namespace Lexemes {
        public class If : Lexeme {
            public LexemeKind kind => LexemeKind.If;
            public required Lexeme condition {get; set;}
            public required Lexeme then {get; set;}
            public required Lexeme otherwise {get; set;}
        }

        public class Let : Lexeme {
            public LexemeKind kind => LexemeKind.Let;
            public required Parameter name {get; set;}
            public required Lexeme value {get; set;}
            public required Lexeme next {get; set;}
        }

        public class Str : Lexeme {
            public LexemeKind kind => LexemeKind.Str;
            public required string value {get; set;}
        }

        public class Bool : Lexeme {
            public LexemeKind kind => LexemeKind.Bool;
            public required bool value {get; set;}
        }

        public class Int : Lexeme {
            public LexemeKind kind => LexemeKind.Int;
            public required int value {get; set;}
        }

        public class Binary : Lexeme {
            public LexemeKind kind => LexemeKind.Binary;
            public required Lexeme lhs {get; set;}
            public required BinaryOp op {get; set;}
            public required Lexeme rhs {get; set;}
        }

        public class Call : Lexeme {
            public LexemeKind kind => LexemeKind.Call;
            public required Lexeme callee {get; set;}
            public required List<Lexeme> arguments {get; set;}
        }

        public class Function : Lexeme {
            public LexemeKind kind => LexemeKind.Function;
            public required List<Parameter> parameters {get; set;}
            public required Lexeme value {get; set;}


        }

        public class Print : Lexeme {
            public LexemeKind kind  => LexemeKind.Print;
            public required Lexeme value {get; set;}
        }

        public class First : Lexeme {
            public LexemeKind kind => LexemeKind.First;
            public required Lexeme value {get; set;}
        }

        public class Second : Lexeme {
            public LexemeKind kind => LexemeKind.Second;
            public required Lexeme value {get; set;}
        }

        public class Tuple : Lexeme {
            public LexemeKind kind => LexemeKind.Tuple;
            public required Lexeme first {get; set;}
            public required Lexeme second {get; set;}
        }

        public class Var : Lexeme {
            
            public LexemeKind kind => LexemeKind.Var;
            public required string text {get; set;}
        }
    }

    
    /// <summary>
    /// Um tipo da ast que pode ser derivado pela field `kind`.
    /// Regras: 
    /// - Todo lexema tem que ter `kind`.
    /// - Todo lexema é um JsonObject.
    /// </summary>
    public interface Lexeme {
        public LexemeKind kind {get;}
    }


    
    public enum LexemeKind {
        Int,
        Str,
        Call,
        Binary,
        Function,
        Let,
        If,
        Print,
        First,
        Second,
        Bool,
        Tuple,
        Var
    }

}