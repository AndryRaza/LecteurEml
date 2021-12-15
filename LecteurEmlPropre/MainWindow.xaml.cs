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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO;

namespace LecteurEmlPropre
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        public MsgReader.Mime.Message eml
        {
            get;
            set;
        }

        private bool pathChoosen = false;


        private void btnPath_Click(object sender, RoutedEventArgs e)
        {

            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();
            dlg.FileName = "Document";
            dlg.DefaultExt = ".eml";
            dlg.Filter = "EML (.eml)|*.eml";

            Nullable<bool> result = dlg.ShowDialog();

            if (result == true)
            {
                pathChoosen = true;
                path.Text = dlg.FileName;
                readEml(path.Text);
            }
        }

        private void readEml(string path)
        {
            var fileInfo = new FileInfo(path);

            eml = MsgReader.Mime.Message.Load(fileInfo);
            if (eml.Headers != null)
            {

                if (eml.Headers.From != null)
                {
                    from.Text = eml.Headers.From.Raw;
                }
    
                if (eml.Headers.To != null)
                {
                    to.Text = String.Join("\r\n", eml.Headers.To);
                }

                if (eml.Headers.Cc != null)
                {
                    cc.Text = String.Join("\r\n", eml.Headers.Cc);
                }

                if(eml.Headers.Subject != null)
                {
                    subject.Text = eml.Headers.Subject;
                }
            }

            if (eml.TextBody != null)
            {
                body.Text = System.Text.Encoding.UTF8.GetString(eml.TextBody.Body);
            }

            if(eml.Attachments != null)
            {
                pj.Text = $"Pièces jointes : \r\n ({eml.Attachments.Count})";
                //string txt = String.Join("\r\n",eml.Attachments);
                string txt = "";
                foreach(var att in eml.Attachments)
                {
                    if (att.IsAttachment)
                    {
                        txt += "\r\n" + att.FileName;
                    }
                 
                }
                pj_.Text = txt.Trim();
                
            }
        }

        private void btnDetails_Click(object sender, RoutedEventArgs e)
        {
            if(pathChoosen)
            {
                var window = new Details(path.Text);
                window.ShowDialog();
            }
           
        }
    }
}
