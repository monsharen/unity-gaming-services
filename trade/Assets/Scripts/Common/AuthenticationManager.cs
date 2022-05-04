using System.Threading.Tasks;
using System;
using UnityEngine;
using Unity.Services.Authentication;


public class AuthenticationManager : MonoBehaviour
{

    public async Task<string> AuthenticatePlayer()
    {
        if (!AuthenticationService.Instance.IsSignedIn)
        {
            await AuthenticationService.Instance.SignInAnonymouslyAsync();

            if (!AuthenticationService.Instance.IsSignedIn)
            {
                throw new InvalidOperationException("failed to log in. Demo will end");
            }
        }
        return AuthenticationService.Instance.PlayerId;
    }
}

