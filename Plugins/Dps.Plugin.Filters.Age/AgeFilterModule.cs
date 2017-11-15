using System;
using System.Linq.Expressions;
using System.Reflection;
using Dps.Infrastructure;
using Dps.Infrastructure.Models;

namespace Dps.Plugin.Filters.Age
{
    public class AgeFilterModule : IFilter
    {
        private string _parameter = "50";

        public string Name
        {
            get { return Assembly.GetAssembly(GetType()).GetName().Name; }
        }

        public Version Version
        {
            get { return new Version(1, 0, 0, 0); }
        }

        public string DefaultParameter
        {
            get
            {
                return _parameter;
            }
        }

        public Expression<Func<Examination, bool>> Filter(string parameter = null)
        {
            int age;
            if (String.IsNullOrEmpty(parameter))
            {
                parameter = DefaultParameter;
            }
            Int32.TryParse(parameter, out age);
            return f => f.Age > age;
        }
    }
}
