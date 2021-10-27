using Xunit;
using GildedRose;
using System.Collections.Generic;

namespace GildedRose.Tests
{
    public class TestAssemblyTests
    {

        Program program = new Program();

        [Fact]
        public void SulfurasNeverDecreaseInQualityOrSellIn_expect_Q80_S0()
        {
            program.AddItem(new Sulfuras
            {
                Name = "Sulfuras, Hand of Ragnaros",
                SellIn = 0,
                Quality = 80
            });
            program.UpdateQuality();
            var localItems = program.GetItems();
            //"Sulfuras", being a legendary item, never has to be sold or decreases in Quality
            Assert.Equal(0, localItems[localItems.Count - 1].SellIn);
            Assert.Equal(80, localItems[localItems.Count - 1].Quality);
        }

        [Fact]
        public void Quality_Never_More_Than_50_expected_Q50_S9()
        {
            //An Item cannot have a quality higher than 50
            program.AddItem(new AgedBrie
            {
                Name = "Aged Brie",
                SellIn = 10,
                Quality = 50,
            });
            program.UpdateQuality();
            var localItems = program.GetItems();
            Assert.Equal(9, localItems[localItems.Count - 1].SellIn);
            Assert.Equal(50, localItems[localItems.Count - 1].Quality);
        }

        [Fact]
        public void Quality_never_negative_expected_Q0_S9()
        {
            program.AddItem(new Item
            {
                Name = "+5 Dexterity Vest",
                SellIn = 10,
                Quality = 0,
            });
            program.UpdateQuality();
            var localItems = program.GetItems();
            Assert.Equal(9, localItems[localItems.Count - 1].SellIn);
            Assert.Equal(0, localItems[localItems.Count - 1].Quality);
        }


        [Fact]
        public void BrieIncreaseQuality_expected_Q1_S1()
        {
            program.AddItem(new AgedBrie
            {
                Name = "Aged Brie",
                SellIn = 2,
                Quality = 0
            });
            program.UpdateQuality();
            var localItems = program.GetItems();
            Assert.Equal(1, localItems[localItems.Count - 1].SellIn);
            Assert.Equal(1, localItems[localItems.Count - 1].Quality);
        }

        [Fact]
        public void DexterityVest_DoubleDecreaseQuality_expected_Q18_SNegative1()
        {
            program.AddItem(new Item
            {
                Name = "+5 Dexterity Vest",
                SellIn = 0,
                Quality = 20,
            });
            program.UpdateQuality();
            var localItems = program.GetItems();
            Assert.Equal(-1, localItems[localItems.Count - 1].SellIn);
            Assert.Equal(18, localItems[localItems.Count - 1].Quality);
        }
        [Fact]
        public void DexterityVest_DecreaseQuality_expected_Q19_S9()
        {
            program.AddItem(new Item
            {
                Name = "+5 Dexterity Vest",
                SellIn = 10,
                Quality = 20,
            });
            program.UpdateQuality();
            var localItems = program.GetItems();
            Assert.Equal(9, localItems[localItems.Count - 1].SellIn);
            Assert.Equal(19, localItems[localItems.Count - 1].Quality);
        }

        [Fact]
        public void Backstage_pass_more_than_10_days()
        {
            //When backstage pass has more than 10 days, quality increases by 1
            program.AddItem(new Backstage
            {
                Name = "Backstage passes to a TAFKAL80ETC concert",
                SellIn = 15,
                Quality = 10,
            });
            program.UpdateQuality();
            var localItems = program.GetItems();
            Assert.Equal(14, localItems[localItems.Count - 1].SellIn);
            Assert.Equal(11, localItems[localItems.Count - 1].Quality);
        }

        [Fact]
        public void Backstage_less_five_days_Q50_S5()
        {
            //When backstage pass has more than 10 days, quality increases by 1
            program.AddItem(new Backstage
            {
                Name = "Backstage passes to a TAFKAL80ETC concert",
                SellIn = 4,
                Quality = 48,
            });
            program.UpdateQuality();
            var localItems = program.GetItems();
            Assert.Equal(3, localItems[localItems.Count - 1].SellIn);
            Assert.Equal(50, localItems[localItems.Count - 1].Quality);
        }

        [Fact]
        public void Backstage_pass_less_than_10_days()
        {
            //When backstage pass has less than 10 days, quality increases by 2
            program.AddItem(new Backstage
            {
                Name = "Backstage passes to a TAFKAL80ETC concert",
                SellIn = 9,
                Quality = 10,
            });
            program.UpdateQuality();
            var localItems = program.GetItems();
            Assert.Equal(8, localItems[localItems.Count - 1].SellIn);
            Assert.Equal(12, localItems[localItems.Count - 1].Quality);
        }

        [Fact]
        public void Backstage_pass_less_than_5_days()
        {
            //When backstage pass has less than 10 days, quality increases by 3
            program.AddItem(new Backstage
            {
                Name = "Backstage passes to a TAFKAL80ETC concert",
                SellIn = 4,
                Quality = 10,
            });
            program.UpdateQuality();
            var localItems = program.GetItems();
            Assert.Equal(3, localItems[localItems.Count - 1].SellIn);
            Assert.Equal(13, localItems[localItems.Count - 1].Quality);
        }

        [Fact]
        public void Backstage_pass_less_than_10_days_Q49()
        {
            //When backstage pass has less than 10 days, quality increases by 3
            program.AddItem(new Backstage
            {
                Name = "Backstage passes to a TAFKAL80ETC concert",
                SellIn = 7,
                Quality = 49,
            });
            program.UpdateQuality();
            var localItems = program.GetItems();
            Assert.Equal(6, localItems[localItems.Count - 1].SellIn);
            Assert.Equal(50, localItems[localItems.Count - 1].Quality);
        }

        [Fact]
        public void Backstage_pass_after_concert_quality_is_0()
        {
            //After the concert, backstage pass quality is 0
            program.AddItem(new Backstage
            {
                Name = "Backstage passes to a TAFKAL80ETC concert",
                SellIn = 0,
                Quality = 10,
            });
            program.UpdateQuality();
            var localItems = program.GetItems();
            Assert.Equal(-1, localItems[localItems.Count - 1].SellIn);
            Assert.Equal(0, localItems[localItems.Count - 1].Quality);
        }


        [Fact]
        public void SellIn_Less_Than_Zero_For_Aged_Brie_Expect_Q41_SNegative2()
        {
            //An Item cannot have a quality higher than 50
            program.AddItem(new AgedBrie
            {
                Name = "Aged Brie",
                SellIn = -1,
                Quality = 40,
            });
            program.UpdateQuality();
            var localItems = program.GetItems();
            Assert.Equal(-2, localItems[localItems.Count - 1].SellIn);
            Assert.Equal(42, localItems[localItems.Count - 1].Quality);
        }

        [Fact]
        public void Conjured_Over_SellDate_Quality_Degrades_By_4()
        {
            //A conjured item over the selldate degrades by 4
            program.AddItem(new Conjured
            {
                Name = "Conjured Mana Cake",
                SellIn = -1,
                Quality = 10,
            });
            program.UpdateQuality();
            var localItems = program.GetItems();
            Assert.Equal(-2, localItems[localItems.Count - 1].SellIn);
            Assert.Equal(6, localItems[localItems.Count - 1].Quality);
        }

        [Fact]
        public void Conjured_Quality_Degrades_By_2()
        {
            program.AddItem(new Conjured
            {
                Name = "Conjured Mana Cake",
                SellIn = 10,
                Quality = 10,
            });
            program.UpdateQuality();
            var localItems = program.GetItems();
            Assert.Equal(9, localItems[localItems.Count - 1].SellIn);
            Assert.Equal(8, localItems[localItems.Count - 1].Quality);
        }
    }
}