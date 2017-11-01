using System;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Sports.Common.WaterPolo;

namespace Sports.Wpf.Common.ViewModel.WaterPolo
{
    public class TeamDisplayViewModel : TeamData
    {
        private static readonly Uri BaseUri = new Uri("pack://application:,,,");
        private ImageSource _image;

        public ImageSource Image
        {
            get
            {
                if (_image == null && FlagUrl != null)
                    _image = GetImage(FlagUrl);
                return _image;
            }
            set
            {
                FlagUrl = null;
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
    }
}