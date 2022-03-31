using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BinPacking
{
    public class Bin
    {
        public List<int> Items { get; set; } = new List<int>();
        public int Size { get; set; } = 10;
        public int Used { get; set; } = 0;

        public void ShowItems()
        {
            if (Items != null)
            {
                foreach (var item in Items)
                {
                    Console.Write($"{item} ");
                }
                Console.WriteLine();
            }
        }

        public bool CheckPackable(int item)
        {
            if (item + Used <= Size)
            {
                return true;
            }
            return false;
        }

        public void PushItem(int item)
        {
            Items?.Add(item);
            Used += item;
        }

        public void PopItem()
        {
            var lastItem = Items[^1];
            Items.Remove(lastItem);
            Used -= lastItem;
        }

        public void DeleteAt(int index)
        {
            var item = Items[index];
            Items.RemoveAt(index);
            Used -= item;
        }

        public void AddAt(int index, int item)
        {
            Items.Insert(index, item);
            Used += item;
        }


    }
}
