using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;

namespace Assignment6AirlineReservation
{
    /// <summary>
    /// Interaction logic for wndAddPassenger.xaml
    /// </summary>
    public partial class wndAddPassenger : Window
    {
        /// <summary>
        /// class passengerManager
        /// </summary>
        clsPassengerManager PassengerManager;
        
        /// <summary>
        /// class main window
        /// </summary>
        MainWindow MainWindow;
        
        
        /// <summary>
        /// private variable to determine that user did or did not press save
        /// </summary>
        private static bool bDidSave = false;

        /// <summary>
        /// private variable to hold the first name of a new passenger
        /// </summary>
        private static string sNewPassengerFirstName = "";

        /// <summary>
        /// private variable to hold the first name of a new passenger
        /// </summary>
        private static string sNewPassengerLastName = "";

        /// <summary>
        /// pubic variable referencing bDidSave
        /// </summary>
        public static bool DidSave
        {
            get { return bDidSave; }
            set { bDidSave = value; }
        }

        /// <summary>
        /// public variable referencing sNewPassengerFirstName
        /// </summary>
        public static string NewPassengerFirstName
        {
            get { return sNewPassengerFirstName; }
            set { sNewPassengerFirstName = value; }

        }

        /// <summary>
        /// public variable referening sNewPassengerLastName
        /// </summary>
        public static string NewPassengerLastName
        {
            get { return sNewPassengerLastName; }
            set { sNewPassengerLastName = value; }
        }

        public wndAddPassenger()
        {
            InitializeComponent();

           

        }

        /// <summary>
        /// Method called when user closes window
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            try
            {
                this.Hide();
                e.Cancel = true;

                // clear text boxes
                clearTextBoxes();
            }
            catch (Exception ex)
            {
                // top level message to handle the exception
                HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name, MethodInfo.GetCurrentMethod().Name, ex.Message);
            }
        }

        /// <summary>
        /// Method called when user clicks on Save.. closes the window
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // if first and last name have at least one character each
                if(FirstNameTextBox.Text.Length > 0 && LastNameTextBox.Text.Length > 0)
                {             
                    // save first and last name
                    NewPassengerFirstName = FirstNameTextBox.Text;
                    NewPassengerLastName = LastNameTextBox.Text;

                    // set a local boolean variable that signals that the user clicked Save and not cancel
                    DidSave = true;

                    

                    //Hide the window
                    this.Hide();

                    // clear text boxes
                    clearTextBoxes();
                }
                else // error label
                {
                    messageLabel.Content = "Please enter first and last name before saving";
                }            
            }
            catch (Exception ex)
            {
                // top level message to handle the exception
                HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name, MethodInfo.GetCurrentMethod().Name, ex.Message);
            }
        }

        /// <summary>
        /// Method called when user selects Cancel.. window closes
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //Hide the window
                this.Hide();

                // clear text boxes
                clearTextBoxes();
            }
            catch (Exception ex)
            {
                // top level message to handle the exception
                HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name, MethodInfo.GetCurrentMethod().Name, ex.Message);
            }
        }

        /// <summary>
        /// Method called when text in text boxes needs to be emptied
        /// </summary>
        private void clearTextBoxes()
        {
            try
            {
                // empty first name
                FirstNameTextBox.Text = "";

                // empty second name
                LastNameTextBox.Text = "";
                
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// Handle the error
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

        private void FirstNameTextBox_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                //Only allow letters to be entered
                if (!(e.Key >= Key.A && e.Key <= Key.Z))
                {
                    //Allow the user to use the backspace, delete, tab and enter
                    if (!(e.Key == Key.Back || e.Key == Key.Delete || e.Key == Key.Tab || e.Key == Key.Enter))
                    {
                        //No other keys allowed besides numbers, backspace, delete, tab, and enter
                        e.Handled = true;
                    }
                }
            }
            catch (System.Exception ex)
            {
                HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                            MethodInfo.GetCurrentMethod().Name, ex.Message);
            }
        }

        private void LastNameTextBox_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                //Only allow letters to be entered
                if (!(e.Key >= Key.A && e.Key <= Key.Z))
                {
                    //Allow the user to use the backspace, delete, tab and enter
                    if (!(e.Key == Key.Back || e.Key == Key.Delete || e.Key == Key.Tab || e.Key == Key.Enter))
                    {
                        //No other keys allowed besides numbers, backspace, delete, tab, and enter
                        e.Handled = true;
                    }
                }
            }
            catch (System.Exception ex)
            {
                HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                            MethodInfo.GetCurrentMethod().Name, ex.Message);
            }
        }
    }
}
