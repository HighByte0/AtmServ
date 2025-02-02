
using AtmView.DAO;
using AtmView.DAO.Common;
using AtmView.Entities;
using System.Collections.Generic;

namespace AtmView.Services
{
    public class ErrTypeIdsService : EntityService<ErrTypeId, int>, IErrTypeIdsService
    {
        IUnitOfWork _unitOfWork;
        IErrTypeIdsRepository _ErrTypeIdsRepository;

        public ErrTypeIdsService(IUnitOfWork unitOfWork, IErrTypeIdsRepository ErrTypeIdsRepository)
            : base(unitOfWork, ErrTypeIdsRepository)
        {
            _unitOfWork = unitOfWork;
            _ErrTypeIdsRepository = ErrTypeIdsRepository;


        }
        public void Commit()
        {
            _unitOfWork.Commit();
        }

        //public List<ErrTypeId> GetreadOnly(int stateId)
        //{
            
        //}

    }
}
