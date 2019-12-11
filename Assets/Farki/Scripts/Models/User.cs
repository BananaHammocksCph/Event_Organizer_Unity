using System;

/// <summary>
/// User schema
/// </summary>

[Serializable]
public class User 
{
    public string _id;
    public Event[] Events;
    public Event Event_Owner;
    public string Name;
    public string Email;
    public string Password;
}

