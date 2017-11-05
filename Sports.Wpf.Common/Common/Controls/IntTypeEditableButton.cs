using System;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using Sports.Common;

namespace Sports.Wpf.Common.Common.Controls
{
    public class IntTypeEditableButton : Button
    {
        public Func<object, object> Increase { get; protected set; }

        public Func<object, object> Decrease { get; protected set; }

        protected override void OnClick()
        {
            base.OnClick();
            Content = Increase != null ? Increase(Content) : ButtonActions.Increase(Content);
        }

        protected override void OnMouseRightButtonDown(MouseButtonEventArgs e)
        {
            base.OnMouseRightButtonDown(e);
            Content = Decrease != null ? Decrease(Content) : ButtonActions.Decrease(Content);
        }
    }

    public class TwentySecondsEditableButton : IntTypeEditableButton
    {
        public TwentySecondsEditableButton()
        {
            Decrease = ButtonActions.DecreaseTwentySeconds;
        }
    }

    public class TimingButton : Button
    {
        public TimingButton()
        {
            Menu mainMenu = new Menu();
            MenuItem item1 = new MenuItem();
            item1.Header = "First";
            mainMenu.Items.Add(item1);

            MenuItem item2 = new MenuItem();
            item2.Header = "Two";
            item1.Items.Add(item2);

            MenuItem item3 = new MenuItem();
            item3.Header = "Third";
            item1.Items.Add(item3);
            ContextMenu = new ContextMenu();
            ContextMenu.Items.Add(mainMenu);
        }
    }

    public static class ButtonActions
    {
        public static object Increase(object content)
        {
            var item = content.ToString().ToInt();
            return (item + 1).ToString();
        }

        public static object Decrease(object content)
        {
            var item = content.ToString().ToInt();
            return (item - 1).ToString();
        }

        public static object DecreaseTwentySeconds(object content)
        {
            return 0;
        }
    }
}