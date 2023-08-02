namespace DevTownCodeChallenge;

public class StarshipList
{
    public int count { get; set; }
    public string next { get; set; }
    public string previous { get; set; }
    public List<Starship> results { get; set; }
    public string error { get; set; }
}