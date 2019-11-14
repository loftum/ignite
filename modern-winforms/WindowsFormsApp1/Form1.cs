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
        private readonly TextBox _input;

        public Form1()
        {
            InitializeComponent();

            _label = new Label
            {
                Text = "(Why is this label here?)",
                Location = new Point(Width - 200, Height - 100),
                Size = new Size(100, 50)
            };
            Controls.Add(_label);


            _input = new TextBox
            {
                Text = "Hest er best på fest",
                PlaceholderText = "Enter message",
                Size = new Size(200, 50),
                Location = new Point(Width / 4, 10)
            };
            Controls.Add(_input);
            var toastButton = new Button
            {
                Text = "Send toast",
                Location = new Point(_input.Location.X + _input.Width, _input.Location.Y),
                Size = new Size(100, 50),
            };
            toastButton.Click += Message_Click;
            Controls.Add(toastButton);
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
                                Text = _input.Text ?? "Hest er best på fest"
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
