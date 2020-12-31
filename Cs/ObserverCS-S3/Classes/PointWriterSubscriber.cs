using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Windows;
using System.Windows.Media;

namespace ObserverCS_S3.Classes
{
    public class PointWriterSubscriber : IObserver<Point>
    {
        private DrawingVisual drawing;

        public PointWriterSubscriber(DrawingVisual drawing)
        {
            this.drawing = drawing;
        }

        public void OnCompleted()
        {
            throw new NotImplementedException();
        }

        public void OnError(Exception error)
        {
            throw new NotImplementedException();
        }

        public void OnNext(Point value)
        {
            DrawingContext drawingContext = drawing.RenderOpen();
            drawingContext.DrawDrawing(drawing.Drawing);
            drawingContext.DrawRectangle(Brushes.White, new Pen(Brushes.White,1), new Rect(1, 1, 120, 14));
            drawingContext.DrawText(
                new FormattedText(
                "x="+value.X+" y="+value.Y, CultureInfo.CurrentCulture, FlowDirection.LeftToRight, new Typeface("Arial"), 12, System.Windows.Media.Brushes.Black, 1

                )
                , new Point(1, 1));

            // Persist the drawing content.
            drawingContext.Close();
        }
    }
}
