using UnityEngine;
using Unity.Services.Core;

public class InitialiseGame : MonoBehaviour
{

    async void Start()
    {
        Debug.Log("initialising Unity gaming services");
        await UnityServices.InitializeAsync();

        Debug.Log("Authenticate player");
        AuthenticationManager authenticationManager = GetComponent<AuthenticationManager>();
        await authenticationManager.AuthenticatePlayer();

        GameManager.Instance.NewGame();
        GameManager.Instance.GoToWorlMap();
    }

}
