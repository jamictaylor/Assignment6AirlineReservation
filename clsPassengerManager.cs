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
    /// Business logic for the clsPassenger class
    /// </summary>
    internal class clsPassengerManager
    {
        // create an object to reference the list of passengers from clsPassengers
        public List<clsPassenger> passengers;

        /// <summary>
        /// create an object of type clsDataAccess to access the database
        /// </summary>
        clsDataAccess db;

        /// <summary>
        /// passenger window class
        /// </summary>
        wndAddPassenger AddPassenger;

        /// <summary>
        /// Method to get the passengers that UI will call up 
        /// </summary>
        /// <param name="sFlightID"></param>
        /// <returns></returns>
        public List<clsPassenger> GetPassengersManager(string sFlightID)
        {
            try
            {
                // instantiate a new DataAccess Class
                db = new clsDataAccess();

                // instantiate a dataset to hold the data we get back
                DataSet ds = new DataSet();

                // The number of return values
                int iRet = 0;

                // create passengers list
                passengers = new List<clsPassenger>();

                // use the data access class in order to execure a SQL statement for passengers
                ds = db.ExecuteSQLStatement(clsSQL.GetPassengers(sFlightID), ref iRet);

                // loop through the dataset. For each Row, create a new clsPassenger and add it to the passengers list
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    // create a new passenger object
                    clsPassenger passenger = new clsPassenger();

                    // passenger ID
                    passenger.PassengerID = dr[0].ToString();

                    // passenger first name
                    passenger.FirstName = dr[1].ToString();

                    // passenger last name
                    passenger.LastName = dr[2].ToString();

                    passenger.SeatNumber = dr[3].ToString();

                    // add the passenger to the list of passengers
                    passengers.Add(passenger);
                }
                // return list of passengers
                return passengers;
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// method to insert a passenger into the database
        /// </summary>
        /// <param name="firstName"></param>
        /// <param name="lastName"></param>
        /// <param name="flightID"></param>
        /// <param name="seatNumber"></param>
        /// <exception cref="Exception"></exception>
        public void InsertPassenger(string firstName, string lastName, string flightID, string seatNumber)
        {
            try
            {
                // instantiate a new DataAccess Class
                db = new clsDataAccess();

                // Number of values affected by the non-query statement    
                int iRet = 0;

                // execute the non-query to insert invoice into Invoice table
                iRet = db.ExecuteNonQuery(clsSQL.InsertPassenger(firstName, lastName));

                // If row was successfully added, get max row
                if (iRet > 0)
                {
                    // variable to hold max passenger num
                    string maxPassengerNum;
                    
                    // Query the max invoice number from the invoice table
                    maxPassengerNum = db.ExecuteScalarSQL(clsSQL.GetMaxPassengerID());

                    // Number of values affected by the non-query statement    
                    int iLinkRet = 0;

                    // insert into Flight_Passenger_Link table using passenger ID
                    iLinkRet = db.ExecuteNonQuery(clsSQL.InsertLinkTable(flightID, maxPassengerNum, seatNumber));
                }

                // confirmation message
               // MainWindow.MessageLabe
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }
    
        /// <summary>
        /// method to delete a passenger from the database
        /// </summary>
        /// <param name="PassengerID"></param>
        public void DeletePassenger(string PassengerID)
        {
            clsDataAccess db = new clsDataAccess();

            // number of rows deleted
            int iDelLink;

            // delete and re-add invoice items
            iDelLink = db.ExecuteNonQuery(clsSQL.DeletePassengerLink(PassengerID));

            // number of rows deleted
            int iDel;

            // delete and re-add invoice items
            iDel = db.ExecuteNonQuery(clsSQL.DeletePassenger(PassengerID));

        }

        public void UpdatePassenger(string FlightID, string PassengerID, string SeatNumber)
        {
            clsDataAccess db = new clsDataAccess();

            // number of rows deleted
            int iDelLink;

            // delete and re-add invoice items
            iDelLink = db.ExecuteNonQuery(clsSQL.UpdateLinkTable(FlightID,PassengerID, SeatNumber));
        }
    }
}
