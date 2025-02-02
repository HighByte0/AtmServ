using AtmView.Entities;
using Dapper;
using System;
using System.Collections.Generic;

namespace AtmView.Services
{
    public interface IAVTransactionService : IEntiteService<AVTransaction>
    {
        void Commit();
        List<AVTransaction> GetTransactions(DateTime SartDate, DateTime EndDate, string ConnectionString, IEnumerable<UserAtm> atmuser);

        AVTransaction FindTransaction(string atmId, DateTime transactionDate, int transactionNumber);

        IEnumerable<T> GetTransactions_usp<T>(string connectionString, string procedureName, DynamicParameters param = null);

    }
}
