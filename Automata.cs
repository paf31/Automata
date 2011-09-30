using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Automata.Classes;
using Automata.Extensions;

namespace Automata
{
    public static class Automata
    {
        public static FDA<I, T> Accept<I, T>(T t)
        {
            return new FDA<I, T>(new Maybe<T>(t), i => null);
        }

        public static FDA<I, T> Reject<I, T>()
        {
            return new FDA<I, T>(new Maybe<T>(), i => null);
        }

        public static FDA<I, I> Any<I>()
        {
            return new FDA<I, I>(new Maybe<I>(), i => new FDA<I, I>(new Maybe<I>(i), i1 => null));
        }

        public static FDA<I, I> Any<I>(Func<I, bool> predicate)
        {
            return new FDA<I, I>(new Maybe<I>(), i => predicate(i) ? new FDA<I, I>(new Maybe<I>(i), i1 => null) : null);
        }

        public static FDA<I, I> Match<I>(I i) where I : IEquatable<I>
        {
            return Any<I>(i1 => i.Equals(i1));
        }

        public static FDA<char, string> MatchString(string s)
        {
            if (string.IsNullOrEmpty(s))
            {
                return Accept<char, string>(string.Empty);
            }

            return from c in Match(s[0])
                   from cs in MatchString(s.Substring(1))
                   select c + cs;
        }
    }
}
