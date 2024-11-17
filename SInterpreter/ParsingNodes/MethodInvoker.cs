using System;
using System.Collections.Generic;

namespace SInterpreter.ParsingNodes
{
    public static class MethodInvoker
    {
        private static readonly Random random = new();
        private static readonly Dictionary<string, Func<object?[], object>> methods = new()
        {
            { "PRINT", Print },
            { "SQRT", Sqrt },
            { "POW", Pow },
            { "SIN", Sin },
            { "COS", Cos },
            { "RAN", Ran },
            { "LENGTH", Length },
            { "LOG", Log },
            { "ABS", Abs },
            { "MAX", Max },
            { "MIN", Min },
            { "TAN", Tan },
            { "ASIN", Asin },
            { "ACOS", Acos },
            { "ATAN", Atan }
        };

        public static object InvokeMethod(string methodName, params object?[] parameters)
        {
            if (methods.TryGetValue(methodName, out var method))
            {
                return method(parameters);
            }
            else
            {
                throw new InvalidOperationException($"Method '{methodName}' is not defined.");
            }
        }

        private static object Print(object?[] parameters)
        {
            ValidateParameterCount(parameters, 1, "PRINT");
            var output = parameters[0] ?? "null";
            Console.WriteLine(output);
            return output;
        }

        private static object Sqrt(object?[] parameters)
        {
            ValidateParameterCount(parameters, 1, "SQRT");
            ValidateParameterType<double>(parameters[0], "SQRT");
            return Math.Sqrt((double)parameters[0]!);
        }

        private static object Pow(object?[] parameters)
        {
            ValidateParameterCount(parameters, 2, "POW");
            ValidateParameterType<double>(parameters[0], "POW base");
            ValidateParameterType<double>(parameters[1], "POW exponent");
            return Math.Pow((double)parameters[0]!, (double)parameters[1]!);
        }

        private static object Sin(object?[] parameters)
        {
            ValidateParameterCount(parameters, 1, "SIN");
            ValidateParameterType<double>(parameters[0], "SIN");

            double degrees = (double)parameters[0]!;
            double radians = degrees * (Math.PI / 180);

            return Math.Sin(radians);
        }

        private static object Cos(object?[] parameters)
        {
            ValidateParameterCount(parameters, 1, "COS");
            ValidateParameterType<double>(parameters[0], "COS");

            double degrees = (double)parameters[0]!;
            double radians = degrees * (Math.PI / 180);

            return Math.Cos(radians);
        }

        private static object Ran(object?[] parameters)
        {
            ValidateParameterCount(parameters, 2, "RAN");
            ValidateParameterType<double>(parameters[0], "RAN min");
            ValidateParameterType<double>(parameters[1], "RAN max");

            double min = (double)parameters[0]!;
            double max = (double)parameters[1]!;
            int intMin = (int)Math.Ceiling(min);
            int intMax = (int)Math.Floor(max);

            int randomInt = random.Next(intMin, intMax + 1);

            return (double)randomInt;
        }
        private static object Length(object?[] parameters)
        {
            ValidateParameterCount(parameters, 1, "LENGTH");
            ValidateParameterType<string>(parameters[0], "");
            return parameters[0]!.ToString()!.Length;
        }
        private static void ValidateParameterCount(object?[] parameters, int expectedCount, string methodName)
        {
            if (parameters.Length != expectedCount)
                throw new ArgumentException($"{methodName} expects {expectedCount} parameter(s).");
        }

        private static void ValidateParameterType<T>(object? parameter, string parameterName)
        {
            if (parameter is not T)
                throw new ArgumentException($"{parameterName} expects a parameter of type {typeof(T).Name}.");
        }
        private static object Log(object?[] parameters)
        {
            ValidateParameterCount(parameters, 1, "LOG");
            ValidateParameterType<double>(parameters[0], "LOG");
            return Math.Log10((double)parameters[0]!);
        }

        private static object Abs(object?[] parameters)
        {
            ValidateParameterCount(parameters, 1, "ABS");
            ValidateParameterType<double>(parameters[0], "ABS");
            return Math.Abs((double)parameters[0]!);
        }

        private static object Max(object?[] parameters)
        {
            ValidateParameterCount(parameters, 2, "MAX");
            ValidateParameterType<double>(parameters[0], "MAX arg1");
            ValidateParameterType<double>(parameters[1], "MAX arg2");
            return Math.Max((double)parameters[0]!, (double)parameters[1]!);
        }

        private static object Min(object?[] parameters)
        {
            ValidateParameterCount(parameters, 2, "MIN");
            ValidateParameterType<double>(parameters[0], "MIN arg1");
            ValidateParameterType<double>(parameters[1], "MIN arg2");
            return Math.Min((double)parameters[0]!, (double)parameters[1]!);
        }

        private static object Tan(object?[] parameters)
        {
            ValidateParameterCount(parameters, 1, "TAN");
            ValidateParameterType<double>(parameters[0], "TAN");

            double degrees = (double)parameters[0]!;
            double radians = degrees * (Math.PI / 180);

            return Math.Tan(radians);
        }

        private static object Asin(object?[] parameters)
        {
            ValidateParameterCount(parameters, 1, "ASIN");
            ValidateParameterType<double>(parameters[0], "ASIN");

            return Math.Asin((double)parameters[0]!) * (180 / Math.PI);
        }

        private static object Acos(object?[] parameters)
        {
            ValidateParameterCount(parameters, 1, "ACOS");
            ValidateParameterType<double>(parameters[0], "ACOS");

            return Math.Acos((double)parameters[0]!) * (180 / Math.PI);
        }

        private static object Atan(object?[] parameters)
        {
            ValidateParameterCount(parameters, 1, "ATAN");
            ValidateParameterType<double>(parameters[0], "ATAN");

            return Math.Atan((double)parameters[0]!) * (180 / Math.PI);
        }
        public static bool MethodExists(string methodName)
        {
            return methods.ContainsKey(methodName);
        }
    }
}
