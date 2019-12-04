using DynamicExpresso;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Dynamic_Expresso
{
    public static class InterpreterExtensions
    {
        public static bool In(this IEnumerable<int> source, int num)
        {
            return InImp(num, source);
        }

        private static bool InImp<T>(T val, IEnumerable<T> source)
        {
            return source != null && source.Any(el => el.Equals(val));
        }

        public static bool Any(this IEnumerable<int> source, string func)
        {
            var whereFunction = new Interpreter().ParseAsDelegate<Func<int, bool>>(func);

            return source.Any(whereFunction);
        }

        public static bool AnyBigger(this IEnumerable<int> source, int num)
        {
            return source.Any(x => x > num);
        }

        public static bool AllSmaller(this IEnumerable<int> source, int num)
        {
            return source.All(x => x < num);
        }
    }
}
