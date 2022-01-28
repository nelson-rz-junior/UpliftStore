using Dapper;
using System;
using System.Collections.Generic;

namespace UpliftStore.DataAccess.Data.Repository.IRepository
{
    public interface ISP_Call : IDisposable
    {
        IEnumerable<T> GetAll<T>(string procedureName, DynamicParameters parameters = null);

        void Execute<T>(string procedureName, DynamicParameters parameters = null);

        T Get<T>(string procedureName, DynamicParameters parameters = null);
    }
}
