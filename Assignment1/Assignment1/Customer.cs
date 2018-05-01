using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment1
{
    // Contains all the methods related to the customer submenu options.
    class Customer
    {
        public void checkBookingFlightStatus()
        {
            // Intitialise for later use.
            SubMenus subMenu = new SubMenus();

            JSON json = new JSON();
            List<Booking> bookingData = json.readBookingData();
            List<Flight> flightData = json.readFlightData();


            Console.Clear();

            bool intTestLoop = true;

            // Test input for booking ID is correct type.
            while (intTestLoop)
            {
                Console.Write("Please enter the booking reference no:");

                // If the input is an int.
                if (int.TryParse(Console.ReadLine(), out int bookingIDTest))
                {
                    // If booking ID does not exist in booking list (i.e. incorrect id entered).
                    if (!bookingData.Any(b => b.bookingID == bookingIDTest))
                    {
                        Console.Write("\nBooking ID is incorrect, please try again.\n\n");
                    }
                    else
                    {
                        intTestLoop = false;
                        bookingID = bookingIDTest;
                    }
                }
                // If the input is NOT an int.
                else
                {
                    Console.Write("\nIncorrect input. Please try again.\n\n");
                }
            }

            // Cycle through the booking where the booking ID matches the one entered by the user.
            foreach(Booking b in bookingData.Where(b => b.bookingID == bookingID))
            {
                flightID = b.flightID;
                customerName = b.customerName;
                bookingStatus = b.bookingStatus;
            }

            foreach (Flight f in flightData.Where(f => f.flightID.ToLower() == flightID.ToLower()))
            {
                flightFrom = f.flightFrom;
                flightTo = f.flightTo;
                flightDateTime = f.flightDateTime;
            }

            // Temporary variable to hold result of converting bookingStatus to something meaningful to user.
            string status;

            // Convert bookingStatus into something meaningful to user.
            if (bookingStatus)
            {
                status = "Confirmed";
            }
            else
            {
                status = "Not Confirmed";
            }

            Console.Write("\n\nYour Booking Details" +
                "\n========================================" +
                "\nReference:\t{0}" +
                "\nStatus:\t\t{1}" +
                "\nName:\t\t{2}" +
                "\nFlight No:\t{3}" +
                "\nFlight From:\t{4}" +
                "\nFlight To:\t{5}" +
                "\nDate:\t\t{6:dd/MM/yyyy}" +
                "\n\nPress any key to return to the menu.",
                bookingID, status, customerName, flightID, flightFrom, flightTo, flightDateTime);

            Console.ReadKey();

            // Return to customer submenu.
            subMenu.customerSubMenu();

        }


        // Properties.
        public string flightID
        {
            get;
            set;
        }

        public string flightFrom
        {
            get;
            set;
        }

        public string flightTo
        {
            get;
            set;
        }
        public DateTime flightDateTime
        {
            get;
            set;
        }
        
        public int bookingID
        {
            get;
            set;
        }

        public string customerName
        {
            get;
            set;
        }

        public bool bookingStatus
        {
            get;
            set;
        }
        
    }
}
