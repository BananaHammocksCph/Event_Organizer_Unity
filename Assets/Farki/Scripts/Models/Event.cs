using System;

[Serializable]
public class Event 
{
    public string _id;
    public string email;
    public string Name;
    public string Description;
    public User[] Users;
    public string Created;
    public string Type;
    public bool Catering;
    public string Catering_Desc;
    public string Date;
    public string Location;
    public Rating[] Ratings;
}
