using DATAACCESS.CRUD;
using ENTITIES_POJO;
using Exceptions;
using System;
using System.Collections.Generic;

namespace COREAPI
{
    public class ExitRampManager
    {
        private ExitRampCrud CrudFactory;
        private TerminalManager TerminalMng;
        //private BusCrud BusFactory;
        private TravelManager TravelMng;
        private RouteCrud RouteFactory;

        public ExitRampManager()
        {
            CrudFactory = new ExitRampCrud();
            TerminalMng = new TerminalManager();
            TravelMng = new TravelManager();
            RouteFactory = new RouteCrud();
        }

        public List<ExitRamp> RetrieveAll()
        {
            List<ExitRamp> lt = CrudFactory.RetrieveAll<ExitRamp>();
            foreach (ExitRamp t in lt)
            {
                BuildEntities(t);
            }
            return lt;
        }

        public ExitRamp RetrieveById(ExitRamp exitRamp)
        {
            ExitRamp be = null;

            try
            {
                be = CrudFactory.Retrieve<ExitRamp>(exitRamp);
                if (be != null)
                {
                    BuildEntities(be);
                }
                else
                {
                    // ExitRamp Not Found.
                    throw new BusinessException(28);
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.GetInstance().Process(ex);
            }

            return be;
        }

        public void Create(ExitRamp exitRamp)
        {
            try
            {
                if (!String.IsNullOrEmpty(exitRamp.Name) && exitRamp.Terminal.IdTerminal > 0)
                {
                    CrudFactory.Create(exitRamp);
                }
                else
                {
                    // Both ExitRamp Name and Terminal ID are required.
                    throw new BusinessException(29);
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.GetInstance().Process(ex);
            }
        }

        public void Update(ExitRamp exitRamp)
        {
            ExitRamp be = null;
            try
            {
                be = CrudFactory.Retrieve<ExitRamp>(exitRamp);
                if (be != null)
                {
                    if (!String.IsNullOrEmpty(exitRamp.Name))
                        CrudFactory.Update(exitRamp);
                    else
                    {
                        // Both ExitRamp Name and Terminal ID are required.
                        throw new BusinessException(29);
                    }
                }
                else
                {
                    // ExitRamp Not Found.
                    throw new BusinessException(28);
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.GetInstance().Process(ex);
            }
        }

        public void Delete(ExitRamp exitRamp)
        {
            CrudFactory.Delete(exitRamp);
        }

        public List<ExitRamp> RetrieveByTerminalId(Terminal terminal)
        {
            List<ExitRamp> lt = CrudFactory.RetrieveByTerminalId<ExitRamp>(terminal);
            foreach (ExitRamp t in lt)
            {
                BuildEntities(t);
            }
            return lt;
        }

        private ExitRamp BuildEntities(ExitRamp pExitRamp)
        {
            if (pExitRamp.Terminal.IdTerminal > 0)
            {
                pExitRamp.Terminal = TerminalMng.RetrieveById(pExitRamp.Terminal);
            }
            if (pExitRamp.Route.IdRoute > 0)
            {
                pExitRamp.Route = RouteFactory.Retrieve<Route>(pExitRamp.Route);

                if (pExitRamp.Route != null)
                {
                    pExitRamp.TravelList = TravelMng.RetrieveTravelByRoute(pExitRamp.Route);
                    var nextTravels = TravelMng.FindNextTravels(pExitRamp.TravelList);
                    if (nextTravels.Count > 0)
                    {
                        pExitRamp.CurrentTravel = nextTravels?[0];
                        if (nextTravels.Count > 1)
                        {
                            pExitRamp.NextTravel = nextTravels?[1];
                        }
                    }
                }
            }
            return pExitRamp;
        }
    }
}
