using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Resources;
using System.Runtime.CompilerServices;
using DAL.Properties;

namespace DAL.Utilities
{
    internal class Check
    {
        public static T NotNull<T>(T value, string parameterName) where T : class
        {
            if (value == null)
            {
                throw new System.ArgumentNullException(parameterName);
            }
            return value;
        }
        public static T? NotNull<T>(T? value, string parameterName) where T : struct
        {
            if (!value.HasValue)
            {
                throw new System.ArgumentNullException(parameterName);
            }
            return value;
        }
        public static string NotEmpty(string value, string parameterName)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new System.ArgumentException(string.Format(Resources.Common_ArgumentIsNullOrWhitespace, parameterName));
            }
            return value;
        }
    }
}
