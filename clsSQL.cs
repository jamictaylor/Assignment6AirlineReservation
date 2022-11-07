using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;


namespace Assignment6AirlineReservation
{
    internal class clsSQL
    {
        /// <summary>
        /// Method to be called to execute the SQL statement to select flightID, flightNumber & Aircraft type
        /// </summary>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public static string GetFlights()
        {
            try
            {
                // Get the flight
                string sSQL = "SELECT Flight_ID, Flight_Number, Aircraft_Type FROM FLIGHT";
                return sSQL;
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "."
                                    + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// Method to be be called to execute the SQL statement to select PassengerID, full name and seat number
        /// </summary>
        /// <param name="sFlightID"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public static string GetPassengers(string sFlightID)
        {
            try
            {
                //Get the passenger ID, First Name, Last name and seat number from database
                string sSQL = "SELECT PASSENGER.Passenger_ID, First_Name, Last_Name, Seat_Number " +
                                "FROM FLIGHT_PASSENGER_LINK, FLIGHT, PASSENGER " +
                                "WHERE FLIGHT.FLIGHT_ID = FLIGHT_PASSENGER_LINK.FLIGHT_ID AND " +
                                "FLIGHT_PASSENGER_LINK.PASSENGER_ID = PASSENGER.PASSENGER_ID AND " +
                                "FLIGHT.FLIGHT_ID = " + sFlightID;

                return sSQL;
            }
            catch (Exception ex)
            {

                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                                    MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }



        /// <summary>
        /// Method to be called to execute the SQL statement to insert into Link Table
        /// </summary>
        /// <param name="sFlightID"></param>
        /// <param name="sPassengerId"></param>
        /// <param name="sSeatNumber"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public static string InsertLinkTable(string sFlightID, string sPassengerId, string sSeatNumber)
        {
            try
            {
                //Get the passenger ID, First Name, Last name and seat number from database
                string sSQL = "INSERT INTO Flight_Passenger_Link(Flight_ID, Passenger_ID, Seat_Number) " + 
                    "VALUES( " + sFlightID + " ," + sPassengerId + " , " + sSeatNumber + ")";

                return sSQL;
            }
            catch (Exception ex)
            {

                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                                    MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        ///  Method to be called to execute the SQL statement to delete a passenger
        /// </summary>
        /// <param name="sPassengerID"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public static string DeletePassenger(string sPassengerID)
        {
            try
            {
                string sSQL = "Delete FROM PASSENGER WHERE PASSENGER_ID = " + sPassengerID;

                return sSQL;

            }
            catch (Exception ex)
            {

                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                                    MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        ///  Method to be called to execute the SQL statement to delete a passenger
        /// </summary>
        /// <param name="sPassengerID"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public static string DeletePassengerLink(string sPassengerID)
        {
            try
            {
                string sSQL = "Delete FROM Flight_Passenger_Link WHERE PASSENGER_ID = " + sPassengerID;

                return sSQL;

            }
            catch (Exception ex)
            {

                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                                    MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// SQL statement to update the seat numbers _ NEW UNUSED
        /// </summary>
        /// <param name="sFlightID"></param>
        /// <param name="sPassengerId"></param>
        /// <param name="sSeatNumber"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public static string UpdateLinkTable(string sFlightID, string sPassengerId, string sSeatNumber)
        {
            try
            {
                //Get the passenger ID, First Name, Last name and seat number from database
                string sSQL = "UPDATE FLIGHT_PASSENGER_LINK " +
                                "SET Seat_Number = '" + sSeatNumber + "' " +
                                "WHERE FLIGHT_ID = "+ sFlightID + " AND PASSENGER_ID = " + sPassengerId;

                return sSQL;
            }
            catch (Exception ex)
            {

                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                                    MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// SQL method to insert new passenger into the passenger table - NEW UNUSED
        /// </summary>
        /// <param name="FirstName"></param>
        /// <param name="LastName"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public static string InsertPassenger(string FirstName, string LastName)
        {
            try
            {
                //Get the passenger ID, First Name, Last name and seat number from database
                string sSQL = "INSERT INTO PASSENGER(First_Name, Last_Name) " +
                        "VALUES('" + FirstName + "','" + LastName + "')";

                return sSQL;
            }
            catch (Exception ex)
            {

                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                                    MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// SQL statement to delete a passenger from the passenger link table
        /// </summary>
        /// <param name="sFlightID"></param>
        /// <param name="PassengerId"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public static string DeletePassengerLink(string sFlightID, string PassengerId)
        {
            try
            {
                //Get the passenger ID, First Name, Last name and seat number from database
                string sSQL = "Delete FROM FLIGHT_PASSENGER_LINK " +
                                 "WHERE FLIGHT_ID = " + sFlightID + " AND " +
                                 "PASSENGER_ID = " + PassengerId;

                return sSQL;
            }
            catch (Exception ex)
            {

                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                                    MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// SQL statement to get max passengerID 
        /// </summary>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public static string GetMaxPassengerID()
        {
            try
            {
                string sSQL = "SELECT MAX(Passenger_ID)" +
                                "FROM Passenger";

                return sSQL;
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "."
                                    + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }
    }
}
