using AtmView.DAO;
using AtmView.DAO.Common;
using AtmView.Entities;

namespace AtmView.Services
{
    public class IncidentService : EntityService<Incident, int>, IIncidentService
    {
        IUnitOfWork _unitOfWork;
        IIncidentRepository _incidentRepository;

        public IncidentService(IUnitOfWork unitOfWork, IIncidentRepository incidentRepository)
            : base(unitOfWork, incidentRepository)
        {
            _unitOfWork = unitOfWork;
            _incidentRepository = incidentRepository;

        }

        public void Commit()
        {
            _unitOfWork.Commit();
        }


    }
}
