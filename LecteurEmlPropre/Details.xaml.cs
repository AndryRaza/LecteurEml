using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace LecteurEmlPropre
{
    /// <summary>
    /// Logique d'interaction pour Details.xaml
    /// </summary>
    public partial class Details : Window
    {
        public string path
        {
            get;
            set;
        }

        public string Text
        {
            get;
            set;
        }


        public Details()
        {
            InitializeComponent();
      
        }

        public Details(string str) : this()
        {
            path = str;
            readEmlRaw(path);
        }

        private void readEmlRaw(string path)
        {
            System.IO.StreamReader sr = new System.IO.StreamReader(path);
            Text = sr.ReadToEnd();
            sr.Close();
            traitement();
        }

        private void traitement()
        {
            string txtContenttype = "";
            string txtReceived = "";

            string[] arr = Text.Split(new string[] { "\r\n", "\n" }, StringSplitOptions.None);
            for(int i = 0;i < arr.Length;i++)
            {
                string str = arr[i];
                if (str.StartsWith("Content-Type") || str.StartsWith("Content-type"))
                {
                    if (txtContenttype == "")
                    {
                        txtContenttype += extractContentType(str);
                    }
                    else
                    {
                        txtContenttype += "\r\n" + extractContentType(str);
                    }
                }

                if (str.StartsWith("Delivered-To") || str.StartsWith("Delivered-to"))
                {
                    deliveredTo.Text = str.Split(' ')[1];
                }

                if(str.StartsWith("Received"))
                {
                    if (received.Text == "")
                    {
                        txtReceived +=  extractReceived(arr, i);
                    }
                    else
                    {
                        txtReceived += "\r\n" + extractReceived(arr, i);
                    }
                   
                }

            }

            contentType.Text = txtContenttype;
            received.Text = txtReceived;
        }

        private string extractContentType(string str)
        {
            string[] arr = str.Split(' ');

            arr = delete(1, arr);


            string txtContenttype = String.Join("", arr);

            return txtContenttype;
        }

        private string extractReceived(string[] arr,int start)
        {
            string txtReceived = String.Join("",delete(1,arr[start].Split(' ')));


            int i = start + 1;

            while(arr[i].StartsWith(" "))
            {
                txtReceived += "\r\n" + arr[i];
                i++;
            }

            return txtReceived;
        }

        //Supprimer un élément dans une liste
        private string[] delete(int pos, string[] arr)
        {
            string[] provi = arr;

            for (int i = pos - 1; i < provi.Length - 1; i++)
            {
                Console.WriteLine(i);
                provi[i] = provi[i + 1];
            }

            return provi;
        }

    }
}
