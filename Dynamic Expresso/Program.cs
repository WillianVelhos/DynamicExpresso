using DynamicExpresso;
using System;
using System.Linq;

namespace Dynamic_Expresso
{
    class Program
    {
        static void Main(string[] args)
        {
            var interpreter = new Interpreter();
            var result = interpreter.Eval("8 /( 2 + 2)");

            Console.WriteLine(result);
            
            interpreter.SetVariable("service", new ServiceExample());
            string expression = "x > 4 ? service.OneMethod() : service.AnotherMethod()";
            Lambda parsedExpression = interpreter.Parse(expression, new Parameter("x", typeof(int)));
            parsedExpression.Invoke(3);

            var prices = new[] { 5, 8, 6, 2 };
            var whereFunction = interpreter.ParseAsDelegate<Func<int, bool>>("arg > 5");
            var count = prices.Where(whereFunction).Count();

            Console.WriteLine(count);

            TesteLinq();

            Console.ReadLine();
        }


        public static void TesteLinq()
        {
            var interpreter = new Interpreter().Reference(typeof(InterpreterExtensions));
            var prices = new[] { 5, 8, 6, 2 };

            var expression = "valores.In(8)";
            interpreter.SetVariable("valores", prices, typeof(int[]));

            var expressionString = "valores.Any(\"arg > 9\")";
            var expressionAnyBigger = "valores.AnyBigger(5)";
            var expressionAllSmaller = "valores.AllSmaller(8)";

            var result = interpreter.Eval(expression);

            var resultString = interpreter.Eval(expressionString);

            var resultAnyBigger = interpreter.Eval(expressionAnyBigger);

            var resultAllSmaller = interpreter.Eval(expressionAllSmaller);

            Console.WriteLine(result);
            Console.WriteLine(resultString);
            Console.WriteLine(resultAllSmaller);
        }
    }
}

