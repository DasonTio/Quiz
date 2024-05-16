using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.IO;
using System.Collections.ObjectModel;

namespace FlashCard;

public class DatabaseConfig : DbContext
{
    public DbSet<Deck> Decks { get; set; }
    public DbSet<Card> Cards { get; set; }
    private string DbPath { get; }

    public DatabaseConfig()
    {
        var folder = Environment.SpecialFolder.LocalApplicationData;
        var path = Environment.GetFolderPath(folder);
        DbPath = System.IO.Path.Join(path, "flashcards.db");

        Database.EnsureCreated();
        SeedData();
    }

    protected override void OnConfiguring(DbContextOptionsBuilder options)
           => options.UseSqlite($"Data Source={DbPath}");

    public void SeedData()
    {
        if (!Decks.Any())
        {
            var deck1 = new Deck { Label = "Trigonometry Card", Description = "Build a habit" };
            var deck2 = new Deck { Label = "Programming Card", Description = "Mastering code" };

            deck1.Cards.Add(new Card { Front = "Card 1 Front", Back = "Card 1 Back" });
            deck1.Cards.Add(new Card { Front = "Card 2 Front", Back = "Card 2 Back" });

            deck2.Cards.Add(new Card { Front = "Card A Front", Back = "Card A Back" });
            deck2.Cards.Add(new Card { Front = "Card B Front", Back = "Card B Back" });

            Decks.AddRange(deck1, deck2);
            SaveChanges();
        }
    }
}