using AtmView.DAO;
using AtmView.DAO.Common;
using AtmView.Entities;

namespace AtmView.Services
{
    public class ParametersService : EntityService<Parameters, int>, IParametersService
    {
        IUnitOfWork _unitOfWork;
        IParametersRepository _parametersRepository;

        public ParametersService(IUnitOfWork unitOfWork, IParametersRepository parametersRepository)
            : base(unitOfWork, parametersRepository)
        {
            _unitOfWork = unitOfWork;
            _parametersRepository = parametersRepository;


        }
        public void Commit()
        {
            _unitOfWork.Commit();
        }
    }
}
