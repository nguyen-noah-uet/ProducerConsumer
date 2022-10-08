namespace ProducerConsumer;

internal class Program
{
	private static void Main(string[] args)
	{
		var producerConsumer = new ProducerConsumer(20);
		Thread producerThread1 = new Thread(producerConsumer.Produce);
		Thread producerThread2 = new Thread(producerConsumer.Produce);
		Thread producerThread3 = new Thread(producerConsumer.Produce);
		Thread consumerThread1 = new Thread(producerConsumer.Consume);
		Thread consumerThread2 = new Thread(producerConsumer.Consume);
		producerThread1.Start();
		producerThread2.Start();
		consumerThread1.Start();
		producerThread3.Start();
		consumerThread2.Start();

	}
}