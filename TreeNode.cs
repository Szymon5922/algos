using Solutions;
using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Remoting.Messaging;
using System.Xml;

namespace leetcode.Solutions
{
    public class TreeNode
    {
        public int val;
        public TreeNode left;
        public TreeNode right;
        public TreeNode(int val = 0, TreeNode left = null, TreeNode right = null)
        {
            this.val = val;
            this.left = left;
            this.right = right;
        }
        public static TreeNode BuildTree(int?[] values)
        {
            if (values == null || values.Length == 0) return null;

            TreeNode root = new TreeNode(values[0].Value);
            Queue<TreeNode> queue = new Queue<TreeNode>();
            queue.Enqueue(root);

            int i = 1;
            while (i < values.Length)
            {
                TreeNode current = queue.Dequeue();

                if (values[i].HasValue)
                {
                    current.left = new TreeNode(values[i].Value);
                    queue.Enqueue(current.left);
                }
                i++;

                if (i < values.Length && values[i].HasValue)
                {
                    current.right = new TreeNode(values[i].Value);
                    queue.Enqueue(current.right);
                }
                i++;
            }

            return root;
        }

        public static bool LeafSimilar(TreeNode root1, TreeNode root2)
        {
            Stack<TreeNode> tree1 = new Stack<TreeNode>();
            Stack<TreeNode> tree2 = new Stack<TreeNode>();
            List<int> leafs1 = new List<int>();
            List<int> leafs2 = new List<int>();
            tree1.Push(root1); tree2.Push(root2);
            bool left = root1.left != null;
            bool right = root1.right != null;
            while (tree1.Count > 0)
            {
                TreeNode cur = tree1.Pop();
                if (cur.right == null && cur.left == null) leafs1.Add(cur.val);
                else if (cur.right != null) tree1.Push(cur.right);
                else if (cur.left != null) tree1.Push(cur.left);
            }
            while (tree2.Count > 0)
            {
                TreeNode cur = tree2.Pop();
                if (cur.right == null && cur.left == null) leafs2.Add(cur.val);
                else if (cur.right != null) tree2.Push(cur.right);
                else if (cur.left != null) tree2.Push(cur.left);
            }
            return leafs1.SequenceEqual(leafs2);
        }

        public static int RecursiveSumOfNodes(TreeNode root)
        {
            if (root == null) return 0;
            return (root.val + RecursiveSumOfNodes(root.left) + RecursiveSumOfNodes(root.right));
        }

        public static int RecursiveSumOfLeafs(TreeNode root)
        {
            int sum = 0;
            if (root.left == null && root.right == null) return root.val;
            if (root.left != null) sum += RecursiveSumOfLeafs(root.left);
            if (root.right != null) sum += RecursiveSumOfLeafs(root.right);
            return sum;
        }

        public static int GoodNodes(TreeNode root)
        {
            int sum = 0;

            void DFS(TreeNode node, int maxVal)
            {
                if (node == null) return;
                if (node.val >= maxVal) sum++;
                maxVal = Math.Max(node.val, maxVal);
                DFS(node.left, maxVal);
                DFS(node.right, maxVal);
            }

            DFS(root, int.MinValue);
            return sum;
        }

        public static int DFSPostOrder(TreeNode node)
        {
            int sum = 0;
            if (node == null) return 0;
            sum += DFSPostOrder(node.left);
            sum += DFSPostOrder(node.right);

            return node.val + sum;
        }
        public static int PathSum(TreeNode node)
        {
            int sum = 0;
            if (node == null) return 0;
            sum += PathSum(node.left);
            Console.WriteLine(sum);
            sum += PathSum(node.right);

            return node.val + sum;
        }

        public static IList<int> RightSideView(TreeNode root)
        {
            if (root is null) return null;
            List<int> result = new List<int>();
            Queue<TreeNode> nodes = new Queue<TreeNode>();
            nodes.Enqueue(root);
            while (nodes.Count > 0)
            {
                int rightNodeVal = 0;
                Queue<TreeNode> nextLevelNodes = new Queue<TreeNode>();
                while (nodes.Count > 0)
                {
                    TreeNode cur = nodes.Dequeue();
                    if (cur.left != null) nextLevelNodes.Enqueue(cur.left);
                    if (cur.right != null) nextLevelNodes.Enqueue(cur.right);
                    rightNodeVal = cur.val;
                }
                result.Add(rightNodeVal);
                nodes = nextLevelNodes;
            }
            return result;
        }

        public static int MaxLevelSum(TreeNode root)
        {
            if (root == null) return 0;
            Queue<TreeNode> nodes = new Queue<TreeNode>();
            int level = 1;
            int maxLevel = 1;
            int maxSum = root.val;
            nodes.Enqueue(root);
            while (nodes.Any())
            {
                Queue<TreeNode> nextLevelNodes = new Queue<TreeNode>();
                int levelSum = 0;
                while (nodes.Any())
                {
                    TreeNode cur = nodes.Dequeue();
                    levelSum += cur.val;
                    if (cur.left != null) nextLevelNodes.Enqueue(cur.left);
                    if (cur.right != null) nextLevelNodes.Enqueue(cur.right);
                }
                if (levelSum > maxSum)
                {
                    maxSum = levelSum;
                    maxLevel = level;
                }
                nodes = nextLevelNodes;
                level++;
            }

            return maxLevel;
        }

        public static TreeNode SearchBST(TreeNode root, int val)
        {
            Stack<TreeNode> nodes = new Stack<TreeNode>();
            nodes.Push(root);
            while (nodes.Any())
            {
                TreeNode cur = nodes.Pop();
                if (cur.val == val) return cur;
                else
                {
                    if (val < cur.val && cur.left != null) nodes.Push(cur.left);
                    else if (cur.right != null) nodes.Push(cur.right);
                }
            }
            return null;
        }

        public static TreeNode DeleteNode(TreeNode root, int key)
        {
            if (root == null) return null;
            if (key < root.val) root.left = DeleteNode(root.left, key);
            else if (key > root.val) root.right = DeleteNode(root.right, key);
            else
            {
                if (root.left == null & root.right == null) return null;
                else if (root.left == null || root.right == null)
                {
                    root = root.left != null ? root.left : root.right;
                }
                else
                {
                    TreeNode tmp = root.right;
                    while (tmp.left != null)
                    {
                        tmp = tmp.left;
                    }
                    root.val = tmp.val;
                    root.right = DeleteNode(tmp.right, key);
                }
            }
            return root;
        }

        public static TreeNode DFSInorder(TreeNode root)
        {
            if (root == null) return null;
            DFSInorder(root.left);
            Console.WriteLine(root.val);
            DFSInorder(root.right);
            return root;
        }

        public static int GetMinimumDifference(TreeNode root)
        {
            int dif = int.MaxValue, prev = dif;
            void DFSinorder(TreeNode node)
            {
                if (node == null) return;
                DFSinorder(node.left);
                dif = Math.Min(dif, Math.Abs(node.val - prev));
                prev = node.val;
                DFSinorder(node.right);
            }
            DFSinorder(root);
            return dif;
        }

        public static int LongestZigZag(TreeNode root)
        {
            int DFS(TreeNode node, int lsum, int rsum)
            {
                if (node == null) return Math.Max(lsum, rsum);
                else return Math.Max(DFS(node.left, rsum + 1, -1), DFS(node.right, -1, lsum + 1));
            }
            return DFS(root, -1, -1);
        }

        public static int DepthOfBST(TreeNode root)
        {
            int maxDepth = 1;
            void DFSinorder(TreeNode node, int i)
            {
                if (node == null) return;
                DFSinorder(node.left, i + 1);
                maxDepth = Math.Max(i, maxDepth);
                DFSinorder(node.right, i + 1);
            }
            DFSinorder(root, 0);
            return maxDepth;
        }
        public static int KthSmallest(TreeNode root, int k)
        {
            int i = 0, solution = 0;
            void DFSinorder(TreeNode node)
            {
                if (node == null) return;
                DFSinorder(node.left);
                i++;
                if (i == k) solution = node.val;
                DFSinorder(node.right);
            }
            DFSinorder(root);
            return solution;
        }

        public static int KthSmallest2(TreeNode root, int k)
        {
            int i = 0, solution = 0;
            int DFSinorder(TreeNode node)
            {
                if (node == null) return 0;
                DFSinorder(node.left);
                i++;
                Console.WriteLine(i);
                if (i == k) solution = node.val;
                DFSinorder(node.right);
                return solution;
            }
            DFSinorder(root);

            return solution;
        }

        public static IList<double> AverageOfLevels(TreeNode root)
        {
            List<double> levelsAvg = new List<double>();
            Queue<TreeNode> nodes = new Queue<TreeNode>();
            nodes.Enqueue(root);
            while (nodes.Any())
            {
                double levelSum = 0;
                foreach (TreeNode node in nodes)
                {
                    levelSum += node.val;
                }
                levelsAvg.Add((double)levelSum / nodes.Count);
                Queue<TreeNode> nextLevelNodes = new Queue<TreeNode>();
                while (nodes.Any())
                {
                    TreeNode curNode = nodes.Dequeue();
                    //Console.WriteLine(curNode.val);
                    if (curNode.left != null) nextLevelNodes.Enqueue(curNode.left);
                    if (curNode.right != null) nextLevelNodes.Enqueue(curNode.right);
                }
                nodes = nextLevelNodes;
            }
            return levelsAvg;
        }

        public static bool IsSameTree(TreeNode p, TreeNode q)
        {
            int DFSPostOrder(TreeNode node)
            {
                if (node == null) return 0;
                Console.WriteLine(DFSPostOrder(node.left));
                Console.WriteLine(DFSPostOrder(node.right));

                return node.val;
            }
            Console.WriteLine(DFSPostOrder(p));
            Console.WriteLine("chuj");
            Console.WriteLine(DFSPostOrder(q));
            return false;
        }
        public static bool HasPathSum(TreeNode root, int targetSum)
        {
            bool hasPath = false;
            int tmpNodeVal = 0, sum = 0;
            void DFSpreorder(TreeNode node)
            {
                if (node == null) return;
                sum += node.val;
                if (sum == targetSum) hasPath = true;
                DFSpreorder(node.left);
                tmpNodeVal = node.val;
                DFSpreorder(node.right);
                tmpNodeVal = node.val;
                sum -= tmpNodeVal;
            }
            DFSpreorder(root);
            return hasPath;
        }
        public static bool HasPathSum2(TreeNode root, int targetSum)
        {
            if (root == null) return false;
            Stack<(TreeNode node, int sum)> stack = new Stack<(TreeNode, int)>();
            stack.Push((root, 0));
            while (stack.Count > 0)
            {
                var (node, sum) = stack.Pop();
                sum += node.val;
                if (node.left == null && node.right == null)
                {
                    if (sum == targetSum) return true;
                }
                if (node.left != null) stack.Push((node.left, sum));
                if (node.right != null) stack.Push((node.right, sum));
            }
            return false;
        }
        public static bool IsSymetric(TreeNode root)
        {
            if (root == null) return true;
            return IsMirror(root.left, root.right);
        }

    public static bool IsMirror(TreeNode left, TreeNode right)
    {
        if (left == null && right == null) return true;
        if (left == null || right == null) return false;

        return (left.val == right.val)
            && IsMirror(left.left, right.right)
            && IsMirror(left.right, right.left);
    }
    public static void DFSinorder(TreeNode root)
        {
            if(root==null) return;
            DFSinorder(root.left);
            Console.WriteLine(root.val);
            DFSinorder(root.right);
        }
    public static IList<IList<int>> LevelOrder (TreeNode root)
        {
            if (root == null) return new List<IList<int>>();
            Queue<TreeNode> level = new Queue<TreeNode>();
            Queue<TreeNode> nextLevel = new Queue<TreeNode>();
            List<int> levelVal = new List<int>();
            List<IList<int>> res = new List<IList<int>>();
            level.Enqueue(root);
            while(level.Count>0)
            {
                TreeNode node = level.Dequeue();
                levelVal.Add(node.val);
                if (node.left != null) nextLevel.Enqueue(node.left);
                if (node.right != null) nextLevel.Enqueue(node.right);
                if(level.Count==0)
                {
                    res.Add(levelVal);
                    levelVal = new List<int>();
                    foreach(TreeNode n in nextLevel)
                    {
                        level.Enqueue(n);
                    }
                    nextLevel.Clear();
                }
            }
            return res;
        }
        public static IList<IList<int>> LevelOrder2 (TreeNode root)
        {
            if (root == null) return new List<IList<int>>();
            List<int> levelVal = new List<int>();
            List<IList<int>> res = new List<IList<int>>();
            Queue<TreeNode> level = new Queue<TreeNode>();
            level.Enqueue(root);
            level.Enqueue(null);
            while(level.Count>0)
            {
                TreeNode node = level.Dequeue();
                if(node!=null)
                {
                    levelVal.Add(node.val);
                    if (node.left != null) level.Enqueue(node.left);
                    if (node.right != null) level.Enqueue(node.right);
                }
                else
                {
                    res.Add(levelVal);
                    if(level.Count>0)
                    {
                        level.Enqueue(null);
                        levelVal = new List<int>();
                    }
                }
            }

            return res;
        }
        public static bool IsValidBST(TreeNode root) 
        {
            Stack<int> nodes = new Stack<int>();
            void DFSinorder(TreeNode node)
            {
                if (node == null) return;
                DFSinorder(node.left);
                nodes.Push(node.val);
                DFSinorder(node.right);
            }
            DFSinorder(root);
            SortedSet<int> chuj = new SortedSet<int>(nodes);
            if (nodes.Reverse().SequenceEqual(new SortedSet<int>(nodes))) return true;
            else return false;
        }
        public static IList<int> InorderTraversal(TreeNode root)
        {
            List<int> nodes = new List<int>();           
            void DFSinorder(TreeNode node)
            {
                if (root == null) return;
                DFSinorder(root.left);
                nodes.Add(root.val);
                DFSinorder(root.right);
            }
            DFSinorder(root);
            return nodes;
        }
        public static TreeNode SortedArrayToBST(int[] nums)
        {
            TreeNode CreateTree(int[] num,int start, int end)
            {
                if (start > end) return null;
                int mid = (start+end) / 2;
                TreeNode node = new TreeNode(num[mid]);
                node.left=CreateTree(num, start, mid-1);
                node.right=CreateTree(num, mid+1, end);
                return node;
            }
            return CreateTree(nums, 0, nums.Length-1);
        }
        public static int FindBottomLeftValue(TreeNode root)
        {
            (int, int) bottomVal = (0, 0);
            int lvl = 0;
            void DFS(TreeNode node)
            {
                if(node == null) 
                    return;

                lvl++;
                if(lvl>bottomVal.Item1)
                {
                    bottomVal.Item1 = lvl;
                    bottomVal.Item2 = node.val;
                }
                DFS(node.left);
                DFS(node.right);
                lvl--;
            }
            DFS(root);
            return bottomVal.Item2;
        }
        public static bool IsEvenOddTree(TreeNode root)
        {
            int treeLevel = 0;
            Queue<TreeNode> curLevel = new Queue<TreeNode>();
            Queue<TreeNode> nextLevel = new Queue<TreeNode>();
            //if(root.left!= null)
            //    nextLevel.Enqueue(root.left);
            //if(root.right != null)
            //    nextLevel.Enqueue(root.right);
            //if (nextLevel.Count == 0)
            //    return true;

            nextLevel.Enqueue(root);

            while(nextLevel.Count > 0) 
            {
                while(nextLevel.Count > 0) 
                {
                    //Console.WriteLine(nextLevel.Peek().val+" lvl:"+treeLevel);
                    TreeNode node = nextLevel.Dequeue();

                    if(IsEven(treeLevel))
                    {
                        if (IsEven(node.val))
                            return false;

                        if(nextLevel.Count>0)
                        {
                            if (!(node.val < nextLevel.Peek().val))
                                return false;
                        }
                    }
                    else
                    {
                        if (!IsEven(node.val))
                            return false;
                        
                        if (nextLevel.Count > 0)
                        {
                            if (!(node.val > nextLevel.Peek().val))
                                return false;
                        }
                    }
                    

                    curLevel.Enqueue(node);
                }
                while(curLevel.Count>0)
                {
                    TreeNode node = curLevel.Dequeue();
                    if(node.left != null)
                        nextLevel.Enqueue(node.left);
                    
                    if(node.right != null)
                        nextLevel.Enqueue(node.right);
                }
                //Console.WriteLine("end of lvl");
                treeLevel++;
            }
            
            bool IsEven(int lvl)
            {
                return lvl % 2 == 0 ? true : false;
            }

            return true;
        }
        public static int SumOfLeftLeaves(TreeNode root)
        {
            int sum = 0;
            void DFS(TreeNode node)
            {
                if (node == null) return;
                DFS(node.left);
                if (node.left != null && node.left.left==null && node.left.right==null)
                    sum+=node.left.val;
                DFS(node.right);
            }

            DFS(root);

            return sum;
        }
        public static int SumNumbers(TreeNode root)
        {
            int sum = 0;

            StringBuilder sb = new StringBuilder();

            void DFS(TreeNode node)
            {
                if (node == null)
                    return;
                sb.Append(node.val);
                if(node.left==null&&node.right==null)
                {
                    sum += Int32.Parse(sb.ToString());
                    sb.Remove(sb.Length - 1, 1);
                    return;
                }
                DFS(node.left);
                DFS(node.right);
                sb.Remove(sb.Length - 1, 1);
            }

            DFS(root);

            return sum;
        }
        public static string SmallestFromLeaf(TreeNode root)
        {
            StringBuilder sb = new StringBuilder();

            List<string> pathStrings = new List<string>();

            void Dfs(TreeNode node)
            {
                if (node == null)
                    return;
                sb.Append((char)(node.val+97));
                if(node.left==null&&node.right==null)
                {
                    pathStrings.Add(new string(sb.ToString().Reverse().ToArray()));
                    sb.Remove(sb.Length - 1,1);
                    return;
                }
                Dfs(node.left);
                Dfs(node.right);
                sb.Remove(sb.Length - 1, 1);
            }

            Dfs(root);

            return pathStrings.Min();
        }
        public static bool EvaluateTree(TreeNode root)
        {
            bool DFS(TreeNode node)
            {
                if (node.val == 2)
                {
                    if (DFS(node.left) || DFS(node.right))
                        return true;
                    else
                        return false;
                }
                else if (node.val == 3)
                {
                    if (DFS(node.left) && DFS(node.right))
                        return true;
                    else
                        return false;
                }
                else if (node.val == 1)
                    return true;
                else
                    return false;
            }
        
        
            return DFS(root);
        }
        public static TreeNode RemoveLeafNodes(TreeNode root,int target)
        {
            void DFS(TreeNode node) 
            {
                if (node == null)
                    return;
                
                DFS(node.left);
                DFS(node.right);

                if(node.left!=null&&isLeaf(node.left))
                {
                    if (node.left.val == target)
                        node.left = null;
                }
                if(node.right!=null&&isLeaf(node.right))
                {
                    if (node.right.val == target)
                        node.right = null;
                }

                
            }

            bool isLeaf(TreeNode node)
            {
                if (node.left == null && node.right == null)
                    return true;
                else
                    return false;
            }

            DFS(root);

            if (isLeaf(root) && root.val == target)
                return null;

            return root;
        }
        public static IList<IList<string>> Partition(string s)
        {
            IList<IList<string>> substrings = new List<IList<string>>();

            DFS(0, new List<string>());

            void DFS(int start, List<string> currentSubstrings)
            {
                if(start >= s.Length)
                    substrings.Add(new List<string>(currentSubstrings));

                for(int end = start; end < s.Length; end++)
                {
                    if(isPalindrome(s,start,end))
                    {
                        currentSubstrings.Add(s.Substring(start, end - start + 1));

                        DFS(end + 1, currentSubstrings);

                        currentSubstrings.RemoveAt(currentSubstrings.Count - 1);
                    }    
                }
            }

            bool isPalindrome(string substring,int l, int r)
            {
                while(l < r)
                {
                    if (substring[l] != substring[r])
                        return false;
                }

                return true;
            }

            return substrings;
        }
        public static TreeNode BalanceBST(TreeNode root)
        {
            List<int> BSTvalues = new List<int>();

            void fillBstValuesList(TreeNode node)
            {
                if (node == null)
                    return;
                fillBstValuesList(node.left);
                BSTvalues.Add(node.val);
                fillBstValuesList(node.right);
            }

            fillBstValuesList(root);

            TreeNode sortedArrayToBST(int start, int end)
            {
                if (start > end)
                    return null;

                int mid = (start + end) / 2;
                TreeNode node = new TreeNode(BSTvalues[mid]);

                node.left = sortedArrayToBST(start, mid-1);
                node.right = sortedArrayToBST(mid+1, end);

                return node;
            }

            return sortedArrayToBST(0,BSTvalues.Count);
        }
        public static ListNode MergeNodes(ListNode head)
        {
            int currentSum = 0;
            ListNode currentZeroNode = head;
            ListNode node = head.next;

            while(node.next != null) 
            {
                if(node.val==0)
                {
                    currentZeroNode.val = currentSum;
                    currentZeroNode.next = node;
                    currentZeroNode = node;
                    currentSum = 0;
                }
                currentSum += node.val;
                node = node.next;
            }
            node.val = currentSum;
            node.next = null;
            currentZeroNode.next = node;
            return head;
        }
        public static TreeNode CreateBinaryTree(int[][] descriptions)
        {
            Dictionary<int,TreeNode> nodes = new Dictionary<int, TreeNode>();
            List<int> childs = new List<int>();

            foreach (int[] description in descriptions)
            {
                int nodeVal = description[0];
                int childVal = description[1];

                childs.Add(childVal);

                if (!nodes.ContainsKey(nodeVal))
                    nodes.Add(nodeVal, new TreeNode(nodeVal));

                if (!nodes.ContainsKey(childVal)) 
                    nodes.Add(childVal,new TreeNode(childVal));

                if (description[2] == 1)
                    nodes[nodeVal].left = nodes[childVal];
                else
                    nodes[nodeVal].right = nodes[childVal];
            }
            
            int root = nodes.Keys.Except(childs).First();

            return nodes[root];
        }
        public static string GetDirections(TreeNode root, int startValue, int destValue)
        {
            StringBuilder pathToStart = new StringBuilder();
            StringBuilder pathToDest = new StringBuilder();

            FindPath(root, pathToDest, destValue);
            FindPath(root, pathToStart, startValue);

            int commonLength = 0;
            while (commonLength < pathToStart.Length
                && commonLength < pathToDest.Length
                && pathToStart[commonLength] == pathToDest[commonLength])
                commonLength++;

            StringBuilder result = new StringBuilder();
            for (int i = commonLength; i < pathToStart.Length; i++)
            {
                result.Append('U');
            }
            result.Append(pathToDest.ToString().Substring(commonLength));

            return result.ToString();

            bool FindPath(TreeNode node, StringBuilder path, int dest)
            {
                if (node == null)
                    return false;

                if (node.val == dest)
                    return true;

                path.Append('L');
                if (FindPath(node.left, path, dest))
                    return true;
                path.Length--;

                path.Append('R');
                if (FindPath(node.right, path, dest))
                    return true;
                path.Length--;

                return false;
            }
        }
        public static IList<TreeNode> DelNodes(TreeNode root, int[] to_delete)
        {
            IList<TreeNode> roots = new List<TreeNode>();

            void DFSpostOrder(TreeNode node)
            {
                if (node == null) 
                    return;

                DFSpostOrder(node.left);
                DFSpostOrder(node.right);
                
                if(node.left!= null)
                {
                    if(to_delete.Contains(node.left.val))
                    {
                        if (node.left.left != null)
                            roots.Add(node.left.left);
                        if (node.left.right != null)
                            roots.Add(node.left.right);

                        node.left = null;
                    }
                }
                if(node.right != null)
                {
                    if(to_delete.Contains(node.right.val))
                    {
                        if (node.right.left != null)
                            roots.Add(node.right.left);
                        if (node.right.right != null)
                            roots.Add(node.right.right);

                        node.right = null;
                    }
                }
            }

            DFSpostOrder(root);

            if (!to_delete.Contains(root.val))
                roots.Add(root);
            else
            {
                if(root.left!=null)
                    roots.Add(root.left); 
                if(root.right!=null) 
                    roots.Add(root.right);
            }

            return roots;
        }
        public static int CountPairs(TreeNode root, int distance)
        {
            int count = 0;
            int level = 0;
            Queue<TreeNode> currentLevel = new Queue<TreeNode>();
            Queue<TreeNode> nextLevel = new Queue<TreeNode>();
            Dictionary<TreeNode,TreeNode> ancestors = new Dictionary<TreeNode,TreeNode>();
            Dictionary<TreeNode, int> nodeLevel = new Dictionary<TreeNode, int>();
            List<TreeNode> leafs = new List<TreeNode>();

            if (root != null)
            {
                currentLevel.Enqueue(root);
                level++;
            }

            while(currentLevel.Count > 0)
            {
                TreeNode currentNode = currentLevel.Dequeue();

                nodeLevel.Add(currentNode, level);

                int childs = 2;
                if (currentNode.left != null)
                {
                    nextLevel.Enqueue(currentNode.left);
                    ancestors.Add(currentNode.left, currentNode);
                }
                else
                    childs--;
                if (currentNode.right != null)
                {
                    nextLevel.Enqueue(currentNode.right);
                    ancestors.Add(currentNode.right, currentNode);
                }
                else
                    childs--;

                if (childs == 0)
                    leafs.Add(currentNode);

                if(currentLevel.Count==0)
                {
                    currentLevel=new Queue<TreeNode>(nextLevel);
                    nextLevel.Clear();
                    level++;
                }
            }

            for (int i = 0; i < leafs.Count-1;i++)
            {
                for(int j = i+1; j < leafs.Count;j++)
                {
                    int currentDistance = 0;
                    TreeNode leftNode = leafs[i];
                    TreeNode rightNode = leafs[j];

                    while (nodeLevel[leftNode] != nodeLevel[rightNode])
                    {
                        if (nodeLevel[leftNode] > nodeLevel[rightNode])
                            leftNode = ancestors[leftNode];
                        else
                            rightNode = ancestors[rightNode];

                        currentDistance++;
                    }

                    while(currentDistance<=distance)
                    {
                        if (leftNode == rightNode)
                        {
                            count++;
                            break;
                        }

                        leftNode = ancestors[leftNode];
                        rightNode = ancestors[rightNode];
                        currentDistance += 2;
                    }
                }
            }    

            return count;
        }
        public static long KthLargestLevelSum(TreeNode root, int k)
        {
            List<long> levelsSums = new List<long>();
            Queue<TreeNode> currentLevel = new Queue<TreeNode>();
            Queue<TreeNode> nextLevel = new Queue<TreeNode>();

            nextLevel.Enqueue(root);

            while (nextLevel.Count > 0)
            {
                long levelSum = 0;
                currentLevel = new Queue<TreeNode>(nextLevel);
                nextLevel.Clear();

                while(currentLevel.Count > 0)
                {
                    TreeNode node = currentLevel.Dequeue();

                    levelSum += node.val;

                    if(node.left != null)
                        nextLevel.Enqueue(node.left);
                    if (node.right != null)
                        nextLevel.Enqueue(node.right);
                }

                levelsSums.Add(levelSum);
            }

            try
            {
                return levelsSums.OrderByDescending(x => x).Skip(k-1).First();
            }
            catch
            {
                return -1;
            }
        }
        public static IList<int> LargestValues(TreeNode root)
        {
            List<int> largestValues = new List<int>();
            Queue<TreeNode> currentlevel = new Queue<TreeNode> ();
            Queue<TreeNode> nextLevel = new Queue<TreeNode> ();

            nextLevel.Enqueue(root);

            while(nextLevel.Any())
            {
                int currentLevelMax = 0;

                currentlevel = new Queue<TreeNode>(nextLevel);
                nextLevel.Clear();

                while(currentlevel.Any())
                {
                    var node = currentlevel.Dequeue();

                    currentLevelMax = Math.Max(currentLevelMax, node.val);

                    if (node.left != null)
                        nextLevel.Enqueue(node.left);

                    if (node.right != null)
                        nextLevel.Enqueue(node.right);
                }

                largestValues.Add(currentLevelMax);
            }

            return largestValues;
        }
    }


}
