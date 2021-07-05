// --------------------------------------------------------------------------------------------------------------------
// <copyright file="PlotViewGtk2.cs" company="OxyPlot">
//   Copyright (c) 2015 OxyPlot contributors
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OxyPlot.GtkSharp
{
    public partial class PlotView
    {
        /// <summary>
        /// Sets the cursor type.
        /// </summary>
        /// <param name="cursorType">The cursor type.</param>
        public void SetCursorType(OxyPlot.CursorType cursorType)
        {
            switch (cursorType)
            {
                case OxyPlot.CursorType.Pan:
                    this.GdkWindow.Cursor = this.PanCursor;
                    break;
                case OxyPlot.CursorType.ZoomRectangle:
                    this.GdkWindow.Cursor = this.ZoomRectangleCursor;
                    break;
                case OxyPlot.CursorType.ZoomHorizontal:
                    this.GdkWindow.Cursor = this.ZoomHorizontalCursor;
                    break;
                case OxyPlot.CursorType.ZoomVertical:
                    this.GdkWindow.Cursor = this.ZoomVerticalCursor;
                    break;
                default:
                    this.GdkWindow.Cursor = new Gdk.Cursor(Gdk.CursorType.Arrow);
                    break;
            }
        }

        /// <summary>
        /// Called when the plot view is exposed.
        /// </summary>
        /// <param name="evnt">The event data.</param>
        /// <returns><c>true</c> if the event was handled, <c>false</c> otherwise.</returns>
        protected override bool OnExposeEvent(Gdk.EventExpose evnt)
        {
            using (var cr = Gdk.CairoHelper.Create(evnt.Window))
            {
                cr.Rectangle(evnt.Area.X, evnt.Area.Y, evnt.Area.Width, evnt.Area.Height);
                cr.Clip();
                this.DrawPlot(cr);
                if (this.trackerLabel != null && this.trackerLabel.Visible)
                    this.trackerLabel.Parent.QueueDraw();
            }

            return base.OnExposeEvent(evnt);
        }

        /// <summary>
        /// Creates the mouse wheel event arguments.
        /// </summary>
        /// <param name="e">The scroll event args.</param>
        /// <returns>Mouse event arguments.</returns>
        private static OxyMouseWheelEventArgs GetMouseWheelEventArgs(Gdk.EventScroll e)
        {
            return new OxyMouseWheelEventArgs
            {
                Delta = e.Direction == Gdk.ScrollDirection.Down ? -120 : 120,
                Position = new ScreenPoint(e.X, e.Y),
                ModifierKeys = ConverterExtensions.GetModifiers(e.State)
            };
        }
    }
}

