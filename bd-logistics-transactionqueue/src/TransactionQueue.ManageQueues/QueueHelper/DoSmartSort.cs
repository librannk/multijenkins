using System;
using System.Linq;
using System.Collections.Generic;
using TransactionQueue.ManageQueues.Registration;
using TransactionQueue.ManageQueues.Business.Models;
using TransactionQueue.ManageQueues.Common.Constants;

namespace TransactionQueue.ManageQueues.QueueHelper
{
    public class DoSmartSort
    {
        public List<TransactionQueueItems> OrderBySmartSort(List<TransactionQueueItems> queueData, string listofSmartCol)
        {
            //Find sort column
            string sortColumns = GetSmartColumn(listofSmartCol);

            //if sorting columns are empty then return the same list
            if (string.IsNullOrWhiteSpace(sortColumns))
            {
                return queueData;
            }
            // Prepare the sorting columns and order into a list of Tuples
            var sortExpressions = new List<Tuple<string, string>>();
            string[] terms = sortColumns.Split(',');
            for (int i = 0; i < terms.Length; i++)
            {
                string[] items = terms[i].Trim().Split(' ');
                var fieldName = items[0].Trim();
                var sortOrder = (items.Length > 1)
                          ? items[1].Trim().ToLower() : "asc";
                if ((sortOrder != "asc") && (sortOrder != "desc"))
                {
                    throw new ArgumentException("Invalid sorting order");
                }
                sortExpressions.Add(new Tuple<string, string>(fieldName, sortOrder));
            }
            // Calling extension method to Apply sorting
            queueData = queueData.MultipleSort(sortExpressions).ToList();
            //return sorted data
            return queueData;
        }
        private string GetSmartColumn(string listofSmartCol)
        {
            string sortColumns = CommonConstant.Status
                + "," + CommonConstant.Type
                +","+CommonConstant.TransactionPriorityOrder;

            if (!string.IsNullOrEmpty(listofSmartCol))
                sortColumns = sortColumns + "," + listofSmartCol;

            sortColumns = sortColumns +
                "," + CommonConstant.ReceivedDT
                + "," + CommonConstant.ComponentNumber;
            return sortColumns;
        }
    }
}
