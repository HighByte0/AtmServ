using AtmView.DAO;
using AtmView.DAO.Common;
using AtmView.Entities;

namespace AtmView.Services
{
    public class LogService : EntityService<Log, int>, ILogService
    {
        IUnitOfWork _unitOfWork;
        ILogRepository _logRepository;

        public LogService(IUnitOfWork unitOfWork, ILogRepository logRepository)
            : base(unitOfWork, logRepository)
        {
            _unitOfWork = unitOfWork;
            _logRepository = logRepository;


        }
        public void Commit()
        {
            _unitOfWork.Commit();
        }

        public void AddLog(Log log)
        {
            _logRepository.Add(log);

        }

    }
}
