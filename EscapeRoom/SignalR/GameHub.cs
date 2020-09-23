using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;

namespace EscapeRoom.SignalR
{

    public class Card
    {
        public int Number { get; set; }
        public bool Hidden { get; set; }
        public bool Discarded { get; set; }
        public string Image { get; set; }



    }
    public class GameHub : Hub
    {
        static List<Card> cards = new List<Card>{new Card
        {
            Number = 1,
            Hidden = true,
            Discarded = false,
        }};

        private static DateTime StartTime = DateTime.Now.AddMinutes(-30);

        public async Task StartGame()
        {
            StartTime = DateTime.Now.AddMinutes(-30);
        }

        public async Task GetTime()
        {
            await Clients.All.SendAsync("getTime", StartTime);
        }

        public async Task JoinGame(string connectionId)
        {
            Debug.Print(connectionId);
        }
        public async Task GetDisplayed()
        {
            await Clients.All.SendAsync("getDisplayed", cards.Where(x=>x.Hidden == false).ToList());
        }

        public async Task ShowCard(int number)
        {
            var card = cards.SingleOrDefault(x => x.Number == number);
            if (card != null)
                card.Hidden = false;

            await GetDisplayed();
        }

        public async Task HideCard(int number)
        {
            var card = cards.SingleOrDefault(x => x.Number == number);
            if (card != null)
                card.Hidden = true;

            await GetDisplayed();
        }

    }
}