using System;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using DevExpress.Mvvm;
using Sports.Common;
using Sports.Common.WaterPolo;
using Supeng.Wpf.Common.ViewModels.Sports;

namespace Sports.Wpf.Common.ViewModel.WaterPolo
{
    public class RaceDisplayViewModel
    {
        public FontControlableViewModel TotalTime { get; set; }
        public FontControlableViewModel Court { get; set; }

        public FontControlableViewModel Timeout { get; set; }

        public TeamDisplayViewModel TeamA { get; set; }

        public TeamDisplayViewModel TeamB { get; set; }


        //public string CourtToString
        //{
        //    get
        //    {
        //        switch (Court.Text.ToInt())
        //        {
        //            case 1:
        //                return "1st";
        //            case 2:
        //                return "2nd";
        //            case 3:
        //                return "3rd";
        //            case 4:
        //                return "4th";
        //        }
        //        return Court.Text.ToInt() > 4 ? $"Extral({Court})" : string.Empty;
        //    }
        //}
    }

    public class TeamDisplayViewModel : ViewModelBase
    {
        private static readonly Uri BaseUri = new Uri("pack://application:,,,");
        private ImageSource _image;

        public FontControlableViewModel TeamName { get; set; }

        public FontControlableViewModel TimeoutCount { get; set; }

        public FontControlableViewModel Score { get; set; }

        public FontControlableViewModel PauseTime { get; set; }

        public FontControlableViewModel TeamMark { get; set; }

        public string FlagUrl { get; set; }

        public PlayerDisplayViewModel[] Players { get; set; }

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
            return new BitmapImage(new Uri(BaseUri, path));
        }
    }

    public class PlayerDisplayViewModel
    {
        public FontControlableViewModel Number { get; set; }

        public FontControlableViewModel Name { get; set; }

        public FontControlableViewModel Foul1 { get; set; }

        public FontControlableViewModel Foul2 { get; set; }

        public FontControlableViewModel Foul3 { get; set; }

        public FontControlableViewModel FoulTime { get; set; }
    }
}