string url = "https://earthquake.usgs.gov/earthquakes/feed/v1.0/summary/4.5_month.geojson";
using (WebClient client = new WebClient())
{
    string json = client.DownloadString(url);
    JObject jsonObject = JObject.Parse(json);
    JArray earthquakes = (JArray)jsonObject["features"];
    foreach (var earthquake in earthquakes)
    {
        string location = (string)earthquake["properties"]["place"];
        double magnitude = (double)earthquake["properties"]["mag"];
        string time = (string)earthquake["properties"]["time"];
        object coordinates = (object)earthquake["geometry"]["coordinates"];
        if (location.Contains("Turkey"))
        {
            DateTime dateTime = UnixTimeStampToDateTime(double.Parse(time) / 1000);
            Console.WriteLine("Location: {0}", location);
            Console.WriteLine("Magnitude: {0}", magnitude);
            Console.WriteLine("Coordinates: {0}", coordinates);
            Console.WriteLine("Time: {0}\n", dateTime);
        }
    }
}
Console.ReadLine();

private static DateTime UnixTimeStampToDateTime(double unixTimeStamp)
{
    DateTime dateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
    dateTime = dateTime.AddSeconds(unixTimeStamp).ToLocalTime();
    return dateTime;
}
