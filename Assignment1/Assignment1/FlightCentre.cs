using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment1
{
    // Contains all the methods related to the flight centre (agent) submenu options.
    class FlightCentre
    {
        // Displays all FUTURE flights and allows the user to make a booking for a specific future flight.
        public void displayScheduledFlightsAgent()
        {
            // Initialise these for later use.
            SubMenus subMenu = new SubMenus();

            JSON json = new JSON();
            List<Flight> flightData = json.readFlightData();
            List<Booking> bookingData = json.readBookingData();


            Console.Clear();

            // If no flight schedules exist.
            if ((flightData != null) && (!flightData.Any()))
            {

                Console.Write("No flight schedules exist." +
                    "\n\nPress any key to return to the menu");
                Console.ReadKey();

                // Return to flight centre submenu.
                subMenu.agentSubMenu();
            }

            else
            {
                // Create temporary list to hold future flights data.
                List<Flight> tempFlightData = new List<Flight>();

                foreach (Flight f in flightData)
                {
                    // Check if time of flight is later than current time.
                    if (DateTime.Compare(DateTime.Now, f.flightDateTime) < 0)
                    {
                        tempFlightData.Add(f);
                    }
                }

                // If there are FUTURE flights.
                if (tempFlightData.Count > 0)
                {

                    Console.Write("Flights\n" +
                        "_______________________________________________________________________________________________________________________\n" +
                        "{0,-10}" + "{1,-15}" + "{2,-30}" + "{3,-12}" + "{4,-10}" + "{5,-12}" + "{6,-10}\n" +
                        "_______________________________________________________________________________________________________________________\n",
                        "ID", "Flight Type", "Flight Details", "Date", "Time", "Price ($)", "Availability");


                    // If no bookings exist in bookings.json file.
                    if ((bookingData != null) && (!bookingData.Any()))
                    {
                        foreach (Flight f in tempFlightData)
                        {
                            totalBookings = 0;
                            flightAvailability = true;

                            // Format list of flight schedules for displaying.
                            Console.Write("{0,-10}" + "{1,-15}" + "{2,-30}" + "{3,-12:dd/MM/yyyy}" + "{4,-10:HH:mm}" + "{5,-10}" + "{6,-10}" + "{7,-10}" + "{8,-10}\n" +
                            "_______________________________________________________________________________________________________________________\n",
                            f.flightID, f.flightType, f.flightFrom + " - " + f.flightTo, f.flightDateTime, f.flightDateTime, f.ticketPrice, f.capacity, totalBookings, flightAvailability);

                        }

                    }

                    // If bookings exist in bookings.json file.
                    else
                    {
                        foreach (Flight f in tempFlightData)
                        {
                            foreach (Booking b in bookingData)
                            {
                                // Count the total number of tickets already booked for the specific flightID.
                                totalBookings = bookingData.Where(x => x.flightID.ToLower() == f.flightID.ToLower()).Sum(x => x.numberOfTickets);
                                // If flight ID is NOT found in any bookings (i.e. bookings = 0)
                                if (totalBookings == 0)
                                {
                                    flightAvailability = true;
                                }
                            }

                            // If seats are still available for the selected flight.
                            if (totalBookings < f.capacity)
                            {
                                flightAvailability = true;
                            }
                            // If all seats for the selected flight are already booked.
                            else
                            {
                                flightAvailability = false;
                            }

                            // Format list of flight schedules for displaying.
                            Console.Write("{0,-10}" + "{1,-15}" + "{2,-30}" + "{3,-12:dd/MM/yyyy}" + "{4,-10:HH:mm}" + "{5,-12}" + "{6,-10}\n" +
                            "_______________________________________________________________________________________________________________________\n",
                            f.flightID, f.flightType, f.flightFrom + " - " + f.flightTo, f.flightDateTime, f.flightDateTime, f.ticketPrice, flightAvailability);

                        }
                    }
                    
                    Console.Write("\nEnter the flight ID to book ticket:");
                    flightID = Console.ReadLine();


                    // If flight ID exists in temporary list of flight schedules.
                    if (tempFlightData.Exists(f => f.flightID.ToLower() == flightID.ToLower()))
                    {
                      
                        // If no bookings exist in bookings.json file.
                        if ((bookingData != null) && (!bookingData.Any()))
                        {
                            flightAvailability = true;
                        }

                        // If bookings exist in bookings.json file.
                        else
                        {
                            foreach (Booking b in bookingData)
                            {
                                // Count the total number of tickets already booked for the specific flightID.
                                totalBookings = bookingData.Where(x => x.flightID == flightID).Sum(x => x.numberOfTickets);
                                // If flight ID is NOT found in any bookings (i.e. bookings = 0)
                                if (totalBookings == 0)
                                {
                                    flightAvailability = true;
                                }
                            }

                            // Check if the selected flight is fully booked.
                            foreach (Flight f in flightData)
                            {
                                if (f.flightID.ToLower() == flightID.ToLower())
                                {
                                    // If seats are still available for the selected flight.
                                    if (totalBookings < f.capacity)
                                    {
                                        flightAvailability = true;
                                    }
                                    // If all seats for the selected flight are already booked.
                                    else
                                    {
                                        flightAvailability = false;
                                    }
                                }
                            }

                        }

                        // If flight is available for booking.
                        if (flightAvailability)
                        {
                            Console.Write("\nBookings are available for this flight.\n" +
                                "Please enter the following details...\n");

                            Console.Write("\nName of ticket agency:");

                            ticketAgent = Console.ReadLine();

                            Console.Write("\nCustomer's name:");

                            customerName = Console.ReadLine();

                            Console.Write("\nCustomer's phone number:");

                            customerPhone = Console.ReadLine();

                            bool intTestLoop = true;

                            foreach (Flight f in tempFlightData)
                            {
                                if (f.flightID.ToLower() == flightID.ToLower())
                                {
                                    // Test input to check if numberOfTickets is correct type.
                                    while (intTestLoop)
                                    {
                                        Console.Write("\nHow many tickets are being booked:");
 
                                        // If the input is an int.
                                        if (int.TryParse(Console.ReadLine(), out int numberOfTicketsTest))
                                        {

                                            // If the booking doesn't exceed the maximum capacity of the flight AND is greater than 0
                                            if ((numberOfTicketsTest <= (f.capacity - totalBookings)) && (numberOfTicketsTest > 0))
                                            {
                                                intTestLoop = false;
                                                numberOfTickets = numberOfTicketsTest;
                                            }
                                            else
                                            {
                                                Console.Write("Invalid amount. Number of tickets exceeds the capacity of the flight.");
                                            }
                                        }
                                        // If the input is NOT an int.
                                        else
                                        {
                                            Console.Write("Incorrect input. Please enter a whole number.");
                                        }
                                    }

                                    // Calculate the cost of the booking.
                                    bookingPrice = numberOfTickets * f.ticketPrice;

                                }
                            }

                            // Base the booking ID on the number of entries in the bookings.json file.
                            bookingID = bookingData.Count + 1;
                            
                            bookingData.Add(new Booking
                            {
                                flightID = flightID,
                                bookingID = bookingID,
                                customerName = customerName,
                                customerPhone = customerPhone,
                                numberOfTickets = numberOfTickets,
                                bookingPrice = bookingPrice,
                                ticketAgent = ticketAgent,
                                bookingStatus = false // False means 'not confirmed'.

                            });

                            json.saveBookingData(bookingData);


                            Console.Write("\n\nYour booking ID is {0}." +
                                "\nPress any key to return to the menu.", bookingID);
                            Console.ReadKey();

                            // Return to flight centre submenu.
                            subMenu.agentSubMenu();
                        }
                        // If flight is NOT available for booking.
                        else
                        {
                            Console.Write("\nNo bookings are able to be made for this flight.\n" +
                                "Press any key to return to the menu.");
                            Console.ReadKey();

                            // Return to flight centre submenu.
                            subMenu.agentSubMenu();
                        }

                    }
                    // If flight ID DOESN'T exist in flight schedules.
                    else
                    {
                        Console.Write("\nNo flights exist with that flight ID.\n" +
                            "Press any key to return to the menu.");
                        Console.ReadKey();

                        // Return to flight centre submenu.
                        subMenu.agentSubMenu();
                    }

                }
                // If there are NO FUTURE flights.
                else
                {
                    Console.Write("There are no upcoming scheduled flights.");
                }

                Console.Write("\nPress any key to return to the menu.");

                Console.ReadKey();

                // Return to flight centre submenu.
                subMenu.agentSubMenu();

            }

        }

        // Searches for flights that match combination of origin and destination.
        public void searchFlight()
        {
            // Intitialise for later use.
            SubMenus subMenu = new SubMenus();

            JSON json = new JSON();
            List<Flight> flightData = json.readFlightData();


            Console.Clear();

            // If no flight schedules exist.
            if ((flightData != null) && (!flightData.Any()))
            {

                Console.Write("No flight schedules exist." +
                    "\n\nPress any key to return to the menu");
                Console.ReadKey();

                // Return to flight centre submenu.
                subMenu.agentSubMenu();
            }
            else
            {
                // Prompt for user input.
                Console.Write("Search For Flight\n" +
                "=============================================\n\n" +
                "Flight from:");

                flightFrom = Console.ReadLine();

                Console.Write("\nFlight to:");

                flightTo = Console.ReadLine();

                
                // Create temporary list to hold search results.
                List<Flight> searchResults = new List<Flight>();

                foreach (Flight f in flightData)
                {
                    if ((flightFrom.ToLower() == f.flightFrom.ToLower()) && (flightTo.ToLower() == f.flightTo.ToLower()))
                    {
                        {
                            searchResults.Add(f);
                        }
                    }
                }

                // If the search finds matching results.
                if (searchResults.Count > 0)
                {
                    Console.Write("\n____________________________________________________________________________________________________________________\n" +
                        "{0,-10}" + "{1,-15}" + "{2,-30}" + "{3,-15}" + "{4,-10}\n" +
                        "____________________________________________________________________________________________________________________\n",
                        "ID", "Flight Type", "Flight Details", "Date", "Time");

                    foreach (Flight f in searchResults)
                    {
                        Console.Write("{0,-10}" + "{1,-15}" + "{2,-30}" + "{3,-15:dd/MM/yyyy}" + "{4,-10:HH:mm}\n" +
                            "____________________________________________________________________________________________________________________\n",
                            f.flightID, f.flightType, f.flightFrom + " - " + f.flightTo, f.flightDateTime, f.flightDateTime);
                    }
                }
                // If NO matches are found.
                else
                {
                    Console.Write("\nNo flights match your search.");
                }

                Console.Write("\n\nPress any key to return to the menu.");

                Console.ReadKey();

                // Return to flight centre submenu.
                subMenu.agentSubMenu();

            }

        }

        // Checks booking status for a specific booking.
        public void checkBookingStatus()
        {
            // Intitialise for later use.
            SubMenus subMenu = new SubMenus();

            JSON json = new JSON();
            List<Booking> bookingData = json.readBookingData();


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
                        Console.Write("\nBooking ID is incorrect, please try again.");
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
                    Console.Write("\nIncorrect input. Please try again.");
                }
            }

            // Cycle through the booking where the booking ID matches the one entered by the user.
            foreach (Booking b in bookingData.Where(b => b.bookingID == bookingID))
            {
                if (b.bookingStatus)
                {
                    Console.Write("\nYour booking status for {0} with ID {1} is confirmed." + 
                        "\nPress any key to return to the menu.", b.customerName, b.bookingID);
                    Console.ReadKey();

                    // Return to flight centre submenu.
                    subMenu.agentSubMenu();
                }
                else
                {
                    Console.Write("\nYour booking status for {0} with ID {1} is not confirmed." +
                        "\nPress any key to return to the menu.", b.customerName, b.bookingID);
                    Console.ReadKey();

                    // Return to flight centre submenu.
                    subMenu.agentSubMenu();
                }
            }
            
        }

        // Adds a new booking for a specific flight.
        public void addBooking()
        {
            // Intitialise for later use.
            SubMenus subMenu = new SubMenus();
            JSON json = new JSON();

            List<Flight> flightData = json.readFlightData();
            List<Booking> bookingData = json.readBookingData();


            Console.Clear();

            Console.Write("Enter the flight ID to book ticket:");
            flightID = Console.ReadLine();



            // If flight ID exists in flight schedules.
            if (flightData.Exists(f => f.flightID.ToLower() == flightID.ToLower()))
            {
                // If no bookings exist in bookings.json file.
                if ((bookingData != null) && (!bookingData.Any()))
                {
                    flightAvailability = true;
                }

                // If bookings exist in bookings.json file.
                else
                {
                    foreach(Booking b in bookingData)
                    {
                        // Count the total number of tickets already booked for the specific flightID.
                        if(b.flightID.ToLower() == flightID.ToLower())
                        {
                            totalBookings += b.numberOfTickets;
                        }
                        // If flight ID is NOT found in any bookings.
                        else
                        {
                            flightAvailability = true;
                        }
                    }
                    
                    // Check if the selected flight is fully booked.
                    foreach (Flight f in flightData)
                    {
                        if(f.flightID.ToLower() == flightID.ToLower())
                        {
                            // If seats are still available for the selected flight.
                            if (totalBookings < f.capacity)
                            {
                                flightAvailability = true;
                            }
                            // If all seats for the selected flight are already booked.
                            else
                            {
                                flightAvailability = false;
                            }
                        }
                    }

                }

                // If flight is available for booking.
                if (flightAvailability)
                {
                    Console.Write("Bookings are available for this flight.\n" +
                        "Please enter the following details...\n");

                    Console.Write("\nName of ticket agency:");

                    ticketAgent = Console.ReadLine();

                    Console.Write("\nCustomer's name:");

                    customerName = Console.ReadLine();

                    Console.Write("\nCustomer's phone number:");

                    customerPhone = Console.ReadLine();

                    bool intTestLoop = true;

                    foreach (Flight f in flightData)
                    {
                        if (f.flightID.ToLower() == flightID.ToLower())
                        {
                            // Test input to check if numberOfTickets is correct type.
                            while (intTestLoop)
                            {
                                Console.Write("\nHow many tickets are being booked:");

                                // If the input is an int.
                                if (int.TryParse(Console.ReadLine(), out int numberOfTicketsTest))
                                {

                                    // If the booking doesn't exceed the maximum capacity of the flight AND is greater than 0
                                    if ((numberOfTicketsTest <= (f.capacity - totalBookings)) && (numberOfTicketsTest > 0))
                                    {
                                        intTestLoop = false;
                                        numberOfTickets = numberOfTicketsTest;
                                    }
                                    else
                                    {
                                        Console.Write("Invalid amount. Number of tickets exceeds the capacity of the flight.");
                                    }
                                }
                                // If the input is NOT an int.
                                else
                                {
                                    Console.Write("Incorrect input. Please enter a whole number.");
                                }
                            }

                            // Calculate the cost of the booking.
                            bookingPrice = numberOfTickets * f.ticketPrice;
                            
                        }
                    }

                    // Base the booking ID on the number of entries in the bookings.json file.
                    bookingID = bookingData.Count + 1;


                    bookingData.Add(new Booking
                    {
                        flightID = flightID,
                        bookingID = bookingID,
                        customerName = customerName,
                        customerPhone = customerPhone,
                        numberOfTickets = numberOfTickets,
                        bookingPrice = bookingPrice,
                        ticketAgent = ticketAgent,
                        // False means 'not confirmed'.
                        bookingStatus = false

                    });

                    json.saveBookingData(bookingData);


                    Console.Write("\n\nYour booking ID is {0}." +
                        "\nPress any key to return to the menu.", bookingID);
                    Console.ReadKey();

                    // Return to flight centre submenu.
                    subMenu.agentSubMenu();
                }
                // If flight is NOT available for booking.
                else
                {
                    Console.Write("\nNo bookings are able to be made for this flight.\n" +
                        "Press any key to return to the menu.");
                    Console.ReadKey();

                    // Return to flight centre submenu.
                    subMenu.agentSubMenu();
                }


            }
            // If flight ID DOESN'T exist in flight schedules.
            else
            {
                Console.Write("\nNo flights exist with that flight ID.\n" +
                    "Press any key to return to the menu.");
                Console.ReadKey();

                // Return to flight centre submenu.
                subMenu.agentSubMenu();
            }

        }


        // Properties.
        public string flightID
        {
            get;
            set;
        }

        public string flightType
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

        public float ticketPrice
        {
            get;
            set;
        }

        public int capacity
        {
            get;
            set;
        }

        public bool flightAvailability
        {
            get;
            set;
        }

        public int bookingID
        {
            get;
            set;
        }

        public string ticketAgent
        {
            get;
            set;
        }

        public string customerName
        {
            get;
            set;
        }

        public string customerPhone
        {
            get;
            set;
        }

        // Number of tickets purchased
        public int numberOfTickets
        {
            get;
            set;
        }

        public float bookingPrice
        {
            get;
            set;
        }

        public bool bookingStatus
        {
            get;
            set;
        }


        // Total number of bookings for flight
        public int totalBookings
        {
            get;
            set;
        }

    }

}



