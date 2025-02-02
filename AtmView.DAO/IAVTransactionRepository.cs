using AtmView.DAO.Common;
using AtmView.Entities;
using Dapper;
using System;
using System.Collections.Generic;

namespace AtmView.DAO
{

    public interface IAVTransactionRepository : IGenericRepo<AVTransaction>
    {
        List<AVTransaction> GetTransactions(DateTime SartDate, DateTime EndDate, string ConnectionString, IEnumerable<UserAtm> atmuser);
        IEnumerable<T> GetTransactions_usp<T>(string connectionString, string procedureName, DynamicParameters param = null);

        AVTransaction FindTransaction(string atmId, DateTime transactionDate, int transactionNumber);
    }
}
