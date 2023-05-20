namespace LemonApi.Models;

public enum SessionState
{
    AWAIT,
    PENDING,
    PLAYING,
    OVER
}
public static class SessionStateExtansion
{
    public static SessionState ToSessionState(string str)
    {
        return str.ToUpper() switch
        {
            "AWAIT" => SessionState.AWAIT,
            "PENDING" => SessionState.PENDING,
            "PLAYING" => SessionState.PLAYING,
            "OVER" => SessionState.OVER,
            _ => throw new Exception("Неизвестный статус сессии")
        };
    }
}