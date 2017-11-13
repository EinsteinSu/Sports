using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using DevExpress.Mvvm;

// The data model defined by this file serves as a representative example of a strongly-typed
// model that supports notification when members are added, removed, or modified.  The property
// names chosen coincide with data bindings in the standard item templates.
//
// Applications may use this model as a starting point and build on it, or discard it entirely and
// replace it with something appropriate to their needs.

namespace Sports.Background.Wpf.DataModel
{
    /// <summary>
    ///     Base class for <see cref="SampleDataItem" /> and <see cref="SampleDataGroup" /> that
    ///     defines properties common to both.
    /// </summary>
    public abstract class SampleDataCommon : ViewModelBase
    {
        private static readonly Uri _baseUri = new Uri("pack://application:,,,");
        private static int count;
        private string _description = string.Empty;
        private ImageSource _image;
        private string _imagePath;
        private string _subtitle = string.Empty;
        private string _title = string.Empty;

        public SampleDataCommon(string title, string subtitle, string imagePath, string description)
        {
            UniqueId = GetUniqueId();
            _title = title;
            _subtitle = subtitle;
            _description = description;
            _imagePath = imagePath;
        }

        public string UniqueId { get; } = string.Empty;

        public string Title
        {
            get => _title;
            set => SetProperty(ref _title, value, "Title");
        }

        public string Subtitle
        {
            get => _subtitle;
            set => SetProperty(ref _subtitle, value, "Subtitle");
        }

        public string Description
        {
            get => _description;
            set => SetProperty(ref _description, value, "Description");
        }

        public ImageSource Image
        {
            get
            {
                if (_image == null && _imagePath != null)
                    _image = GetImage(_imagePath);
                return _image;
            }
            set
            {
                _imagePath = null;
                SetProperty(ref _image, value, "Image");
            }
        }

        private static BitmapImage GetImage(string path)
        {
#if SILVERLIGHT
            return new BitmapImage(new Uri("../"  + path, UriKind.RelativeOrAbsolute));
#else
            return new BitmapImage(new Uri(_baseUri, path));
#endif
        }

        private static string GetUniqueId()
        {
            return "Item" + count++;
        }

        public override string ToString()
        {
            return Title;
        }
    }

    /// <summary>
    ///     Generic item data model.
    /// </summary>
    public class SampleDataItem : SampleDataCommon
    {
        private string _content = string.Empty;
        private string _GroupHeader;
        private bool _IsFlowBreak;

        public SampleDataItem(string title, string subtitle, string imagePath, string description, string content)
            : this(title, subtitle, imagePath, description, content, false, string.Empty)
        {
        }

        public SampleDataItem(string title, string subtitle, string imagePath, string description, string content,
            bool isFlowBreak, string groupHeader)
            : base(title, subtitle, imagePath, description)
        {
            _content = content;
            IsFlowBreak = isFlowBreak;
            GroupHeader = groupHeader;
        }

        public string Content
        {
            get => _content;
            set => SetProperty(ref _content, value, "Content");
        }

        public bool IsFlowBreak
        {
            get => _IsFlowBreak;
            set => SetProperty(ref _IsFlowBreak, value, "IsFlowBreak");
        }

        public string GroupHeader
        {
            get => _GroupHeader;
            set => SetProperty(ref _GroupHeader, value, "GroupHeader");
        }
    }

    /// <summary>
    ///     Creates a collection of groups and items with hard-coded content.
    ///     SampleDataSource initializes with placeholder data rather than live production
    ///     data so that sample data is provided at both design-time and run-time.
    /// </summary>
    public sealed class SampleDataSource
    {
        private readonly ObservableCollection<SampleDataItem> _items;

        public SampleDataSource()
        {
            _items = new ObservableCollection<SampleDataItem>();
            var ITEM_CONTENT = string.Format("Item Content: {0}\n\n{0}\n\n{0}\n\n{0}\n\n{0}\n\n{0}\n\n{0}",
                "Curabitur class aliquam vestibulum nam curae maecenas sed integer cras phasellus suspendisse quisque donec dis praesent accumsan bibendum pellentesque condimentum adipiscing etiam consequat vivamus dictumst aliquam duis convallis scelerisque est parturient ullamcorper aliquet fusce suspendisse nunc hac eleifend amet blandit facilisi condimentum commodo scelerisque faucibus aenean ullamcorper ante mauris dignissim consectetuer nullam lorem vestibulum habitant conubia elementum pellentesque morbi facilisis arcu sollicitudin diam cubilia aptent vestibulum auctor eget dapibus pellentesque inceptos leo egestas interdum nulla consectetuer suspendisse adipiscing pellentesque proin lobortis sollicitudin augue elit mus congue fermentum parturient fringilla euismod feugiat");
            var ITEM_DESCRIPTION =
                "Item Description: Pellentesque porta, mauris quis interdum vehicula, urna sapien ultrices velit, nec venenatis dui odio in augue. Cras posuere, enim a cursus convallis, neque turpis malesuada erat, ut adipiscing neque tortor ac erat.";

            _items.Add(new SampleDataItem(
                "Item Title: 1",
                "Item Subtitle: 1",
                "Assets/LightGray.png",
                ITEM_DESCRIPTION,
                ITEM_CONTENT, true, "Group 1"));
            _items.Add(new SampleDataItem(
                "Item Title: 2",
                "Item Subtitle: 2",
                "Assets/DarkGray.png",
                ITEM_DESCRIPTION,
                ITEM_CONTENT));
            _items.Add(new SampleDataItem(
                "Item Title: 3",
                "Item Subtitle: 3",
                "Assets/MediumGray.png",
                ITEM_DESCRIPTION,
                ITEM_CONTENT));
            _items.Add(new SampleDataItem(
                "Item Title: 4",
                "Item Subtitle: 4",
                "Assets/DarkGray.png",
                ITEM_DESCRIPTION,
                ITEM_CONTENT));
            _items.Add(new SampleDataItem(
                "Item Title: 5",
                "Item Subtitle: 5",
                "Assets/DarkGray.png",
                ITEM_DESCRIPTION,
                ITEM_CONTENT));
            _items.Add(new SampleDataItem(
                "Item Title: 6",
                "Item Subtitle: 6",
                "Assets/DarkGray.png",
                ITEM_DESCRIPTION,
                ITEM_CONTENT));


            _items.Add(new SampleDataItem(
                "Item Title: 1",
                "Item Subtitle: 1",
                "Assets/MediumGray.png",
                ITEM_DESCRIPTION,
                ITEM_CONTENT, true, "Group 2"));
            _items.Add(new SampleDataItem(
                "Item Title: 2",
                "Item Subtitle: 2",
                "Assets/DarkGray.png",
                ITEM_DESCRIPTION,
                ITEM_CONTENT));
            _items.Add(new SampleDataItem(
                "Item Title: 3",
                "Item Subtitle: 3",
                "Assets/DarkGray.png",
                ITEM_DESCRIPTION,
                ITEM_CONTENT));

            _items.Add(new SampleDataItem(
                "Item Title: 1",
                "Item Subtitle: 1",
                "Assets/MediumGray.png",
                ITEM_DESCRIPTION,
                ITEM_CONTENT, true, "Group 3"));
            _items.Add(new SampleDataItem(
                "Item Title: 2",
                "Item Subtitle: 2",
                "Assets/DarkGray.png",
                ITEM_DESCRIPTION,
                ITEM_CONTENT));
            _items.Add(new SampleDataItem(
                "Item Title: 3",
                "Item Subtitle: 3",
                "Assets/LightGray.png",
                ITEM_DESCRIPTION,
                ITEM_CONTENT));
            _items.Add(new SampleDataItem(
                "Item Title: 4",
                "Item Subtitle: 4",
                "Assets/DarkGray.png",
                ITEM_DESCRIPTION,
                ITEM_CONTENT));
            _items.Add(new SampleDataItem(
                "Item Title: 5",
                "Item Subtitle: 5",
                "Assets/DarkGray.png",
                ITEM_DESCRIPTION,
                ITEM_CONTENT));
            _items.Add(new SampleDataItem(
                "Item Title: 6",
                "Item Subtitle: 6",
                "Assets/DarkGray.png",
                ITEM_DESCRIPTION,
                ITEM_CONTENT));
            _items.Add(new SampleDataItem(
                "Item Title: 7",
                "Item Subtitle: 7",
                "Assets/DarkGray.png",
                ITEM_DESCRIPTION,
                ITEM_CONTENT));
            _items.Add(new SampleDataItem(
                "Item Title: 8",
                "Item Subtitle: 8",
                "Assets/DarkGray.png",
                ITEM_DESCRIPTION,
                ITEM_CONTENT));

            _items.Add(new SampleDataItem(
                "Item Title: 1",
                "Item Subtitle: 1",
                "Assets/MediumGray.png",
                ITEM_DESCRIPTION,
                ITEM_CONTENT, true, "Group 4"));
            _items.Add(new SampleDataItem(
                "Item Title: 2",
                "Item Subtitle: 2",
                "Assets/DarkGray.png",
                ITEM_DESCRIPTION,
                ITEM_CONTENT));
            _items.Add(new SampleDataItem(
                "Item Title: 3",
                "Item Subtitle: 3",
                "Assets/LightGray.png",
                ITEM_DESCRIPTION,
                ITEM_CONTENT));
            _items.Add(new SampleDataItem(
                "Item Title: 4",
                "Item Subtitle: 4",
                "Assets/DarkGray.png",
                ITEM_DESCRIPTION,
                ITEM_CONTENT));
        }

        public static SampleDataSource Instance { get; } = new SampleDataSource();

        public ObservableCollection<SampleDataItem> Items => Instance._items;

        public static SampleDataItem GetItem(string uniqueId)
        {
            // Simple linear search is acceptable for small data sets
            var matches = Instance.Items.Where(item => item.UniqueId.Equals(uniqueId));
            if (matches.Count() == 1) return matches.First();
            return null;
        }
    }
}