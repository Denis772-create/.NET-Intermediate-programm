using RabbitMQ.Client;
using System.Threading.Channels;

namespace DataCapture.Service;

internal class Program
{
    static void Main()
    {
        using var connection = GetRabbitMqConnection();
        using var channel = ConfigureRabbitMqChanel(connection);

        var pdfFiles = Directory.GetFiles("Data", "*.pdf");
        foreach (var pdfFile in pdfFiles)
        {
            var fileBytes = File.ReadAllBytes(pdfFile);

            var chunkSize = 1024 * 1024; // 1 MB
            var totalChunks = (int)Math.Ceiling((double)fileBytes.Length / chunkSize);

            // unique sequence identifier for this set of messages
            var sequenceId = Guid.NewGuid().ToString();

            // send data as a sequence of messages
            for (var i = 0; i < totalChunks; i++)
            {
                var offset = i * chunkSize;
                var remainingBytes = Math.Min(chunkSize, fileBytes.Length - offset);

                var chunk = new byte[remainingBytes];
                Buffer.BlockCopy(fileBytes, offset, chunk, 0, remainingBytes);

                // mark each message with sequence identification fields
                var properties = channel.CreateBasicProperties();
                properties.Headers = new Dictionary<string, object>
                {
                    { "SequenceId", sequenceId },
                    { "PositionId", i },
                    { "SizeOrEnd", i == totalChunks - 1 ? totalChunks : 0 } // 0 if not the last, totalChunks if the last
                };

                channel.BasicPublish("data_capture_exchange", "data_capture_queue", properties, chunk);
                Console.WriteLine($"Sent chunk {i + 1}/{totalChunks} to Processing service.");
            }
        }

        channel.Close();
        connection.Close();
    }

    static IConnection GetRabbitMqConnection()
    {
        var factory = new ConnectionFactory
        {
            Uri = new Uri("amqps://twqacgdc:DXaKVDPdSj-nYPyfaRpkQPLGeH_DeiRk@moose.rmq.cloudamqp.com/twqacgdc")
        };
        return factory.CreateConnection();
    }

    static IModel ConfigureRabbitMqChanel(IConnection connection)
    {
        var channel = connection.CreateModel();

        channel.QueueDeclare("data_capture_queue", false, false, false);
        channel.ExchangeDeclare("data_capture_exchange", ExchangeType.Direct,
            arguments: new Dictionary<string, object>
            {
                { "x-max-length-bytes", 2097152 } // message limit 2 MB
            });
        channel.QueueBind("data_capture_queue", "data_capture_exchange", "data_capture_queue");

        return channel;
    }
}