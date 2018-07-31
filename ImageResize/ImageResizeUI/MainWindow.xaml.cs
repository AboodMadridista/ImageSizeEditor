using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Drawing;

namespace ImageResizeUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = this;
            Log("Image Resize UI Started");
        }

        HashSet<string> postfixes; /// to avoid repeated postfixes hashset was used
        string path = string.Empty;
        private void Select_Directory_Click(object sender, RoutedEventArgs e)
        {
            Log("Select File Directory...");
            postfixes = new HashSet<string>(); /// for each selection time new hashset list defined
            FolderBrowserDialog fbd = new FolderBrowserDialog(); /// for each selection time new folder browser dialog defined
            /// show dialog and get the selected path
            DialogResult dr = fbd.ShowDialog(); 
            path = fbd.SelectedPath; 

            Log("Select File Directory Result " + dr.ToString());

            if (dr.ToString().Equals("OK"))
            {
                Log("Selected File Directory: " + path);

                string[] files = Directory.GetFiles(path);
                foreach (string item in files)
                {
                    postfixes.Add(item.Split('.').Last()); /// add file postfix
                }

                Log("Directory Contains " + files.Length + " Files");

                comboBox.ItemsSource = postfixes; /// set combobox items 
                comboBox.SelectedIndex = 0; /// select first element as default

                Log(postfixes.Count + " Postfixes Detected...");
            }
        }

        public Image resizeImage(Image imgToResize)
        {
            Log("Fix Sized (640x133) Process Start...");
            return (Image)(new Bitmap(imgToResize, new System.Drawing.Size(640, 133))); /// image resizing process
        }
        public Image resizeImage(Image imgToResize, System.Drawing.Size size)
        {
            Log("Selected Sized (" + size.Width + "x" + size.Height +") Process Start...");
            return (Image)(new Bitmap(imgToResize, size)); /// image resizing process
        }
        private void Resize_Click(object sender, RoutedEventArgs e)
        {
            System.Drawing.Size size;
            if (string.IsNullOrEmpty(txtHeight.Text) || string.IsNullOrEmpty(txtWidth.Text))
            {
                Log("Fix Size Selected (640x133)");
                size = new System.Drawing.Size(640,133);
            }
            else
            {
                size = new System.Drawing.Size(Int32.Parse(txtWidth.Text), Int32.Parse(txtHeight.Text)); /// get size values from texboxes
                Log("Selected Size (" + size.Width + "x" + size.Height + ")");
            }

            /// resize all images from selected path
            string[] filePaths = Directory.GetFiles(path, "*." + comboBox.SelectedItem.ToString());
            foreach (string item in filePaths)
            {
                string yol = item.Substring(path.Length).Replace(@"\", "");
                resizeImage(Image.FromFile(item, true), size).Save(path + "\\" + yol.Split('.').First() + "_" + Guid.NewGuid());
                Log(item + " File Resized...");

            }
        }

        private void txtWidth_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            System.Windows.Controls.TextBox temp = (sender as System.Windows.Controls.TextBox);

            if (temp.Text.Length > 0)
            {
                this.hintWidth.Visibility = Visibility.Collapsed; /// set hint text collapsed
            }
            else
            {
                this.hintWidth.Visibility = Visibility.Visible; /// set hint text visible
            }

            int index = 0;
            foreach (char item in temp.Text)
            {
                if (!char.IsDigit(item))
                {
                    (sender as System.Windows.Controls.TextBox).Text = temp.Text.Remove(index, 1); /// if added char is not number then delete char
                }
                index++;
            }

            /// set textbox cursor to last charachter
            if (!(temp.Text.Length < 1))
            {
                (sender as System.Windows.Controls.TextBox).SelectionStart = temp.Text.Length;
                (sender as System.Windows.Controls.TextBox).SelectionLength = 0;
            }
        }

        private void txtHeight_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            System.Windows.Controls.TextBox temp = (sender as System.Windows.Controls.TextBox);

            if (temp.Text.Length > 0)
            {
                this.hintHeight.Visibility = Visibility.Collapsed; /// set hint text collapsed
            }
            else
            {
                this.hintHeight.Visibility = Visibility.Visible; /// set hint text visible
            }

            int index = 0;
            foreach (char item in temp.Text)
            {
                if (!char.IsDigit(item))
                {
                    (sender as System.Windows.Controls.TextBox).Text = temp.Text.Remove(index, 1); /// if added char is not number then delete char
                }
                index++;
            }

            /// set textbox cursor to last charachter
            if (!(temp.Text.Length < 1))
            {
                (sender as System.Windows.Controls.TextBox).SelectionStart = temp.Text.Length;
                (sender as System.Windows.Controls.TextBox).SelectionLength = 0;
            }
        }

        private void Log(string input)
        {
            this.rtb.AppendText(Logger.Write(input) + Environment.NewLine); /// write log lines to rich text box
        }
    }
}
