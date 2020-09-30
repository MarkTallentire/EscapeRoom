using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using EscapeRoom.Classes;
using Microsoft.AspNetCore.SignalR;

namespace EscapeRoom.SignalR
{
    public class Player
    {
        public string HubConnectionId { get; set; }
        public string PlayerName { get; set; }
    }

    /// <summary>
    /// The game hub doubles up as a gamemanager for this app as it only supports one game at a time
    /// (Remember this is for a team building exercise for work and not a public app)
    /// </summary>
    public class GameHub : Hub
    {
        static List<Card> cards = new List<Card>();
        private static List<Player> players = new List<Player>();
        private static DateTime StartTime = DateTime.Now.AddMinutes(-30);

        static GameHub()
        {
            BuildDeck();
        }

        public static void BuildDeck()
        {
            //Hard code all 66 cards for now to make this work
            cards.AddRange(new Card[66]
            {
                new Card("Start", CardColor.Grey),
                new Card("1", CardColor.Blue),
                new Card("2", CardColor.Red),
                new Card("3", CardColor.Blue),
                new Card("4", CardColor.Blue),
                new Card("5", CardColor.Red),
                new Card("7", CardColor.Grey),
                new Card("8", CardColor.Blue),
                new Card("9", CardColor.Red),
                new Card("10", CardColor.Blue),
                new Card("11", CardColor.Blue),
                new Card("12", CardColor.Blue),
                new Card("16", CardColor.Blue),
                new Card("17", CardColor.Grey),
                new Card("18", CardColor.Grey),
                new Card("21", CardColor.Blue),
                new Card("23", CardColor.Yellow),
                new Card("24", CardColor.Green),
                new Card("25", CardColor.Red),
                new Card("27", CardColor.Red),
                new Card("29", CardColor.Red),
                new Card("30", CardColor.Red),
                new Card("34", CardColor.Red),
                new Card("36", CardColor.Red),
                new Card("37", CardColor.Blue),
                new Card("46", CardColor.Blue),
                new Card("47", CardColor.Red),
                new Card("49", CardColor.Red),
                new Card("51", CardColor.Grey),
                new Card("52", CardColor.Blue),
                new Card("55", CardColor.Blue),
                new Card("57", CardColor.Grey),
                new Card("58", CardColor.Grey),
                new Card("61", CardColor.Red),
                new Card("63", CardColor.Grey),
                new Card("65", CardColor.Red),
                new Card("66", CardColor.Grey),
                new Card("68", CardColor.Grey),
                new Card("72", CardColor.Red),
                new Card("77", CardColor.Grey),
                new Card("78", CardColor.Red),
                new Card("80", CardColor.Grey),
                new Card("84", CardColor.Red),
                new Card("85", CardColor.Grey),
                new Card("90", CardColor.Grey),
                new Card("93", CardColor.Green),
                new Card("94", CardColor.Grey),
                new Card("95", CardColor.Red),
                new Card("97", CardColor.Yellow),
                new Card("99", CardColor.Grey),
                new Card("A", CardColor.Grey),
                new Card("B", CardColor.Grey),
                new Card("D", CardColor.Grey),
                new Card("K", CardColor.Grey),
                new Card("L", CardColor.Grey),
                new Card("N", CardColor.Grey),
                new Card("O", CardColor.Grey),
                new Card("Q", CardColor.Yellow),
                new Card("R", CardColor.Grey),
                new Card("T", CardColor.Grey),
                new Card("U", CardColor.Grey),
                new Card("V", CardColor.Grey),
                new Card("W", CardColor.Yellow),
                new Card("X", CardColor.Grey),
                new Card("Y", CardColor.Grey),
                new Card("Z", CardColor.Grey)
            });

            //Add Reveals
            AddRevealsToCards();

            //Add Discards
            AddDiscardsToCards();
        }

        private static void AddDiscardsToCards()
        {
            cards.SingleOrDefault(x => x.Id == "7").Removes.Add(cards.SingleOrDefault(x => x.Id == "R"));
            
            cards.SingleOrDefault(x => x.Id == "12").Removes.Add(cards.SingleOrDefault(x => x.Id == "2"));
            cards.SingleOrDefault(x => x.Id == "12").Removes.Add(cards.SingleOrDefault(x => x.Id == "10"));
            
            cards.SingleOrDefault(x => x.Id == "30").Removes.Add(cards.SingleOrDefault(x => x.Id == "3"));
            cards.SingleOrDefault(x => x.Id == "30").Removes.Add(cards.SingleOrDefault(x => x.Id == "27"));
            
            cards.SingleOrDefault(x => x.Id == "37").Removes.Add(cards.SingleOrDefault(x => x.Id == "8"));
            cards.SingleOrDefault(x => x.Id == "37").Removes.Add(cards.SingleOrDefault(x => x.Id == "29"));
            
            cards.SingleOrDefault(x => x.Id == "46").Removes.Add(cards.SingleOrDefault(x => x.Id == "9"));
            cards.SingleOrDefault(x => x.Id == "46").Removes.Add(cards.SingleOrDefault(x => x.Id == "37"));
            
            cards.SingleOrDefault(x => x.Id == "47").Removes.Add(cards.SingleOrDefault(x => x.Id == "11"));
            cards.SingleOrDefault(x => x.Id == "47").Removes.Add(cards.SingleOrDefault(x => x.Id == "36"));
            
            cards.SingleOrDefault(x => x.Id == "51").Removes.Add(cards.SingleOrDefault(x => x.Id == "5"));
            cards.SingleOrDefault(x => x.Id == "51").Removes.Add(cards.SingleOrDefault(x => x.Id == "46"));
            
            cards.SingleOrDefault(x => x.Id == "55").Removes.Add(cards.SingleOrDefault(x => x.Id == "21"));
            cards.SingleOrDefault(x => x.Id == "55").Removes.Add(cards.SingleOrDefault(x => x.Id == "34"));
            
            cards.SingleOrDefault(x => x.Id == "58").Removes.Add(cards.SingleOrDefault(x => x.Id == "18"));
            cards.SingleOrDefault(x => x.Id == "58").Removes.Add(cards.SingleOrDefault(x => x.Id == "49"));
            
            cards.SingleOrDefault(x => x.Id == "61").Removes.Add(cards.SingleOrDefault(x => x.Id == "47"));
            cards.SingleOrDefault(x => x.Id == "61").Removes.Add(cards.SingleOrDefault(x => x.Id == "7"));
            
            cards.SingleOrDefault(x => x.Id == "65").Removes.Add(cards.SingleOrDefault(x => x.Id == "4"));
            cards.SingleOrDefault(x => x.Id == "65").Removes.Add(cards.SingleOrDefault(x => x.Id == "61"));
            
            cards.SingleOrDefault(x => x.Id == "66").Removes.Add(cards.SingleOrDefault(x => x.Id == "1"));
            cards.SingleOrDefault(x => x.Id == "66").Removes.Add(cards.SingleOrDefault(x => x.Id == "65"));
            
            cards.SingleOrDefault(x => x.Id == "77").Removes.Add(cards.SingleOrDefault(x => x.Id == "25"));
            cards.SingleOrDefault(x => x.Id == "77").Removes.Add(cards.SingleOrDefault(x => x.Id == "52"));
            
            cards.SingleOrDefault(x => x.Id == "78").Removes.Add(cards.SingleOrDefault(x => x.Id == "24"));
            cards.SingleOrDefault(x => x.Id == "78").Removes.Add(cards.SingleOrDefault(x => x.Id == "A"));
            cards.SingleOrDefault(x => x.Id == "78").Removes.Add(cards.SingleOrDefault(x => x.Id == "O"));
            cards.SingleOrDefault(x => x.Id == "78").Removes.Add(cards.SingleOrDefault(x => x.Id == "N"));
            cards.SingleOrDefault(x => x.Id == "78").Removes.Add(cards.SingleOrDefault(x => x.Id == "U"));
            cards.SingleOrDefault(x => x.Id == "78").Removes.Add(cards.SingleOrDefault(x => x.Id == "66"));
            cards.SingleOrDefault(x => x.Id == "78").Removes.Add(cards.SingleOrDefault(x => x.Id == "85"));
            
            cards.SingleOrDefault(x => x.Id == "84").Removes.Add(cards.SingleOrDefault(x => x.Id == "12"));
            cards.SingleOrDefault(x => x.Id == "84").Removes.Add(cards.SingleOrDefault(x => x.Id == "72"));
            
            cards.SingleOrDefault(x => x.Id == "85").Removes.Add(cards.SingleOrDefault(x => x.Id == "30"));
            cards.SingleOrDefault(x => x.Id == "85").Removes.Add(cards.SingleOrDefault(x => x.Id == "55"));
            
            cards.SingleOrDefault(x => x.Id == "94").Removes.Add(cards.SingleOrDefault(x => x.Id == "78"));
            cards.SingleOrDefault(x => x.Id == "94").Removes.Add(cards.SingleOrDefault(x => x.Id == "16"));
            
            cards.SingleOrDefault(x => x.Id == "97").Removes.Add(cards.SingleOrDefault(x => x.Id == "95"));
            cards.SingleOrDefault(x => x.Id == "97").Removes.Add(cards.SingleOrDefault(x => x.Id == "99"));
            
            cards.SingleOrDefault(x => x.Id == "99").Removes.Add(cards.SingleOrDefault(x => x.Id == "94"));
            cards.SingleOrDefault(x => x.Id == "99").Removes.Add(cards.SingleOrDefault(x => x.Id == "84"));
            
            cards.SingleOrDefault(x => x.Id == "B").Removes.Add(cards.SingleOrDefault(x => x.Id == "23"));
            cards.SingleOrDefault(x => x.Id == "B").Removes.Add(cards.SingleOrDefault(x => x.Id == "D"));
            cards.SingleOrDefault(x => x.Id == "B").Removes.Add(cards.SingleOrDefault(x => x.Id == "Y"));
            cards.SingleOrDefault(x => x.Id == "B").Removes.Add(cards.SingleOrDefault(x => x.Id == "Z"));
            
            cards.SingleOrDefault(x => x.Id == "K").Removes.Add(cards.SingleOrDefault(x => x.Id == "Q"));
            
            cards.SingleOrDefault(x => x.Id == "L").Removes.Add(cards.SingleOrDefault(x => x.Id == "93"));
            
            // cards.SingleOrDefault(x => x.Id == "Q").Removes.Add(cards.SingleOrDefault(x => x.Id == "58"));
            
            
            cards.SingleOrDefault(x => x.Id == "X").Removes.Add(cards.SingleOrDefault(x => x.Id == "W"));
            cards.SingleOrDefault(x => x.Id == "X").Removes.Add(cards.SingleOrDefault(x => x.Id == "T"));
        }

        private static void AddRevealsToCards()
        {
            cards.SingleOrDefault(x => x.Id == "Start").Reveals.Add(cards.SingleOrDefault(x => x.Id == "8"));
            cards.SingleOrDefault(x => x.Id == "Start").Reveals.Add(cards.SingleOrDefault(x => x.Id == "N"));
            cards.SingleOrDefault(x => x.Id == "Start").Reveals.Add(cards.SingleOrDefault(x => x.Id == "9"));
            cards.SingleOrDefault(x => x.Id == "Start").Reveals.Add(cards.SingleOrDefault(x => x.Id == "29"));
            cards.SingleOrDefault(x => x.Id == "Start").Reveals.Add(cards.SingleOrDefault(x => x.Id == "W"));
            cards.SingleOrDefault(x => x.Id == "Start").Reveals.Add(cards.SingleOrDefault(x => x.Id == "34"));

            cards.SingleOrDefault(x => x.Id == "46").Reveals.Add(cards.SingleOrDefault(x => x.Id == "T"));

            cards.SingleOrDefault(x => x.Id == "51").Reveals.Add(cards.SingleOrDefault(x => x.Id == "D"));
            cards.SingleOrDefault(x => x.Id == "51").Reveals.Add(cards.SingleOrDefault(x => x.Id == "21"));

            cards.SingleOrDefault(x => x.Id == "58").Reveals.Add(cards.SingleOrDefault(x => x.Id == "Q"));

            cards.SingleOrDefault(x => x.Id == "B").Reveals.Add(cards.SingleOrDefault(x => x.Id == "93"));
            cards.SingleOrDefault(x => x.Id == "B").Reveals.Add(cards.SingleOrDefault(x => x.Id == "11"));
            cards.SingleOrDefault(x => x.Id == "B").Reveals.Add(cards.SingleOrDefault(x => x.Id == "27"));
            cards.SingleOrDefault(x => x.Id == "B").Reveals.Add(cards.SingleOrDefault(x => x.Id == "4"));

            cards.SingleOrDefault(x => x.Id == "K").Reveals.Add(cards.SingleOrDefault(x => x.Id == "1"));
            cards.SingleOrDefault(x => x.Id == "K").Reveals.Add(cards.SingleOrDefault(x => x.Id == "24"));
            cards.SingleOrDefault(x => x.Id == "K").Reveals.Add(cards.SingleOrDefault(x => x.Id == "16"));
            cards.SingleOrDefault(x => x.Id == "K").Reveals.Add(cards.SingleOrDefault(x => x.Id == "A"));
            cards.SingleOrDefault(x => x.Id == "K").Reveals.Add(cards.SingleOrDefault(x => x.Id == "U"));
            cards.SingleOrDefault(x => x.Id == "K").Reveals.Add(cards.SingleOrDefault(x => x.Id == "2"));

            cards.SingleOrDefault(x => x.Id == "L").Reveals.Add(cards.SingleOrDefault(x => x.Id == "72"));
            cards.SingleOrDefault(x => x.Id == "L").Reveals.Add(cards.SingleOrDefault(x => x.Id == "95"));
            
            cards.SingleOrDefault(x => x.Id == "N").Reveals.Add(cards.SingleOrDefault(x => x.Id == "O"));
            cards.SingleOrDefault(x => x.Id == "N").Reveals.Add(cards.SingleOrDefault(x => x.Id == "36"));
            
            cards.SingleOrDefault(x => x.Id == "Q").Reveals.Add(cards.SingleOrDefault(x => x.Id == "3"));
            
            cards.SingleOrDefault(x => x.Id == "X").Reveals.Add(cards.SingleOrDefault(x => x.Id == "52"));
            cards.SingleOrDefault(x => x.Id == "X").Reveals.Add(cards.SingleOrDefault(x => x.Id == "5"));
            
            cards.SingleOrDefault(x => x.Id == "77").Reveals.Add(cards.SingleOrDefault(x => x.Id == "10"));
            cards.SingleOrDefault(x => x.Id == "77").Reveals.Add(cards.SingleOrDefault(x => x.Id == "Y"));
            cards.SingleOrDefault(x => x.Id == "77").Reveals.Add(cards.SingleOrDefault(x => x.Id == "R"));
            cards.SingleOrDefault(x => x.Id == "77").Reveals.Add(cards.SingleOrDefault(x => x.Id == "Z"));
            cards.SingleOrDefault(x => x.Id == "77").Reveals.Add(cards.SingleOrDefault(x => x.Id == "23"));
        }


        public async Task TryAddCards(string red, string blue)
        {
            await Clients.All.SendAsync("tryAddCards", "");
            
            if (red == "4071" && blue == "4071")
            {
                await ShowCard("X");
                await Clients.All.SendAsync("tryAddCards", "Revealed Card X");
                return;
            }

            if (red == "25" && blue == "25")
            {
                await ShowCard("25");
                await Clients.All.SendAsync("tryAddCards", "Revealed Card 25");
                return;
            }

            if (red == "L" && blue == "L")
            {
                await ShowCard("L");
                await Clients.All.SendAsync("tryAddCards", "Revealed Card L");
                return;
            }

            if (red == "18" && blue == "18")
            {
                await ShowCard("18");
                await Clients.All.SendAsync("tryAddCards", "Revealed Card 18");
                return;
            }

            if (red == "49" && blue == "49")
            {
                await ShowCard("49");
                await Clients.All.SendAsync("tryAddCards", "Revealed Card 49");
                return;
            }

            if (red == "07" && blue == "07")
            {
                await ShowCard("7");
                await Clients.All.SendAsync("tryAddCards", "Revealed Card 7");
                return;
            }

            if (red == "9153" && blue == "9153")
            {
                await ShowCard("B");
                await Clients.All.SendAsync("tryAddCards", "Revealed Card B");
                return;
            }

            if (red == "4444" && blue == "4444")
            {
                await ShowCard("K");
                await Clients.All.SendAsync("tryAddCards", "Revealed Card K");
                return;
            }

            if (red == "78" && blue == "78")
            {
                await ShowCard("78");
                await Clients.All.SendAsync("tryAddCards", "Revealed Card 78");
                return;
            }


            var blueCard = cards.SingleOrDefault(x => x.Id == blue);
            var redCard = cards.SingleOrDefault(x => x.Id == red);

            if (blueCard == null || redCard == null)
            {
                await Clients.All.SendAsync("Incorrect Card Combination Submitted");
                return;
            }

            var combinedCard = cards.SingleOrDefault(x => x.Id == (int.Parse(blue) + int.Parse(red)).ToString());
            if (combinedCard != null)
            {
                await Clients.All.SendAsync("tryAddCards",
                    $"Correct card combination {red} + {blue} unlocked card {combinedCard.Id}");
                await ShowCard(combinedCard.Id);
            }
        }

        public async Task StartGame()
        {
            StartTime = DateTime.Now.AddMinutes(-30);
        }

        public async Task PlayIntro()
        {
            await Clients.All.SendAsync("playIntro", true);
        }

        public async Task PlayClockHint()
        {
            await Clients.All.SendAsync("playClockHint", true);
        }

        public async Task GetTime()
        {
            await Clients.All.SendAsync("getTime", StartTime);
        }


        public async Task JoinGame(string connectionId)
        {
            if (players.Any(x => x.HubConnectionId == connectionId))
                return;

            var player = new Player()
            {
                HubConnectionId = connectionId
            };

            players.Add(player);
        }

        public async Task GetPlayerList()
        {
            await Clients.All.SendAsync("playerList", players);
        }

        public async Task GetDisplayed()
        {
            await Clients.All.SendAsync("getDisplayed", cards.Where(x => x.Hidden == false).ToList());
        }

        public async Task ShowCard(string number)
        {
            var card = cards.SingleOrDefault(x => x.Id == number);
            if (card != null)
                card.Hidden = false;

            foreach (var reveal in card.Reveals)
                await ShowCard(reveal.Id);

            foreach (var remove in card.Removes)
                await HideCard(remove.Id);


            await GetDisplayed();
            await GetCards();
        }

        public async Task HideCard(string number)
        {
            var card = cards.SingleOrDefault(x => x.Id == number);
            if (card != null)
                card.Hidden = true;

            await GetDisplayed();
            await GetCards();
        }

        public override async Task OnDisconnectedAsync(Exception exception)
        {
            var player = players.SingleOrDefault(x => x.HubConnectionId == Context.ConnectionId);
            if (player != null)
                players.Remove(player);

            await GetPlayerList();
        }

        public async Task GetCards()
        {

            await Clients.All.SendAsync("getCards", cards.ToList());
        }

        public async Task GiveHint(string hint)
        {
            await Clients.All.SendAsync("giveHint", hint);
        }
    }
}