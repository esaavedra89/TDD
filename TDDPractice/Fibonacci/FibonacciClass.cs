
namespace TDDPractice.Fibonacci
{
    public class FibonacciClass
    {
        public int GetFibonacci(int index) 
        {
            if (index == 0) return 0;
            if (index == 1) return 1;
            return GetFibonacci(index - 1) + GetFibonacci(index - 2);
        }
    }
}
