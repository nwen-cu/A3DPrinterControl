using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Reflection;
using System.Resources;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace A3DPrinterControl
{
	public class ImageResources
	{
		private static Dictionary<(string, string, int, int), ImageSource> imageBuffer = new Dictionary<(string, string, int, int), ImageSource>();

		public static ImageSource Load(string resource, string name, int width = 16, int height = 16)
		{
			if (imageBuffer.ContainsKey((resource, name, width, height))) return imageBuffer[(resource, name, width, height)];
			var t = typeof(Icons);
			ResourceManager rm = new ResourceManager(MethodBase.GetCurrentMethod().DeclaringType.Namespace + ".Icons", Assembly.GetEntryAssembly());

			byte[] data = rm.GetObject(name) as byte[];
			BitmapImage img = new BitmapImage();
			using (MemoryStream stream = new MemoryStream(data))
			{
				stream.Seek(0, SeekOrigin.Begin);
				img.BeginInit();
				img.StreamSource = stream;
				img.CacheOption = BitmapCacheOption.OnLoad;
				img.EndInit();
			}
			var newimg = new TransformedBitmap(img, new ScaleTransform((double)width / img.PixelWidth, (double)height / img.PixelHeight));
			imageBuffer.Add((resource, name, width, height), newimg);
			return newimg;
		}
	}
}
