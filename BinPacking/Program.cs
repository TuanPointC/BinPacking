using BinPacking;

var ListItems = new List<int>()
{
    5,7,5,2,4,2,5,1,6
};



var bestListBins = new ListBin();
var listBins = new ListBin();

bestListBins.BestFirst(new List<int>(ListItems));

var sc_max = bestListBins.Score();


listBins.BestFirst(new List<int>(ListItems));

for (var i = 0; i < 10; i++)
{
    listBins.LocalSearch();
    var sc_current = listBins.Score();
    if (sc_current < sc_max)
    {
        bestListBins.ListBins = new List<Bin>(listBins.ListBins);
        sc_max = sc_current;
    }
}

bestListBins.ShowListBins();