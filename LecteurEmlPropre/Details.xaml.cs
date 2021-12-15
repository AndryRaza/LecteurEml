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
            string Text = sr.ReadToEnd();
            sr.Close();
            contenu.Text = Text;
        }

    }
}
