using System;
using System.Collections.Generic;

[Serializable]
public class EventResponse
{
    public string status;
    public Event[] data;
}

[Serializable]
public class UserResponse
{
    public string status;
    public User data;
}

[Serializable]
public class E_User
{
    public string status;
    public string[] data;
}

[Serializable]
public class UID
{
    public string Id;
}

[Serializable]
public class RatinResponse
{
    public string status;
    public Rating[] data;
}