using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Newtonsoft.Json;

namespace Assignment1
{
    // Contains all the methods related to the serialization and deserialization of the JSON files.
    class JSON
    {
        // Save flight schedule data to json file.
        public void saveFlightData(List<Flight> flightList)
        {

            File.WriteAllText("flightdata.json", JsonConvert.SerializeObject(flightList, Formatting.Indented));
        }

        // Read flight schedule data from json file.
        public List<Flight> readFlightData()
        {
            // Test if json file for flight schedules already exists.
            if (File.Exists("flightData.json"))
            {
                return JsonConvert.DeserializeObject<List<Flight>>(File.ReadAllText("flightdata.json"));
            }
            else
            {
                return new List<Flight>();
            }
        }

        // Save booking data to json file.
        public void saveBookingData(List<Booking> bookingData)
        {
            File.WriteAllText("bookings.json", JsonConvert.SerializeObject(bookingData, Formatting.Indented));
        }

        // Read booking data from json file.
        public List<Booking> readBookingData()
        {
            // Test if json file for bookings already exists.
            if (File.Exists("bookings.json"))
            {
                return JsonConvert.DeserializeObject<List<Booking>>(File.ReadAllText("bookings.json"));
            }
            else
            {
                return new List<Booking>();
            }

        }

        // Save ticket agent data to json file.
        public void saveTicketAgentData(List<Agent> agentData)
        {
            File.WriteAllText("agents.json", JsonConvert.SerializeObject(agentData, Formatting.Indented));
        }

        // Read ticket agent data from json file.
        public List<Agent> readTicketAgentData()
        {
            // Test if json file for ticket agents already exists.
            if (File.Exists("agents.json"))
            {
                return JsonConvert.DeserializeObject<List<Agent>>(File.ReadAllText("agents.json"));
            }
            else
            {
                return new List<Agent>();
            }
        }

    }

    public class Flight
    {
        public string flightID { get; set; }
        public string flightType { get; set; }
        public string flightFrom { get; set; }
        public string flightTo { get; set; }

        public DateTime flightDateTime { get; set; }

        public float ticketPrice { get; set; }
        public int capacity { get; set; }

    }

    class Booking
    {
        public string flightID { get; set; }
        public int bookingID { get; set; }
        public string ticketAgent { get; set; }
        public string customerName { get; set; }
        public string customerPhone { get; set; }
        public int numberOfTickets { get; set; }
        public float bookingPrice { get; set; }
        public bool bookingStatus { get; set; }

    }

    class Agent
    {
        public int agentID { get; set; }
        public string companyName { get; set; }
        public string companyAddress { get; set; }
        public string contactName { get; set; }
        public string contactPhone { get; set; }

    }
}