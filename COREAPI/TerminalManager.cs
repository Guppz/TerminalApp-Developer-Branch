using DATAACCESS.CRUD;
using ENTITIES_POJO;
using Exceptions;
using System;
using System.Collections.Generic;

namespace COREAPI
{
    public class TerminalManager
    {
        private TerminalCrud CrudFactory;
        private LocationCrud LocCRUD = new LocationCrud();

        public TerminalManager()
        {
            CrudFactory = new TerminalCrud();
        }

        public List<Terminal> RetrieveAll()
        {
            List<Terminal> lt = CrudFactory.RetrieveAll<Terminal>();
            foreach (Terminal t in lt)
            {
                BuildLocation(t);
            }
            return lt;
        }

        public Terminal RetrieveById(Terminal terminal)
        {
            Terminal be = null;

            try
            {
                be = CrudFactory.Retrieve<Terminal>(terminal);
                if (be != null)
                {
                    BuildLocation(be);
                }
                else
                {
                    // Terminal no encontrada
                    throw new BusinessException(21);
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.GetInstance().Process(ex);
            }

            return be;
        }

        public void Create(Terminal terminal)
        {
            try
            {
                if (!String.IsNullOrEmpty(terminal.Name) && terminal.Location != null)
                {
                    if (terminal.Location.IdLocation > 0)
                    {
                        LocCRUD.Update(terminal.Location);
                    }
                    else
                    {
                        LocCRUD.Create(terminal.Location);
                        terminal.Location = LocCRUD.RetrieveLast<Location>();
                    }
                    CrudFactory.Create(terminal);
                }
                else
                {
                    // Both Terminal Name and Location are required.
                    throw new BusinessException(22);
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.GetInstance().Process(ex);
            }
        }

        public void Update(Terminal terminal)
        {
            Terminal be = null;
            try
            {
                be = CrudFactory.Retrieve<Terminal>(terminal);
                if (be != null)
                {
                    if (!String.IsNullOrEmpty(terminal.Name) && terminal.Location != null)
                        CrudFactory.Update(terminal);
                    else
                    {
                        // Both Terminal Name and Location are required.
                        throw new BusinessException(22);
                    }
                }
                else
                {
                    // Terminal Not Found.
                    throw new BusinessException(21);
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.GetInstance().Process(ex);
            }
        }

        public void Delete(Terminal terminal)
        {
            CrudFactory.Delete(terminal);
        }

        public List<String> RetrieveProfitsReport(Terminal terminal)
        {
            throw new System.NotImplementedException();
        }

        private Terminal BuildLocation(Terminal pTerminal)
        {
            Location loc = null;
            loc = LocCRUD.Retrieve<Location>(pTerminal.Location);

            if (loc != null)
            {
                pTerminal.Location = loc;
            }
            else
            {
                // Location Not Found.
                throw new BusinessException(23);
            }

            return pTerminal;
        }
    }
}
