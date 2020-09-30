using System.Collections.Generic;

namespace EscapeRoom.Classes
{
    public class Card
    {
        public string Id { get; set; } 
        public bool Hidden { get; set; } = true;

        public CardColor Color { get; set; }
        
        public List<Card> Reveals { get; set; }
        public List<Card> Removes { get; set; }

        public Card(string id, CardColor color)
        {
            Id = id;
            Color = color;
            Reveals = new List<Card>();
            Removes = new List<Card>();
        }
    }

    public enum CardColor
    {
        Red,
        Green,
        Blue,
        Grey,
        Yellow
    }
}