using System;
using System.IO;
using System.Collections.Generic;
using SInterpreter.ParsingNodes;

namespace SInterpreter
{
    internal class MainClass
    {
        public static void Main(string[] args)
        {
            string filePath = args[0];

            if (!File.Exists(filePath))
            {
                Console.WriteLine($"Error: File '{filePath}' does not exist.");
                return;
            }

            string Code = File.ReadAllText(filePath);

            Queue<Token> TokensQueue = new();
            Lexer lexer = new(Code);

            while (lexer.HasNext())
            {
                lexer.FindType(out Token tok);
                TokensQueue.Enqueue(tok);
            }
            try
            {


                Parser parser = new([.. TokensQueue], 0);
                parser.ParseCheck();

                Console.WriteLine("Code parsed successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }
    }
}
