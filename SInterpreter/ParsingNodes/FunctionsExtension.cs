using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SInterpreter;

namespace SInterpreter.ParsingNodes
{
    public class FunctionsExtension
    {
        public static int[] ParseBlockCode(Tokens endToken, Parser parser)
        {
            parser.Check(Tokens.LBRACE);
            int[] BodyRange = new int[2];
            BodyRange[0] = parser.Pos;
            while (parser.LT[parser.Pos].Type != endToken)
            {
                BodyRange[1] = parser.Pos;
                parser.Pos++;
            }
            return BodyRange;
        }
    }
}
