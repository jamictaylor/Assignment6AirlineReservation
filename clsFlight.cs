using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Assignment6AirlineReservation
{
    internal class clsFlight
    {
        #region private attributes
        /// <summary>
        /// private variable to hold the flightID for each flight
        /// </summary>
        string sFlightID = "";

        /// <summary>
        /// private variable to hold the flightNumber for each flight (as a string)
        /// </summary>
        string sFlightNumber;

        /// <summary>
        /// private variable to hold the aircraft type for each flight
        /// </summary>
        string sAircraftType = "";
        #endregion

        #region public properties
        /// <summary>
        /// public property of sFlightID
        /// </summary>
        public string FlightID { get { return sFlightID; } set { sFlightID = value; } }

        /// <summary>
        /// public property of sFlightNumber
        /// </summary>
        public string FlightNumber { get { return sFlightNumber; } set { sFlightNumber = value; } }

        /// <summary>
        /// public property of sAircraftType
        /// </summary>
        public string AircraftType { get { return sAircraftType; } set { sAircraftType = value; } }
        #endregion

        /// <summary>
        /// override ToString method to be called by the combobox
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            try
            {
                // return the flightNumber and Aircraft type to be displayed in combobox
                return FlightNumber + " - " + AircraftType;
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }
    }
}
