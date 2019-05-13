
using System.Linq;
using System.Collections.Generic;
using TransactionQueue.ManageQueues.Business.Models;

namespace TransactionQueue.ManageQueues.QueueHelper
{
    public class SortColumns
    {
        public static string GetSmartColumn(List<TransactionPriority> transactionPriorities)
        {
            //var smartSortOrder = transactionPriorities.Where(smartSort => smartSort.SmartSortData.Where(smartCol => smartCol.SmartSortOrder == 1)).ToList();
            //var smartSortColumn = transactionPriorities.Select(x => x.SmartSortData.OrderBy(y => y.SmartSortOrder).Select(z=>z.SmartSortColumn)).ToList();
            //var smart = transactionPriorities.Where(smartSort => smartSort.SmartSortData.Any(data => data.SmartSortColumn.ColumnName == "Location")).FirstOrDefault();
            string SmartSortColumn = null;
            var smartSortOrder = transactionPriorities.Where(x=>x.SmartSortData.Count>0).Select(x => x.SmartSortData.OrderBy(y => y.SmartSortOrder)).ToList();
            foreach (var item in smartSortOrder)
            {
                var colCollection = item.Select(x => x.SmartSortColumn);
                foreach (var itemCol in colCollection)
                {
                    var col = "";
                    if (!string.IsNullOrEmpty(itemCol))
                    {
                        col = itemCol + " asc";
                        SmartSortColumn = SmartSortColumn == null ? col : SmartSortColumn + "," + col;
                    }
                }
                
            }
            return SmartSortColumn;
        }
    }
}
