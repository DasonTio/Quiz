using System;
using System.Collections.Generic;
using System.Linq;
using System.Collections.ObjectModel;

namespace FlashCard;

public class Deck : ICloneable
{
    public int DeckId { get; set; }

    public string? Label { get; set; }
    public string? Description { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public DateTime? OpenedAt { get; set; }

    public ICollection<Card> Cards { get; set; }

    public Deck()
    {
        Cards = [];
    }

    public object Clone()
    {
        return this.MemberwiseClone();
    }

    public object DeepClone()
    {
        // Deep clone
        Deck clonedDeck = (Deck)this.MemberwiseClone();
        clonedDeck.Cards = this.Cards.Select(card => (Card)card.DeepClone()).ToList();
        return clonedDeck;
    }
}
