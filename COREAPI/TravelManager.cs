using DATAACCESS.CRUD;
using ENTITIES_POJO;
using Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace COREAPI
{
    public class TravelManager
    {
        private TravelCrud CrudFactory;

        public TravelManager()
        {
            CrudFactory = new TravelCrud();
        }

        public void Create(Travel travel)
        {
            try
            {
                VerifyIsValidSchedule(travel);
                ValidateIsAvailableSchedule(travel.Schedule);
                CrudFactory.Create(travel);
            }
            catch (Exception bex)
            {
                ExceptionManager.GetInstance().Process(bex);
            }
        }

        private void VerifyIsValidSchedule(Travel travel)
        {
            travel.Schedule.Route = travel.Route;
            travel.Schedule.Day = new ScheduleManager().GetDayByName(travel.Schedule);
            Schedule schedule = new ScheduleCrud().RetrieveScheduleByDayHour<Schedule>(travel.Schedule);

            if (schedule == null)
            {
                throw new BusinessException(63);
            }
            else
            {
                travel.Schedule = schedule;
            }        
        }

        private void ValidateIsAvailableSchedule(Schedule schedule)
        {
            if (schedule.Available == 0)
            {
                throw new BusinessException(64);
            }
        }

        public void Update(Travel travel)
        {
            try
            {
                Schedule ScheduleInfoBeforeUpdate = new ScheduleManager().RetrieveById(new Schedule { IdSchedule = travel.Schedule.IdSchedule });
                VerifyIsValidSchedule(travel);
                ValidateNewTravelInfo(travel, ScheduleInfoBeforeUpdate);
                CrudFactory.Update(travel);
            }
            catch (Exception bex)
            {
                ExceptionManager.GetInstance().Process(bex);
            }
        }

        public void ValidateNewTravelInfo(Travel travel, Schedule scheduleInfoBeforeUpdate)
        {
            if (scheduleInfoBeforeUpdate.DepartHour != travel.Schedule.DepartHour || scheduleInfoBeforeUpdate.Day != travel.Schedule.Day)
            {
                ValidateIsAvailableSchedule(travel.Schedule);
            }
        }

        public Travel RetrieveById(int idTravel)
        {
            throw new System.NotImplementedException();
        }

        public List<Travel> RetrieveAll()
        {
            List<Travel> LstTravels = null;

            try
            {
                LstTravels = CrudFactory.RetrieveAll<Travel>();
                GenerateList(LstTravels);
            }
            catch (Exception bex)
            {
                ExceptionManager.GetInstance().Process(bex);
            }

            return LstTravels;
        }

        public void Delete(Travel travel)
        {
            CrudFactory.Delete(travel);
        }

        public List<Travel> RetrieveTravlesByRoute(int idRoute)
        {
            throw new System.NotImplementedException();
        }

        public void GenerateList(List<Travel> lstTravels)
        {
            foreach (Travel travel in lstTravels)
            {
                BuildObjects(travel);
            }
        }

        public void BuildObjects(Travel travel)
        {
            travel.Schedule = new ScheduleCrud().Retrieve<Schedule>(travel.Schedule);
            travel.Bus = new BusCrud().Retrieve<Bus>(travel.Bus);
            travel.Bus.Driver = new DriverCrud().Retrieve<Driver>(travel.Bus.Driver);
            travel.Route = new RouteCrud().Retrieve<Route>(travel.Route);
            travel.Route.RouteCompany = new CompanyCrud().Retrieve<Company>(travel.Route.RouteCompany);

        }

        public List<Travel> RetrieveTravelByRoute(Route route)
        {
            List<Travel> LstTravels = null;

            try
            {
                LstTravels = CrudFactory.RetrieveTravelByRoute<Travel>(route);
                GenerateList(LstTravels);
            }
            catch (Exception bex)
            {
                ExceptionManager.GetInstance().Process(bex);
            }

            return LstTravels;
        }

        public List<Travel> FindNextTravels(List<Travel> pTravelList)
        {
            List<Travel> LstTravels = null;
            List<Travel> auxTravelList= null;

            var utc = DateTime.UtcNow;
            utc = utc.AddHours(-6);
            var day = GetWeekDay(utc);

            LstTravels = FindTravelsTodayAfterCurrentHour(pTravelList, utc);


            if (LstTravels?.Count < 2)
            {
                auxTravelList = FindNextTravels(pTravelList, utc, day);
                if (auxTravelList?.Count > 0)
                {
                    LstTravels.AddRange(auxTravelList);
                }
            }

            return LstTravels;
        }

        private List<Travel> FindTravelsTodayAfterCurrentHour(List<Travel> pTravelList, DateTime date)
        {
            var scheduleMng = new ScheduleManager();
            var day = GetWeekDay(date);
            var minutesOfToday = (date.Hour * 60) + date.Minute;

            List<Travel> filteredList = pTravelList.Where(travel=>( travel.Schedule.Day.Equals(day) && travel.Schedule.DepartHourAsMinutes >= minutesOfToday )).ToList();

            filteredList = filteredList.OrderBy(travel => (travel.Schedule.DepartHourAsMinutes)).ToList();

            return filteredList;
        }

        private List<Travel> FindNextTravels(List<Travel> pTravelList, DateTime date, string pBreakDay)
        {
            date = date.AddDays(1);

            List<Travel> filteredList = null;
            var scheduleMng = new ScheduleManager();
            var day = GetWeekDay(date);

            if (!pBreakDay.Equals(day))
            {
                filteredList = pTravelList.Where(travel => (travel.Schedule.Day.Equals(day))).ToList();
                filteredList = filteredList.OrderBy(travel => (travel.Schedule.DepartHourAsMinutes)).ToList();

                if (filteredList?.Count == 0)
                {
                    FindNextTravels(pTravelList, date, pBreakDay);
                }
            }

            return filteredList;
        }

        private string GetWeekDay(DateTime date)
        {
            var scheduleMng = new ScheduleManager();
            var day = "" + (1+ (int)date.DayOfWeek);
            var fakeSchedule = new Schedule
            {
                Day = day
            };
            day = scheduleMng.GetDayByName(fakeSchedule);

            return day;
        }
    }
}
