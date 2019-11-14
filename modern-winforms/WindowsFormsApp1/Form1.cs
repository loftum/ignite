using Microsoft.Toolkit.Uwp.Notifications;
using System;
using System.Drawing;
using System.Windows.Forms;
using Windows.UI.Notifications;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        private readonly Label _label;

        public Form1()
        {
            InitializeComponent();
            var button = new Button
            {
                Text = "Malplaced button",
                Location = new Point(50, 120),
                Size = new Size(100, 50),
            };
            button.Click += Message_Click;
            Controls.Add(button);

            _label = new Label
            {
                Text = "Hello",
                Location = new Point(Width - 200, Height - 100),
                Size = new Size(100, 50)
            };
            Controls.Add(_label);
        }

        private void Message_Click(object sender, EventArgs e)
        {
            ShowToastNotification();
        }

        private void ShowToastNotification()
        {
            var notificationManager = ToastNotificationManager.GetDefault();
            var toastContent = new ToastContent
            {
                Visual = new ToastVisual
                {
                    BindingGeneric = new ToastBindingGeneric
                    {
                        Children =
                        {
                            new AdaptiveText
                            {
                                Text = "Hest er best paa fest!"
                            }
                        }
                    }
                },
                Actions = new ToastActionsCustom
                {
                    Buttons =
                    {
                        new ToastButton("Ingen protest!", "action=NoProtest")
                        {
                            ActivationType = ToastActivationType.Foreground
                        }
                    }
                }
            };

            var doc = new Windows.Data.Xml.Dom.XmlDocument();
            doc.LoadXml(toastContent.GetContent());
            var toast = new ToastNotification(doc);
            notificationManager.CreateToastNotifier().Show(toast);
        }
    }
}
