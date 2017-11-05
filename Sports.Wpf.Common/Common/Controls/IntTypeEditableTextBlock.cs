using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using Sports.Common;

namespace Sports.Wpf.Common.Common.Controls
{
    public class IntTypeEditableTextBlock : TextBlock
    {
        public IntTypeEditableTextBlock()
        {

        }

        protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e)
        {
            base.OnMouseLeftButtonDown(e);
            var item = Text.ToInt();
            Text = (item + 1).ToString();
        }

        protected override void OnMouseRightButtonDown(MouseButtonEventArgs e)
        {
            base.OnMouseRightButtonDown(e);
            var item = Text.ToInt();
            Text = (item - 1).ToString();
        }
    }
}
