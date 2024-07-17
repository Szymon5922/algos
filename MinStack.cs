using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace leetcode.Solutions
{
    internal class MinStack
    {
        Stack<int> values;
        Stack<int> minValues;
        int minVal;
        public MinStack()
        {
            values= new Stack<int>();
            minValues = new Stack<int>();
        }

        public void Push(int val)
        {
            if(!values.Any()||val<=minVal)
            {
                minVal=val;
                minValues.Push(val);
            }            
            values.Push(val);            
        }

        public void Pop()
        {            
            if(minVal == values.Pop())
            {
                minValues.Pop();
                if(minValues.Any())
                    minVal = minValues.Peek();
            }
        }

        public int Top()
        {
            return values.Peek();
        }

        public int GetMin()
        {
            return minVal;
        }
    }
}
