using System.Collections.Generic;

namespace GildedRose.Console
{
    public class Program
    {
        public IList<Item> Items;
        static void Main(string[] args)
        {
            System.Console.WriteLine("OMGHAI!");

            var app = new Program()
                          {
                              Items = new List<Item>
                                          {
                                              new Item {Name = "+5 Dexterity Vest", SellIn = 10, Quality = 20},
                                              new Item {Name = "Aged Brie", SellIn = 2, Quality = 0},
                                              new Item {Name = "Elixir of the Mongoose", SellIn = 5, Quality = 7},
                                              new Item {Name = "Sulfuras, Hand of Ragnaros", SellIn = 0, Quality = 80},
                                              new Item
                                                  {
                                                      Name = "Backstage passes to a TAFKAL80ETC concert",
                                                      SellIn = 15,
                                                      Quality = 20
                                                  },
                                              new Item {Name = "Conjured Mana Cake", SellIn = 3, Quality = 6}
                                          }

                          };

            app.UpdateQuality();

            System.Console.WriteLine("Press any key to exit...");

        }

        public void UpdateQuality()
        {
            
            foreach (var item in Items)
            {

                /* This is specific to the current items, it works, but the one below works for a new category of items (Backstage Passes, Sulfuras, ...)
                switch(item.Name)
                {
                    case "Aged Brie":
                        UpdateQuality(item, +1);
                        break;
                    case "Sulfuras, Hand of Ragnaros":
                        // SulfurasNeverDecreasesQuality
                        // SulfurasNeverToBeSold
                        // SulfurasQualityIs80NeverAlters
                        item.SellIn = 0;
                        item.Quality = 80;
                        continue;                        
                    case "Backstage passes to a TAFKAL80ETC concert":
                        UpdateBackstage(item);
                        break;
                    case "Conjured Mana Cake":
                        UpdateQuality(item, -2);
                        break;
                    default: 
                        UpdateQuality(item, -1);
                        break;
                }
                */
                
                if (item.Name.StartsWith("Aged Brie",System.StringComparison.InvariantCultureIgnoreCase ) )
                {
                    UpdateQuality(item, +1);
                }
                else if (item.Name.StartsWith("Sulfuras", System.StringComparison.InvariantCultureIgnoreCase))
                {
                    // SulfurasNeverDecreasesQuality
                    // SulfurasNeverToBeSold
                    // SulfurasQualityIs80NeverAlters
                    item.SellIn = 0;
                    item.Quality = 80;
                    continue;
                }
                else if (item.Name.StartsWith("Backstage passes", System.StringComparison.InvariantCultureIgnoreCase))
                {
                    UpdateBackstage(item);
                }
                else if (item.Name.StartsWith("Conjured", System.StringComparison.InvariantCultureIgnoreCase))
                {
                    UpdateQuality(item, -2);
                }
                else
                {
                    UpdateQuality(item, -1);
                }
                
                UpdateSellIn(item);
            }
        }

        public void UpdateBackstage(Item item)
        {
            // BackstagePassesQualityDropsTo0AfterTheConcert
            // BackstagePassesQualityIncreasesby3WhenThereAre5DaysOrLess
            // BackstagePassesQualityIncreasesby2WhenThereAre10DaysOrLess            
            if (item.SellIn <= 0)
            {
                item.Quality = 0;
            }
            else
            if (item.SellIn <= 5)
            {
                UpdateQuality(item, +3);
            }else                
            if(item.SellIn <= 10)
            {
                UpdateQuality(item, +2);
            }
            else
            {
                UpdateQuality(item, +1);                   
            }

        }

        public void UpdateQuality(Item item, int quality)
        {
            //SellByDatePassed - Once the sell by date has passed, Quality degrades twice as fast
            if(item.SellIn <= 0)
            {
                quality *= 2;
            }

            item.Quality += quality;
            item.Quality = item.Quality < 0 ? 0 : item.Quality; // QualityIsNeverNegative

            item.Quality = item.Quality > 50 ? 50 : item.Quality; // ItemNeverQualityIncreaseAbove50
        }

        public void UpdateSellIn(Item item)
        {            
            item.SellIn -= 1;
            item.SellIn = item.SellIn < 0 ? 0 : item.SellIn; // SellInIsNeverNegative
        }

    }

    public class Item
    {
        public string Name { get; set; }

        public int SellIn { get; set; }

        public int Quality { get; set; }
    }

}
