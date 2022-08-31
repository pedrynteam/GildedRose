using GildedRose.Console;
using System.Collections.Generic;
using Xunit;
using System.Linq;

namespace GildedRosesTests
{
    public class GildedRosesUnitTest
    {
        private Program handler;

        [Fact]
        // Conjured" items degrade in Quality twice as fast as normal items
        public void ConjuredItemsDegradeQualityTwiceAsFast()
        {
            //Arrange
            List<Item> Items = new()
            {
                new Item { Name = "Conjured Mana Cake", SellIn = 4, Quality = 6 }
            };

            handler = new Program { Items = Items };

            // Act
            handler.UpdateQuality();

            // Assert
            Assert.Equal(4, handler.Items.FirstOrDefault().Quality);
        }

        [Fact]
        // Once the sell by date has passed, Quality degrades twice as fast
        public void SellByDatePassed()
        {
            //Arrange
            List<Item> Items = new()
            {
                new Item { Name = "+5 Dexterity Vest", SellIn = 0, Quality = 4 }
            };

            handler = new Program { Items = Items };

            // Act
            handler.UpdateQuality();

            // Assert
            Assert.Equal(2, handler.Items.FirstOrDefault().Quality);
        }

        [Fact]
        // - The Quality of an item is never negative
        public void QualityIsNeverNegative()
        {
            //Arrange
            List<Item> Items = new()
            {
                new Item { Name = "+5 Dexterity Vest", SellIn = 0, Quality = 0 }
            };

            handler = new Program { Items = Items };

            // Act
            handler.UpdateQuality();

            // Assert
            Assert.Equal(0, handler.Items.FirstOrDefault().Quality);
        }

        [Fact]
        // "Aged Brie" actually increases in Quality the older it gets
        public void AgedBrieIncreasesQuality()
        {
            //Arrange
            List<Item> Items = new()
            {
                new Item { Name = "Aged Brie", SellIn = 2, Quality = 4 }
            };

            handler = new Program { Items = Items };

            // Act
            handler.UpdateQuality();

            // Assert
            Assert.Equal(5, handler.Items.FirstOrDefault().Quality);
            Assert.Equal(1, handler.Items.FirstOrDefault().SellIn);
        }

        [Fact]
        // The Quality of an item is never more than 50
        public void QualityNeverMoreThan50()
        {
            //Arrange
            List<Item> Items = new()
            {
                new Item { Name = "Aged Brie", SellIn = 0, Quality = 50 }
            };

            handler = new Program { Items = Items };

            // Act
            handler.UpdateQuality();

            // Assert
            Assert.Equal(50, handler.Items.FirstOrDefault().Quality);
            Assert.Equal(0, handler.Items.FirstOrDefault().SellIn);
        }

        [Fact]
        // "Sulfuras", being a legendary item, never has to be sold or decreases in Quality
        public void SulfurasNeverToBeSold()
        {
            //Arrange
            List<Item> Items = new()
            {
                new Item { Name = "Sulfuras, Hand of Ragnaros", SellIn = 2, Quality = 80 }
            };

            handler = new Program { Items = Items };

            // Act
            handler.UpdateQuality();

            // Assert
            Assert.Equal(0, handler.Items.FirstOrDefault().SellIn);
        }

        [Fact]
        // "Sulfuras", being a legendary item, never has to be sold or decreases in Quality
        public void SulfurasNeverDecreasesQuality()
        {
            //Arrange
            List<Item> Items = new()
            {
                new Item { Name = "Sulfuras, Hand of Ragnaros", SellIn = 0, Quality = 80 }
            };

            handler = new Program { Items = Items };

            // Act
            handler.UpdateQuality();

            // Assert
            Assert.Equal(80, handler.Items.FirstOrDefault().Quality);
            Assert.Equal(0, handler.Items.FirstOrDefault().SellIn);
        }

        [Fact]
        // Backstage passes Quality increases by 2 when there are 10 days or less
        public void BackstagePassesQualityIncreasesby2WhenThereAre10DaysOrLess()
        {
            //Arrange
            List<Item> Items = new()
            {
                new Item { Name = "Backstage passes to a TAFKAL80ETC concert", SellIn = 10, Quality = 12 }
            };

            handler = new Program { Items = Items };

            // Act
            handler.UpdateQuality();

            // Assert
            Assert.Equal(14, handler.Items.FirstOrDefault().Quality);
        }

        [Fact]
        // Backstage passes Quality by 3 when there are 5 days or less 
        public void BackstagePassesQualityIncreasesby3WhenThereAre5DaysOrLess()
        {
            //Arrange
            List<Item> Items = new()
            {
                new Item { Name = "Backstage passes to a TAFKAL80ETC concert", SellIn = 5, Quality = 4 }
            };

            handler = new Program { Items = Items };

            // Act
            handler.UpdateQuality();

            // Assert
            Assert.Equal(7, handler.Items.FirstOrDefault().Quality);
        }

        [Fact]
        // Backstage passes Quality drops to 0 after the concert
        public void BackstagePassesQualityDropsTo0AfterTheConcert()
        {
            //Arrange
            List<Item> Items = new()
            {
                new Item { Name = "Backstage passes to a TAFKAL80ETC concert", SellIn = 0, Quality = 4 }
            };

            handler = new Program { Items = Items };

            // Act
            handler.UpdateQuality();

            // Assert
            Assert.Equal(0, handler.Items.FirstOrDefault().Quality);
        }

        [Fact]
        // An item can never have its Quality increase above 50
        public void ItemNeverQualityIncreaseAbove50()
        {
            //Arrange
            List<Item> Items = new()
            {
                new Item { Name = "Backstage passes to a TAFKAL80ETC concert", SellIn = 3, Quality = 49 }
            };

            handler = new Program { Items = Items };

            // Act
            handler.UpdateQuality();

            // Assert
            Assert.Equal(50, handler.Items.FirstOrDefault().Quality);
        }

        [Fact]
        // "Sulfuras" is a legendary item and as such its Quality is 80 and it never alters.
        public void SulfurasQualityIs80NeverAlters()
        {
            //Arrange
            List<Item> Items = new()
            {
                new Item { Name = "Sulfuras, Hand of Ragnaros", SellIn = 0, Quality = 82 }
            };

            handler = new Program { Items = Items };

            // Act
            handler.UpdateQuality();

            // Assert
            Assert.Equal(80, handler.Items.FirstOrDefault().Quality);
        }

        [Fact]        
        public void SellInDecreaseTest()
        {
            //Arrange
            List<Item> Items = new()
            {
                new Item { Name = "+5 Dexterity Vest", SellIn = 10, Quality = 20 }
            };

            handler = new Program { Items = Items };

            // Act
            handler.UpdateSellIn(Items.FirstOrDefault());

            // Assert
            Assert.Equal(9, handler.Items.FirstOrDefault().SellIn);
        }

        [Fact]
        public void SellInIsNeverNegative()
        {
            //Arrange
            List<Item> Items = new()
            {
                new Item { Name = "+5 Dexterity Vest", SellIn = 0, Quality = 20 }
            };

            handler = new Program { Items = Items };

            // Act
            handler.UpdateSellIn(Items.FirstOrDefault());

            // Assert
            Assert.Equal(0, handler.Items.FirstOrDefault().SellIn);
        }

        [Fact]
        public void UpdateQualityDecreaseTest()
        {
            //Arrange
            List<Item> Items = new()
            {
                new Item { Name = "+5 Dexterity Vest", SellIn = 10, Quality = 20 }
            };

            handler = new Program { Items = Items };

            // Act
            handler.UpdateQuality(Items.FirstOrDefault(), -1);

            // Assert
            Assert.Equal(19, handler.Items.FirstOrDefault().Quality);
        }

        [Fact]
        public void UpdateQualityIncreaseTest()
        {
            //Arrange
            List<Item> Items = new()
            {
                new Item { Name = "+5 Dexterity Vest", SellIn = 10, Quality = 20 }
            };

            handler = new Program { Items = Items };

            // Act
            handler.UpdateQuality(Items.FirstOrDefault(), 1);

            // Assert
            Assert.Equal(21, handler.Items.FirstOrDefault().Quality);
        }

        [Fact]
        public void UpdateQualitySellByDatePassed()
        {
            //Arrange
            List<Item> Items = new()
            {
                new Item { Name = "+5 Dexterity Vest", SellIn = 0, Quality = 20 }
            };

            handler = new Program { Items = Items };

            // Act
            handler.UpdateQuality(Items.FirstOrDefault(), -1);

            // Assert
            Assert.Equal(18, handler.Items.FirstOrDefault().Quality);
        }

        [Fact]
        public void QualityIsNeverNegativeTest()
        {
            //Arrange
            List<Item> Items = new()
            {
                new Item { Name = "+5 Dexterity Vest", SellIn = 0, Quality = 0 }
            };

            handler = new Program { Items = Items };

            // Act
            handler.UpdateQuality(Items.FirstOrDefault(), -1);

            // Assert
            Assert.Equal(0, handler.Items.FirstOrDefault().Quality);
        }

        [Fact]
        public void ItemNeverQualityIncreaseAbove50Test()
        {
            //Arrange
            List<Item> Items = new()
            {
                new Item { Name = "+5 Dexterity Vest", SellIn = 0, Quality = 50 }
            };

            handler = new Program { Items = Items };

            // Act
            handler.UpdateQuality(Items.FirstOrDefault(), 1);

            // Assert
            Assert.Equal(50, handler.Items.FirstOrDefault().Quality);
        }
        
        [Fact]        
        public void UpdateBackstageBackstagePassesQualityIncreasesby3WhenThereAre5DaysOrLess()
        {
            //Arrange
            List<Item> Items = new()
            {
                new Item { Name = "Backstage passes to a TAFKAL80ETC concert", SellIn = 3, Quality = 12 }
            };

            handler = new Program { Items = Items };

            // Act
            handler.UpdateBackstage(Items.FirstOrDefault());

            // Assert
            Assert.Equal(15, handler.Items.FirstOrDefault().Quality);
        }

        [Fact]
        public void UpdateBackstageBackstagePassesQualityIncreasesby2WhenThereAre10DaysOrLess()
        {
            //Arrange
            List<Item> Items = new()
            {
                new Item { Name = "Backstage passes to a TAFKAL80ETC concert", SellIn = 7, Quality = 12 }
            };

            handler = new Program { Items = Items };

            // Act
            handler.UpdateBackstage(Items.FirstOrDefault());

            // Assert
            Assert.Equal(14, handler.Items.FirstOrDefault().Quality);
        }

        [Fact]
        public void UpdateBackstageBackstagePassesQualityIncreasesby1WhenThereAre11DaysOrMore()
        {
            //Arrange
            List<Item> Items = new()
            {
                new Item { Name = "Backstage passes to a TAFKAL80ETC concert", SellIn = 11, Quality = 12 }
            };

            handler = new Program { Items = Items };

            // Act
            handler.UpdateBackstage(Items.FirstOrDefault());

            // Assert
            Assert.Equal(13, handler.Items.FirstOrDefault().Quality);
        }

        [Fact]
        public void UpdateBackstagePassesQualityDropsTo0AfterTheConcert()
        {
            //Arrange
            List<Item> Items = new()
            {
                new Item { Name = "Backstage passes to a TAFKAL80ETC concert", SellIn = 0, Quality = 12 }
            };

            handler = new Program { Items = Items };

            // Act
            handler.UpdateBackstage(Items.FirstOrDefault());

            // Assert
            Assert.Equal(0, handler.Items.FirstOrDefault().Quality);
        }
    }
}
