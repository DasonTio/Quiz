using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System;
using System.IO;

namespace FlashCard;

public class Card : ICloneable
{
    public int? DeckId { get; set; }
    public int? CardId { get; set; }
    public string? Front { get; set; }
    public string? Back { get; set; }
    public object Clone()
    {
        // Shallow clone
        return this.MemberwiseClone();
    }

    public object DeepClone()
    {
        // Deep clone
        return this.MemberwiseClone();
    }
}

