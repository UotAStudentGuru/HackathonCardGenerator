using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using HackathonCardGenerator.Models;

namespace HackathonCardGenerator.Helpers
{
    public static class CardGenerator
    {
        private static readonly System.Windows.Point TextPoint = new System.Windows.Point(90, 600);

        public static void Generator(IEnumerable<Participant> participants, string path)
        {
            var image = GetCard().ToBitmapImage();

            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);

            foreach (var i in participants)
            {
                var subfolder = (string.IsNullOrWhiteSpace(i.TeamName) ? "Χωρίς ομάδα" : i.TeamName) + "/";
                if (!Directory.Exists(path + subfolder))
                {
                    Directory.CreateDirectory(path + subfolder);
                }

                var bframe = MergeImages(image, i, TextPoint);
                BitmapEncoder encoder = new PngBitmapEncoder();
                encoder.Frames.Add(BitmapFrame.Create(bframe));

                using (var fileStream = new FileStream(path + subfolder + i.Name + " " +i.Surname + ".png", FileMode.Create))
                {
                    encoder.Save(fileStream);
                }
            }
        }
       
        private static BitmapFrame MergeImages(ImageSource background, Participant participant, System.Windows.Point tLocation)
        {
            var visual = new DrawingVisual();

            using (DrawingContext drawingContext = visual.RenderOpen())
            {
                drawingContext.DrawImage(background,
                    new Rect { Height = background.Height, Width = background.Width, X = 0, Y = 0 });
                drawingContext.DrawText(
                    new FormattedText(participant.Name, CultureInfo.InvariantCulture, FlowDirection.LeftToRight,
                        new Typeface("Segoe UI"), 140, System.Windows.Media.Brushes.Black), tLocation);
                tLocation.Y += 150;
                drawingContext.DrawText(
                    new FormattedText(participant.Surname, CultureInfo.InvariantCulture, FlowDirection.LeftToRight,
                        new Typeface("Segoe UI"), 140, System.Windows.Media.Brushes.Black), tLocation);
                tLocation.X += 5;
                if (participant.TeamName != null)
                {
                    tLocation.Y += 200;
                    drawingContext.DrawText(
                        new FormattedText(participant.TeamName, CultureInfo.InvariantCulture, FlowDirection.LeftToRight,
                            new Typeface("Segoe UI"), 72, System.Windows.Media.Brushes.Black), tLocation);
                    tLocation.Y += 100;
                }
                else
                    tLocation.Y += 200;
                drawingContext.DrawText(
                    new FormattedText(participant.Type.ToString(), CultureInfo.InvariantCulture, FlowDirection.LeftToRight,
                        new Typeface("Segoe UI"), 72, System.Windows.Media.Brushes.Black), tLocation);
            }
            var bitmap = new RenderTargetBitmap((int)background.Width, (int)background.Height, 96, 96,
                PixelFormats.Pbgra32);
            bitmap.Render(visual);

            return BitmapFrame.Create(bitmap);
        }

        private static Bitmap GetCard()
        {
            return Properties.Resources.card;
        }

        private static BitmapImage ToBitmapImage(this Bitmap src)
        {
            MemoryStream ms = new MemoryStream();
            ((System.Drawing.Bitmap)src).Save(ms, System.Drawing.Imaging.ImageFormat.Bmp);
            BitmapImage image = new BitmapImage();
            image.BeginInit();
            ms.Seek(0, SeekOrigin.Begin);
            image.StreamSource = ms;
            image.EndInit();
            return image;
        }
    }
}
