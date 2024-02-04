using System.ComponentModel;
using System.Text.Json.Serialization;
using IWMB01_Flugsuche_App.Converters;

namespace IWMB01_Flugsuche_App.Models;

public class FlightResponseModel
{
    public NearestAirportResource NearestAirportResource { get; set; }
    public AirportResource AirportResource { get; set; }
    public ScheduleResource ScheduleResource { get; set; }
}

public class NearestAirportResource
{
    public Airports Airports { get; set; }
    public Meta Meta { get; set; }
}

public class AirportResource
{
    public Airports Airports { get; set; }
    public Meta Meta { get; set; }
}

public class ScheduleResource
{
    public Schedule[] Schedule { get; set; }
    public Meta Meta { get; set; }
}

public class Schedule
{
    public TotalJourney TotalJourney { get; set; }
    [JsonConverter(typeof(SingleObjectOrArrayJsonConverter<Flight>))]
    public List<Flight> Flight { get; set; }
}

public class Flight
{
    public Departure Departure { get; set; }
    public Arrival Arrival { get; set; }
    public MarketingCarrier MarketingCarrier { get; set; }
    public Equipment Equipment { get; set; }
    public Details Details { get; set; }
    public OperatingCarrier OperatingCarrier { get; set; }
}

public class Departure
{
    public string AirportCode { get; set; }
    public ScheduledTimeLocal ScheduledTimeLocal { get; set; }
    public Terminal Terminal { get; set; }
}
public class Terminal
{
    public string Name { get; set; }
}

public class Arrival
{
    public string AirportCode { get; set; }
    public ScheduledTimeLocal ScheduledTimeLocal { get; set; }
}

public class ScheduledTimeLocal
{
    public DateTime DateTime { get; set; }
}

public class MarketingCarrier
{
    public string AirlineID { get; set; }
    public string FlightNumber { get; set; }
}

public class Equipment
{
    public string AircraftCode { get; set; }
    public OnBoardEquipment OnBoardEquipment { get; set; }
}

public class OnBoardEquipment
{
    public bool InflightEntertainment { get; set; }
    public Compartment[] Compartment { get; set; }
}

public class Compartment
{
    public string ClassCode { get; set; }
    public string ClassDesc { get; set; }
    public bool FlyNet { get; set; }
    public bool SeatPower { get; set; }
    public bool Usb { get; set; }
    public bool LiveTv { get; set; }
}

public class Details
{
    public Stops Stops { get; set; }
    public string DaysOfOperation { get; set; }
    public DatePeriod DatePeriod { get; set; }
}

public class Stops
{
    public int StopQuantity { get; set; }
}

public class DatePeriod
{
    public string Effective { get; set; }
    public string Expiration { get; set; }
}

public class OperatingCarrier
{
    public string AirlineID { get; set; }
}

public class TotalJourney
{
    public string Duration { get; set; }
}

public class Airports
{
    [JsonConverter(typeof(SingleObjectOrArrayJsonConverter<Airport>))]
    public List<Airport> Airport { get; set; }
}

public class Airport
{
    public string AirportCode { get; set; }
    public Position Position { get; set; }
    public string CityCode { get; set; }
    public string CountryCode { get; set; }
    public string LocationType { get; set; }
    public Names Names { get; set; }
    public Distance Distance { get; set; }
}

public class Position
{
    public Coordinate Coordinate { get; set; }
}

public class Coordinate
{
    public double Latitude { get; set; }
    public double Longitude { get; set; }
}

public class Names
{
    public Name Name { get; set; }
}

public class Name
{
    [JsonPropertyName("@LanguageCode")]
    public string LanguageCode { get; set; }
    [JsonPropertyName("$")]
    public string City { get; set; }
}

public class Distance
{
    public int Value { get; set; }
    public string UOM { get; set; }
}

public class Meta
{
    public string _Version { get; set; }
    public Link[] Link { get; set; }
}

public class Link
{
    public string _Href { get; set; }
    public string _Rel { get; set; }
}

