
using System.Windows;
using System.Windows.Media.Imaging;
namespace ImageSlicer
{
    internal class Slicer
    {
        public Action<Exception> OnException;
        public List<BitmapSource> Slice(BitmapSource sourceImage, int onePartScale, int xMargin, int yMargin)
        {
            try
            {
                int widthCount = sourceImage.PixelWidth / onePartScale;
                int heighCount = sourceImage.PixelHeight / onePartScale;

                List<BitmapSource> result = new List<BitmapSource>();
                CroppedBitmap imagePart;
                for (int y = 0; y < heighCount; y++)
                {
                    for (int x = 0; x < widthCount; x++)
                    {
                        if (x != 0 && y != 0)
                        {
                            imagePart = new CroppedBitmap(sourceImage, new Int32Rect(x * onePartScale + xMargin, y * onePartScale + yMargin, onePartScale, onePartScale));
                        }
                        else if (x == 0 && y != 0)
                        {
                            imagePart = new CroppedBitmap(sourceImage, new Int32Rect(x * onePartScale, y * onePartScale + yMargin, onePartScale, onePartScale));
                        }
                        else if (x != 0 && y == 0)
                        {
                            imagePart = new CroppedBitmap(sourceImage, new Int32Rect(x * onePartScale + xMargin, y * onePartScale, onePartScale, onePartScale));
                        }
                        else
                        {
                            imagePart = new CroppedBitmap(sourceImage, new Int32Rect(x * onePartScale, y * onePartScale, onePartScale, onePartScale));
                        }
                        result.Add(imagePart);
                    }
                }
                return result;
            }
            catch (Exception ex)
            {
                OnException?.Invoke(ex);
                return null;
            }

        }
    }
}
