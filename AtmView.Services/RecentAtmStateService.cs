using AtmView.DAO;
using AtmView.DAO.Common;
using AtmView.Entities;
using System;

namespace AtmView.Services
{
    public class RecentAtmStateService : EntityService<RecentAtmState, int>, IRecentAtmStateService
    {
        IUnitOfWork _unitOfWork;
        IRecentAtmStateRepository _recentAtmStateRepository;

        public RecentAtmStateService(IUnitOfWork unitOfWork, IRecentAtmStateRepository recentAtmStateRepository)
            : base(unitOfWork, recentAtmStateRepository)
        {
            _unitOfWork = unitOfWork;
            _recentAtmStateRepository = recentAtmStateRepository;

        }

        public void Commit()
        {
            _unitOfWork.Commit();
        }

        void IRecentAtmStateService.Commit()
        {
            throw new NotImplementedException();
        }

        int IRecentAtmStateService.getStateTypeCode(RecentAtmState recentatmState)
        {
            return (recentatmState.Connected ? recentatmState.LastStateType : 5);
        }
    }
}