using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment1
{
    // Contains all the methods related to the employee submenu options.
    class Employee
    {
        // Displays all scheduled flights.
        public void displayScheduledFlights()
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
                    "\nPress any key to return to the menu");
                Console.ReadKey();

                // Return to employee submenu.
                subMenu.employeeSubMenu();
            }
            else
            {

                Console.Write("Flights\n" +
                    "_______________________________________________________________________________________________________________________\n" +
                    "{0,-10}" + "{1,-15}" + "{2,-30}" + "{3,-12}" + "{4,-10}" + "{5,-10}" + "{6,-10}" + "{7,-10}" + "{8,-10}\n" +
                    "_______________________________________________________________________________________________________________________\n",
                    "ID", "Flight Type", "Flight Details", "Date", "Time", "Price ($)", "Capacity", "Booked", "Availability");


                // If no bookings exist in bookings.json file.
                if ((bookingData != null) && (!bookingData.Any()))
                {
                    foreach (Flight f in flightData)
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
                    foreach (Flight f in flightData)
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
                        Console.Write("{0,-10}" + "{1,-15}" + "{2,-30}" + "{3,-12:dd/MM/yyyy}" + "{4,-10:HH:mm}" + "{5,-10}" + "{6,-10}" + "{7,-10}" + "{8,-10}\n" +
                        "_______________________________________________________________________________________________________________________\n",
                        f.flightID, f.flightType, f.flightFrom + " - " + f.flightTo, f.flightDateTime, f.flightDateTime, f.ticketPrice, f.capacity, totalBookings, flightAvailability);

                    }

                }

                Console.Write("\nPress any key to return to the menu.");

                Console.ReadKey();

                // Return to employee submenu.
                subMenu.employeeSubMenu();

            }
        }

        // Adds new flight schedule.
        public void addFlightSchedule()
        {
            // Initialise these for later use.
            SubMenus subMenu = new SubMenus();
            
            JSON json = new JSON();
            List<Flight> flightData = json.readFlightData();


            Console.Clear();

            Console.Write("Add New Flight Schedule\n" +
                "=============================================\n\n" +
                "Enter the flight ID:");

            flightID = Console.ReadLine();

            Console.Write("\nEnter the flight type:");
            flightType = Console.ReadLine();

            Console.Write("\nFlight from:");
            flightFrom = Console.ReadLine();

            Console.Write("\nFlight to:");
            flightTo = Console.ReadLine();


            bool dateTimeTestLoop = true;

            // Test input for date and time is correct type.
            while (dateTimeTestLoop)
            {
                Console.Write("\nDate of flight (dd/mm/yyyy):");
                string flightDate = Console.ReadLine();

                Console.Write("\nTime of flight (24hr time - hh:mm):");
                string flightTime = Console.ReadLine();

                if (DateTime.TryParse(flightDate + " " + flightTime, out DateTime dateTimeTest))
                {
                    dateTimeTestLoop = false;
                    flightDateTime = dateTimeTest;
                }
                else
                {
                    Console.Write("Incorrect date/time. Please try again.");
                }

            }
            
            bool floatTestLoop = true;

            // Test input for ticket price is correct type.
            while (floatTestLoop)
            {
                Console.Write("\nTicket Price ($):");

                // If the input is a float.
                if (float.TryParse(Console.ReadLine(), out float ticketPriceTest))
                {
                    floatTestLoop = false;
                    ticketPrice = ticketPriceTest;
                }
                // If the input is NOT a float.
                else
                {
                    Console.Write("Incorrect input. Please enter a number.");
                }
            }

            bool intTestLoop = true;

            // Test input for capacity is correct type.
            while (intTestLoop)
            {
                Console.Write("\nCapacity:");

                // If the input is an int.
                if (int.TryParse(Console.ReadLine(), out int capacityTest))
                {
                    intTestLoop = false;
                    capacity = capacityTest;
                }
                // If the input is NOT an int.
                else
                {
                    Console.Write("Incorrect input. Please enter a whole number.");
                }
            }

            // If no flight schedules exist.
            if ((flightData != null) && (!flightData.Any()))
            {

                flightData.Add(new Flight
                {
                    flightID = flightID,
                    flightType = flightType,
                    flightFrom = flightFrom,
                    flightTo = flightTo,
                    flightDateTime = flightDateTime,
                    ticketPrice = ticketPrice,
                    capacity = capacity
                });
                
                json.saveFlightData(flightData);
            }
            else
            {
                // Test if flight ID already exists. 
                foreach (Flight f in flightData)
                {
                    if (f.flightID.ToLower() == flightID.ToLower())
                    {
                        Console.Write("\nDetails already exist for this flight ID.\n" +
                        "Press any key to try again.");
                        Console.ReadKey();

                        // Return to employee submenu.
                        subMenu.employeeSubMenu();
                    }
                }

                // If flight ID doesn't already exist.
                flightData.Add(new Flight
                {
                    flightID = flightID,
                    flightType = flightType,
                    flightFrom = flightFrom,
                    flightTo = flightTo,
                    flightDateTime = flightDateTime,
                    ticketPrice = ticketPrice,
                    capacity = capacity
                });

                json.saveFlightData(flightData);
            }

            Console.Write("\nFlight schedule has been successfully added.\n\n" +
                "Press any key to return to the menu.");
            Console.ReadKey();

            // Return to employee submenu.
            subMenu.employeeSubMenu();

        }

        //  Displays all ticket bookings for a selected flight.
        public void displayAllBookings()
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
                    "\nPress any key to return to the menu");
                Console.ReadKey();

                // Return to employee submenu.
                subMenu.employeeSubMenu();
            }
            else
            {

                // If no bookings exist in bookings.json file.
                if ((bookingData != null) && (!bookingData.Any()))
                {
                    Console.Write("No bookings exist for any flights." +
                        "\n\nPress any key to return to the menu.");

                    Console.ReadKey();

                    // Return to employee submenu.
                    subMenu.employeeSubMenu();

                }

                // If bookings exist in bookings.json file.
                else
                {

                    Console.Write("Flights\n" +
                        "_______________________________________________________________________________________________________________________\n" +
                        "{0,-10}" + "{1,-15}" + "{2,-30}" + "{3,-12}" + "{4,-10}" + "{5,-10}" + "{6,-10}" + "{7,-10}" + "{8,-10}\n" +
                        "_______________________________________________________________________________________________________________________\n",
                        "ID", "Flight Type", "Flight Details", "Date", "Time", "Price ($)", "Capacity", "Booked", "Availability");

                    foreach (Flight f in flightData)
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
                        Console.Write("{0,-10}" + "{1,-15}" + "{2,-30}" + "{3,-12:dd/MM/yyyy}" + "{4,-10:HH:mm}" + "{5,-10}" + "{6,-10}" + "{7,-10}" + "{8,-10}\n" +
                        "_______________________________________________________________________________________________________________________\n",
                        f.flightID, f.flightType, f.flightFrom + " - " + f.flightTo, f.flightDateTime, f.flightDateTime, f.ticketPrice, f.capacity, totalBookings, flightAvailability);

                    }

                }
                
                bool flightIDTestLoop = true;
                
                while (flightIDTestLoop)
                {
                    Console.Write("\nEnter the flight ID:");

                    flightID = Console.ReadLine();

                    // If flight ID doesn't exist (i.e. incorrectly entered).
                    if (!flightData.Any(f => f.flightID.ToLower() == flightID.ToLower()))
                    {
                        Console.Write("Incorrect flight ID. Please try again.");
                    }
                    else
                    {
                        // If no bookings exist for the selected flight.
                        if (!bookingData.Any(b => b.flightID.ToLower() == flightID.ToLower()))
                        {
                            Console.Write("\nNo bookings exist for the selected flight." +
                                "\n\nPress any key to return to the menu.");

                            Console.ReadKey();

                            // Return to employee submenu.
                            subMenu.employeeSubMenu();
                        }
                        else
                        {

                            // Create temporary list to hold booking data results for specific flight.
                            List<Booking> tempBookingData = new List<Booking>();
                            flightIDTestLoop = false;

                            // Retrieve list of all bookings with status(boolean) as confirmed (true) or not confirmed (false).
                            foreach (Booking b in bookingData)
                            {
                                if (b.flightID.ToLower() == flightID.ToLower())
                                {
                                    tempBookingData.Add(b);
                                }
                            }


                            Console.Write("\n\nTicket Bookings for flight {0}\n" +
                                "_______________________________________________________________________________________________________________________\n" +
                                "{1,-20}" + "{2,-20}" + "{3,-20}" + "{4,-27}" + "{5,-15}" + "{6,-10}\n" +
                                "_______________________________________________________________________________________________________________________\n",
                                flightID, "Booking ID", "Name", "No. of Tickets", "Ticket Agent", "Price", "Status");


                            // Format list of bookings for displaying.
                            foreach (Booking b in tempBookingData)
                            {
                                // Temporary variable to hold result of converting bookingStatus to something meaningful to user.
                                string status;

                                // Convert bookingStatus into something meaningful to user.
                                if (b.bookingStatus)
                                {
                                    status = "Confirmed";
                                }
                                else
                                {
                                    status = "Not Confirmed";
                                }

                                Console.Write("{0,-20}" + "{1,-20}" + "{2,-20}" + "{3,-27}" + "${4,-14}" + "{5,-10}\n" +
                                    "_______________________________________________________________________________________________________________________\n",
                                    b.bookingID, b.customerName, b.numberOfTickets, b.ticketAgent, b.bookingPrice, status);
                            }

                            bool intTestLoop = true;

                            while (intTestLoop)
                            {
                                Console.Write("\nEnter the booking ID:");

                                // If the input is an int.
                                if (int.TryParse(Console.ReadLine(), out int bookingIDTest))
                                {
                                    // If booking ID does not exist in temporary booking list (i.e. incorrect id entered).
                                    if (!tempBookingData.Any(b => b.bookingID == bookingIDTest))
                                    {
                                        Console.Write("\nBooking ID is incorrect, please try again.");
                                    }
                                    else
                                    {
                                        intTestLoop = false;

                                        // If booking is already confirmed.
                                        foreach (Booking b in tempBookingData.Where(b => b.bookingID == bookingIDTest))
                                        {
                                            if (b.bookingStatus)
                                            {
                                                Console.Write("\nBooking is already confirmed." +
                                                    "\nPress any key to return to the menu");
                                                Console.ReadKey();

                                                // Return to employee submenu.
                                                subMenu.employeeSubMenu();

                                            }
                                            else
                                            {
                                                b.bookingStatus = true;

                                                // Return change in status back to original booking list.
                                                foreach (Booking c in bookingData.Where(c => c.bookingID == bookingIDTest))
                                                {
                                                    c.bookingStatus = b.bookingStatus;
                                                    json.saveBookingData(bookingData);
                                                }

                                                Console.Write("\nBooking is confirmed." +
                                                    "\nPress any key to return to the menu.");
                                                Console.ReadKey();

                                                // Return to employee submenu.
                                                subMenu.employeeSubMenu();

                                            }
                                        }
                                    }
                                }
                                // If the input is NOT an int.
                                else
                                {
                                    Console.Write("\nBooking ID is incorrect, please try again.");
                                }
                            }
                        }
                    }
                }
            }
        }

        // Adds new ticket agent.
        public void addTicketAgent()
        {

            // Initialise these for later use.
            SubMenus subMenu = new SubMenus();

            JSON json = new JSON();
            List<Agent> agentData = json.readTicketAgentData();
            

            Console.Clear();

            Console.Write("Add New Ticket Agent\n" +
                "=============================================\n\n" +
                "Enter the company name:");

            companyName = Console.ReadLine();

            Console.Write("\nEnter the business address:");
            companyAddress = Console.ReadLine();

            Console.Write("\nContact person name:");
            contactName = Console.ReadLine();

            Console.Write("\nContact person phone:");
            contactPhone = Console.ReadLine();

            // If no ticket agents exist.
            if ((agentData != null) && (!agentData.Any()))
            {
                agentData.Add(new Agent
                {
                    agentID = 1,
                    companyName = companyName,
                    companyAddress = companyAddress,
                    contactName = contactName,
                    contactPhone = contactPhone,
                    
                });
                
                json.saveTicketAgentData(agentData);

            }
            else
            {
                // Test if ticket agent already exists. 
                foreach (Agent a in agentData)
                {
                    if ((a.companyName.ToLower() == companyName.ToLower()) && (a.companyAddress.ToLower() == companyAddress.ToLower()) 
                        && (a.contactName.ToLower() == contactName.ToLower()) && (a.contactPhone.ToLower() == contactPhone.ToLower()))
                    {
                        Console.Write("\nDetails already exist for this agent.\n" +
                        "Press any key to try again.");
                        Console.ReadKey();

                        // Return to employee submenu.
                        subMenu.employeeSubMenu();
                    }
                }
                                
                int newAgentID = agentData.Count() + 1;
                
                // If ticket agent doesn't already exist.
                agentData.Add(new Agent
                    {
                        agentID = newAgentID,
                        companyName = companyName,
                        companyAddress = companyAddress,
                        contactName = contactName,
                        contactPhone = contactPhone,
                        
                    });

                json.saveTicketAgentData(agentData);
            }

            Console.Write("\nThe new agent ID is {0}" +
                    "\n\nPress any key to return to the menu.", agentData.Count());

            Console.ReadKey();
            
            // Return to employee submenu.
            subMenu.employeeSubMenu();
            
        }

        // Displays all ticket agents.
        public void displayAllAgents()
        {
             
            // Initialise these for later use.
            SubMenus subMenu = new SubMenus();

            JSON json = new JSON();
            List<Agent> agentData = json.readTicketAgentData();


            Console.Clear();

            // If no ticket agents exist.
            if ((agentData != null) && (!agentData.Any()))
            {

                Console.Write("No ticket agents exist." +
                    "\nPress any key to return to the menu");
                Console.ReadKey();

                // Return to employee submenu.
                subMenu.employeeSubMenu();
            }
            else
            {

                Console.Write("Ticket Agents\n" +
                    "_______________________________________________________________________________________________________________________\n" +
                    "{0,-10}" + "{1,-20}" + "{2,-50}" + "{3,-20}" + "{4,-10}\n" +
                    "_______________________________________________________________________________________________________________________\n",
                    "Agent ID", "Business Name", "Address", "Contact Name", "Phone No");

                // Format list of ticket agents for displaying.
                foreach (Agent a in agentData)
                {
                    Console.Write("{0,-10}" + "{1,-20}" + "{2,-50}" + "{3,-20}" + "{4,-10}\n" +
                        "_______________________________________________________________________________________________________________________\n",
                        a.agentID, a.companyName, a.companyAddress, a.contactName, a.contactPhone);
                }
                

                Console.Write("\nPress any key to return to the menu.");

                Console.ReadKey();

                // Return to employee submenu.
                subMenu.employeeSubMenu();

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
        
        public DateTime flightDateTime
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

        public string companyName
        {
            get;
            set;
        }

        public string companyAddress
        {
            get;
            set;
        }

        public string contactName
        {
            get;
            set;
        }

        public string contactPhone
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

        public bool flightAvailability
        {
            get;
            set;
        }


    }
}
