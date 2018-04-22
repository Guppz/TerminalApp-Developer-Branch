using DATAACCESS.CRUD;
using ENTITIES_POJO;
using Exceptions;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace COREAPI
{
    public class ScheduleManager
    {
        private ScheduleCrud CrudFactory;

        public ScheduleManager()
        {
            CrudFactory = new ScheduleCrud();
        }

        public void Create(Schedule schedule)
        {
            try
            {
                schedule.Day = GetDayByName(schedule);
                CrudFactory.Create(schedule);
            }
            catch (Exception bex)
            {
                ExceptionManager.GetInstance().Process(bex);
            }
        }

        public void Update(Schedule schedule)
        {
            Schedule be = null;
            try
            {
                be = CrudFactory.Retrieve<Schedule>(schedule);
                if (be != null)
                {
                    schedule.Day = GetDayByName(schedule);
                    CrudFactory.Update(schedule);
                }
                else
                {
                    // Schedule Not Found.
                    throw new BusinessException(62);
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.GetInstance().Process(ex);
            }
        }

        public Schedule RetrieveById(Schedule schedule)
        {
            Schedule be = null;

            try
            {
                be = CrudFactory.Retrieve<Schedule> (schedule);
                if (be != null)
                {
                    BuildObjects(be);
                }
                else
                {
                    // Horario no encontrado
                    throw new BusinessException(62);
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.GetInstance().Process(ex);
            }

            return be;
        }

        public List<Schedule> RetrieveAll()
        {
            List<Schedule> LstSchedules = null;

            try
            {
                LstSchedules = CrudFactory.RetrieveAll<Schedule>();
                GenerateList(LstSchedules);
            }
            catch (Exception bex)
            {
                ExceptionManager.GetInstance().Process(bex);
            }

            return LstSchedules;
        }

        public void Delete(Schedule schedule)
        {
            CrudFactory.Delete(schedule);
        }

        public List<Schedule> RetrieveSchedulesByCompany(int idCompany)
        {
            throw new System.NotImplementedException();
        }

        public Schedule RetrieveScheduleByDayHour(Schedule schedule)
        {
            schedule.Day = GetDayByName(schedule);
            Schedule NewSchedule = CrudFactory.RetrieveScheduleByDayHour<Schedule>(schedule);

            if(NewSchedule != null)
            {
                BuildObjects(NewSchedule);
            }
  
            return NewSchedule;
        }

        public string GetDayByName(Schedule schedule)
        {
            switch (Int32.Parse(schedule.Day))
            {
                case 1:
                    return "Domingo";

                case 2:
                    return "Lunes";

                case 3:
                    return "Martes";

                case 4:
                    return "Miércoles";

                case 5:
                    return "Jueves";

                case 6:
                    return "Viernes";

                case 7:
                    return "Sábado";

                default:
                    return "";
            }
        }

        public void GenerateList(List<Schedule> lstSchedules)
        {
            foreach (Schedule schedule in lstSchedules)
            {
                BuildObjects(schedule);
            }
        }

        public void BuildObjects(Schedule schedule)
        {
            schedule.Route = new RouteCrud().Retrieve<Route>(schedule.Route);
            schedule.Route.RouteTerminal = new TerminalCrud().Retrieve<Terminal>(schedule.Route.RouteTerminal);
            schedule.Route.RouteCompany = new CompanyCrud().Retrieve<Company>(schedule.Route.RouteCompany);
        }
    }
}
