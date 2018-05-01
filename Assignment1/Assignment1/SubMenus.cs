using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment1
{
    // Contains all the methods related to the submenus.
    class SubMenus
    {
        // Displays the employee submenu options
        public void employeeSubMenu()
        {
            bool exitMenuLoop = false;

            while (!exitMenuLoop)
            {
                Console.Clear();

                Console.Write("Welcome to Around-The-World (Employee)\n" +
                "=============================================\n" +
                "\t1. Display All Scheduled Flights\n\n" +
                "\t2. Add New Flight Schedule\n\n" +
                "\t3. Display All Ticket Bookings\n\n" +
                "\t4. Add New Ticket Agent\n\n" +
                "\t5. Display All Ticket Agents\n\n" +
                "\t6. Return to Main Menu\n\n" +
                "\t7. Quit\n\n" +
                "Enter an option:");

                string menuSelectString = Console.ReadLine();
                int menuSelectInt;
                bool menuSelectTest = int.TryParse(menuSelectString, out menuSelectInt);

                // If the input is an int
                if (menuSelectTest)
                {
                    // If the option selected is an int, between 1-7 (inclusive).
                    if (menuSelectInt > 0 & menuSelectInt < 8)
                    {
                        switch (menuSelectInt)
                        {
                            case 1:
                                {
                                    exitMenuLoop = true;
                                    Employee employee = new Employee();
                                    employee.displayScheduledFlights();
                                    break;
                                }
                            case 2:
                                {
                                    exitMenuLoop = true;
                                    Employee employee = new Employee();
                                    employee.addFlightSchedule();
                                    break;
                                }
                            case 3:
                                {
                                    exitMenuLoop = true;
                                    Employee employee = new Employee();
                                    employee.displayAllBookings();
                                    break;
                                }
                            case 4:
                                {
                                    exitMenuLoop = true;
                                    Employee employee = new Employee();
                                    employee.addTicketAgent();
                                    break;
                                }
                            case 5:
                                {
                                    exitMenuLoop = true;
                                    Employee employee = new Employee();
                                    employee.displayAllAgents();
                                    break;
                                }
                            case 6:
                                {
                                    exitMenuLoop = true;
                                    returnMainMenu();
                                    break;
                                }
                            case 7:
                                {
                                    Environment.Exit(0);
                                    break;
                                }
                        }
                    }
                    // If the option selected is an int, but NOT between 1-7 (inclusive).
                    else
                    {
                        Console.WriteLine("\nIncorrect input. The number has to be between 1 and 7." +
                            "\nPress any key to try again.");
                        Console.ReadKey();
                    }
                }
                // If the option selected is NOT an int.
                else
                {
                    Console.Write("\nIncorrect input. Please enter a whole number between 1 and 7." +
                        "\nPress any key to try again.");
                    Console.ReadKey();
                }
            }
        }

        // Displays the flight centre (agent) submenu options
        public void agentSubMenu()
        {
            bool exitMenuLoop = false;

            while (!exitMenuLoop)
            {
                Console.Clear();

                Console.Write("Welcome to Around-The-World (Flight Centre)\n" +
                "================================================\n" +
                "\t1. Display All Scheduled Flights\n\n" +
                "\t2. Search for Flight\n\n" +
                "\t3. Check Booking Status\n\n" +
                "\t4. Add New Booking\n\n" +
                "\t5. Return to Main Menu\n\n" +
                "\t6. Quit\n\n" +
                "Enter an option:");

                string menuSelectString = Console.ReadLine();
                int menuSelectInt;
                bool menuSelectTest = int.TryParse(menuSelectString, out menuSelectInt);

                // If the input is an int
                if (menuSelectTest)
                {
                    // If the option selected is an int, between 1-6 (inclusive).
                    if (menuSelectInt > 0 & menuSelectInt < 7)
                    {
                        switch (menuSelectInt)
                        {
                            case 1:
                                {
                                    exitMenuLoop = true;
                                    FlightCentre agent = new FlightCentre();
                                    agent.displayScheduledFlightsAgent();
                                    break;
                                }
                            case 2:
                                {
                                    exitMenuLoop = true;
                                    FlightCentre agent = new FlightCentre();
                                    agent.searchFlight();
                                    break;
                                }
                            case 3:
                                {
                                    exitMenuLoop = true;
                                    FlightCentre agent = new FlightCentre();
                                    agent.checkBookingStatus();
                                    break;
                                }
                            case 4:
                                {
                                    exitMenuLoop = true;
                                    Console.Clear();
                                    FlightCentre agent = new FlightCentre();
                                    agent.addBooking();
                                    break;
                                }
                            case 5:
                                {
                                    exitMenuLoop = true;
                                    returnMainMenu();
                                    break;
                                }
                            case 6:
                                {
                                    Environment.Exit(0);
                                    break;
                                }
                        }
                    }
                    // If the option selected is an int, but NOT between 1-6 (inclusive).
                    else
                    {
                        Console.WriteLine("\nIncorrect input. The number has to be between 1 and 6." +
                            "\nPress any key to try again.");
                        Console.ReadKey();
                    }
                }
                // If the option selected is NOT an int.
                else
                {
                    Console.Write("\nIncorrect input. Please enter a whole number between 1 and 6." +
                        "\nPress any key to try again.");
                    Console.ReadKey();
                }
            }
        }

        // Displays the customer submenu options
        public void customerSubMenu()
        {
            bool exitMenuLoop = false;

            while (!exitMenuLoop)
            {
                Console.Clear();

                Console.Write("Welcome to Around-The-World (Customer)\n" +
                "================================================\n" +
                "\t1. Check Booking and Flight Status\n\n" +
                "\t2. Return to Main Menu\n\n" +
                "\t3. Quit\n\n" +
                "Enter an option:");

                string menuSelectString = Console.ReadLine();
                int menuSelectInt;
                bool menuSelectTest = int.TryParse(menuSelectString, out menuSelectInt);

                // If the input is an int
                if (menuSelectTest)
                {
                    // If the option selected is an int, between 1-3 (inclusive).
                    if (menuSelectInt > 0 & menuSelectInt < 4)
                    {
                        switch (menuSelectInt)
                        {
                            case 1:
                                {
                                    exitMenuLoop = true;
                                    Customer customer = new Customer();
                                    customer.checkBookingFlightStatus();
                                    break;
                                }
                            case 2:
                                {
                                    exitMenuLoop = true;
                                    returnMainMenu();
                                    break;
                                }
                            case 3:
                                {
                                    Environment.Exit(0);
                                    break;
                                }
                        }
                    }
                    // If the option selected is an int, but NOT between 1-3 (inclusive).
                    else
                    {
                        Console.WriteLine("\nIncorrect input. The number has to be between 1 and 3." +
                            "\nPress any key to try again.");
                        Console.ReadKey();
                    }
                }
                // If the option selected is NOT an int.
                else
                {
                    Console.Write("\nIncorrect input. Please enter a whole number between 1 and 3." +
                        "\nPress any key to try again.");
                    Console.ReadKey();
                }
            }
        }
        
        // Displays the main menu.
        public void returnMainMenu()
        {
            MainMenu mainMenu = new MainMenu();
            mainMenu.initMenu();
        }
    }
}
