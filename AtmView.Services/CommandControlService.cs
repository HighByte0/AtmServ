using AtmView.DAO;
using AtmView.DAO.Common;
using AtmView.Entities;

namespace AtmView.Services
{
    public class CommandControlService : EntityService<CommandControl, int>, ICommandControlService
    {
        IUnitOfWork _unitOfWork;
        ICommandControlRepository _commandControlRepository;

        public CommandControlService(IUnitOfWork unitOfWork, ICommandControlRepository commandControlRepository)
            : base(unitOfWork, commandControlRepository)
        {
            _unitOfWork = unitOfWork;
            _commandControlRepository = commandControlRepository;

        }

        public void Commit()
        {
            _unitOfWork.Commit();
        }


    }
}
