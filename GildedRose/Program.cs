using System.Collections.Generic;
using System;

namespace GildedRose
{
    public class Program
    {
        IList<Item> Items;

        public Program()
        {
            Items = new List<Item>();
        }
        static void Main(string[] args)
        {
            System.Console.WriteLine("OMGHAI!");

            var app = new Program()
            {
                Items = new List<Item>
                {
                    new Item {Name = "+5 Dexterity Vest", SellIn = 10, Quality = 20},
                    new AgedBrie {Name = "Aged Brie", SellIn = 2, Quality = 0},
                    new Item {Name = "Elixir of the Mongoose", SellIn = 5, Quality = 7},
                    new Sulfuras {Name = "Sulfuras, Hand of Ragnaros", SellIn = 0, Quality = 80},
                    new Sulfuras {Name = "Sulfuras, Hand of Ragnaros", SellIn = -1, Quality = 80},
                    new Backstage
                        {
                            Name = "Backstage passes to a TAFKAL80ETC concert",
                            SellIn = 15,
                            Quality = 20
                        },
                    new Backstage
                    {
                        Name = "Backstage passes to a TAFKAL80ETC concert",
                        SellIn = 10,
                        Quality = 49
                    },
                    new Backstage
                    {
                        Name = "Backstage passes to a TAFKAL80ETC concert",
                        SellIn = 5,
                        Quality = 49
                    },
                    new Conjured {Name = "Conjured Mana Cake", SellIn = 3, Quality = 6}
                }
            };

            for (var i = 0; i < 31; i++)
            {
                Console.WriteLine("-------- day " + i + " --------");
                Console.WriteLine("name, sellIn, quality");
                for (var j = 0; j < app.Items.Count; j++)
                {
                    Console.WriteLine(app.Items[j].Name + ", " + app.Items[j].SellIn + ", " + app.Items[j].Quality);
                }
                Console.WriteLine("");
                app.UpdateQuality();
            }
        }


        public void UpdateQuality()
        {
            var items = GetItems();
            for (var i = 0; i < items.Count; i++)
            {

                items[i].Update();
            }
        }
        public IList<Item> GetItems()
        {
            return Items;
        }

        public void AddItem(Item newItem)
        {
            Items.Add(newItem);
        }

    }

    public class Item
    {
        public string Name { get; set; }

        public int SellIn { get; set; }

        public int Quality { get; set; }

        public virtual void Update()
        {
            this.SellIn--;
            bool overSellDate = this.SellIn < 0;
            if (overSellDate && this.Quality > 1)
            {
                this.Quality = this.Quality - 2;
            }
            else if (this.Quality > 0)
            {
                this.Quality--;
            }
        }

    }

    public class AgedBrie : Item
    {
        public override void Update()
        {
            this.SellIn--;
            bool overSellDate = this.SellIn < 0;
            if (overSellDate && this.Quality < 49)
            {
                this.Quality = this.Quality + 2;
            }
            else if (this.Quality < 50)
            {
                this.Quality++;
            }
        }
    }

    public class Backstage : Item
    {
        public override void Update()
        {
            this.SellIn--;

            if (this.SellIn < 0)
            {
                this.Quality = 0;
            }
            else if (this.SellIn < 6)
            {
                if (this.Quality < 48)
                {
                    this.Quality = this.Quality + 3;
                }
                else
                {
                    this.Quality = 50;
                }
            }
            else if (this.SellIn < 11)
            {
                if (this.Quality < 49)
                {
                    this.Quality = this.Quality + 2;
                }
                else
                {
                    this.Quality = 50;
                }
            }
            else
            {
                this.Quality++;
            }
        }
    }

    public class Sulfuras : Item
    {
        public override void Update()
        {

        }
    }

    public class Conjured : Item
    {
        public override void Update()
        {
            //Conjured logic here
            this.SellIn--;
            bool overSellDate = this.SellIn < 0;
            if (overSellDate && this.Quality > 3)
            {
                this.Quality = this.Quality - 4;
            }
            else if (this.Quality > 1)
            {
                this.Quality = this.Quality - 2;
            }
        }
    }

}