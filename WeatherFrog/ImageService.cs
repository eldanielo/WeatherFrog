using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.IO.IsolatedStorage;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Imaging;
using System.Windows.Resources;
using WeatherFrog.Model;

namespace WeatherFrog
{
    public class ImageService
    {
        private readonly static ImageService instance = new ImageService();

         private ImageService() { }

         public static ImageService getInstance()
        {
            return instance;
        }

         public void writeImg(String path, WriteableBitmap writeableBitmap)
         {
             try
             {


                 using (
                 var isoFileStream = new IsolatedStorageFileStream(path, FileMode.OpenOrCreate, IsolatedStorageFile.GetUserStoreForApplication()))
                 {
                     writeableBitmap.SaveJpeg(isoFileStream, writeableBitmap.PixelWidth, writeableBitmap.PixelHeight, 0, 100);
                 }

             }
             catch (Exception ex)
             {
                 Debug.WriteLine(ex.Message);
                 throw;
             }
             Debug.WriteLine("picture saved");

         }

         Station tmp;
         public void DownloadImagefromServer(string url, Station station)
         {
             tmp = station;
             WebClient client = new WebClient();
             client.OpenReadCompleted += new OpenReadCompletedEventHandler(client_OpenReadCompleted);
             client.OpenReadAsync(new Uri(url, UriKind.Absolute));
         }
         void client_OpenReadCompleted(object sender, OpenReadCompletedEventArgs e)
         {
             BitmapImage bitmap = new BitmapImage();
             bitmap.SetSource(e.Result);
             Debug.WriteLine("image downloaded " + bitmap);

             String tempJPEG = tmp.id+".jpg";

          
             using (IsolatedStorageFile myIsolatedStorage = IsolatedStorageFile.GetUserStoreForApplication())
             {
                 if (myIsolatedStorage.FileExists(tempJPEG))
                 {
                     myIsolatedStorage.DeleteFile(tempJPEG);
                 }

                 IsolatedStorageFileStream fileStream = myIsolatedStorage.CreateFile(tempJPEG);

                 StreamResourceInfo sri = null;
                 Uri uri = new Uri(tempJPEG, UriKind.Relative);
                 sri = Application.GetResourceStream(uri);
                  WriteableBitmap wb = new WriteableBitmap(bitmap);
                 Extensions.SaveJpeg(wb, fileStream, wb.PixelWidth, wb.PixelHeight, 0, 85);               
                 fileStream.Close();
                 
             }
             Debug.WriteLine("image saved " + tempJPEG);

            
         }
         public BitmapImage openImage(Station station) {
             BitmapImage bi;
             using (IsolatedStorageFile myIsolatedStorage = IsolatedStorageFile.GetUserStoreForApplication())
             {

                 using (IsolatedStorageFileStream fileStream = myIsolatedStorage.OpenFile(station.id+".jpg", FileMode.Open, FileAccess.Read))
                 {
                     bi = new BitmapImage();
                     bi.SetSource(fileStream);

                 }
             }
             Debug.WriteLine("image Opened");
             return bi;

         }

         

    }
}
