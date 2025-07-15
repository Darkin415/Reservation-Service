namespace SeatReservation.Domain;

public class User
{
    public User()
    {
        
    }
    private List<SocialNetwork> _social = [];
    
    public Guid Id { get; set; }
    
    public Details Details { get; set; }
    
    
}

public record SocialNetwork
{
    public SocialNetwork()
    {
        
    }

    public string Name { get; }

    public string Link  {get; }
    
}
public record Details
{

    public Details()
    {
        
    }
    
    public string Description { get; }
    
    public string FIO { get; }

    public IReadOnlyList<SocialNetwork> Social { get; set; }
}


