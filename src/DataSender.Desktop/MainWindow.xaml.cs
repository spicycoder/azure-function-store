using System.Diagnostics;
using System.Text;
using System.Windows;
using Azure.Messaging.EventHubs;
using Azure.Messaging.EventHubs.Producer;
using Domain;
using MahApps.Metro.Controls;
using Newtonsoft.Json;

namespace DataSender.Desktop
{
    public partial class MainWindow : MetroWindow
    {
        private const string ConnectionString = "<Replace with your connection string>";
        private const string EventHubName = "mongo-eventhub";

        public MainWindow()
        {
            InitializeComponent();
        }

        private async void SendButton_OnClick(object sender, RoutedEventArgs e)
        {
            var product = new Product
            {
                Name = NameTextbox.Text,
                Description = DescriptionTextbox.Text,
                Price = PriceNumeric.Value.Value
            };

            Debug.WriteLine(product);

            await using (var producerClient = new EventHubProducerClient(ConnectionString, EventHubName))
            {
                using EventDataBatch eventBatch = await producerClient.CreateBatchAsync();
                
                var data = JsonConvert.SerializeObject(product);
                eventBatch.TryAdd(new EventData(Encoding.UTF8.GetBytes(data)));

                await producerClient.SendAsync(eventBatch);
            }
        }
    }
}
