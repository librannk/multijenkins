
using System;
using System.Collections.Generic;
using System.Linq;

namespace TransactionQueue.ManageQueues.Registration
{
    public static class MultiSortingUtility
    {
        public static IEnumerable<T> MultipleSort<T>(this IEnumerable<T> data,
                  List<Tuple<string, string>> sortExpressions)
        {
            // If sort expression is null then return the same list
            if ((sortExpressions == null) || (sortExpressions.Count <= 0))
            {
                return data;
            }
            // Let's sort it
            IEnumerable<T> query = from item in data select item;
            IOrderedEnumerable<T> orderedQuery = null;

            for (int i = 0; i < sortExpressions.Count; i++)
            {
                var index = i;
                Func<T, object> expression = item => item.GetType()
                               .GetProperty(sortExpressions[index].Item1)
                               .GetValue(item, null);

                if (sortExpressions[index].Item2 == "asc")
                {
                    orderedQuery = (index == 0) ? query.OrderBy(expression)
                      : orderedQuery.ThenBy(expression);
                }
                else
                {
                    orderedQuery = (index == 0) ? query.OrderByDescending(expression)
                             : orderedQuery.ThenByDescending(expression);
                }
            }
            query = orderedQuery;
            return query;
        }
    }
}
