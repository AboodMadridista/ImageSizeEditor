using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageResize
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Dosya Yolunu Giriniz: ");
            string path = Console.ReadLine();
            Console.WriteLine("Dosya Uzantısı Giriniz(jpg,png vb): ");
            string postfix = "*." + Console.ReadLine().ToLower();
            string[] filePaths = Directory.GetFiles(path, postfix);
            int i = 0;
            foreach (var item in filePaths)
            {
                Console.WriteLine(++i + ". Görsel İçin Dosya Adı Giriniz: ");
                string name = Console.ReadLine().ToLower().Replace(' ', '_').Replace('ç', 'c').Replace('ğ', 'g').Replace('ü', 'u').Replace('ı', 'i').Replace('ö', 'o').Replace('ş', 's');
                resizeImage(Image.FromFile(item, true)).Save(path + "\\" + name + ".png");
            }
        }
        public static Image resizeImage(Image imgToResize)
        {

            return (Image)(new Bitmap(imgToResize, new Size(640,133)));
        }
        public static Image resizeImage(Image imgToResize, Size size)
        {
            return (Image)(new Bitmap(imgToResize, size));
        }
    }
}
