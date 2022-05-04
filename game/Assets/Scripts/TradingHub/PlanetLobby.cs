using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;
using Unity.Services.Authentication;
using Unity.Services.Lobbies;
using Unity.Services.Lobbies.Models;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class PlanetLobby : MonoBehaviour
{

    public Button refuelButton;
    public Button leaveButton;
    public Button buySellButton;
    public Text adventureText;
    public Text creditText;
    public Text fuelText;

    private string log = "";
    private string location;
    private string newLobbyName;
    private string lobbyMode = "chat";
    private int maxPlayers = 8;
    private bool isPrivate = false;
    private Lobby currentLobby;
    private Player loggedInPlayer;

    void Awake() 
    {
        location = GameManager.Instance.CurrentPlayerTradingHub.Name;
        newLobbyName = GameManager.Instance.CurrentPlayerTradingHub.Name + Guid.NewGuid();
        Log("You’ve arrived at " + GameManager.Instance.CurrentPlayerTradingHub.Name);

        refuelButton.onClick.AddListener(Refuel);
        leaveButton.onClick.AddListener(LeaveAsync);
        buySellButton.onClick.AddListener(BuySell);

        UpdateUI();
    }

    void Start()
    {
        InitLobby();        
    }

    public void BuySell()
    {
        if (GameManager.Instance.HasCargo)
        {
            GameManager.Instance.Sell(GameManager.Instance.CurrentPlayerTradingHub);
        }
        else
        {
            GameManager.Instance.Buy(GameManager.Instance.CurrentPlayerTradingHub);    
        }
        UpdateUI();
    }

    public void Refuel() 
    {
        if (GameManager.Instance.Refuel()) {
            UpdateUI();
        }
    }

    public async void LeaveAsync()
    {
        await LeaveLobby();
        GameManager.Instance.GoToWorlMap();
    }

    void UpdateUI() {
        creditText.text = "Credits: " + GameManager.Instance.Credits;
        fuelText.text = "Fuel: " + GameManager.Instance.Fuel + "%";
        adventureText.text = log;

        refuelButton.gameObject.SetActive(GameManager.Instance.Fuel < 100);
        buySellButton.GetComponentInChildren<Text>().text = GetBuySellButtonText();
    }

    async void InitLobby() {
        try
        {
            await GetOrCreateLobby();
            UpdateAdventureText(currentLobby.Players.Count);
            UpdateUI();
        }
        catch (Exception ex)
        {
            Debug.LogException(ex);
        }
    }

    void UpdateAdventureText(int playerCount) {
        if (playerCount > 1) {
            Log("Looks like you’re not the first to arrive. " +
            "Better leave your engines running and keep your guns nearby in case things get heated.");
        }
        else 
        {
            Log("The place is empty except for some shady-looking traders by the cantina.");
        }
    }

    async void OnApplicationQuit()
    {
        await LeaveLobby();
        await DeleteLobby();
    }

    async Task LeaveLobby() 
    {
        await Lobbies.Instance.RemovePlayerAsync(
            lobbyId: currentLobby.Id,
            playerId: loggedInPlayer.Id);

        Debug.Log($"Left lobby {currentLobby.Name} ({currentLobby.Id})");

        loggedInPlayer = null;
    }

    private async Task DeleteLobby() {
        var localPlayerId = AuthenticationService.Instance.PlayerId;

        if (currentLobby != null && currentLobby.HostId.Equals(localPlayerId))
        {
            await Lobbies.Instance.DeleteLobbyAsync(currentLobby.Id);
            Debug.Log($"Deleted lobby {currentLobby.Name} ({currentLobby.Id})");
            currentLobby = null;
        }
    }


    private async Task GetOrCreateLobby()
    {
        loggedInPlayer = await GetPlayerFromAnonymousLoginAsync();

        List<QueryFilter> queryFilters = new List<QueryFilter>
        {
            new QueryFilter(
                field: QueryFilter.FieldOptions.AvailableSlots,
                op: QueryFilter.OpOptions.GT,
                value: "0"),
            new QueryFilter(
                field: QueryFilter.FieldOptions.S1,
                op: QueryFilter.OpOptions.EQ,
                value: location),
            new QueryFilter(
                field: QueryFilter.FieldOptions.S2,
                op: QueryFilter.OpOptions.EQ,
                value: lobbyMode),
        };

        List<QueryOrder> queryOrdering = new List<QueryOrder>
        {
            new QueryOrder(true, QueryOrder.FieldOptions.AvailableSlots),
            new QueryOrder(false, QueryOrder.FieldOptions.Created),
            new QueryOrder(false, QueryOrder.FieldOptions.Name),
        };

        QueryResponse response = await Lobbies.Instance.QueryLobbiesAsync(new QueryLobbiesOptions()
        {
            Count = 20, // Override default number of results to return
            Filters = queryFilters,
            Order = queryOrdering,
        });

        List<Lobby> foundLobbies = response.Results;

        if (foundLobbies.Any()) // Try to join a random lobby if one exists
        {
            var randomLobby = foundLobbies[Random.Range(0, foundLobbies.Count)];

            currentLobby = await Lobbies.Instance.JoinLobbyByIdAsync(
                lobbyId: randomLobby.Id,
                options: new JoinLobbyByIdOptions()
                {
                    Player = loggedInPlayer
                });

            Debug.Log($"Joined lobby {currentLobby.Name} ({currentLobby.Id})");
        }
        else // Didn't find any lobbies, create a new lobby
        {
            var lobbyData = new Dictionary<string, DataObject>()
            {
                ["Location"] = new DataObject(DataObject.VisibilityOptions.Public, location, DataObject.IndexOptions.S1),
                ["GameMode"] = new DataObject(DataObject.VisibilityOptions.Public, lobbyMode, DataObject.IndexOptions.S2),
            };

            currentLobby = await Lobbies.Instance.CreateLobbyAsync(
                lobbyName: newLobbyName,
                maxPlayers: maxPlayers,
                options: new CreateLobbyOptions()
                {
                    Data = lobbyData,
                    IsPrivate = isPrivate,
                    Player = loggedInPlayer
                });

            Debug.Log($"Created new lobby {currentLobby.Name} ({currentLobby.Id})");
        }
    }

    async Task<Player> GetPlayerFromAnonymousLoginAsync()
    {
        AuthenticationManager authenticationManager = GetComponent<AuthenticationManager>();
        string playerId = await authenticationManager.AuthenticatePlayer();

        Debug.Log("Player signed in as " + playerId);

        // Player objects have Get-only properties, so you need to initialize the data bag here if you want to use it
        return new Player(playerId, null, new Dictionary<string, PlayerDataObject>());
    }

    private void Log(string message)
    {
        if (log.Length > 0)
        {
            log += "\n\n";
        }
        log += message;
    }

    private string GetBuySellButtonText()
    {
        if (GameManager.Instance.HasCargo)
        {
            return "Sell cargo for $" + GameManager.Instance.CurrentPlayerTradingHub.SellPrice;
        }
        return "Buy space dust for $" + GameManager.Instance.CurrentPlayerTradingHub.BuyPrice;
    }
}
