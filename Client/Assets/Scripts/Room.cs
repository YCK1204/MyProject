public class Room
{
    public string RoomTitle { get; private set; }
    public string SteamId { get; private set; }
    public int Theme { get; private set; }
    public int PlayerCount { get; private set; }
    public int MaxPlayers { get; private set; } // 최대 인원 추가
    public string Password { get; private set; }

    public Room(string roomTitle, string steamId, int theme, int playerCount, int maxPlayers, string password)
    {
        RoomTitle = roomTitle;
        SteamId = steamId;
        Theme = theme;
        PlayerCount = playerCount;
        MaxPlayers = maxPlayers;
        Password = password;
    }
}
