using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment1
{
    class MainMenu
    {
        // Displays the main menu options.
        public void initMenu()
        {
            bool exitMenuLoop = false;

            while (!exitMenuLoop)
            {
                Console.Clear();

                Console.Write("Welcome to Around-The-World\n" +
                            "================================================\n" +
                            "\t1. Airline Employee\n\n" +
                            "\t2. Ticket Agent\n\n" +
                            "\t3. Customer\n\n" +
                            "\t4. Quit\n\n" +
                            "Enter an option:");

                string menuSelectString = Console.ReadLine();
                int menuSelectInt;
                bool menuSelectTest = int.TryParse(menuSelectString, out menuSelectInt);
                
                // If the input is an int
                if (menuSelectTest)
                {
                    // If the option selected is an int, between 1-4 (inclusive).
                    if (menuSelectInt > 0 & menuSelectInt < 5)
                    {
                        switch (menuSelectInt)
                        {
                            case 1:
                                {
                                    // Display employee submenu.
                                    exitMenuLoop = true;
                                    SubMenus subMenu = new SubMenus();
                                    subMenu.employeeSubMenu();
                                    break;
                                }
                            case 2:
                                {
                                    // Display flight centre (agent) submenu.
                                    exitMenuLoop = true;
                                    SubMenus subMenu = new SubMenus();
                                    subMenu.agentSubMenu();
                                    break;
                                }
                            case 3:
                                {
                                    // Display customer submenu.
                                    exitMenuLoop = true;
                                    SubMenus subMenu = new SubMenus();
                                    subMenu.customerSubMenu();
                                    break;
                                }
                            case 4:
                                {
                                    Environment.Exit(0);
                                    break;
                                }
                        }
                    }
                    // If the option selected is an int, but NOT between 1-4 (inclusive).
                    else
                    {
                        Console.WriteLine("\nIncorrect input. The number has to be between 1 and 4." +
                            "\nPress any key to try again.");
                        Console.ReadKey();
                    }
                }
                // If the option selected is NOT an int.
                else
                {
                    Console.Write("\nIncorrect input. Please enter a whole number between 1 and 4." +
                        "\nPress any key to try again.");
                    Console.ReadKey();    
                }
            }
        }

    }
}
