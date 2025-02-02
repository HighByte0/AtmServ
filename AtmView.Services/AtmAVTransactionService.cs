using AtmView.Common;
using AtmView.DAO;
using AtmView.Entities;
using System.Collections.Generic;

namespace AtmView.Services
{
    public class AtmAVTransactionService
    {
        public List<AVTransaction> GetListTransaction(string connectionString, AVTransactionParams avParams)
        {
            List<AVTransaction> list = new List<AVTransaction>();

            list = new AVTransactionRepo().GetListTransaction(connectionString, avParams);
            return list;
        }


        public void Update(string connectionString, AVTransactionParams avParams)
        {

            new AVTransactionRepo().Update(connectionString, avParams);

        }




    }
}
