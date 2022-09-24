namespace ProducerConsumer;

public class ProducerConsumer
{
	private Queue<int> _buffer;
	private Semaphore _noOfEmptySlots;
	private Semaphore _noOfFilledSlots;
	private Semaphore _mutex;
	private DateTime _start;

	public ProducerConsumer(int bufferSize)
	{
		_buffer = new Queue<int>(bufferSize);
		_noOfEmptySlots = new Semaphore(initialCount: bufferSize, maximumCount: bufferSize);
		_noOfFilledSlots = new Semaphore(initialCount: 0, maximumCount: bufferSize);
		_mutex = new Semaphore(initialCount: 1, maximumCount: 1);
		_start = DateTime.Now;
	}
	public void Produce()
	{
		do
		{
			// Wait until _noOfEmptySlots > 0 (_buffer is not full), then decrement _noOfEmptySlots by 1.
			_noOfEmptySlots.WaitOne();
			// Acquire lock.
			_mutex.WaitOne();

			// Insert data.
			#region CriticalSection
			int producedValue = Random.Shared.Next(0, 100);
			_buffer.Enqueue(producedValue);
			Console.Write("Added " + producedValue + ".\t");
			ShowInfo();
			Thread.Sleep(1000);
			#endregion

			// Release lock.
			_mutex.Release();
			// Increment _noOfFilledSlots by 1.
			_noOfFilledSlots.Release();



		} while ((DateTime.Now - _start).Seconds < 10);
	}


	public void Consume()
	{
		do
		{
			// Wait until _noOfFilledSlot > 0 (_buffer is not empty), then decrement _noOfFilledSlot by 1.
			_noOfFilledSlots.WaitOne();
			// Acquire lock.
			_mutex.WaitOne();

			// Remove data
			#region CriticalSection
			int consumedValue = _buffer.Dequeue();
			Console.Write("Removed " + consumedValue + ".\t");
			ShowInfo();
			Thread.Sleep(1000);
			#endregion
			// Release lock.
			_mutex.Release();
			// Increment _noOfEmptySlots by 1.
			_noOfEmptySlots.Release();



		} while ((DateTime.Now - _start).Seconds < 10);
	}
	private void ShowInfo()
	{
		Console.Write("Buffer: ");
		foreach (int i1 in _buffer)
		{
			Console.Write(i1 + " ");
		}

		Console.WriteLine();
	}

}