using DATAACCESS.CRUD;
using ENTITIES_POJO;
using Exceptions;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Net;
using System.Xml;

namespace COREAPI
{
    public class RouteManager
    {
        private RouteCrud CrudFactory;
        private CompanyCrud CCrud;
        private TerminalCrud TCrud;
        private LocationCrud LCrud;
        private SystemParamCrud SPCrud;

        public RouteManager()
        {
            CrudFactory = new RouteCrud();
            CCrud = new CompanyCrud();
            TCrud = new TerminalCrud();
            LCrud = new LocationCrud();
            SPCrud = new SystemParamCrud();
    }

    public List<Route> RetrieveAll()
        {
            List<Route> lt = CrudFactory.RetrieveAll<Route>();
            foreach (var route in lt)
            {
                route.RouteCompany = CCrud.Retrieve<Company>(route.RouteCompany);
                route.RouteTerminal = TCrud.Retrieve<Terminal>(route.RouteTerminal);
                route.BusStops = LCrud.RetrieveAllByRoute<Location>(route);

            }
            return lt;
        }

        public List<Route> RetrieveAllByTerminal(Terminal terminal)
        {
            List<Route> lt = CrudFactory.RetrieveAllByTerminal<Route>(terminal);
            foreach (var route in lt)
            {
                route.RouteCompany = CCrud.Retrieve<Company>(route.RouteCompany);
                route.RouteTerminal = TCrud.Retrieve<Terminal>(route.RouteTerminal);
                route.BusStops = LCrud.RetrieveAllByRoute<Location>(route);

            }
            return lt;
        }

        public List<Route> RetrieveRoutesByCompany(Company company)
        {
            List<Route> LstRoutes = null;

            try
            {
                LstRoutes = CrudFactory.RetrieveRoutesByCompany<Route>(company);
                foreach (var route in LstRoutes)
                {
                    route.RouteCompany = CCrud.Retrieve<Company>(route.RouteCompany);
                    route.RouteTerminal = TCrud.Retrieve<Terminal>(route.RouteTerminal);
                    route.BusStops = LCrud.RetrieveAllByRoute<Location>(route);

                }
                //GenerateList(LstRoutes);
                return LstRoutes;
            }
            catch (Exception bex)
            {
                ExceptionManager.GetInstance().Process(bex);
            }

            return LstRoutes;
        }

        public void Create(Route route)
        {
            try
            {
                ValidateIsNotExistingRoute(route);
                GetRouteDistanceAndDuration(route);
                CalculateRoutePrice(route);
                Route NewRoute = CrudFactory.CreateRoute(route);
                new LocationCrud().AddLocationToRoute(new Location { IdLocation = route.BusStops[0].IdLocation }, NewRoute);

                for (var i = 1; i < route.BusStops.Count; i++)
                {
                    Location NewLocation = new LocationCrud().CreateLocation(route.BusStops[i]);
                    new LocationCrud().AddLocationToRoute(NewLocation, NewRoute);
                }
            }
            catch (Exception bex)
            {
                ExceptionManager.GetInstance().Process(bex);
            }
        }

        public void Delete(Route route)
        {
            try
            {
                CrudFactory.Delete(route);

            }
            catch (Exception bex)
            {
                ExceptionManager.GetInstance().Process(bex);
            }
        }

        public void Update(Route route)
        {
            try
            {
                
                GetRouteDistanceAndDuration(route);
                CalculateRoutePrice(route);
                CrudFactory.Update(route);
                CrudFactory.DeleteLocationByRoute(route);
                new LocationCrud().AddLocationToRoute(new Location { IdLocation = route.BusStops[0].IdLocation }, route);

                for (var i = 1; i < route.BusStops.Count; i++)
                {
                    Location NewLocation = new LocationCrud().CreateLocation(route.BusStops[i]);
                    new LocationCrud().AddLocationToRoute(NewLocation, route);
                }
            }
            catch (Exception bex)
            {
                ExceptionManager.GetInstance().Process(bex);
            }
        }

        public Route RetrieveById(int idRoute)
        {
            throw new System.NotImplementedException();
        }

        public string ShowBusCurrentLocation(Bus bus)
        {
            throw new System.NotImplementedException();
        }

        public void GenerateList(List<Route> lstRoutes)
        {
            if (lstRoutes.Count > 0)
            {
                foreach (Route route in lstRoutes)
                {
                    BuildObjects(route);
                }
            }
            else
            {
                throw new BusinessException(5);
            }
        }

        public void BuildObjects(Route route)
        {
            route.RouteCompany = new CompanyCrud().Retrieve<Company>(route.RouteCompany);
            //  route.Location = new LocationCrud().Retrieve<Location>(route.Location);
            route.SchedulesList = new ScheduleCrud().RetrieveSchedulesByCompany<Schedule>(route.RouteCompany);
        }

        public void GetRouteDistanceAndDuration(Route route)
        {
            Terminal TerminalLocation = new TerminalManager().RetrieveById(route.RouteTerminal);
            route.BusStops.Insert(0, TerminalLocation.Location);
            var EstimatedTime = 0.0;
            var EstimatedDistance = 0.0;

            for (var i = 0; i < route.BusStops.Count; i++)
            {
                if (i < route.BusStops.Count - 1)
                {
                    string url = @"http://maps.googleapis.com/maps/api/distancematrix/xml?origins=" + route.BusStops[i].Name + "&destinations=" + route.BusStops[i + 1].Name + "&sensor=false";
                    HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                    WebResponse response = request.GetResponse();
                    Stream dataStream = response.GetResponseStream();
                    StreamReader sreader = new StreamReader(dataStream);
                    string responsereader = sreader.ReadToEnd();
                    response.Close();

                    DataSet ds = new DataSet();
                    ds.ReadXml(new XmlTextReader(new StringReader(responsereader)));

                    if (ds.Tables.Count > 0)
                    {
                        if (ds.Tables["element"].Rows[0]["status"].ToString() == "OK")
                        {
                            EstimatedTime = EstimatedTime + GetTimeFormatted(ds.Tables["duration"].Rows[0]["text"].ToString());
                            EstimatedDistance = EstimatedDistance + GetDistanceFormatted(ds.Tables["distance"].Rows[0]["text"].ToString());
                        }
                    }
                }
            }

            route.Distance = Math.Round(EstimatedDistance, 2);
            route.EstimatedTime = EstimatedTime;
        }

        public double GetTimeFormatted(string time)
        {
            var TimeSplitted = time.Split(' ');
            return Convert.ToDouble(TimeSplitted[0]);
        }

        public float GetDistanceFormatted(string distance)
        {
            var DistanceSplitted = distance.Split(' ');
            return float.Parse(DistanceSplitted[0], System.Globalization.CultureInfo.InvariantCulture);
        }

        public void ValidateIsNotExistingRoute(Route route)
        {
            Route NewRoute = CrudFactory.RetrieveByName<Route>(route);

            if (NewRoute != null)
            {
                throw new BusinessException(61);
            }
        }

        public void CalculateRoutePrice(Route route)
        {
            SystemParam Param = SPCrud.GetPricePerKM<SystemParam>();
            route.Price = (route.Distance * Convert.ToDouble(Param.Value));
        }
    }
}
