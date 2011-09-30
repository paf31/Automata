using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Automata.Classes;

namespace Automata.Extensions
{
    public static class FDAExtensions
    {
        public static FDA<I, T1> Select<I, T, T1>(this FDA<I, T> a, Func<T, T1> f)
        {
            return new FDA<I, T1>(a.Value.Select(f),
                i =>
                {
                    var next = a.Continue(i);
                    return next == null ? null : next.Select(f);
                });
        }

        public static FDA<I, T1> SelectMany<I, T, T1>(this FDA<I, T> a,
            Func<T, FDA<I, T1>> f)
        {
            return a.Value.HasValue ?
                f(a.Value.Value)
                : new FDA<I, T1>(new Maybe<T1>(),
                    i =>
                    {
                        var next = a.Continue(i);
                        return next == null ? null : next.SelectMany(f);
                    });
        }

        public static FDA<I, T2> SelectMany<I, T, T1, T2>(this FDA<I, T> a,
            Func<T, FDA<I, T1>> s,
            Func<T, T1, T2> projector)
        {
            return a.SelectMany(x => s(x).Select(y => projector(x, y)));
        }

        public static FDA<I, T> Or<I, T>(this FDA<I, T> a1, FDA<I, T> a2)
        {
            return new FDA<I, T>(a1.Value.HasValue ? a1.Value : a2.Value,
                i => a1.Continue(i) ?? a2.Continue(i));
        }

        public static FDA<I, IEnumerable<T>> AllowMany<I, T>(this FDA<I, T> a)
        {
            return (from t in a
                    from ts in a.AllowMany()
                    select new[] { t }.Concat(ts)).Or(Automata.Accept<I, IEnumerable<T>>(Enumerable.Empty<T>()));
        }

        public static Maybe<T> Accepts<I, T>(this FDA<I, T> a, IEnumerable<I> input)
        {
            if (input.Any())
            {
                var next = a.Continue(input.FirstOrDefault());

                if (next != null)
                {
                    return next.Accepts(input.Skip(1));
                }
            }

            return a.Value;
        }
    }
}
