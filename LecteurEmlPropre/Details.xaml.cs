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

        private List<string> arrHeaders = new List<string>();


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
            extractGlobal(arr);


            //for (int i = 0; i < arr.Length; i++)
            //{
            //    string str = arr[i];
            //    if (str.StartsWith("Content-Type") || str.StartsWith("Content-type"))
            //    {

            //        txtContenttype += "\r\n" + extractContentType(str);

            //    }

            //    if (str.StartsWith("Delivered-To") || str.StartsWith("Delivered-to"))
            //    {
            //        deliveredTo.Text = str.Split(' ')[1];
            //    }

            //    if (str.StartsWith("Received"))
            //    {
            //        txtReceived += "\r\n" + extractReceived(arr, i);
            //    }

            //}

            //contentType.Text = txtContenttype.Trim();
            //received.Text = txtReceived.Trim();

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

        //Supprimer un élément dans une liste à la position pos (logique tu vois)
        private string[] delete(int pos, string[] arr)
        {
            string[] provi = arr;

            for (int i = pos - 1; i < provi.Length - 1; i++)
            {
                provi[i] = provi[i + 1];
            }

            return provi;
        }

        private void extractGlobal(string[] arr)
        {
            foreach(string txt in arr)
            {
                string[] provi = txt.Split(' ');
                if(provi[0].EndsWith(":"))
                {
                    arrHeaders.Add(provi[0]);
                }
            }

            createHeadersRow();
        
        }

        private void createHeadersRow()
        {
            int i = 0;
            foreach(string txt in arrHeaders)
            {
                content.RowDefinitions.Add(new RowDefinition());
                TextBlock t = new TextBlock();
                t.Text = txt;
                Grid.SetRow(t, i);
                Grid.SetColumn(t, 0);
                content.Children.Add(t);
                i++;
            }
        }

        //Faire une liste de couple (texte,distance entre les headers) 
    }
}
