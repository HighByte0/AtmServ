using AtmView.DAO;
using AtmView.DAO.Common;
using AtmView.Entities;
using Dapper;
using System;
using System.Collections.Generic;

namespace AtmView.Services
{
    public class AVTransactionService : EntiteService<AVTransaction>, IAVTransactionService
    {
        IUnitOfWork _unitOfWork;
        IAVTransactionRepository _AVTransactionRepository;

        public AVTransactionService(IUnitOfWork unitOfWork, IAVTransactionRepository AVTransactionRepository)
            : base(unitOfWork, AVTransactionRepository)
        {
            _unitOfWork = unitOfWork;
            _AVTransactionRepository = AVTransactionRepository;

        }

        public void Commit()
        {
            _unitOfWork.Commit();
        }

        public List<AVTransaction> GetTransactions(DateTime SartDate, DateTime EndDate, string ConnectionString, IEnumerable<UserAtm> atmuser)
        {
            List<AVTransaction> list = _AVTransactionRepository.GetTransactions(SartDate, EndDate, ConnectionString, atmuser);
            return list;
        }

        public IEnumerable<T> GetTransactions_usp<T>(string connectionString, string procedureName, DynamicParameters param = null)
        {
            return _AVTransactionRepository.GetTransactions_usp<T>(connectionString, procedureName, param);
        }

        public AVTransaction FindTransaction(string atmId, DateTime transactionDate, int transactionNumber)
        {
            return _AVTransactionRepository.FindTransaction(atmId, transactionDate, transactionNumber);
               
        }

    }



}
