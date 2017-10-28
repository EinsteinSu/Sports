using System;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using DevExpress.Mvvm;

namespace Sports.Wpf.Common.DataModel
{
    /// <inheritdoc />
    /// <summary>
    ///     Base class for <see cref="T:Sports.Race.Console.DataModel.SampleDataItem" /> and <see cref="!:SampleDataGroup" />
    ///     that
    ///     defines properties common to both.
    /// </summary>
    public abstract class UIDataCommon : ViewModelBase
    {
        private static readonly Uri BaseUri = new Uri("pack://application:,,,");
        private static int _count;
        private string _description;
        private ImageSource _image;
        private string _imagePath;
        private string _subtitle;
        private string _title;
        private int _uniqueId;

        public UIDataCommon(int uniqueId, string title, string subtitle, string imagePath, string description)
        {
            _uniqueId = uniqueId;
            _title = title;
            _subtitle = subtitle;
            _description = description;
            _imagePath = imagePath;
        }

        public int UniqueId
        {
            get => _uniqueId;
            set => SetProperty(ref _uniqueId, value, "UniqueId");
        }

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
            return new BitmapImage(new Uri(BaseUri, path));
#endif
        }

        public override string ToString()
        {
            return Title;
        }
    }
}