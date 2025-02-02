using AtmView.DAO;
using AtmView.DAO.Common;
using AtmView.Entities;
using System;
using System.Linq;

namespace AtmView.Services
{
    public class StateService : EntityService<State, int>, IStateService
    {
        IUnitOfWork _unitOfWork;
        IStateRepository _stateRepository;

        public StateService(IUnitOfWork unitOfWork, IStateRepository stateRepository)
            : base(unitOfWork, stateRepository)
        {
            _unitOfWork = unitOfWork;
            _stateRepository = stateRepository;

        }

        public void Commit()
        {
            _unitOfWork.Commit();
        }


        public void Add(State state)
        {
            //recuperer le  plus recent state de l atm , comparer le total de l objet CashStocks  (un state a normalment un seul cachstock)
            //si le total passé en param  est superieur a celui deja en base alors on alimente la table doneordder  
            //  DoneOrder_Date  date alimlentation 
            //DoneOrder_Amount diference des totaux (param - dernier state)
            //DoneOrderState : selectionner le dernier order valid duatm => amount , si amount = DoneOrder_Amount alors state=OK, puis cloturer de la table order le dernier order de l atm qui est a l etat validé
            // si DoneOrder_Amount < amount alors state dificit puis mettre error dans l etat de la table order le dernier order de l atm qui est a l etat validé
            // si DoneOrder_Amount > amount alors state execdent puis mettre error dans l etat de la table order le dernier order de l atm qui est a l etat validé
            //CashProvider_Id reccuprer cette valeur depuis la table order 
            //AddedAstatemount  --> changer le nom vers initial amount : reccuperer le total depiis cashstock de state  
            //statebefioreid et after

            State recentSate = _stateRepository.GetAll(elt => elt.Atm_Id == state.Atm_Id).OrderByDescending(e => e.Id).FirstOrDefault();
            int recentTotal = 0;
            int paramTotal = 0;
            if (recentSate.CashStocks.FirstOrDefault().Total.HasValue)
                recentTotal = recentSate.CashStocks.FirstOrDefault().Total.Value;

            if (state.CashStocks.FirstOrDefault().Total.HasValue)
                paramTotal = state.CashStocks.FirstOrDefault().Total.Value;
            if (paramTotal > recentTotal)
            {
                //alimenter la table doneOrder
                DateTime doneOrderDate = DateTime.Now;
                int DoneOrderAmount = paramTotal - recentTotal;
                string atm = state.Atm_Id;
                // OrderState DoneOrderState { get; set; } // 0:OK ; 1: ERROR ; 2: ERROR WITH DEFICIT ; 3: ERROR WITH EXCEDENT ; 4: 
#pragma warning disable CS0219 // La variable 'recentValidOrder' est assignée, mais sa valeur n'est jamais utilisée
                var recentValidOrder = 1;
#pragma warning restore CS0219 // La variable 'recentValidOrder' est assignée, mais sa valeur n'est jamais utilisée


            }

            _stateRepository.Add(state);
            _stateRepository.Save();

        }
        //public void Delete (State _state)
        //{
        //    _stateRepository.Delete(_state);
        //}


    }
}
