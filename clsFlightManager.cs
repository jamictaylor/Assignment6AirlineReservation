using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Assignment6AirlineReservation
{
    /// <summary>
    /// Business logic for the flight class
    /// </summary>
    internal class clsFlightManager
    {
        // create an object to reference the list of flights from clsFlight
        public List<clsFlight> flights;

        /// <summary>
        /// Method to get the flights that UI will call up to get flights
        /// </summary>
        public List<clsFlight> GetFlightsManager()
        {
            try
            {
                // instantiate DataAccess Class
                clsDataAccess db = new clsDataAccess();

                // instantiate a dataset to hold the data we get back
                DataSet ds = new DataSet();

                // The number of return values
                int iRet = 0;

                // create initial flight list
                flights = new List<clsFlight>();

                // use the data access class in order to execute a SQL statement
                ds = db.ExecuteSQLStatement(clsSQL.GetFlights(), ref iRet);

                // loop through the dataset. for each Row, create a new clsFlight, fill it up add List of Flights
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    // create a new flight object
                    clsFlight f = new clsFlight();

                    // flightID
                    f.FlightID = dr[0].ToString();

                    // flight Name
                    f.FlightNumber = dr[1].ToString();

                    // aircraft type
                    f.AircraftType = dr[2].ToString();

                    // add to the list of flights
                    flights.Add(f);
                }

                // return this list
                return flights;
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }
    }
}
