using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Threading.Tasks;

namespace ServerBlazor
{
    public class SignalRService
    {
        public HubConnection HubConnection { get; private set; }
        public ILocalStorageService LocalStorage { get; private set; }
        private string Token { get; set; }

        public SignalRService(NavigationManager navigationManager, ILocalStorageService localStorage)
        {
            LocalStorage = localStorage;
            HubConnection = new HubConnectionBuilder()
                .WithUrl(navigationManager.ToAbsoluteUri("/notihub"), options =>
                {
                    options.AccessTokenProvider = () => Task.FromResult(Token);
                })
                .Build();
        }

        public async Task StartConnectionAsync()
        {
            if (HubConnection.State == HubConnectionState.Disconnected)
            {
                await HubConnection.StartAsync();
            }
        }

        public async Task StopConnectionAsync()
        {
            if (HubConnection.State == HubConnectionState.Connected)
            {
                await HubConnection.StopAsync();
            }
        }

        public void SetToken(string token)
        {
            Token = token;
        }
    }
}
