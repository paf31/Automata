using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Automata.Classes
{
    public class Maybe<T>
    {
        public T Value
        {
            get;
            set;
        }

        public bool HasValue
        {
            get;
            set;
        }

        public Maybe(T value)
        {
            Value = value;
            HasValue = true;
        }

        public Maybe()
        {
            HasValue = false;
        }

        public override string ToString()
        {
            return HasValue ? Value.ToString() : null;
        }
    }
}
