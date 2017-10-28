using System.Windows;
using System.Windows.Controls;

namespace Sports.Wpf.Common.DataModel
{
    /// <inheritdoc />
    /// <summary>
    ///     Generic item data model.
    /// </summary>
    public class UIDataItem : UIDataCommon
    {
        private FrameworkElement _content;
        private string _groupHeader;
        private bool _isFlowBreak;

        public UIDataItem(int id,string title, string subtitle, string imagePath, string description, FrameworkElement content)
            : this(id,title, subtitle, imagePath, description, content, false, string.Empty)
        {
        }

        public UIDataItem(int id,string title, string subtitle, string imagePath, string description, FrameworkElement content,
            bool isFlowBreak, string groupHeader)
            : base(id,title, subtitle, imagePath, description)
        {
            _content = content;
            IsFlowBreak = isFlowBreak;
            GroupHeader = groupHeader;
        }

        public FrameworkElement Content
        {
            get => _content;
            set => SetProperty(ref _content, value, "Content");
        }

        public bool IsFlowBreak
        {
            get => _isFlowBreak;
            set => SetProperty(ref _isFlowBreak, value, "IsFlowBreak");
        }

        public string GroupHeader
        {
            get => _groupHeader;
            set => SetProperty(ref _groupHeader, value, "GroupHeader");
        }
    }
}