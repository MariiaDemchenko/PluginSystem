using System;
using System.Linq.Expressions;
using System.Reflection;
using Dps.Infrastructure;
using Dps.Infrastructure.Models;

namespace Dps.Plugin.Filters.Smoker
{
    public class SmokerFilterModule : IFilter
    {
        private string _defaultParameter = "No";

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
                return _defaultParameter;
            }
        }

        public Expression<Func<Examination, bool>> Filter(string parameter = null)
        {

            if (String.IsNullOrEmpty(parameter))
            {
                parameter = DefaultParameter;
            }
            var isSmoker = parameter == "Yes";
            return f => f.Smoker == isSmoker;
        }
    }
}
