using System.Collections.Generic;
using Xunit;

namespace csharpcore
{
    public class GildedRoseTest
    {
        private const string NormalItemName = "foo";
        private const string AgedBrieName = "Aged Brie";
        private const string SulfurasHandOfRagnarosName = "Sulfuras, Hand of Ragnaros";
        private const string BackstagePassesName = "Backstage passes to a TAFKAL80ETC concert";

        private const int MaximumQuality = 50;

        [Fact]
        public void NormalItemDecreasesSellIn()
        {
            const int sellIn = 2;
            IList<Item> items = new List<Item> {new Item {Name = NormalItemName, SellIn = sellIn, Quality = 0}};
            GildedRose app = new GildedRose(items);
            app.UpdateQuality();
            Assert.Equal(sellIn - 1, items[0].SellIn);
        }

        [Fact]
        public void GildedRoseWithTwoItemsAppliesSellInDownToBoth()
        {
            const int sellIn = 2;
            IList<Item> items = new List<Item>
            {
                new Item {Name = SulfurasHandOfRagnarosName, SellIn = sellIn, Quality = 0},
                new Item {Name = NormalItemName, SellIn = sellIn, Quality = 5}
            };
            GildedRose app = new GildedRose(items);
            app.UpdateQuality();
            Assert.Equal(sellIn - 1, items[1].SellIn);
        }      
        
        [Fact]
        public void GildedRoseWithTwoItemsAppliesQualityDownToBoth()
        {
            const int quality = 5;
            IList<Item> items = new List<Item>
            {
                new Item {Name = SulfurasHandOfRagnarosName, SellIn = 2, Quality = 0},
                new Item {Name = NormalItemName, SellIn = 4, Quality = quality}
            };
            GildedRose app = new GildedRose(items);
            app.UpdateQuality();
            Assert.Equal(quality - 1, items[1].Quality);
        }

        [Fact]
        public void NormalItemZeroSellIn()
        {
            const int sellIn = 0;
            IList<Item> items = new List<Item> {new Item {Name = NormalItemName, SellIn = sellIn, Quality = 0}};
            GildedRose app = new GildedRose(items);
            app.UpdateQuality();
            Assert.Equal(sellIn - 1, items[0].SellIn);
        }

        [Fact]
        public void NormalItemDecreasesQualityOneBeforeSellIn()
        {
            const int quality = 2;
            IList<Item> items = new List<Item> {new Item {Name = NormalItemName, SellIn = 1, Quality = quality}};
            GildedRose app = new GildedRose(items);
            app.UpdateQuality();
            Assert.Equal(quality - 1, items[0].Quality);
        }

        [Fact]
        public void NormalItemDecreasesQualityByTwoWithZeroSellIn()
        {
            const int quality = 2;
            IList<Item> items = new List<Item> {new Item {Name = NormalItemName, SellIn = 0, Quality = quality}};
            GildedRose app = new GildedRose(items);
            app.UpdateQuality();
            Assert.Equal(quality - 2, items[0].Quality);
        }

        [Fact]
        public void BrieQualityGoesUpTwoUnitsAfterZeroSellIn()
        {
            const int quality = 2;
            IList<Item> items = new List<Item> {new Item {Name = AgedBrieName, SellIn = 0, Quality = quality}};
            GildedRose app = new GildedRose(items);
            app.UpdateQuality();
            Assert.Equal(quality + 2, items[0].Quality);
        }

        [Fact]
        public void BrieQualityGoesUpOneUnitsBeforeSellIn()
        {
            const int quality = 2;
            IList<Item> items = new List<Item> {new Item {Name = AgedBrieName, SellIn = 2, Quality = quality}};
            GildedRose app = new GildedRose(items);
            app.UpdateQuality();
            Assert.Equal(quality + 1, items[0].Quality);
        }

        [Fact]
        public void BrieQuality49AfterSellInGoesToFifty()
        {
            IList<Item> items = new List<Item> {new Item {Name = AgedBrieName, SellIn = -1, Quality = 49}};
            GildedRose app = new GildedRose(items);
            app.UpdateQuality();
            Assert.Equal(MaximumQuality, items[0].Quality);
        }

        [Fact]
        public void BrieQuality50BeforeSellInStaysAtFifty()
        {
            IList<Item> items = new List<Item> {new Item {Name = AgedBrieName, SellIn = 1, Quality = MaximumQuality}};
            GildedRose app = new GildedRose(items);
            app.UpdateQuality();
            Assert.Equal(MaximumQuality, items[0].Quality);
        }

        [Fact]
        public void SulfurasSellInIsInvariant()
        {
            const int sellIn = 1;
            IList<Item> items = new List<Item>
                {new Item {Name = SulfurasHandOfRagnarosName, SellIn = sellIn, Quality = 50}};
            GildedRose app = new GildedRose(items);
            app.UpdateQuality();
            Assert.Equal(sellIn, items[0].SellIn);
        }

        [Fact]
        public void SulfurasQualityIsInvariant()
        {
            const int quality = 53;
            IList<Item> items = new List<Item>
                {new Item {Name = SulfurasHandOfRagnarosName, SellIn = 1, Quality = quality}};
            GildedRose app = new GildedRose(items);
            app.UpdateQuality();
            Assert.Equal(quality, items[0].Quality);
        }

        [Fact]
        public void BackstagePasses11DaysSellInIncreasesQuality()
        {
            const int quality = 1;
            IList<Item> items = new List<Item> {new Item {Name = BackstagePassesName, SellIn = 11, Quality = quality}};
            GildedRose app = new GildedRose(items);
            app.UpdateQuality();
            Assert.Equal(quality + 1, items[0].Quality);
        }

        [Fact]
        public void BackstagePasses10DaysSellInIncreasesQualityByTwo()
        {
            const int quality = 1;
            IList<Item> items = new List<Item> {new Item {Name = BackstagePassesName, SellIn = 10, Quality = quality}};
            GildedRose app = new GildedRose(items);
            app.UpdateQuality();
            Assert.Equal(quality + 2, items[0].Quality);
        }

        [Fact]
        public void BackstagePasses6DaysSellInIncreasesQualityByTwo()
        {
            const int quality = 1;
            IList<Item> items = new List<Item> {new Item {Name = BackstagePassesName, SellIn = 6, Quality = quality}};
            GildedRose app = new GildedRose(items);
            app.UpdateQuality();
            Assert.Equal(quality + 2, items[0].Quality);
        }

        [Fact]
        public void BackstagePasses5DaysSellInIncreasesQualityByThree()
        {
            const int quality = 1;
            IList<Item> items = new List<Item> {new Item {Name = BackstagePassesName, SellIn = 5, Quality = quality}};
            GildedRose app = new GildedRose(items);
            app.UpdateQuality();
            Assert.Equal(quality + 3, items[0].Quality);
        }

        [Fact]
        public void BackstagePassesQuality49With5DaysGoesTo50()
        {
            IList<Item> items = new List<Item> {new Item {Name = BackstagePassesName, SellIn = 5, Quality = 49}};
            GildedRose app = new GildedRose(items);
            app.UpdateQuality();
            Assert.Equal(MaximumQuality, items[0].Quality);
        }

        [Fact]
        public void BackstagePassesSellIn0GoesToQuality0()
        {
            IList<Item> items = new List<Item> {new Item {Name = BackstagePassesName, SellIn = 0, Quality = 49}};
            GildedRose app = new GildedRose(items);
            app.UpdateQuality();
            Assert.Equal(0, items[0].Quality);
        }

        [Fact(Skip = "Not implemented")]
        public void ConjuredBreadBeforeSellInQualityDownTwo()
        {
            const int quality = 49;
            IList<Item> items = new List<Item> {new Item {Name = "Conjured Bread", SellIn = 2, Quality = quality}};
            GildedRose app = new GildedRose(items);
            app.UpdateQuality();
            Assert.Equal(quality - 2, items[0].Quality);
        }

        [Fact(Skip = "Not implemented")]
        public void ConjuredMuffinsBeforeSellInQualityDownTwo()
        {
            const int quality = 49;
            IList<Item> items = new List<Item> {new Item {Name = "Conjured Muffins", SellIn = 2, Quality = quality}};
            GildedRose app = new GildedRose(items);
            app.UpdateQuality();
            Assert.Equal(quality - 2, items[0].Quality);
        }
        
        [Fact(Skip = "Not implemented")]
        public void ConjuredNoSpaceBeforeSellInQualityDownOne()
        {
            const int quality = 49;
            IList<Item> items = new List<Item> {new Item {Name = "ConjuredNoSpace", SellIn = 2, Quality = quality}};
            GildedRose app = new GildedRose(items);
            app.UpdateQuality();
            Assert.Equal(quality - 1, items[0].Quality);
        }

        [Fact(Skip = "Not implemented")]
        public void ConjuredBreadZeroSellInQualityDownFour()
        {
            const int quality = 49;
            IList<Item> items = new List<Item> {new Item {Name = "Conjured Bread", SellIn = 0, Quality = quality}};
            GildedRose app = new GildedRose(items);
            app.UpdateQuality();
            Assert.Equal(quality - 4, items[0].Quality);
        }      
        
        [Fact(Skip = "Not implemented")]
        public void ConjuredBreadQuality1QualityTo0()
        {
            IList<Item> items = new List<Item> {new Item {Name = "Conjured Bread", SellIn = 0, Quality = 1}};
            GildedRose app = new GildedRose(items);
            app.UpdateQuality();
            Assert.Equal(0, items[0].Quality);
        }
    }
}