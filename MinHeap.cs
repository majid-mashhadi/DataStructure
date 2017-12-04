using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStructure
{

    public delegate int HeapComparer(object a, object b);

    public class MinHeap<T>
    {
        private int maxHeapSize = 10;
        private int size = 0;
        private T[] heapData;//
        private HeapComparer comparer;
        public MinHeap(HeapComparer comparer)
        {
            heapData = new T[maxHeapSize];
            this.comparer = comparer;
        }
        public MinHeap()
        {
            heapData = new T[maxHeapSize];
            this.comparer = null;
        }
        private int getLeftChildIndex(int index) { return index * 2 + 1; }
        private int getRightChildIndex(int index) { return index * 2 + 2; }
        private int getParentIndex(int index) { return (index - 1) / 2; }

        private bool hasLeftChild(int index) { return getLeftChildIndex(index) <= size; }
        private bool hasRightChild(int index) { return getRightChildIndex(index) <= size; }
        private bool hasParent(int index) { return getParentIndex(index) >= 0; }

        private T getLeftChildData(int index) { return heapData[getLeftChildIndex(index)]; }
        private T getRightChildData(int index) { return heapData[getRightChildIndex(index)]; }
        private T getParentData(int index) { return heapData[getParentIndex(index)]; }

        private void swap(int pIndex, int cIndex)
        {
            T temp = heapData[cIndex];
            heapData[cIndex] = heapData[pIndex];
            heapData[pIndex] = temp;
        }
        private void ensureCapacity()
        {
            if (size == maxHeapSize)
            {
                Array.Resize(ref heapData, maxHeapSize * 2);
                maxHeapSize *= 2;
            }
        }
        public int Count { get { return this.size; } }
        public T peek()
        {
            if (size == 0) throw new IndexOutOfRangeException();
            return heapData[0];
        }

        public T poll()
        {
            T head = peek();
            size--;
            heapData[0] = heapData[size];
            heapifyDown();
            return head;
        }

        public void Add(T data)
        {
            ensureCapacity();

            heapData[size] = data;
            size++;
            heapifyUp();
        }

        public void heapifyUp()
        {
            int index = size - 1;
             while (hasParent(index) && comparer (getParentData(index) , heapData[index]) > 0 )
            {
                int pIndex = getParentIndex(index);
                swap(index, pIndex);
                index = pIndex;
            }
        }
        public void heapifyDown()
        {
            int index = 0;
            while (hasLeftChild(index))
            {
                int smallestIndex = getLeftChildIndex(index);
                if (hasRightChild(index) && comparer(getRightChildData(index) , getLeftChildData(index)) < 0 )
                    smallestIndex = getRightChildIndex(index);
                if (comparer ( heapData[index] , heapData[smallestIndex]) < 0 ) break;
                else
                {
                    swap(index, smallestIndex);
                }
                index = smallestIndex;
            }
        }
        public void travers()
        {
            System.Console.WriteLine(string.Join(",", heapData));
        }
    }
}
