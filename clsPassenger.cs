using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Assignment6AirlineReservation
{
    internal class clsPassenger
    {
        #region private attributes
        /// <summary>
        /// private variable to hold the passengerID for each passenger on a flight
        /// </summary>
        private string sPassengerID = "";

        /// <summary>
        /// private variable to hold the first name for each passenger on a flight
        /// </summary>
        private string sFirstName = "";

        /// <summary>
        /// private variable to hold the last name for each passenger on a flight
        /// </summary>
        string sLastName = "";

        /// <summary>
        /// private variable to hold teh sean number for each passenger on a flight
        /// </summary>
        private string sSeatNumber = "";
        #endregion

        #region public properties
        /// <summary>
        /// public property of sPassengerID
        /// </summary>
        public string PassengerID
        {
            get { return sPassengerID; }
            set { sPassengerID = value; }
        }

        /// <summary>
        /// public property of sFirstName
        /// </summary>
        public string FirstName
        {
            get { return sFirstName; }
            set { sFirstName = value; }
        }

        /// <summary>
        /// public property of sLastName
        /// </summary>
        public string LastName
        {
            get { return sLastName; }
            set { sLastName = value; }
        }

        /// <summary>
        /// public property of sSeatNumber
        /// </summary>
        public string SeatNumber
        {
            get { return sSeatNumber; }
            set { sSeatNumber = value; }
        }
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
                return FirstName + " " + LastName;
            }
            catch (Exception ex)
            {
                throw new System.Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }
    }
}
