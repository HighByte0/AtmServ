using AtmView.DAO;
using AtmView.DAO.Common;
using AtmView.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AtmView.Services
{
    public class ParametersAgencyService : EntityService<ParametersAgency, int>, IParametersAgencyService
    {
        IUnitOfWork _unitOfWork;
        IParametersAgencyRepository _parametersRepository;

        public ParametersAgencyService(IUnitOfWork unitOfWork, IParametersAgencyRepository parametersRepository)
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
