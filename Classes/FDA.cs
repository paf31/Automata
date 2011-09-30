using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Automata.Classes
{
    public class FDA<I, T>
    {
        public Maybe<T> Value
        {
            get;
            set;
        }

        public Func<I, FDA<I, T>> Continue
        {
            get;
            set;
        }

        public FDA(Maybe<T> value, Func<I, FDA<I, T>> cont)
        {
            Value = value;
            Continue = cont;
        }
    }
}
