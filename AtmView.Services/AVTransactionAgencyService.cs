using AtmView.DAO;
using AtmView.DAO.Common;
using AtmView.Entities;

namespace AtmView.Services
{
    public class AVTransactionAgencyService : EntiteService<AVTransactionAgency>, IAVTransactionAgencyService
    {
        IUnitOfWork _unitOfWork;
        IAVTransactionAgencyRepository _AVTransactionAgencyRepository;

        public AVTransactionAgencyService(IUnitOfWork unitOfWork, IAVTransactionAgencyRepository AVTransactionAgencyRepository)
            : base(unitOfWork, AVTransactionAgencyRepository)
        {
            _unitOfWork = unitOfWork;
            _AVTransactionAgencyRepository = AVTransactionAgencyRepository;

        }

        public void Commit()
        {
            _unitOfWork.Commit();
        }


    }



}
