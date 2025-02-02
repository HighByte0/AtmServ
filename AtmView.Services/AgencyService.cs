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
    public class AgencyService : EntityService<Agency, string>, IAgencyService  
    {
        IUnitOfWork _unitOfWork;
        IAgencyRepository _AgencyRepository;

        public AgencyService(IUnitOfWork unitOfWork, IAgencyRepository agencyRepository)
        : base(unitOfWork, agencyRepository)
        {
            _unitOfWork = unitOfWork;
            _AgencyRepository = agencyRepository;

        }


        public void Commit()
        {
            _unitOfWork.Commit();
        }

        public void DeleteAgency(string agencyId, string ConnectionString)
        {
            _AgencyRepository.DeleteAgency(agencyId, ConnectionString);
        }
    }
}
