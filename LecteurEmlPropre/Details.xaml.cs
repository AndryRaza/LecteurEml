﻿using System;
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

            string[] arr = Text.Split(new string[] { "\r\n", "\n" }, StringSplitOptions.None);

            foreach (string str in arr)
            {

                if (str.StartsWith("Content-Type"))
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
            }

            contentType.Text = txtContenttype;
        }

        private string extractContentType(string str)
        {
            string[] arr = str.Split(' ');

            arr = delete(1, arr);


            string txtContenttype = String.Join("", arr);

            return txtContenttype;
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
