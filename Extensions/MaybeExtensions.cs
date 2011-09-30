using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Automata.Classes;

namespace Automata.Extensions
{
    public static class MaybeExtensions
    {
        public static Maybe<T> Select<S, T>(this Maybe<S> m, Func<S, T> f)
        {
            return m.HasValue ? new Maybe<T>(f(m.Value)) : new Maybe<T>();
        }

        public static Maybe<T> SelectMany<S, T>(this Maybe<S> m, Func<S, Maybe<T>> f)
        {
            return m.HasValue ? f(m.Value) : new Maybe<T>();
        }
    }
}
