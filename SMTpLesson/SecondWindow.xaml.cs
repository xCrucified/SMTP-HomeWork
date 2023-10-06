using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
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
using MimeKit;
using System.Xml.Linq;

namespace SMTpLesson
{
    /// <summary>
    /// Interaction logic for SecondWindow.xaml
    /// </summary>
    public partial class SecondWindow : Window
    {
        string myMailAddress;
        string accountPassword;

        public SecondWindow(string pass, string login)
        {
            InitializeComponent();
            myMailAddress = login;
            accountPassword = pass;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Boolean IsActiveBtn = true;
            MailMessage mail = new MailMessage(myMailAddress, toTxtBox.Text)
            {
                Subject = subjectTxtBox.Text,
                Body = $"<p>{bodyTxtBox.Text}</p>",
                IsBodyHtml = true,
            };

            if (chkBox.IsChecked == true)
            {
                mail.Priority = MailPriority.High;
            }
            while (IsActiveBtn != false)
            {

                var result = MessageBox.Show("Do you want to attach a file?", "Attach File", MessageBoxButton.YesNo);
            
                if (result == MessageBoxResult.Yes)
                {
                        OpenFileDialog dialog = new OpenFileDialog();
                        if (dialog.ShowDialog() == true)
                            mail.Attachments.Add(new Attachment(dialog.FileName));
                }
                if (result == MessageBoxResult.No)
                    IsActiveBtn = false;

            }
            
            
            SmtpClient client = new SmtpClient("smtp.gmail.com", 587)
            {
                Credentials = new NetworkCredential(myMailAddress, accountPassword),
                EnableSsl = true
            };

            client.Send(mail);

        }
    }
}
