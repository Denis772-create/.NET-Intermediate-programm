using System.Text;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace Processing.Service;

class ProcessingService
{
    // for this example we can store chunks in memory
    private static readonly Dictionary<string, List<byte[]>> SequenceChunks = new();

    static void Main()
    {
        using var connection = GetRabbitMqConnection();
        using var channel = ConfigureRabbitMqChanel(connection);

        // folder to store incoming messages
        Directory.CreateDirectory("IncomingMessages");

        var consumer = new EventingBasicConsumer(channel);
        consumer.Received += (_, ea) =>
        {
            var body = ea.Body.ToArray();
            var properties = ea.BasicProperties;

            var sequenceId = Encoding.UTF8.GetString((byte[])properties.Headers["SequenceId"]);
            var positionId = (int)properties.Headers["PositionId"];
            var sizeOrEnd = (int)properties.Headers["SizeOrEnd"];

            Console.WriteLine($"Received chunk #{positionId + 1}. Last chunk: {sizeOrEnd != 0}");

            if (!SequenceChunks.ContainsKey(sequenceId))
            {
                SequenceChunks[sequenceId] = new List<byte[]>();
            }
            SequenceChunks[sequenceId].Add(body);

            // if the last chunk, assemble the complete data
            if (positionId == sizeOrEnd - 1)
            {
                Console.WriteLine($"Assembling complete data for sequence {sequenceId}");
                AssembleAndProcessData(sequenceId);
            }
        };
        channel.BasicConsume("data_capture_queue", autoAck: true, consumer: consumer);

        Console.ReadLine();
        channel.Close();
        connection.Close();
    }

    static void AssembleAndProcessData(string sequenceId)
    {
        if (SequenceChunks.ContainsKey(sequenceId))
        {
            var chunks = SequenceChunks[sequenceId];
            var completeData = chunks.SelectMany(chunk => chunk).ToArray();

            // save the complete data to a PDF file
            var completeDataFilePath = $"IncomingMessages/{sequenceId}_complete_data.pdf";
            File.WriteAllBytes(completeDataFilePath, completeData);
            Console.WriteLine($"Processed complete data saved as PDF: {completeDataFilePath}");

            // clear the chunks for the processed sequence
            SequenceChunks.Remove(sequenceId);
        }
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