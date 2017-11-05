using System.Collections.ObjectModel;
using System.Linq;

// The data model defined by this file serves as a representative example of a strongly-typed
// model that supports notification when members are added, removed, or modified.  The property
// names chosen coincide with data bindings in the standard item templates.
//
// Applications may use this model as a starting point and build on it, or discard it entirely and
// replace it with something appropriate to their needs.

namespace Sports.Wpf.Common.DataModel
{
    /// <summary>
    ///     Creates a collection of groups and items with hard-coded content.
    ///     SampleDataSource initializes with placeholder data rather than live production
    ///     data so that sample data is provided at both design-time and run-time.
    /// </summary>
    public sealed class UIDataSource
    {
        private readonly ObservableCollection<UIDataItem> _items;

        public UIDataSource()
        {
            _items = new ObservableCollection<UIDataItem>();
        }

        public static UIDataSource Instance { get; } = new UIDataSource();

        public ObservableCollection<UIDataItem> Items => Instance._items;

        public static UIDataItem GetItem(string uniqueId)
        {
            // Simple linear search is acceptable for small data sets
            var matches = Instance.Items.Where(item => item.UniqueId.Equals(uniqueId));
            if (matches.Count() == 1) return matches.First();
            return null;
        }
    }
}