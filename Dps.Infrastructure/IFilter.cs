using System;
using System.Linq.Expressions;
using Dps.Infrastructure.Models;

namespace Dps.Infrastructure
{
    public interface IFilter
    {
        /// <summary>
        /// Уникальное имя плагина
        /// </summary>
        string Name { get; }

        /// <summary>
        /// Версия плагина
        /// </summary>
        Version Version { get; }

        Expression<Func<Examination, bool>> Filter(string parameter);

        string DefaultParameter { get; }
    };
}
