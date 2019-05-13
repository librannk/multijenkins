using System;
using System.Collections.Generic;
using System.Text;

namespace BD.Core.ElasticClient.Mongo
{
    public interface IListAccountMap
    {
        AccountMap GetOrAddAccountMap(string accountKey, string connectionString);
    }
}