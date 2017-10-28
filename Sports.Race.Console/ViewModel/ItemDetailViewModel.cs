using System;
using System.Linq;
using Sports.Race.Console.DataModel;
using Sports.Wpf.Common.DataModel;
using Sports.Wpf.Common.ViewModel;

namespace Sports.Race.Console.ViewModel
{
    //A View Model for an ItemDetailPage
    public class ItemDetailViewModel : ItemDetailViewModelBase
    {
        protected override UIDataItem GetItem(int id)
        {
            return RaceUIDataSource.Items.FirstOrDefault(f => f.UniqueId == id);
        }
    }
}