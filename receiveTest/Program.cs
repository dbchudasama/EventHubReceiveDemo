using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Azure.EventHubs;
using Microsoft.Azure.EventHubs.Processor;
using System.Threading.Tasks;

namespace receiveTest
{
    public class Program
    {
        //Setting the connection credentials in order to be able to connect to the Azure Eventhub
        private const string EhConnectionString = "Endpoint=sb://eventhubdemodiv.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=FveFVfhwjuVuhOYO9qMYx8OejbiuSzIUt95ZGCZkl94=";
        private const string EhEntityPath = "eventhubdemodiv";
        private const string StorageContainerName = "demoblob";
        private const string StorageAccountName = "mystorageaccountsuk";
        private const string StorageAccountKey = "AyDXAIoBFndHMGY+of31fBLXSnfmor2v2xEal4V3iwxx52wdbBrt/WDz+IMU0IC8Bbjl0dfW98ACKobdiA0Yjg==";

        private static readonly string StorageConnectionString = string.Format("DefaultEndpointsProtocol=https;AccountName={0};AccountKey={1}", StorageAccountName, StorageAccountKey);

        //Main Method
        static void Main(string[] args)
        {
            MainAsync(args).GetAwaiter().GetResult();
        }

        private static async Task MainAsync(string[] args)
        {
            Console.WriteLine("Registering EventProcessor...");

            var eventProcessorHost = new EventProcessorHost(
                EhEntityPath,
                PartitionReceiver.DefaultConsumerGroupName,
                EhConnectionString,
                StorageConnectionString,
                StorageContainerName);

            // Registers the Event Processor Host and starts receiving messages
            await eventProcessorHost.RegisterEventProcessorAsync<SimpleEventProcessor>();

            Console.WriteLine("Receiving. Press ENTER to stop worker.");
            Console.ReadLine();

            // Disposes of the Event Processor Host
            await eventProcessorHost.UnregisterEventProcessorAsync();
        }
    }
}
