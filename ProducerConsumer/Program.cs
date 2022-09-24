namespace ProducerConsumer;

internal class Program
{
	private static void Main(string[] args)
	{
		var producerConsumer = new ProducerConsumer(20);
		Thread producerThread = new Thread(producerConsumer.Produce);
		Thread consumerThread = new Thread(producerConsumer.Consume);
		producerThread.Start();
		consumerThread.Start();
	}
}