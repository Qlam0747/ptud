using Microsoft.Maui.Controls;
using Microsoft.Maui.Storage;
using CommunityToolkit.Maui.Views;
using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using Sharing_Place.Models;

namespace Sharing_Place.Views
{
    public partial class Message : ContentPage
    {
        private readonly UdpClient udpClient;
        private readonly IPEndPoint remoteEndPoint;
        private readonly string clientId;
        private readonly User1 chatUser;

        public Message(User1 user)
        {
            InitializeComponent();
            chatUser = user;

            udpClient = new UdpClient();
            remoteEndPoint = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 7070);

            clientId = "YourClientId";

            Task.Run(ReceiveMessagesAsync);

            Title = user.Name;
            DisplayUserInfo(user);
        }
        //trạng thái người dùng
        public void DisplayUserInfo(User1 user)
        {
            
            UserImage.Source = user.ImgAvt; 
            UserNameLabel.Text = user.Name;
            UserStatusLabel.Text = user.IsOnline ? "Online" : "Offline";
        }

        private async void OnSendMessageTapped(object sender, EventArgs e)
        {
            var messageText = MessageEntry.Text;
            var receiverId = "ReceiverClientId";

            if (!string.IsNullOrEmpty(messageText))
            {
                MessageEntry.Text = string.Empty;
                AddMessage(messageText, true);
                await SendMessageAsync(messageText, receiverId);
            }
        }

        private void AddMessage(string messageText, bool isSentByUser)
        {
            var messageLabel = new Label
            {
                Text = messageText,
                HorizontalOptions = isSentByUser ? LayoutOptions.EndAndExpand : LayoutOptions.StartAndExpand,
                BackgroundColor = isSentByUser ? Colors.LightBlue : Colors.LightGray,
                Padding = new Thickness(10),
                Margin = new Thickness(10, 10, 10, 0),
                HorizontalTextAlignment = TextAlignment.Start,
                VerticalTextAlignment = TextAlignment.Center,
                LineBreakMode = LineBreakMode.WordWrap,
                MaximumWidthRequest = 250
            };

            var timestampLabel = new Label
            {
                Text = DateTime.Now.ToString("h:mm tt"),
                HorizontalOptions = LayoutOptions.End,
                FontSize = 10,
                TextColor = Colors.Gray,
                Margin = new Thickness(0, 0, 0, 5)
            };

            var containerLayout = new StackLayout
            {
                Orientation = StackOrientation.Vertical,
                HorizontalOptions = isSentByUser ? LayoutOptions.EndAndExpand : LayoutOptions.StartAndExpand,
                Children = { messageLabel, timestampLabel }
            };

            MessagesStack.Children.Add(containerLayout);
            MessagesScrollView.ScrollToAsync(0, MessagesStack.Height, true);
        }

        private void AddMedia(string filePath, bool isImage, bool isSentByUser)
        {
            View mediaView;

            if (isImage)
            {
                mediaView = new Image
                {
                    Source = ImageSource.FromFile(filePath),
                    WidthRequest = 200,
                    HeightRequest = 200,
                    Aspect = Aspect.AspectFit
                };
            }
            else
            {
                mediaView = new MediaElement
                {
                    Source = filePath,
                    WidthRequest = 300,
                    HeightRequest = 200,
                    ShouldAutoPlay = false,
                    ShouldShowPlaybackControls = true
                };
            }

            var timestampLabel = new Label
            {
                Text = DateTime.Now.ToString("h:mm tt"),
                HorizontalOptions = LayoutOptions.End,
                FontSize = 10,
                TextColor = Colors.Gray,
                Margin = new Thickness(0, 0, 0, 5)
            };

            var containerLayout = new StackLayout
            {
                Orientation = StackOrientation.Vertical,
                HorizontalOptions = isSentByUser ? LayoutOptions.EndAndExpand : LayoutOptions.StartAndExpand,
                Children = { mediaView }
            };

            MessagesStack.Children.Add(containerLayout);
            MessagesScrollView.ScrollToAsync(0, MessagesStack.Height, true);
        }

        private void ReceiveMessage(string messageText)
        {
            if (messageText.StartsWith("./emotion"))
            {
                var parts = messageText.Split(' ');
                var senderId = parts[1];
                var emotion = parts[3];
                AddEmotion(senderId, emotion, senderId == clientId);
            }
            else if (messageText.StartsWith("./file"))
            {
                var parts = messageText.Split(' ');
                var fileType = parts[1];
                var filePath = Path.Combine(FileSystem.CacheDirectory, parts[2]);
                AddMedia(filePath, fileType == "image", false);
            }
            else
            {
                AddMessage(messageText, false);
            }
        }

        private void AddEmotion(string senderId, string emotion, bool isSentByUser)
        {
            var emotionLabel = new Label
            {
                Text = emotion,
                HorizontalOptions = isSentByUser ? LayoutOptions.EndAndExpand : LayoutOptions.StartAndExpand,
                Padding = new Thickness(0),  
                Margin = new Thickness(10, 10, 10, 0),
                LineBreakMode = LineBreakMode.NoWrap,  
                FontSize = 30  
            };

            var emotionLayout = new StackLayout
            {
                Orientation = StackOrientation.Horizontal,
                HorizontalOptions = isSentByUser ? LayoutOptions.EndAndExpand : LayoutOptions.StartAndExpand,
                Children = { emotionLabel }
            };

            MessagesStack.Children.Add(emotionLayout);
            MessagesScrollView.ScrollToAsync(0, MessagesStack.Height, true);
        }

        private async Task SendMessageAsync(string message, string receiverId)
        {
            try
            {
                string formattedMessage = $"./message {clientId} {receiverId} {message}";
                var messageBytes = Encoding.UTF8.GetBytes(formattedMessage);
                await udpClient.SendAsync(messageBytes, messageBytes.Length, remoteEndPoint);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error sending message: {ex.Message}");
            }
        }

        private async Task ReceiveMessagesAsync()
        {
            using var udpClient = new UdpClient(7070);
            while (true)
            {
                try
                {
                    var result = await udpClient.ReceiveAsync();
                    var message = Encoding.UTF8.GetString(result.Buffer);

                    if (message.StartsWith("./file"))
                    {
                        var parts = message.Split(' ');
                        var fileType = parts[1];
                        var fileName = parts[2];
                        var fileId = parts[3];
                        var fileSize = long.Parse(parts[4]);

                        await ReceiveFileAsync(udpClient, fileName, fileSize, fileType);
                    }
                    else
                    {
                        
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error receiving message: {ex.Message}");
                }
            }
        }
        private async Task ReceiveFileAsync(UdpClient udpClient, string fileName, long fileSize, string fileType)
        {
            try
            {
                var filePath = Path.Combine(FileSystem.CacheDirectory, fileName);
                using (var fileStream = File.Create(filePath))
                {
                    long totalBytesReceived = 0;
                    while (totalBytesReceived < fileSize)
                    {
                        var result = await udpClient.ReceiveAsync();
                        await fileStream.WriteAsync(result.Buffer, 0, result.Buffer.Length);
                        totalBytesReceived += result.Buffer.Length;

                        // Update progress
                        double progress = (double)totalBytesReceived / fileSize;
                        Console.WriteLine($"Receive progress: {progress:P2}");
                    }
                }

                Console.WriteLine("File received successfully");
                MainThread.BeginInvokeOnMainThread(() =>
                {
                    AddMedia(filePath, fileType == "picture", false);
                });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error receiving file: {ex.Message}");
            }
        }

        private void OnEmotionButtonClicked(object sender, EventArgs e)
        {
            EmotionGrid.IsVisible = !EmotionGrid.IsVisible;
        }

        private void OnSendEmotionTapped(object sender, EventArgs e)
        {
            if (sender is Button button)
            {
                var emotion = button.Text;
                SendEmotion(emotion);
                AddEmotion(clientId, emotion, true);
                EmotionGrid.IsVisible = false;
            }
        }

        private async void SendEmotion(string emotion)
        {
            try
            {
                var messageId = Guid.NewGuid().ToString();
                var senderId = "SenderId";
                var command = $"./emotion {senderId} {messageId} {emotion}";
                var messageBytes = Encoding.UTF8.GetBytes(command);
                await udpClient.SendAsync(messageBytes, messageBytes.Length, remoteEndPoint);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error sending emotion: {ex.Message}");
            }
        }

        private void OnFileButtonClicked(object sender, EventArgs e)
        {
            FileStack.IsVisible = !FileStack.IsVisible;
        }

        private async void OnSelectFileTapped(object sender, EventArgs e)
        {
            if (sender is Button button)
            {
                var fileType = button.Text.ToLower();
                var options = new PickOptions
                {
                    PickerTitle = "Please select a file",
                    FileTypes = fileType == "picture" ? FilePickerFileType.Images : FilePickerFileType.Videos
                };

                var fileResult = await FilePicker.Default.PickAsync(options);

                if (fileResult != null)
                {
                    var filePath = fileResult.FullPath;
                    var isImage = fileType == "picture";

                    AddMedia(filePath, isImage, true);
                    await SendFileAsync(filePath, fileType);
                    FileStack.IsVisible = false;
                }
            }
        }

        private async Task SendFileAsync(string filePath, string fileType)
        {
            try
            {
                var fileInfo = new FileInfo(filePath);
                if (fileInfo.Length > 100 * 1024 * 1024) // 100MB in bytes
                {
                    await DisplayAlert("Error", "File is too large. Maximum size is 100MB.", "OK");
                    return;
                }

                var fileName = Path.GetFileName(filePath);
                var fileId = Guid.NewGuid().ToString();
                var message = $"./file {fileType} {fileName} {fileId} {fileInfo.Length}";
                var messageBytes = Encoding.UTF8.GetBytes(message);

                // Send file info
                await udpClient.SendAsync(messageBytes, messageBytes.Length, remoteEndPoint);

                // Send file data in chunks
                using (var fileStream = File.OpenRead(filePath))
                {
                    byte[] buffer = new byte[60000]; // slightly less than UDP packet size limit
                    int bytesRead;
                    long totalBytesSent = 0;
                    while ((bytesRead = await fileStream.ReadAsync(buffer, 0, buffer.Length)) > 0)
                    {
                        await udpClient.SendAsync(buffer, bytesRead, remoteEndPoint);
                        totalBytesSent += bytesRead;

                        // Update progress (you can use this to update a progress bar in the UI)
                        double progress = (double)totalBytesSent / fileInfo.Length;
                        Console.WriteLine($"Send progress: {progress:P2}");
                    }
                }

                Console.WriteLine("File sent successfully");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error sending file: {ex.Message}");
                await DisplayAlert("Error", $"Failed to send file: {ex.Message}", "OK");
            }
        }
    }
}