using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BinPacking
{
    public class ListBin
    {
        public List<Bin> ListBins = new() { new Bin() };

        public void FirstFit(List<int> ListItems)
        {
            ListBins = new() { new Bin() };
            while (ListItems.Count > 0)
            {
                for (var i = 0; i < ListBins.Count; i++)
                {
                    if (ListBins[i].CheckPackable(ListItems[0]))
                    {
                        ListBins[i].PushItem(ListItems[0]);
                        ListItems.RemoveAt(0);
                        break;
                    }
                    else
                    {
                        if (i == ListBins.Count - 1)
                        {
                            ListBins.Add(new Bin());
                            ListBins[i + 1].PushItem(ListItems[0]);
                            ListItems.RemoveAt(0);
                            break;
                        }
                    }
                }
            }
        }

        public void BestFirst(List<int> ListItems)
        {
            //ListBins = new() { new Bin() };
            while (ListItems.Count > 0)
            {
                var index = 0;
                var space = 10;
                var isFind = false;
                for (var i = 0; i < ListBins.Count; i++)
                {
                    if (ListBins[i].CheckPackable(ListItems[0]))
                    {
                        if (space > 10 - ListBins[i].Used - ListItems[0])
                        {
                            index = i;
                            space = 10 - ListBins[i].Used - ListItems[0];
                            isFind = true;
                        }
                    }
                    else
                    {
                        if (i == ListBins.Count - 1 && !isFind)
                        {
                            ListBins.Add(new Bin());
                            index = i + 1;
                            space = 10;
                        }
                    }
                }
                ListBins[index].PushItem(ListItems[0]);
                ListItems.RemoveAt(0);
            }
        }

        public void ShowListBins()
        {
            foreach (var bin in ListBins)
            {
                bin.ShowItems();
            }
        }

        public double Score()
        {
            double ListBinsLength = ListBins.Count;
            double Space_max = 0;
            foreach (var bin in ListBins)
            {
                Space_max = Math.Max(Space_max, 10 - bin.Used);
            }
            return (ListBinsLength - Convert.ToDouble(0.1 * Space_max));
        }

        public static bool Swap(Bin bin1, int index1, Bin bin2, int index2)
        {
            var item1 = bin1.Items[index1];
            var item2 = bin2.Items[index2];
            if (item1 <= (10 - bin2.Used) + item2 && item2 <= (10 - bin1.Used) + item1)
            {
                bin1.Items[index1] = item2;
                bin1.Used += item2 - item1;
                bin2.Items[index2] = item1;
                bin2.Used += item1 - item2;
            }
            return false;
        }

        public bool BetterNeighbor()
        {
            double sc_current = Score();

            // Di chuyen 1 item den cac bin moi
            for (var i = 0; i < ListBins.Count; i++)
            {
                for (var j = 0; j < ListBins[i].Items.Count; j++)
                {
                    var currentItem = ListBins[i].Items[j];

                    for (var k = 0; k < ListBins.Count; k++)
                    {
                        if (k == i)
                        {
                            continue;
                        }
                        if (ListBins[k].CheckPackable(currentItem))
                        {
                            // Remove currentItem in current bin
                            ListBins[i].DeleteAt(j);
                            ListBins[k].PushItem(currentItem);
                            if (sc_current > Score())
                            {
                                return true;
                            }
                            ListBins[k].PopItem();
                            ListBins[i].AddAt(j, currentItem);
                        }
                    }
                }
            }

            // Swap 2 item cua 2 bin
            for (var i = 0; i < ListBins.Count - 1; i++)
            {
                for (var j = i + 1; j < ListBins.Count; j++)
                {
                    for (var index_1 = 0; index_1 < ListBins[i].Items.Count; index_1++)
                    {
                        for (var index_2 = 0; index_2 < ListBins[j].Items.Count; index_2++)
                        {
                            if (Swap(ListBins[i], index_1, ListBins[j], index_2))
                            {
                                if (Score() < sc_current)
                                {
                                    return true;
                                }
                            }
                            _ = Swap(ListBins[i], index_1, ListBins[j], index_2);
                        }
                    }
                }
            }

            return false;
        }

        public bool LocalSearch()
        {
            while (BetterNeighbor())
            {
                return true;
            }
            return false;
        }

    }
}
