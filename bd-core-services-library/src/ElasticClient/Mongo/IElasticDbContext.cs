using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;

namespace BD.Core.ElasticClient.Mongo
{
    interface IElasticDbContext
    {
        IMongoDatabase GetContext();
    }
}
