using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace leetcode.Solutions
{
    public class ListNode
    {
        public int val;
        public ListNode next;
        public ListNode(int val = 0, ListNode next = null)
        {
            this.val = val;
            this.next = next;
        }
    
        public ListNode OddEvenList(ListNode head)
        {
            ListNode odd = head, even = head.next, evenHead = head.next;

            while (odd.next!=null&& even.next!=null) 
            { 
                odd.next = odd.next.next;
                even.next = even.next.next;

                odd = odd.next;
                even= even.next;
            }
            
            odd.next = evenHead;
            return head;
        }
        public static int PairSum(ListNode head)
        {
            if (head.next == null) return 0;
            else if (head.next.next == null) return (head.val + head.next.val);
            LinkedList<ListNode> list = new LinkedList<ListNode>();
            while (head != null) 
            {
                list.AddLast(head);
                head = head.next;
            }
            var first = list.First; var last = list.Last;
            int max = first.Value.val + last.Value.val;
            while (first.Next != last) 
            {
                first = first.Next; last = last.Previous;
                max = Math.Max(max, first.Value.val+last.Value.val);
            }

            return max;
        }
        public static ListNode MergeTwoLists(ListNode list1 , ListNode list2)
        {
            if(list1 == null) return list2;
            if(list2 == null) return list1;

            if (list1.val < list2.val)
            {
                list1.next = MergeTwoLists(list1.next, list2);
                return list1;
            }
            else
            {
                list2.next = MergeTwoLists(list1, list2.next);
                return list2;
            }
        }

        public static ListNode DeleteDuplicates(ListNode head)
        {
            if(head == null||head.next==null) return head;
            ListNode cur = head , prev = null;
            while(cur!=null&&cur.next!=null) 
            { 
                if (cur.val == cur.next.val)
                {
                    while (cur != null && cur.next != null && cur.val == cur.next.val)
                    {
                        cur = cur.next;
                    }
                    if (prev == null) head = cur.next;
                    else prev.next = cur.next;
                    cur = cur.next;
                }
                else
                {
                    prev = cur;
                    cur = cur.next;
                }                                                
            }
            return head;
        }
        public static bool HasCycle(ListNode head)
        {
            ListNode fastWalker = head.next,slowWalker=head;
            while(fastWalker!=null&&fastWalker.next!=null) 
            {
                fastWalker = fastWalker.next.next;
                slowWalker = slowWalker.next;
                
                if(fastWalker==slowWalker) 
                    return true;
            }
            return false;
        }
        public static ListNode MiddleNode(ListNode head)
        {
            ListNode slowWalker = head, fastWalker = head;
            while(fastWalker.next!=null && fastWalker.next.next!=null)
            {
                slowWalker = slowWalker.next;
                fastWalker = fastWalker.next.next;                
            }
            return fastWalker.next == null ? slowWalker : slowWalker.next;
        }
        public static ListNode RemoveNthFromEnd(ListNode head,int n)
        {
            ListNode frontPointer = head, backPointer = head; 
            for(int i = 0; i < n; i++)
            {
                frontPointer = frontPointer.next;
            }
            while(frontPointer.next!=null)
            {
                frontPointer = frontPointer.next;
                backPointer = backPointer.next;
            }
            
            backPointer.next = backPointer.next.next;

            return head;
        }
        public static ListNode MergeBetween(ListNode list1, int a, int b, ListNode list2)
        {
            ListNode removeStart = null, removeEnd = null, curNode = list1;
            for(int i = 0; i <= b; i++)
            {
                if (i == a-1)
                    removeStart = curNode;
                if(i == b)
                    removeEnd = curNode;

                curNode = curNode.next;
            }
            curNode = list2;
            while(curNode.next != null) 
            {
                curNode = curNode.next;
            }
            removeStart.next.next = list2;
            curNode.next = removeEnd;

            return list1;
        }
        public static ListNode ReverseList(ListNode head)
        {
            if (head == null || head.next == null)
                return head;

            ListNode prevNode = head, curNode = head.next, nextNode = null;

            prevNode.next = null;

            while(curNode != null) 
            {
                nextNode = curNode.next;
                curNode.next = prevNode;
                prevNode = curNode;
                curNode = nextNode;
            }

            return prevNode;
        }
        public static bool IsPalindrome(ListNode head)
        {
            ListNode slowHead = head, fastHead = head;

            while(fastHead != null)
            {
                slowHead = slowHead.next;
                fastHead = fastHead.next.next;
            }

            ReverseList(slowHead);
            
            while(head != null)
            {
                if (head.val != slowHead.val)
                    return false;
                head = head.next;
                slowHead = slowHead.next;
            }

            return true;
        }
        public static void ReorderList(ListNode head)
        {
            if (head == null || head.next == null)
                return;

            ListNode fastHead = head, slowHead = head;

            while (fastHead != null && fastHead.next != null)
            {
                slowHead = slowHead.next;
                fastHead = fastHead.next.next;
            }

            ListNode prevNode = null, curNode = slowHead.next, nextNode = null;
            slowHead.next = null;

            while (curNode != null)
            {
                nextNode = curNode.next;
                curNode.next = prevNode;
                prevNode = curNode;
                curNode = nextNode;
            }

            ListNode node1 = head, node2 = prevNode, node1next = null, node2next = null;
            while (node2 != null)
            {
                node1next = node1.next;
                node1.next = node2;
                node2next = node2.next;
                node2.next = node1next;
                node1 = node1next;
                node2 = node2next;
            }

        }
        public static int[] NodesBetweenCriticalPoints(ListNode head)
        {
            if (head.next == null)
                return new int[] { -1, -1 };

            List<int> criticalPoints = new List<int>();

            ListNode prevNode = head;
            ListNode curNode = head.next;
            ListNode nextNode = curNode.next;

            int[] localValues = new int[3];
            int i = 1;
            while (nextNode != null)
            {
                localValues[0] = prevNode.val;
                localValues[1] = curNode.val;
                localValues[2] = nextNode.val;

                if (localValues[1] != localValues[0] && localValues[1] != localValues[2])
                {
                    if (localValues[1] == localValues.Max() || localValues[1] == localValues.Min())
                        criticalPoints.Add(i);
                }

                prevNode = curNode;
                curNode = nextNode;
                nextNode = nextNode.next;
                i++;
            }

            if (criticalPoints.Count > 1)
            {
                int maxDistance = criticalPoints.Last() - criticalPoints.First();
                int minDistance = maxDistance;

                for(int j = 0; j < criticalPoints.Count-1;j++)
                {
                    minDistance = Math.Min(criticalPoints[j + 1] - criticalPoints[j],minDistance);
                }

                return new int[] { minDistance, maxDistance };
            }
            else
                return new int[] { -1, -1 };
        }
        public static ListNode ModifiedList (int[] nums,ListNode head)
        {
            HashSet<int> numsToDelete = new HashSet<int>(nums);
            ListNode node = head;

            while(node != null)
            {
                ListNode nextNode = node.next;
             
                while(nextNode!=null&&numsToDelete.Contains(nextNode.val))
                    nextNode = nextNode.next;

                node.next = nextNode;

                node = node.next;
            }

            if(numsToDelete.Contains(head.val))
                head = head.next;

            return head;
        }
        public static ListNode[] SplitListsToParts(ListNode head, int k)
        {
            int nodesCount = 0;
            ListNode node = head;
            ListNode[] parts = new ListNode[k];

            // Liczenie liczby węzłów w liście
            while (node != null)
            {
                nodesCount++;
                node = node.next;
            }

            int nodesPerPart = nodesCount / k; // Ilość węzłów w każdej części
            int additionalPartsCount = nodesCount % k; // Ilość części z dodatkowym węzłem

            node = head;
            for (int i = 0; i < k; i++)
            {
                ListNode currentHead = node;
                int currentPartSize = nodesPerPart + (i < additionalPartsCount ? 1 : 0); // Rozmiar bieżącej części

                for (int j = 0; j < currentPartSize - 1; j++)
                {
                    if (node != null) node = node.next;
                }

                if (node != null)
                {
                    ListNode nextPartHead = node.next;
                    node.next = null; // Oddziel aktualną część
                    node = nextPartHead;
                }

                parts[i] = currentHead; // Dodaj bieżącą część do wynikowej tablicy
            }

            return parts;
        }
    }


}
