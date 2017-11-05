using DevExpress.Mvvm;

namespace Sports.Wpf.Common.Common.Interfaces
{
    public interface IEditableControl<T>
    {
        T Value { get; set; }

        DelegateCommand LeftButtonClickCommand { get; }

        DelegateCommand RightButtonClickCommand { get; }
    }

    public class IntTypeEditableControl : IEditableControl<int>
    {
        public IntTypeEditableControl(int value)
        {
            Value = value;
        }

        public int Value { get; set; }

        public DelegateCommand LeftButtonClickCommand
        {
            get
            {
                return new DelegateCommand(() => Value++);
            }
        }

        public DelegateCommand RightButtonClickCommand
        {
            get
            {
                return new DelegateCommand(() => Value--);
            }
        }
    }
}