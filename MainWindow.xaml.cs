using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Assignment6AirlineReservation
{
    public partial class MainWindow : Window
    {
        /// <summary>
        /// class that displays the add passenger window
        /// </summary>
        wndAddPassenger wndAddPassengerForm;

        /// <summary>
        /// flight manager class
        /// </summary>
        clsFlightManager flightManager;

        /// <summary>
        /// passenger manager class
        /// </summary>
        clsPassengerManager passengerManager;

        /// <summary>
        /// Create an object of type clsDataAccess to access the database
        /// </summary>
        clsDataAccess db;
      
        /// <summary>
        /// class selected flight
        /// </summary>
        clsFlight clsSelectedFlight;
        
        /// <summary>
        /// create a list of type clsPassenger to hold a list of passengers
        /// </summary>
        List<clsPassenger> lstPassenger;

        /// <summary>
        /// Passenger class
        /// </summary>
        clsPassenger Passenger;

        #region private variables
        /// <summary>
        /// private variable to hold change seat mode
        /// </summary>
        private bool bChangeSeatMode = false;

        /// <summary>
        /// private variable to hold add passenger mode
        /// </summary>
        private bool bAddPassengerMode = false;
        #endregion

        #region public attributes
        /// <summary>
        /// references bChangeSeatMode
        /// </summary>
        public bool ChangeSeatMode
        {
            get { return bChangeSeatMode; }
            set { bChangeSeatMode = value; }
        }

        /// <summary>
        /// references bAddPassengerMode
        /// </summary>
        public bool AddPassengerMode
        {
            get { return bAddPassengerMode; }
            set { bAddPassengerMode = value; }
        }
        #endregion

        public MainWindow()
        {
            InitializeComponent();
            Application.Current.ShutdownMode = ShutdownMode.OnMainWindowClose;

            // instantiate a new Add Passenger Window
            wndAddPassengerForm = new wndAddPassenger();

            // instantiate a new flightmanager class
            flightManager = new clsFlightManager();

            // instantiate a new clsDataAccess class
            db = new clsDataAccess();

            // bind flights to its items source property and display in chooseFlightCompbo Box
            chooseFlightComboBox.ItemsSource = flightManager.GetFlightsManager();

            // instantiate a passenger manager class
            passengerManager = new clsPassengerManager();

            // instantiate the list of passengers
            lstPassenger = new List<clsPassenger>(); 

            // start with add passenger disabled until flight is selected
            addPassengerButton.IsEnabled = false;

            // start with passenger box disabled until flight is selected
            choosePassengerComboBox.IsEnabled = false;

            // start with change seat box disabled
            changeSeatButton.IsEnabled = false;

            // delete passenger disabled
            deletePassengerButton.IsEnabled = false; 
        }

        /// <summary>
        /// Method called when a flight is selected
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void chooseFlightComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
                    {
            try
            {
                // clear any messages
                MessageLabel.Content = "";

                // ensure passenger box has no selection
                choosePassengerComboBox.SelectedItem = null;
                    
                //  selected flight assignment
                clsSelectedFlight = (clsFlight)chooseFlightComboBox.SelectedItem;

                // if FLIGHT A380 is selected
                if (clsSelectedFlight.FlightID == "1")
                {
                    // make canvas for A380 visible
                    CanvasA380.Visibility = Visibility.Visible;

                    // hide canvas 767
                    Canvas767.Visibility = Visibility.Collapsed;
                }
                // if flight 767 is selected
                else
                {
                    // hide canvas ea380
                    CanvasA380.Visibility = Visibility.Collapsed;

                    // make canvas for 767 visible
                    Canvas767.Visibility = Visibility.Visible;
                }

                // bind passengeres to its items source property and display in choosePassengerCompbo Box
                choosePassengerComboBox.ItemsSource = passengerManager.GetPassengersManager(clsSelectedFlight.FlightID);

                // create list of passengers for selected flight
                lstPassenger = passengerManager.GetPassengersManager(clsSelectedFlight.FlightID);

                // enable add passenger button
                addPassengerButton.IsEnabled = true;

                // enable passenger combo box
                choosePassengerComboBox.IsEnabled = true;

                // fill passenger seats with passengers from database
                FillPassengerSeats();                
            }
            catch (Exception ex)
            {
                HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                    MethodInfo.GetCurrentMethod().Name, ex.Message);
            }
        }

        /// <summary>
        /// Method called internally to fill the passenger seats with the correct informationa and color
        /// </summary>
        /// <exception cref="Exception"></exception>
        private void FillPassengerSeats()
        {
            try
            {
                // variable to hold seat number
                string sSeatNumber;

                // reset all seats to blue
                if (clsSelectedFlight.FlightID == "1")
                {
                    // color blue if seat is not taken
                    foreach (Label MyLabel in cA380_Seats.Children)
                    {
                        MyLabel.Background = seatEmptyBlue.Background;

                        //Get the seat number
                        sSeatNumber = (string)MyLabel.Content;
                    }
                }
                else if (clsSelectedFlight.FlightID == "2") // flight 2
                {
                    // color blue if seat is not taken
                    foreach (Label MyLabel in c767_Seats.Children)
                    {
                        MyLabel.Background = seatEmptyBlue.Background;
                    }
                }

                // loop through each passenger in the list
                for (int i = 0; i < lstPassenger.Count; i++)
                {
                    // create an object to hold the passenger
                    Passenger = (clsPassenger)lstPassenger[i];
                    
                    // if flight is 1
                    if (clsSelectedFlight.FlightID == "1")
                    {
                        // compare the passengers seat to the labels content
                        foreach (Label MyLabel in cA380_Seats.Children)
                        {
                            //Get the seat number
                            sSeatNumber = (string)MyLabel.Content;
                            
                            // if the seats match, change color to red to show taken
                            if(sSeatNumber == Passenger.SeatNumber)
                            {
                                MyLabel.Background = seatTakenRed.Background;
                            }
                        }
                    }
                    else if (clsSelectedFlight.FlightID == "2") // if flight 2
                    {
                        // compare the passengers seat to the labels content
                        foreach (Label MyLabel in c767_Seats.Children)
                        {
                            //Get the seat number
                            sSeatNumber = (string)MyLabel.Content;

                            // if the seats match, change color to red to show taken
                            if (sSeatNumber == Passenger.SeatNumber)
                            {
                                MyLabel.Background = seatTakenRed.Background;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// Method called when the add passenger button is clicked
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void addPassengerButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // clear any messages
                MessageLabel.Content = "";

                // Hide the main window
                this.Hide();

                // Show the add passenger form and wait for confirmation of save
                wndAddPassengerForm.ShowDialog();

                // Show the main form
                this.Show();

                // erase any previous passenger selections
                choosePassengerComboBox.Text = "";

                // once screen returns, if passenger did save:
                if (wndAddPassenger.DidSave == true)
                {
                    // give user instructions
                    MessageLabel.Content = "Select a seat for " + wndAddPassenger.NewPassengerFirstName + " " + wndAddPassenger.NewPassengerLastName + " to save passenger and seat for flight.";

                    // rebuild list
                    lstPassenger = passengerManager.GetPassengersManager(clsSelectedFlight.FlightID);

                    // reset passenger seats so there is no selection
                    FillPassengerSeats();

                    // set to add passenger mode as true
                    AddPassengerMode = true;

                    // ensure only option is a seat click
                    addPassengerButton.IsEnabled = false;
                    deletePassengerButton.IsEnabled = false;
                    changeSeatButton.IsEnabled = false;
                    choosePassengerComboBox.IsEnabled = false;
                    chooseFlightComboBox.IsEnabled = false;
                    
                }
                else // if new passenger didn't save
                {
                    MessageLabel.Content = "Passenger didn't save, enter passenger again and press save";
                }
                
            }
            catch (Exception ex)
            {
                // top level message to handle the exception
                HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name, MethodInfo.GetCurrentMethod().Name, ex.Message);
            }
        }

        /// <summary>
        /// Method called when a seat is clicked on
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Seat_Click(object sender, MouseButtonEventArgs e)
        {
            try
            {
                // clear any messages
                MessageLabel.Content = "";

                // Get the label that the user clicked
                Label MyLabel = (Label)sender;

                // variable to hold the seat number
                string sSeatNumber;

                // regular seat selection, no mode
                if (bAddPassengerMode == false && bChangeSeatMode == false)
                {
                    // check to see if the seat background is red to display in combo box
                    if(MyLabel.Background == seatTakenRed.Background)
                    {
                        // Turn the seat Green
                        MyLabel.Background = seatSelectedGreen.Background;

                        // Get the seat number text
                        sSeatNumber = (string)MyLabel.Content;

                        // loop through the items in the combo box
                        for (int i = 0; i < choosePassengerComboBox.Items.Count; i++)
                        {
                            // Extract the passenger from the combo box
                            Passenger = (clsPassenger)choosePassengerComboBox.Items[i];

                            // if the seat number matches then select the passenger in the combo box
                            if (sSeatNumber == Passenger.SeatNumber)
                            {
                                // Display selected passenger in combo box
                                choosePassengerComboBox.SelectedItem = Passenger;
                            }
                        }
                    }
                    // if seat is embty, just change color
                    if (MyLabel.Background == seatEmptyBlue.Background) 
                    {
                        // erase any previous selection
                        FillPassengerSeats();

                        // empty selected passenger combo box
                        choosePassengerComboBox.SelectedItem = null;

                        // Turn the seat Green
                        MyLabel.Background = seatSelectedGreen.Background;

                        // Get the seat number text
                        sSeatNumber = (string)MyLabel.Content;
                    }

                    
                }
                // If in add passenger mode
                if (bAddPassengerMode == true)
                {
                    // get validated first and last names from the add passenger window
                    string firstName = wndAddPassenger.NewPassengerFirstName;
                    string lastName = wndAddPassenger.NewPassengerLastName;

                    // change seat color to taken
                    MyLabel.Background = seatTakenRed.Background;

                    // insert into passenger table and link table
                    passengerManager.InsertPassenger(wndAddPassenger.NewPassengerFirstName, wndAddPassenger.NewPassengerLastName, clsSelectedFlight.FlightID, MyLabel.Content.ToString());
                    
                    // change DidSave variable back to false
                    wndAddPassenger.DidSave = false;

                    // change add passenger mode to false
                    bAddPassengerMode = false;

                    // bind passengeres to its items source property and display in choosePassengerCompbo Box
                    choosePassengerComboBox.ItemsSource = passengerManager.GetPassengersManager(clsSelectedFlight.FlightID);

                    // rebuild list
                    lstPassenger = passengerManager.GetPassengersManager(clsSelectedFlight.FlightID);

                    // confirmation 
                    MessageLabel.Content = "Your seat has been saved";

                    // enable buttons
                    EnableButtons();

                }
                // if in change seat mode
                if (bChangeSeatMode == true)
                {
                    if (MyLabel.Background == seatEmptyBlue.Background) // seat not taken
                    {
                        // Turn the seat Green
                        MyLabel.Background = seatSelectedGreen.Background;

                        // Get the seat number text
                        sSeatNumber = (string)MyLabel.Content;

                        // object to hold the passenger
                        clsPassenger Passenger = new clsPassenger();

                        // assign the selected item to the passenger object
                        Passenger = (clsPassenger)choosePassengerComboBox.SelectedItem;

                        // update link table with new seat number
                        passengerManager.UpdatePassenger(clsSelectedFlight.FlightID, Passenger.PassengerID, sSeatNumber);

                        // rerun the combo box to reflect changes
                        choosePassengerComboBox.ItemsSource = passengerManager.GetPassengersManager(clsSelectedFlight.FlightID);

                        // rebuild list
                        lstPassenger = passengerManager.GetPassengersManager(clsSelectedFlight.FlightID);

                        // refill the flight seat colors to reflect the change
                        FillPassengerSeats();

                        // clear any messages
                        MessageLabel.Content = "Passenger " + Passenger.FirstName + " " + Passenger.LastName + "'s seat has been changed.";

                        // set boolean to false
                        bChangeSeatMode = false;
                        
                        // enable buttons again
                        EnableButtons();
                    }
                    else
                    {
                        MessageLabel.Content = "Please select a blue seat.";
                    }
                }
            }
            catch (Exception ex)
            {
                // top level message to handle the exception
                HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name, MethodInfo.GetCurrentMethod().Name, ex.Message);
            }
        }

        /// <summary>
        /// method called when user selects the Change Seat button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void changeSeatButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // clear any messages
                MessageLabel.Content = "";

                if (choosePassengerComboBox.SelectedItem == null)
                {
                    MessageLabel.Content = "Please select a passenger first";
                }
                else // if there is a passenger selected
                {
                    // disable other buttons.
                    addPassengerButton.IsEnabled = false;
                    deletePassengerButton.IsEnabled = false;
                    chooseFlightComboBox.IsEnabled = false;
                    choosePassengerComboBox.IsEnabled = false;

                    // change seat mode to true
                    bChangeSeatMode = true;

                    // message
                    MessageLabel.Content = "Please select a new seat.";

                }
            }
            catch (Exception ex)
            {
                // top level message to handle the exception
                HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name, MethodInfo.GetCurrentMethod().Name, ex.Message);
            }
        }

        /// <summary>
        /// Method called when user selects the delete passegner button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void deletePassengerButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // clear any messages
                MessageLabel.Content = "";

                if (choosePassengerComboBox != null)
                {
                    // object to hold the passenger
                    clsPassenger Passenger = new clsPassenger();

                    // assign the selected item to the passenger object
                    Passenger = (clsPassenger)choosePassengerComboBox.SelectedItem;

                    // remove from link and passenger tables
                    passengerManager.DeletePassenger(Passenger.PassengerID);

                    // rerun the combo box to reflect changes
                    choosePassengerComboBox.ItemsSource = passengerManager.GetPassengersManager(clsSelectedFlight.FlightID);

                    // rebuild list
                    lstPassenger = passengerManager.GetPassengersManager(clsSelectedFlight.FlightID);

                    // refill the flight seat colors to reflect the change
                    FillPassengerSeats();

                    // clear any messages
                    MessageLabel.Content = "Passenger " + Passenger.FirstName + " " + Passenger.LastName + " has been deleted.";

                    // enable buttons
                    EnableButtons();
                }
                else
                {
                    MessageLabel.Content = "Select a passenger or passengers seat to delete passenger";
                }
                
            }
            catch (Exception ex)
            {
                // top level message to handle the exception
                HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name, MethodInfo.GetCurrentMethod().Name, ex.Message);
            }
        }

        /// <summary>
        ///  method called when user selects a passenger from the combo box
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void choosePassengerComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                // clear any messages
                MessageLabel.Content = "";  
            
                // first, fill seats to make sure seating colors are up to date and reset
                FillPassengerSeats();

                // variable to hold the selected item
                Passenger = (clsPassenger)choosePassengerComboBox.SelectedItem;

                // variable to hold the seat number
                string sSeatNumber;

                // if an item in passenger combo box has been selected and combo box is populated
                if (choosePassengerComboBox.SelectedItem != null)
                {
                    // if flight is 1
                    if (clsSelectedFlight.FlightID == "1")
                    {
                        // compare the passengers seat to the labels content
                        foreach (Label MyLabel in cA380_Seats.Children)
                        {
                            //Get the seat number
                            sSeatNumber = (string)MyLabel.Content;

                            // if the seats match, change color to red to show taken
                            if (sSeatNumber == Passenger.SeatNumber)
                            {
                                // change passengers selected seat to green
                                MyLabel.Background = seatSelectedGreen.Background;

                                // update label to passenger seat number
                                passengersSeatBoxLabel.Content = sSeatNumber;
                            }
                        }
                    }
                    else if (clsSelectedFlight.FlightID == "2") // if flight 2
                    {
                        // compare the passengers seat to the labels content
                        foreach (Label MyLabel in c767_Seats.Children)
                        {
                            //Get the seat number
                            sSeatNumber = (string)MyLabel.Content;

                            // if the seats match, change color to red to show taken
                            if (sSeatNumber == Passenger.SeatNumber)
                            {
                                // change passengers selected seat to green
                                MyLabel.Background = seatSelectedGreen.Background;

                                // update label to passenger seat number
                                passengersSeatBoxLabel.Content = sSeatNumber;
                            }
                        }
                    }      
                }

                // enable buttons to change seat, delete passenger or add passenger
                changeSeatButton.IsEnabled = true;
                deletePassengerButton.IsEnabled = true;
                addPassengerButton.IsEnabled = true;
            }
            catch (Exception ex)
            {
                // top level message to handle the exception
                HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name, MethodInfo.GetCurrentMethod().Name, ex.Message);
            }
        }

        private void EnableButtons()
        {
            try
            {
                // enable all buttons.  
                addPassengerButton.IsEnabled = true;
                deletePassengerButton.IsEnabled = true;
                choosePassengerComboBox.IsEnabled = true;
                Canvas767.IsEnabled = true;
                CanvasA380.IsEnabled = true;
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// handle error method to be called if a try is missed
        /// </summary>
        /// <param name="sClass"></param>
        /// <param name="sMethod"></param>
        /// <param name="sMessage"></param>
        public void HandleError(string sClass, string sMethod, string sMessage)
        {
            try
            {
                // would write to a file or database here
                MessageBox.Show(sClass + "." + sMethod + " -> " + sMessage);
            }
            catch (Exception ex)
            {
                System.IO.File.AppendAllText("C:\\Error.txt", Environment.NewLine + "HandleError Exception: " + ex.Message);
            }
        }

        
    }

}

