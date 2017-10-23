using System.Windows;

namespace Sports.Wpf.Common.DataModel
{
    /// <inheritdoc />
    /// <summary>
    ///     Generic item data model.
    /// </summary>
    public class UIDataItem : UIDataCommon
    {
        private FrameworkContentElement _content;
        private string _groupHeader;
        private bool _isFlowBreak;

        public UIDataItem(string title, string subtitle, string imagePath, string description, FrameworkContentElement content)
            : this(title, subtitle, imagePath, description, content, false, string.Empty)
        {
        }

        public UIDataItem(string title, string subtitle, string imagePath, string description, FrameworkContentElement content,
            bool isFlowBreak, string groupHeader)
            : base(title, subtitle, imagePath, description)
        {
            _content = content;
            IsFlowBreak = isFlowBreak;
            GroupHeader = groupHeader;
        }

        public FrameworkContentElement Content
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