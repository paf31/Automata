using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Automata.Classes;
using Automata.Extensions;

namespace Automata
{
    public static class Program
    {
        static FDA<char, byte> Digit = 
            from c in Automata.Any<char>(char.IsNumber)
            select byte.Parse(c.ToString());

        static FDA<char, int> Sum =
            from d1 in Digit
            from r in
                (from plus in Automata.Match('+')
                 from sum in Sum
                 select d1 + sum)
                 .Or(from d2 in Digit
                     from r1 in
                         (from plus in Automata.Match('+')
                          from sum in Sum
                          select sum).Or(Automata.Accept<char, int>(0))
                     select d1 * 10 + d2 + r1)
                     .Or(Automata.Accept<char, int>(d1))
            select r;

        public static void Main(string[] args) 
        {
            Console.WriteLine(string.Format("12+3+45+6+78+9={0}", Sum.Accepts("12+3+45+6+78+9")));
        }
    }
}
