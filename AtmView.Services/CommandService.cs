using AtmView.DAO;
using AtmView.DAO.Common;
using AtmView.Entities;

namespace AtmView.Services
{
    public class CommandService : EntityService<Command, int>, ICommandService
    {
        IUnitOfWork _unitOfWork;
        ICommandRepository _commandRepository;

        public CommandService(IUnitOfWork unitOfWork, ICommandRepository commandRepository)
            : base(unitOfWork, commandRepository)
        {
            _unitOfWork = unitOfWork;
            _commandRepository = commandRepository;

        }

        public void Commit()
        {
            _unitOfWork.Commit();
        }


    }
}
