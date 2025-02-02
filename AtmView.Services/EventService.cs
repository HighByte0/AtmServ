using AtmView.DAO;
using AtmView.DAO.Common;
using AtmView.Entities;

namespace AtmView.Services
{
    public class EventService : EntityService<Event, int>, IEventService
    {
        IUnitOfWork _unitOfWork;
        IEventRepository _eventRepository;

        public EventService(IUnitOfWork unitOfWork, IEventRepository eventRepository)
            : base(unitOfWork, eventRepository)
        {
            _unitOfWork = unitOfWork;
            _eventRepository = eventRepository;


        }
        public void Commit()
        {
            _unitOfWork.Commit();
        }
    }
}
