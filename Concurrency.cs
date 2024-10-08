using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace leetcode.Solutions
{
    public class Foo
    {
        SemaphoreSlim semaphoreSecond;
        SemaphoreSlim semaphoreThird;
        public Foo()
        {
            semaphoreSecond = new SemaphoreSlim(1);
            semaphoreThird = new SemaphoreSlim(1);
        }
        public void First(Action printFirst)
        {
            printFirst();
            semaphoreSecond.Release();
        }
        public void Second(Action printSecond) 
        {
            semaphoreSecond.Wait();
            printSecond(); 
            semaphoreThird.Release();
        }
        public void Third(Action printThird)
        {
            semaphoreThird.Wait();
            printThird();
        }
    }
}
