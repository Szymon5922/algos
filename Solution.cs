using leetcode.Solutions;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection.Emit;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Solutions
{
    public class Solution
    {
        public static string ReverseWords(string s)
        {
            string[] words = s.Split(' ')
                            .Where(x => !(string.IsNullOrWhiteSpace(x) && string.IsNullOrEmpty(x))).ToArray();
            string reversed = string.Empty;
            foreach (string word in words.Reverse()) reversed += word + " ";
            return reversed.Trim();
        }
        public static int MaxVowels(string s, int k)
        {
            string vowels = "aeiou";
            int winsum = 0;
            for (int i = 0; i < k; i++)
            {
                if (vowels.Contains(s[i])) winsum++;
            }
            if (winsum == k) { return k; }
            int maxsum = winsum;
            for (int i = k; i < s.Length; i++)
            {
                if (vowels.Contains(s[i - k])) winsum--;
                if (vowels.Contains(s[i])) winsum++;
                maxsum = Math.Max(winsum, maxsum);
            }
            return maxsum;
        }

        public static int longestOnes(int[] nums, int k)
        {
            int i = 0, j = 0, maxLen = 0;
            if (k == 0)
            {
                while (true)
                {
                    i = Array.IndexOf(nums, 1, i);
                    if (i != -1)
                    {
                        j = Array.IndexOf(nums, 0, i + 1);
                        if (j == -1) j = nums.Length;
                        j--;
                    }
                    else break;
                    maxLen = Math.Max(maxLen, (j - i) + 1);
                    i = j + 1;
                }
                return maxLen;
            }
            else
            {
                int zeros = 0; j = -1;
                while (zeros < k && j < nums.Length - 1)
                {
                    j++;
                    if (nums[j] == 0)
                    {
                        zeros++;
                    }
                }
                if (j + 1 < nums.Length && nums[j + 1] == 1)
                {
                    j = Array.IndexOf(nums, 0, j + 1);
                    if (j == -1) j = nums.Length; else j--;
                }
                if (j == nums.Length) return j;
                else maxLen = j + 1;
                if (j == -1) j = 0;
                while (j < nums.Length)
                {
                    i++;
                    if (nums[i - 1] == 0)
                    {
                        j++;
                        if (j + 1 < nums.Length && nums[j + 1] == 1)
                        {
                            j = Array.IndexOf(nums, 0, j + 1);
                            if (j == -1) j = nums.Length; else j--;
                        }
                        if (j == nums.Length || j == i || k == 0) maxLen = Math.Max(j - (i + 1), maxLen);
                        else maxLen = Math.Max(j - (i - 1), maxLen);
                    }

                }
                return maxLen;
            }
        }

        public static bool IncreasingTriplet(int[] nums)
        {
            for (int i = 0; i < nums.Length - 2; i++)
            {
                if (nums.Distinct().Count() < 3) return false;
                int num3 = int.MinValue;
                IEnumerable<int> nums2 = nums.Skip(i).Where(x => x > nums[i]);
                if (nums2.Count() == 0 || nums2.Distinct().Count() < 2) continue;
                else
                {
                    for (int j = 0; j < nums2.Count(); j++)
                    {
                        try
                        {
                            num3 = nums2.Skip(j + 1).First(x => x > nums2.ElementAt(j));
                            return true;
                        }
                        catch (InvalidOperationException)
                        {
                            continue;
                        }
                    }
                }
            }
            return false;
        }

        public static int LargestAltitude(int[] gain)
        {
            int prefixSum = 0, highestAlt = 0;
            foreach (int num in gain)
            {
                prefixSum += num;
                highestAlt = Math.Max(prefixSum, highestAlt);
            }
            return highestAlt;
        }

        public static int PivotIndex(int[] nums)
        {
            int leftSum = 0, rightsum = nums.Sum() - nums[0];
            for (int i = 1; i < nums.Length; i++)
            {
                if (leftSum == rightsum) return i - 1;
                else
                {
                    leftSum += nums[i - 1];
                    rightsum -= nums[i];
                }
            }
            return -1;
        }

        public IList<IList<int>> FindDifference(int[] nums1, int[] nums2)
        {
            HashSet<int> hash1 = new HashSet<int>(nums1);
            HashSet<int> hash2 = new HashSet<int>(nums2);
            IList<int> except1 = new List<int>(hash1.Except(hash2));
            IList<int> except2 = new List<int>(hash2.Except(hash1));
            IList<IList<int>> result = new List<IList<int>>();
            result.Add(except1);
            result.Add(except2);
            return result;
        }

        public static bool UniqueOccurences(int[] arr)
        {
            HashSet<int> numbers = new HashSet<int>(arr);
            HashSet<int> occurences = new HashSet<int>();
            foreach (int number in numbers)
            {
                int occurence = arr.Count(x => x == number);
                if (occurences.Contains(occurence)) return false;
                else occurences.Add(occurence);
            }
            return true;
        }

        public static bool CloseStrings(string word1, string word2)
        {
            if (word1.Length != word2.Length) return false;
            HashSet<char> letters1 = new HashSet<char>(word1);
            HashSet<char> letters2 = new HashSet<char>(word2);
            if (!letters1.SetEquals(letters2)) return false;
            List<int> occurences1 = new List<int>();
            List<int> occurences2 = new List<int>();
            foreach (char letter in letters1)
            {
                occurences1.Add(word1.Count(c => c == letter));
            }
            foreach (char letter in letters2)
            {
                occurences2.Add(word2.Count(c => c == letter));
            }
            if (occurences1.SequenceEqual(occurences2)) return true;
            else return false;
        }
        public static int EqualPairs(int[][] grid)
        {
            List<int[]> rows = new List<int[]>(grid);

            List<int[]> columns = new List<int[]>();
            int pairs = 0;
            for (int i = 0; i < grid.Length; i++)
            {
                int[] column = new int[grid.Length];
                for (int j = 0; j < grid.Length; j++)
                {
                    column[j] = grid[j][i];
                }
                columns.Add(column);

            }
            foreach (int[] row in rows)
            {
                //if (columns.Any(p => p.SequenceEqual(row))) pairs++;
                pairs += columns.Count(p => p.SequenceEqual(row));
            }
            return pairs;
        }

        public static string RemoveStars(string s)
        {
            Stack<char> chars = new Stack<char>();
            foreach (char c in s)
            {
                if (c != '*') chars.Push(c);
                else chars.Pop();
            }
            char[] charsArray = chars.ToArray();
            Array.Reverse(charsArray);
            return new string(charsArray);
        }

        public static string DecodeString(string s)
        {
            Stack<char> chars = new Stack<char>();
            for (int i = 0; i < s.Length; i++)
            {
                if (s[i] != ']') chars.Push(s[i]);
                else
                {
                    string tmp = string.Empty;
                    while (chars.Peek() != '[')
                    {
                        tmp += chars.Pop();
                    }
                    tmp.Reverse();
                    chars.Pop();
                    string tmp2 = string.Empty, nums = string.Empty;
                    int multiplier;
                    while (chars.Count > 0 && Char.IsDigit(chars.Peek()))
                    {
                        nums += chars.Pop();
                    }
                    if (nums.Length == 1)
                    {
                        multiplier = nums[0] - '0';
                    }
                    else
                    {
                        char[] digitArray = nums.ToCharArray();
                        Array.Reverse(digitArray);
                        multiplier = Int32.Parse(new string(digitArray));
                    }
                    for (; multiplier > 0; multiplier--)
                    {
                        tmp2 += tmp;
                    }
                    for (int j = tmp2.Length; j > 0; j--)
                    { chars.Push(tmp2[j - 1]); }
                }
            }
            char[] charsArray = chars.ToArray();
            Array.Reverse(charsArray);
            return new string(charsArray);
        }

        public static string PredictPartyVictory(string senate)
        {
            while (senate.Contains('D') && senate.Contains('R'))
            {
                Queue<char> senateQueue = new Queue<char>();
                char actual = senate[0]; char negative;
                if (actual == 'D') negative = 'R'; else negative = 'D';
                for (int i = 1, j = senate.IndexOf(negative); i < senate.Length; i++)
                {
                    if (i == j) continue;
                    else senateQueue.Enqueue(senate[i]);
                }
                senateQueue.Enqueue(actual);
                senate = new string(senateQueue.ToArray());
            }
            if (senate[0] == 'R') return "Radiant";
            else return "Dire";
        }

        public static int[] AsteroidCollision(int[] asteroids)
        {
            Stack<int> stack = new Stack<int>();
            int actual, next;

            stack.Push(asteroids[0]);
            for (int i = 1; i < asteroids.Length; i++)
            {
                next = asteroids[i];
                try
                { actual = stack.Peek(); }
                catch (System.InvalidOperationException)
                {
                    stack.Push(next);
                    continue;
                }
                bool positive = actual > 0;
                bool negative = next < 0;
                bool colision = positive && negative;
                if (colision)
                {
                    if (actual > Math.Abs(next))
                    {
                        continue;
                    }
                    else if (actual < Math.Abs(next))
                    {
                        stack.Pop();
                        i--;
                    }
                    else//actual==next
                    {
                        stack.Pop();
                    }
                    continue;
                }
                else
                {
                    stack.Push(next);
                }


            }

            return stack.Reverse().ToArray();
        }

        public static bool CanConstruct(string ransomNote, string magazine)
        {
            Dictionary<char, int> letterOccurences = new Dictionary<char, int>();
            foreach (char c in magazine.Distinct())
            {
                letterOccurences.Add(c, magazine.Count(x => x == c));
            }
            foreach (char c in ransomNote.Distinct())
            {
                if (letterOccurences.ContainsKey(c))
                {
                    if (letterOccurences[c] <= ransomNote.Count(x => x == c)) return false;
                }
                else return false;
            }
            return true;
        }

        public static bool IsIsomorphic(string s, string t)
        {
            Dictionary<char, char> map = new Dictionary<char, char>();
            for (int i = 0; i < s.Length; i++)
            {
                if (map.ContainsKey(s[i]))
                {
                    if (map[s[i]] != t[i]) return false;
                }
                else if (map.ContainsValue(t[i]))
                {
                    if (map
                        .FirstOrDefault(x => x.Value == s[i])
                        .Key != t[i])
                        return false;
                }
                else map.Add(s[i], t[i]);
            }
            return true;
        }

        public static bool WordPattern(string pattern, string s)
        {
            string[] words = s.Split(' ');
            if (pattern.Length != words.Length) return false;
            Dictionary<char, string> wordToPattern = new Dictionary<char, string>();
            for (int i = 0; i < pattern.Length; i++)
            {
                if (wordToPattern.ContainsKey(pattern[i]))
                {
                    if (wordToPattern[pattern[i]] != words[i]) return false;
                }
                else if (wordToPattern.ContainsValue(words[i]))
                {
                    if (wordToPattern.FirstOrDefault(x => x.Value == words[i]).Key != pattern[i]) return false;
                }
                else wordToPattern.Add(pattern[i], words[i]);
            }
            return true;
        }

        public static int LongestConsecutive(int[] nums)
        {
            if (nums == null) return 0;
            SortedSet<int> sortedNums = new SortedSet<int>(nums);
            int maxSum = 0, sum = 1;
            int point = sortedNums.Min;
            foreach (int num in sortedNums)
            {
                if (point + 1 == num) sum++;
                else
                {
                    maxSum = Math.Max(sum, maxSum);
                    sum = 1;
                }
                point = num;
            }
            maxSum = Math.Max(sum, maxSum);
            return maxSum;
        }

        public static string SimplifyingPath(string path)
        {
            string[] folders = path.Split(new[] {'/'},StringSplitOptions.RemoveEmptyEntries);
            Stack<string> folderStack = new Stack<string>();
            foreach(string f in folders)
            {
                if (f != "..") folderStack.Push(f);
                else if (f == ".." && folderStack.Count > 0) folderStack.Pop();
                else continue;
            }
            if (folderStack.Count == 0) return "/";
            return String.Join("/", folderStack.Reverse().ToArray());
        }


        public static string RandomVerse(string lyrics)
        {
            string[] lines = lyrics.Split(new[] { Environment.NewLine },StringSplitOptions.RemoveEmptyEntries);
            Random r = new Random();
            int rInt = r.Next(0,lines.Length);
            int rInt2 = r.Next(0, lines.Length);
            return lines[rInt] + "\r\n" + lines[rInt2];
        }

        public static int LengthOfLastWord(string s)
        {
            return s.TrimEnd().Substring(s.TrimEnd().LastIndexOf(' ')+1).Length;
        }

        public static void Rotate(ref int[] nums, int k)
        {
            if (nums.Length <= 1 || k == 0) return;
            if(k>nums.Length)k = k % nums.Length;
            int[] nums2 = (int[])nums.Clone();
            int len = nums.Length, j = len - k, i = 0;
            while (i < len)
            {
                nums[i++] = nums2[j++];
                if (j == len) j = 0;
            }
        }

        public static int[] TwoSum(int[] numbers, int target)
        {
            int leftPtr = 0, rightPtr = numbers.Length - 1, ptrSum = numbers[leftPtr] + numbers[rightPtr];
            while(ptrSum != target) 
            {
                if (ptrSum > target) rightPtr--;
                else leftPtr++;
                ptrSum = numbers[leftPtr] + numbers[rightPtr];
            }
            leftPtr++;rightPtr++;
            return new int[] {leftPtr,rightPtr};
        }

        public static int MinSubArrayLen(int target, int[] nums)
        {
            if(nums.Max() >= target) return 1;
            int leftPtr = 0, rightPtr = 0, sum = nums[leftPtr], subArrayLen = 1, minSubArrayLen = nums.Length;
            while (sum < target&&rightPtr<nums.Length-1)
            {
                rightPtr++;
                sum += nums[rightPtr];
                subArrayLen++;
            }
            if (sum < target) return 0;
            while(leftPtr!=rightPtr)
            {
                if(sum>=target)
                {
                    minSubArrayLen = Math.Min(minSubArrayLen, subArrayLen);
                    sum -= nums[leftPtr];
                    leftPtr++;
                    subArrayLen--;
                }
                else if (rightPtr<nums.Length-1)
                {
                    rightPtr++;
                    sum += nums[rightPtr];
                    subArrayLen++;
                }
                else
                {
                    sum -= nums[leftPtr];
                    leftPtr++;
                    subArrayLen--;
                }
            }
            return minSubArrayLen;
        }

        public static bool IsValid(string s)
        {
            if (s.Length % 2 != 0) return false;
            else
            {
                byte[] asciiBytes = Encoding.ASCII.GetBytes(s);
                Stack<byte> asciiBytesStack = new Stack<byte>();
                asciiBytesStack.Push(asciiBytes[0]);
                for (int i = 1; i < s.Length; i++)
                {
                    if (asciiBytes[i] == 40 || asciiBytes[i]==91 || asciiBytes[i]==123)
                    {
                        asciiBytesStack.Push(asciiBytes[i]);
                    }
                    else
                    {
                        if (asciiBytesStack.Count == 0) return false;
                        if (asciiBytes[i] == 41) { if (asciiBytesStack.Pop() != 40) return false; }
                        else { if (asciiBytesStack.Pop() + 2 != asciiBytes[i]) return false; }
                    }                                        
                }
                if (asciiBytesStack.Count != 0) return false;
            }
            return true;
        }
        public static int LengthOfLongestSubstring(string s)
        {
            Dictionary<char, int> chars = new Dictionary<char, int>();
            int lPtr = 0, pPtr = 0, maxLen = 0;
            //chars.Add(s[0], 0);
            while (pPtr < s.Length)
            {
                if (!chars.ContainsKey(s[pPtr]))
                {
                    chars.Add(s[pPtr], pPtr);
                    pPtr++;
                }
                else if (s[lPtr] == s[pPtr]) 
                {
                    maxLen = Math.Max(maxLen, chars.Count);
                    chars[s[lPtr]] = pPtr;
                    pPtr++;
                    lPtr++; 
                }
                else
                {
                    maxLen = Math.Max(maxLen, chars.Count);
                    lPtr = s.IndexOf(s[pPtr], lPtr + 1);
                    chars = chars.Where(d => d.Value >= lPtr).ToDictionary(d => d.Key, d => d.Value);
                }
            }
            maxLen = Math.Max(maxLen, chars.Count);

            return maxLen;
        }

        public static IList<IList<string>> GroupAnagrams(string[] strs)
        {
            List<IList<string>> anagramGroups = new List<IList<string>>();
            List<string> words = new List<string>(strs);
            Dictionary<string, List<string>> anagrams = new Dictionary<string, List<string>>();
            foreach(string w in words) 
            {
                string sortedWord = String.Concat(w.OrderBy(c => c));
                if(!anagrams.ContainsKey(sortedWord))
                {
                    anagrams.Add(sortedWord, new List<string>());

                }               
                anagrams[sortedWord].Add(w);                
            }
            foreach(var list in anagrams)
            {
                anagramGroups.Add(list.Value);
            }
            return anagramGroups;
        }
        public static int HammingWeight(uint n)
        {
            int count = 0;
            while (n != 0)
            {
                count++;
                n &= (n - 1);
            }
            return count;
        }
        public static bool ArrayStringsEqual(string[] word1, string[] word2)
        {
            int i1 = 0, i2 = 0, s1 = 0, s2 = 0;
            while (s1 < word1.Length && s2 < word2.Length)
            {
                if (word1[s1][i1] == word2[s2][i2])
                {
                    i1++; i2++;
                    if (i1 == word1[s1].Length)
                    {
                        s1++;
                        i1 = 0;
                    }
                    if (i2 == word2[s2].Length)
                    {
                        s2++;
                        i2 = 0;
                    }
                }
                else return false;
            }
            if (s1 < word1.Length || s2 < word2.Length) return false;
            else return true;
        }
        public static bool ArrayStringsEqual2(string[] word1, string[] word2)
        {
            if (string.Join("",word1)==string.Join("", word2)) return true;
            else return false;
        }
        public static string LargestGoodInteger(string num)
        {
            string[] substrings = { "999", "888", "777", "666", "555", "444", "333", "222", "111", "000" };
            foreach(string substring in substrings)
            {
                if (num.IndexOf(substring) != -1) return substring;
            }
            return ("");
        }
        public static int RemoveDuplicates(int[] nums)
        {
            int[] res = new HashSet<int>(nums).ToArray();
            for(int i = 0; i < res.Length; i++)
            {
                nums[i] = res[i];
            }
            return res.Length;
        }
        public static int TotalMoney(int n)
        {
            float sum = 0, weeks = n / 7, lastWeekDays = n % 7, presentWeek = 1;            
            for(; presentWeek <= weeks; presentWeek++)
            {
                sum += ((2 * presentWeek + 6) / 2) * ((presentWeek + 6) - presentWeek + 1);
            }
            if(lastWeekDays!=0)
            {
                sum += ((2 * presentWeek + (lastWeekDays-1)) / 2) * ((presentWeek + (lastWeekDays - 1)) - presentWeek + 1);
            }
            return (int)sum;
        }
        public static string LargestOddNumber(string num)
        {
            for (int i = num.Length-1;i >= 0;i--)
            {
                if (((byte)num[i]) % 2 != 0) return num.Substring(0, i+1);
            }                        
            return "";
        }
        public static int FindSpecialInteger(int[] arr)
        {
            int quarterLen = arr.Length / 4;
            for (int i = 0; i < arr.Length;)
            {
                int lastIndex = Array.LastIndexOf(arr,arr[i]);
                int occurences = lastIndex-i;
                if (occurences >= quarterLen) return arr[i];
                else i = lastIndex+1;
            }
            return 0;
        }
        public static int MaxProduct(int[] nums)
        {
            int first = nums.Max();
            if (Array.IndexOf(nums, first, Array.IndexOf(nums, first)+1) == -1)
            {
                int second = nums.Where(n => n != first).Max();
                return (first-1)*(second-1);
            }
            else return (int)Math.Pow(first-1,2);
        }
        public static int[][] Merge(int[][] intervals)
        {
            Array.Sort(intervals, (x,y)=> x[0].CompareTo(y[0]));
            List<int[]> res = new List<int[]>();
            for(int i = 0; i < intervals.Length; i++)
            {
                int index = res.FindIndex(interval => interval[1] >= intervals[i][0]);
                if (index == -1) res.Add(intervals[i]);
                else if (res[index][1] <= intervals[i][1]) continue;
                else res[index][1] = intervals[i][1];
            }
            return res.ToArray();
        }
        public static int NumSpecial(int[][] mat)
        {
            int res = 0;
            for(int i = 0; i < mat.Length; i++)
            {
                int index = Array.IndexOf(mat[i], 1);
                if (index == -1|| Array.IndexOf(mat[i], 1, index + 1) != -1) continue;                
                else
                {
                    int j = 0;
                    for(; j < mat.Length;j++)
                    {
                        if (i == j) continue;
                        if (mat[j][index] == 1) break;
                    }
                    if (j==mat.Length) res++;
                }
            }            
            return res;
        }
        public static int[][] OnesMinusZeros(int[][] grid)
        {
            int[][] res = new int[grid.Length][];
            int[] CalcRowsDiffs()
            {
                int[] rowDiff = new int[grid.Length];
                for(int i = 0;i<grid.Length;i++)
                {
                    int diff = 0;
                    for(int j = 0; j < grid[i].Length;j++)
                    {
                        if (grid[i][j] == 1) diff++;
                        else diff--;
                    }
                    rowDiff[i] = diff;
                }
                return rowDiff;
            }
            int[] CalcColsDiffs()
            {
                int[] colDiff = new int[grid[0].Length];
                for(int i = 0;i<colDiff.Length;i++)
                {
                    int diff = 0;
                    for(int j = 0; j < grid.Length;j++)
                    {
                        if (grid[j][i] == 1) diff++;
                        else diff--;
                    }
                    colDiff[i] = diff;
                }
                return colDiff;
            }
            int[] rowDiffs = CalcRowsDiffs();
            int[] colsDiffs = CalcColsDiffs();
            for(int i = 0; i < res.Length; i++)
            {
                int[] row = new int[colsDiffs.Length];
                for(int j = 0; j < row.Length; j++)
                {
                    row[j] = rowDiffs[i] + colsDiffs[j];
                }
                res[i] = row;
            }
            return res;
        }
        public static string DestCity(IList<IList<string>> paths)
        {
            HashSet<string> from = paths.Select(path => path[0]).ToHashSet();
            HashSet<string> to = paths.Select(path => path[1]).ToHashSet();            
            return to.Except(from).First();
        }        
        public static int MaxProductDifference(int[] nums)
        {
            Array.Sort(nums);
            return (nums[0] * nums[1]) - (nums[nums.Length] * nums[nums.Length-1]);
        }
        public static int[][] ImageSmoother(int[][] img)
        {
            int[][] res = new int[img.Length][];
            int iLen = img.Length;
            for(int i = 0; i < iLen; i++)
            {
                int jLen = img[i].Length;
                res[i] = new int[jLen];
                for(int j = 0; j < jLen; j++)
                {
                    int sum = 0, counter = 1;              
                    if (i > 0)
                    {
                        if (img[i-1].Length-1>j)
                        {
                            sum+=(img[i - 1][j]);
                            sum+=(img[i - 1][j+1]);
                            counter += 2;
                        }
                        else if (img[i-1].Length-1==j)
                        {
                            sum+=(img[i - 1][j]);
                            counter++;
                        }
                        if (j>0)
                        {
                            sum+=(img[i - 1][j -1]);
                            counter++;
                        }
                    }
                    if(i<img.Length-1)
                    {
                        if (img[i+1].Length-1>j)
                        {
                            sum+=(img[i + 1][j]);
                            sum+=(img[i + 1][j + 1]);
                            counter+=2;
                        }
                        else if (img[i + 1].Length - 1 == j)
                        {
                            sum+=(img[i + 1][j]);
                            counter++;
                        }
                        if (j>0)
                        {
                            sum+=(img[i + 1][j - 1]);
                            counter++;
                        }
                    }
                    if(j>0)
                    {
                        sum += (img[i][j - 1]);
                        counter++;
                    }
                    if (j < img[i].Length-1)
                    {
                        sum += (img[i][j + 1]);
                        counter++;
                    }
                    sum += (img[i][j]);
                    res[i][j] = sum / counter;
                }
            }
            return res;
        }
        public static int BuyChoco(int[] prices, int money)
        {
            Array.Sort(prices);
            return money - (prices[0] + prices[1]) < 0 ? money : money - (prices[0] + prices[1]);
        }
        public static long SmallestNumber(long num)
        {            
            bool positive = num > 0 ? true : false;
            Stack<int> digitsStack = new Stack<int>();
            if (!positive) num *= -1;
            do
            {
                digitsStack.Push((int)(num % 10));
                num /= 10;

            } while (num != 0);
            if (digitsStack.Count == 1) return digitsStack.Peek();
            int[] digits = digitsStack.ToArray();
            Array.Sort(digits);
            if (digits[digits.Length - 1] == 0) return 0;            
            if(positive)
            {
                if (digits[0] == 0)
                {
                    int nonZeroIndex = Array.FindIndex(digits, x => x != 0);                    
                    digits[0] = digits[nonZeroIndex];
                    digits[nonZeroIndex] = 0;
                }
                return long.Parse(string.Join("", digits)); ;
            }
            else
            {
                return long.Parse("-"+string.Join("", digits.Reverse()));                
            }            
        }
        public static int MaxOfVerticalArea(int[][] points)
        {            
            int maxArea = 0;
            int[] xAxis = new int[points.Length];
            for (int i = 0; i < points.Length; i++)
            {
                xAxis[i] = points[i][0];
            }
            Array.Sort(xAxis);
            if (xAxis.Length == 2) return xAxis[1] - xAxis[0];
            for(int i = 0; i < points.Length - 1; i++)
            {
                maxArea = Math.Max(maxArea, xAxis[i + 1] - xAxis[i]);
            }
            return maxArea;
        }
        public static int MaxScore(string s)
        {
            int right = 0, left = 0, res = 0;
            for (int i = 1; i < s.Length; i++)
            {
                if (s[i] == '1') right++;
            }
            if (s[0] == '0') left++;
            res = left + right;
            if (s.Length == 2) return res;
            for (int i = 1; i < s.Length; i++)
            {
                if (s[i] == '0') left++;
                else right--;
                res = Math.Max(res, right + left);
            }            
            return res;
        }
        public static bool IsPathCrossing(string path)
        {
            HashSet<int[]> points = new HashSet<int[]>();
            points.Add(new int[] { 0, 0 });
            int x = 0, y = 0;
            for(int i = 0; i < path.Length; i++)
            {                
                if (path[i] == 'N') y++;
                else if (path[i] == 'S') y--;
                else if (path[i] == 'E') x++;
                else x--;
                int[] point = new int[] { x, y };
                if (points.Where(arr => arr[0] == x).Any(arr => arr[1] == y)) return true;
                else points.Add(point);
            }            
            return false;
        }
        public static int MinOperations(string s)
        {
            int zeroStart(string bin)
            {
                int operations = 0;
                for(int i = 0; i < bin.Length; i+=2)
                {
                    if (bin[i] != '0') operations++;
                }                
                for(int i = 1; i < bin.Length; i+=2)
                {
                    if (bin[i] != '1') operations++;
                }
                return operations;
            }
            int oneStart(string bin)
            {
                int operations = 0;
                for (int i = 0; i < bin.Length; i += 2)
                {
                    if (bin[i] != '1') operations++;
                }
                for (int i = 1; i < bin.Length; i += 2)
                {
                    if (bin[i] != '0') operations++;
                }
                return operations;
            }
            return Math.Min(zeroStart(s), oneStart(s));            
        }
        public static int GetLengthOfOptimalCompression(string s, int k)
        {
            List<(char, int)> chars = new List<(char, int)>();
            int sum = 1;
            for(int i = 1; i < s.Length; i++)
            {                
                if (s[i - 1] == s[i])
                {
                    sum++;
                }
                else
                {
                    chars.Add((s[i - 1], sum));
                    sum = 1;
                }
                if(i == s.Length - 1) chars.Add((s[i], sum));
            }
            int[] prefixSum = new int[chars.Count];
            sum = 0;
            for(int i = 0; i < prefixSum.Length; i++)
            {
                if (chars[i].Item2 == 1)
                {
                    sum++;
                }
                else
                {
                    sum += chars[i].Item2.ToString().Length + 1;
                }
                prefixSum[i] = sum;
            }
            if (k == 0) return prefixSum[prefixSum.Length - 1];
            else return 0;
        }
        public static int MinDifficulty(int[] jobDifficulty, int d)
        {
            if(jobDifficulty.Length<d) return -1;
            if (jobDifficulty.Length == d) return jobDifficulty.Sum();

            List<List<(int, int)>> days = new List<List<(int, int)>>(d);

            IEnumerable<int> SubArray(int[] array, int offset, int length)
            {
                return array.Skip(offset).Take(length);
            }                       
            
            List<(int, int)> firstDay = new List<(int, int)>();
            for(int i = 1;  i <= jobDifficulty.Length - (d-1); i++)
            {
                firstDay.Add((SubArray(jobDifficulty, 0, i).Max(), SubArray(jobDifficulty, 0, i).Count()));
            }
            days.Add(firstDay);
            d--;
            
            for(int i = 1; i < d; i++)
            {
                for(int j = (jobDifficulty.Length - (d - 1))-1; j < i;j--)
                {

                }
            }

            return 0;
        }
        public static bool MakeEqual(string[] words)
        {
            Dictionary<char, int> occurences = new Dictionary<char, int>();
            foreach(string word in words) 
            {
                foreach(char c in word)
                {
                    if(occurences.ContainsKey(c)) occurences[c]++;
                    else occurences.Add(c, 1);
                }
            }
            foreach(char c in occurences.Keys)
            {
                if (occurences[c] % words.Length != 0) return false;
            }
            return true;
        }
        public static int MinOperations(int[] nums)
        {
            int res = 0;
            Dictionary<int, int> occurences = new Dictionary<int, int>();
            foreach(int n in nums)
            {
                if (occurences.ContainsKey(n)) occurences[n]++;
                else occurences.Add(n, 1);
            }
            if (occurences.ContainsValue(1)) return -1;
            foreach(int n in occurences.Keys)
            {
                int occurence = occurences[n];
                if (occurence % 3 == 0) res += occurence / 3;
                else
                {                    
                    while(occurence>0)
                    {
                        occurence -= 2;
                        res++;
                        if (occurence % 3 == 0)
                        {
                            res += occurence / 3;
                            break;
                        }
                    }                    
                }
            }            
            return res;
        }
        public static bool HalvesAreAlike(string s)
        {
            int[] vowels = { 97, 101, 105, 111, 117 };
            int vowelsDiff = 0;
            s = s.ToLower();
            for(int i =  0; i < s.Length/2; i++)
            {
                if (vowels.Contains((int)s[i])) vowelsDiff++;
            }
            for(int i = s.Length/2; i < vowelsDiff; i++)
            {
                if (vowels.Contains((int)s[i])) vowelsDiff--;
            }
            if (vowelsDiff == 0) return true;
            else return false;
        }

        public class MyQueue
        {
            private Stack<int> stack1 { get; set; }
            private Stack<int> stackReversed { get; set; }
            private bool isReversed = false;
            public MyQueue() 
            {
                stack1 = new Stack<int>();
            }
            public void Push(int x)
            {
                if(isReversed)
                {
                    stack1 = new Stack<int>(stackReversed);
                    isReversed = false;
                }
                else
                {
                    stack1.Push(x);
                }
            }
            public int Pop()
            {
                if(isReversed) return stackReversed.Pop();
                else
                {
                    stackReversed = new Stack<int>(stack1);
                    isReversed = true;
                    return stackReversed.Pop();
                }
            }
            public int Peek()
            {
                if (isReversed) return stackReversed.Peek();
                else
                {
                    stackReversed = new Stack<int>(stack1);
                    isReversed = true;
                    return stackReversed.Peek();
                }
            }
            public bool Empty()
            {
                if(isReversed) return stackReversed.Count != 0 ? false : true;
                else return stack1.Count != 0 ? false : true;
            }
        }
        public int EvalRON(string[] tokens)
        {
            Stack<int> digits = new Stack<int>();
            foreach (string token in tokens)
            {
                int digit;
                if (Int32.TryParse(token, out digit)) digits.Push(digit);
                else Operation(token);
            }
            void Operation(string sign)
            {
                int digit1, digit2;
                GetTwoDigits(out digit1, out digit2);
                switch (sign)
                {
                    case "+":
                        digits.Push(digit1 + digit2);
                        break;
                    case "-":
                        digits.Push(digit1 - digit2);
                        break;
                    case "*":
                        digits.Push(digit1 * digit2);
                        break;
                    case "/":
                        digits.Push(digit1 / digit2);
                        break;
                }
            }
            void GetTwoDigits(out int digit1, out int digit2)
            {
                digit2 = digits.Pop();
                digit1 = digits.Pop();
            }
            return digits.Peek();
        }
        public int[] DailyTemperatures(int[] temperatures)
        {
            int[] res = new int[temperatures.Length];            
            for (int i = 0; i < temperatures.Length-1; i++)
            {
                int index = Array.FindIndex(temperatures,i+1,x => x > temperatures[i]);
                res[i] = index != -1 ? index - i : 0;
                while (i < res.Length - 1 && temperatures[i] == temperatures[i+1])
                {
                    res[i+1] = index != -1 ? index - (i + 1) : 0;
                    i++;
                }
            }
            res[res.Length - 1] = 0;
            return res;
        }
        public static int[][] DivideArray(int[] nums, int k)
        {
            int[][] dividedArrays = new int[nums.Length / 3][];
            Array.Sort<int>(nums);
            for(int i = 0, j = 0; i <  nums.Length; i+=3, j++) 
            {
                if (nums[i + 2] - nums[i] <= k)
                {
                    dividedArrays[j] = nums.Skip(i).Take(3).ToArray();
                }
                else
                {
                    dividedArrays = new int[0][];
                    break;
                }
            }
            return dividedArrays;
        }
        public IList<int> SequentialDigits(int low, int high)
        {
            const string sequenceTemplate = "123456789";
            List<int> sequencesInRange = new List<int>();

            for(int i = 2; i <= sequenceTemplate.Length; i++)
            {
                for (int j = 0;  j + i <= sequenceTemplate.Length; j++)
                {
                    int sequence = Int32.Parse(sequenceTemplate.Substring(j, i));
                    if (sequence>=low&&sequence<=high)
                    {
                        sequencesInRange.Add(sequence);
                    }
                }
            }

            return sequencesInRange;
        }
        public static string FrequencySort(string s)
        {
            string res = string.Empty;
            char[] chars = s.ToCharArray();
            Dictionary<char, int> charsFrequency = new Dictionary<char,int>();
            
            foreach(char c in chars)
            {
                if (charsFrequency.ContainsKey(c))
                    charsFrequency[c]++;
                else
                    charsFrequency.Add(c, 1);
            }
            foreach(char c in charsFrequency.OrderByDescending(x => x.Value).ToDictionary(k => k.Key).Keys)
            {
                for(int occurences = charsFrequency[c]; occurences > 0; occurences--)
                    res += c;
            }           
            
            return res;
        }

        public static int NumSquares(int n)
        {
            double squareRoot = Math.Sqrt(n);
            if (squareRoot % 1 == 0) return 1;
            while ((n & 3) == 0)
            {
                n >>= 2; 
            }
            if ((n & 7) == 7)
            {
                return 4;
            }
            List<int> squares = new List<int>();
            for(int i = (int)squareRoot; i > 0; i--)
            {
                squares.Add(i * i);
            }
            for(int i = 0; i < squares.Count; i++)
            {
                int index = squares.FindIndex(x => x == n - squares[i]);
                if (index != -1)
                    return 2;
            }
            return 3; 
        }

        public static IList<int> LargestDivisibleSubset(int[] nums)
        {
            Array.Sort(nums);
            List<List<int>> subsets = new List<List<int>>();
            for (int i = nums.Length - 1; i >= 0; i--)
            {
                List<int> dividers = new List<int>();
                dividers.Add(nums[i]);
                for (int j = i - 1; j >= 0; j--)
                {
                    if (dividers[dividers.Count - 1] % nums[j] == 0)
                        dividers.Add(nums[j]);
                }
                subsets.Add(dividers);
            }
            for (int i = 0; i < subsets.Count; i++)
            {                
                for (int j = i+1; j < subsets.Count; j++)
                {
                    if (subsets[i][0] % subsets[j][0] ==0)
                        subsets[i].Add(subsets[j][0]);
                }
                
            }
            int maxLength = subsets.Max(x => x.Count);
            return (List<int>)subsets.Where(x => x.Count == maxLength).First();
        }
        public static int CountSubstring(string s)
        {
            int palindromsCount = 0;
            for(int i = 0; i < s.Length; i++)
            {
                for(int j = 0; j+i < s.Length;j++)
                {
                    string subStr = s.Substring(i, j + 1);
                    bool isPalindrome = true;
                    for(int k = 0, l =  subStr.Length-1; k < l; k++, l--)
                    {
                        if (subStr[k] != subStr[l])
                        {
                            isPalindrome = false;
                            break; 
                        }
                    }
                    if(isPalindrome)
                        palindromsCount++;
                }
            }
            return palindromsCount;
        }
        public static int[] RearrangeArray(int[] nums)
        {
            int[] positiveNums = new int[nums.Length / 2], negativeNums = new int[nums.Length/2];
            
            for(int i = 0,j = 0, k = 0; i < nums.Length; i++)
            {
                if(nums[i]<0)
                    negativeNums[j++] = nums[i];
                else
                    positiveNums[k++] = nums[i];
            }
            for(int i = 0, j = 0; i < positiveNums.Length; i++)
            {
                nums[j++] = positiveNums[i];                
                nums[j++] = negativeNums[i];
            }
            return nums;
        }
        public static long LargestPerimeter(int[] nums)
        {
            Array.Sort(nums);
            long[] prefixes = new long[nums.Length];
            long prefixSum = 0;
            for(int i = 0; i < nums.Length; i++)
            {
                prefixSum += nums[i];
                prefixes[i]= prefixSum;
            }
            for(int i = nums.Length-1; i >= 0; i--)
            {
                if ((float)(prefixes[i] / 2) > (float)nums[i])
                    return i < 2 ? -1 : prefixes[i];
            }
            return -1;
        }
        public static int FindLeastNumOfUniqueInts(int[] arr, int k)
        {
            Dictionary<int, int> occurencesDic = new Dictionary<int, int>();
            int uniqueIntegers = 0;

            foreach(int n in arr)
            {
                if (occurencesDic.ContainsKey(n))
                    occurencesDic[n]++;
                else
                    occurencesDic.Add(n, 1);
            }

            List<int> occurences = occurencesDic.Select(kvp => kvp.Value).ToList();
            occurences.Sort();

            for(int i = 0; i < occurences.Count; i++)
            {
                if (occurences[i] > k)
                    break;
                else
                {
                    k -= occurences[i];
                    occurences[i] = 0;
                }
            }
            foreach(int o in occurences)
            {
                if (o != 0)
                    uniqueIntegers++;
            }

            return uniqueIntegers;
        }
        public static int RangeBitwiseAnd(int left, int right)
        {
            BitArray lBits = new BitArray(new int[] {left});
            BitArray rBits = new BitArray(new int[] {right});
            BitArray resBits = new BitArray(32);
            int res = 0;
            for(int i = 31;i >=0 && lBits[i] == rBits[i];i--)
            {
                resBits[i] = lBits[i];                
            }
            for(int i = 0; i < 32; i++)
            {
                if (resBits[i])
                    res += (int)Math.Pow(2, i);
            }
            return res;
        }
        public static int FindJudge(int n, int[][] trust)
        {
            if (n == 1) return 1;
            Dictionary<int, int> inDegree = new Dictionary<int, int>();
            HashSet<int> hasOutDegree = new HashSet<int>();
            for(int i = 0; i < trust.Length;i++)
            {
                if (inDegree.ContainsKey(trust[i][1]))
                    inDegree[trust[i][1]]++;
                else
                    inDegree.Add(trust[i][1], 1);

                hasOutDegree.Add(trust[i][0]);
            }

            inDegree = inDegree.Keys.Except(hasOutDegree).ToDictionary(t => t, t => inDegree[t]);
            int judge = inDegree.FirstOrDefault(x => x.Value == n - 1).Key;
            return judge != 0 ? judge : -1;
        }
        public static int FindCheapestPrice(int n, int[][] flights, int src, int dst, int k)
        {
            int[,] citiesMatrix = new int[n, n];
            foreach (int[] flight in flights)
            {
                citiesMatrix[flight[0], flight[1]] = flight[2];
            }
            
            List<List<int>> paths = new List<List<int>>();
            paths.Add(new List<int> { dst });
            bool atSrc = false;
            while (!atSrc)
            {
                bool possibleDest = true;
                atSrc = true;
                for (int j = 0; j < paths.Count; j++)
                {
                    int currCity = paths[j].Last();
                    if (currCity != src)
                    {
                        List<int> prevCity = new List<int>();
                        for (int i = 0; i < n; i++)
                        {
                            if (citiesMatrix[i, currCity] != 0)
                                prevCity.Add(i);
                        }
                        if (prevCity.Count > 1)
                        {
                            for (int l = 1; l < prevCity.Count; l++)
                            {
                                List<int> tmp = new List<int>(paths[j]);
                                tmp.Add(prevCity[l]);
                                paths.Add(tmp);
                            }
                        }
                        if (prevCity.Count > 0)
                            paths[j].Add(prevCity[0]);
                        else possibleDest = false;
                    }
                }
                if (!possibleDest)
                    return -1;
                foreach(List<int> path in paths)
                {
                    if (path.Last() != src)
                        atSrc = false;
                }
            }
            List<int> costs = new List<int>();
            foreach(List<int> path in paths)
            {
                int sum = 0;
                if (path.Count > k + 2)
                    continue;

                for(int i = path.Count-1; i > 0; i--) 
                {
                    sum += citiesMatrix[path[i],path[i - 1]];
                }
                costs.Add(sum);
            }
            return costs.Min();
        }
        public static string MaximumOddBinaryNumber(string s)
        {
            int ones = s.Count(c => c == '1');
            return new string('1', ones - 1)+new string('0', s.Length-ones)+'1';
        }
        public static int BagOfTokensScore(int[] tokens, int power)
        {
            if (tokens.Length == 1)
                if (tokens[0] > power)
                    return 0;
                else 
                    return 1;
            int score = 0, maxScore = 0;
            int lPtr = 0, rPtr = tokens.Length - 1;
            Array.Sort(tokens);
            while(lPtr<rPtr)
            {
                while (power >= tokens[lPtr])
                {
                    score++;
                    power -= tokens[lPtr];
                    maxScore = Math.Max(maxScore, score);
                    lPtr++;
                }
                if (score > 0)
                {
                    power += tokens[rPtr];
                    score--;
                    rPtr--;
                }
                else break;
            }            
            return maxScore;
        }
        public static int MaxFrequencyElements(int[] nums)
        {
            Dictionary<int,int> numsFrequency = new Dictionary<int,int>();
            foreach(int n in nums) 
            { 
                if(numsFrequency.ContainsKey(n))
                    numsFrequency[n]++;
                else
                    numsFrequency.Add(n, 1);
            }
            int maxFreq = numsFrequency.Values.Max();
            int maxNums = 0;
            foreach(int value in numsFrequency.Values)
            {
                if (value == maxFreq)
                    maxNums++;
            }
            
            return maxNums*maxFreq;
        }
        public static int GetCommon(int[] nums1, int[] nums2)
        {
            int num1Ptr = 0, num2Ptr = 0;
            while (num1Ptr < nums1.Length && num2Ptr < nums2.Length)
            {
                if (nums1[num1Ptr] == nums2[num2Ptr])
                    return nums1[num1Ptr];

                if (nums1[num1Ptr] > nums2[num2Ptr])
                    num2Ptr++;
                else
                    num1Ptr++;
            }
            return -1;
        }

        public static int FindCenter(int[][] edges)
        {
            int center = -1;
            for(int i = 0; i < edges.Length-1; i++)
            {
                if (edges[i + 1].Contains(edges[i][0]))
                    center = edges[i][0];
                else center = edges[i][1];
            }
            return center;
        }
        public static int PivotInteger(int n)
        {
            return Math.Sqrt((n*(n+1))/2)%1==0? (int)Math.Sqrt((n * (n + 1)) / 2):-1;            
        }
        public static int NumSubarraysWithSum(int[] nums,int goal)
        {
            int ones = 0, res = 0, initOnes = 0, initLen;
            if (goal == 0)
                initLen = 1;
            else
                initLen = goal;

            for (int i = 0; i < initLen; i++)
            {
                if (nums[i] == 1)
                    initOnes++;
            }
            if (initOnes == goal)
                res++;

            for (int i = initLen; i < nums.Length; i++)
            {
                ones = initOnes;
                for (int j = 0; j < nums.Length - i; j++)
                {
                    if (nums[j] == 1)
                        ones--;
                    if (nums[j + i] == 1)
                        ones++;
                    if (ones == goal)
                        res++;
                }
                if (nums[i] == 1)
                    initOnes++;
                if (initOnes == goal)
                    res++;
            }

            return res;
        }
        public static int[] ProductExceptSelf(int[] nums)
        {
            int[] prefixes = new int[nums.Length];
            int[] sufixes = new int[nums.Length];

            prefixes[0] = 1;
            sufixes[sufixes.Length-1] = 1;

            for(int i = 1, j = nums.Length-2; j >= 0; i++,j--)
            {
                prefixes[i] = prefixes[i - 1] * nums[i-1];
                sufixes[j] = sufixes[j + 1] * nums[j + 1];
            }

            for(int i = 0;i < nums.Length;i++)
            {
                nums[i] = prefixes[i] * sufixes[i];
            }

            return nums;
        }
        public static int FindMaxLength(int[] nums)
        {
            int[] sums = new int[nums.Length];
            int sum = 0;
            int maxSubArrLen = 0;
            for (int i = 0; i < nums.Length; i++)
            {
                if (nums[i] == 1)
                    sum++;
                else
                    sum--;

                sums[i] = sum;
            }
            for (int i = 0; i < sums.Length - 1; i++)
            {
                if (sums[i] == 0)
                    maxSubArrLen = Math.Max(maxSubArrLen, i + 1);
                else
                {
                    int indexOfNext = Array.LastIndexOf(sums, sums[i]);
                    if (indexOfNext != -1)
                        maxSubArrLen = Math.Max(maxSubArrLen, indexOfNext - i);
                }
            }

            return maxSubArrLen;
        }
        public static int[][] Insert(int [][] intervals, int[] newInterval)
        {
            if (intervals.Length == 0)
                return new int[][] { newInterval };
            
            List<int[]> res = new List<int[]>();

            if (intervals.Last()[1] > newInterval[0])
            {
                res.AddRange(intervals);
                res.Add(newInterval);
                return res.ToArray();
            }

            int start=0, end=0, j=0;            
            for(int i = 0; i < intervals.Length; i++)
            {
                if (intervals[i][1] < newInterval[0])
                {
                    res.Add(intervals[i]);
                    continue;
                }
                else if (intervals[i][1] >= newInterval[0])
                {
                    start = Math.Min(intervals[i][0], newInterval[0]);
                    if (intervals[i][0] > newInterval[1])
                    {
                        end = newInterval[1];
                        i--;
                    }
                    else if (intervals[i][1] >= newInterval[1])
                        end = intervals[i][1]; 
                    else
                    {
                        bool isEndDefined = false;
                        while(!isEndDefined)
                        {
                            if (i == intervals.Length - 1|| intervals[i + 1][0] > newInterval[1])
                            {
                                end = newInterval[1];
                                isEndDefined = true;
                            }                            
                            else if (intervals[i + 1][0] == newInterval[1] || intervals[i + 1][1] >= newInterval[1])
                            {
                                end = intervals[i + 1][1];
                                i++;
                                isEndDefined = true;
                            }
                            else
                                i++;
                        }
                    }
                    res.Add(new int[] { start, end });
                    j = i + 1;
                    break;
                }
            }
            
            for(;j<intervals.Length;j++)
                res.Add(intervals[j]);

            return res.ToArray();
        }
        public static int FindDuplicate(int[] nums)
        {

            Dictionary<int, bool> isSeen = new Dictionary<int, bool>();
            
            for(int i = 1; i < nums.Length; i++)
            {
                isSeen.Add(i,false);
            }

            foreach(int i in nums)
            {
                if (isSeen[i] == true)
                    return i;
                else
                    isSeen[i] = true;
            }

            return 0;
        }
        public static IList<int> FindDuplicates(int[] nums)
        {
            List<int> rangeArray = new List<int>(nums.Length);
            for(int i = 1; i <= nums.Length; i++) 
                rangeArray.Add(i);

            List<int> numsList = nums.ToList();

            foreach (int i in rangeArray)
                numsList.Remove(i);

            return nums;
        }
        public static int FirstMissingPositive(int[] nums)
        {
            int min = nums.Length, max = 0;
            foreach(int n in nums)
            {
                if(n < min&& n>0) min = n;
                if(n > max) max = n;
            }
            return min != 0 ? min : max;
        }
        public static int NumSubarrayProductLessThanK(int[] nums,int k)
        {
            int sum = 0, left = 0, product = 1;
            
            for(int right = 0; right < nums.Length; right++)
            {
                product *= nums[right];
                while(product >= k && left<=right)
                {
                    product /= nums[left];
                    left++;
                }
                sum += right - left + 1;
            }

            return sum;
        }
        public static int MaxSubarrayLength(int[] nums, int k)
        {
            int maxLen = 0;
            Dictionary<int, int> frequency = new Dictionary<int, int>();

            for (int right = 0, left = 0; right < nums.Length;right++)
            {
                if (frequency.ContainsKey(nums[right]))
                    frequency[nums[right]]++;
                else
                    frequency.Add(nums[right], 1);

                while (frequency.ContainsValue(k + 1))
                {
                    frequency[nums[left]]--;
                    left++;
                }
                maxLen = Math.Max((right - left) + 1, maxLen);                

            }
            return maxLen;
        }

        public static int CountSubarrays(int[] nums, int k)
        {
            int maxN = nums.Max();
            int ans = 0;

            for(int left = 0, right = 0, maxNumOccurency = 0; right < nums.Length; right++) 
            {
                if (nums[right] == maxN)
                    maxNumOccurency++;

                while(maxNumOccurency==k)
                {
                    if (nums[left] == maxN)
                        maxNumOccurency--;
                    left++;
                }
                ans+=left;
            }
            return ans;
        }
        public static int SubarraysWithKDistinct(int[] nums, int k)
        {
            int ans = 0;

            Dictionary<int, int> numsInWindow;
            Dictionary<int, int> initWindow = new Dictionary<int, int>();

            for(int i = 0; i < k; i++)
            {
                if (initWindow.ContainsKey(nums[i]))
                    initWindow[nums[i]]++;
                else
                    initWindow.Add(nums[i], 1);
            }
             
            for(int i = k; i < nums.Length; i++)
            {
                if (initWindow.ContainsKey(nums[i]))
                    initWindow[nums[i]]++;
                else
                    initWindow.Add(nums[i], 1);

                numsInWindow = new Dictionary<int, int>(initWindow);
                 
                for(int j = 0; j + i <=  nums.Length; j++)
                {
                    if (numsInWindow.Count == k)
                        ans++;

                    if (j + i == nums.Length)
                        break;

                    if (numsInWindow.ContainsKey(nums[i + j]))
                        numsInWindow[nums[i + j]]++;
                    else
                        numsInWindow.Add(nums[i+j], 1);
                }

            }

            return ans;
        }
        public static long CountSubarrays(int[] nums,int minK, int maxK)
        {
            long ans = 0;
            int windowMin = int.MaxValue, windowMax = int.MaxValue;
            int minIndex=-1, maxIndex=-1;

            for(int left = 0, right = 0; right < nums.Length; right++)
            {
                if (nums[right] > maxK || nums[right] < minK || right==nums.Length-1)
                {
                    if(windowMin == minK && windowMax == maxK)
                    {
                        if(maxIndex>minIndex)
                        {
                            ans += (right - maxIndex);
                            ans += left - minIndex;
                        }
                        else
                        {
                            ans += (right - minIndex);
                            ans += left - maxIndex;
                        }
                        ans++;
                    }
                    
                    left = right + 1;

                    windowMin = int.MaxValue; 
                    windowMax = int.MaxValue;
                    continue;
                }

                if(nums[right] < windowMin)
                {
                    windowMin = nums[right];
                    minIndex = right;
                }
                if (nums[right] > windowMax)
                {
                    windowMax = nums[right];
                    maxIndex = right;
                }
            }

            return ans;
        }
        public static int LengthOfLastWord2(string s)
        {
            return s.TrimEnd().Length - s.TrimEnd().LastIndexOf(' ') + 1;
        }
        public static bool IsIsomorphic2(string s,string t)
        {
            Dictionary<char, char> map = new Dictionary<char, char>();
            for(int i = 0; i < s.Length; i++)
            {
                if (map.ContainsKey(s[i]))
                {
                    if (map[s[i]] != t[i])
                        return false;
                }
                else
                {
                    if (map.ContainsValue(t[i]))
                        return false;

                    map.Add(s[i], t[i]); 
                }
            }
            return true;
        }
        public static bool Exist(char[][] board, string word)
        {
            List<(char, int,int)> chars = new List<(char, int,int)>();
            for(int i = 0; i < board.Length; i++) 
            {
                for(int j = 0; j < board[i].Length; j++)
                {
                    if (word.Contains(board[i][j]))
                        chars.Add((board[i][j], i, j));
                }
            }
            foreach(char c in word)
            {
                if (chars.FindIndex(x => x.Item1 == c)==-1)
                        return false;
            }
            return true;
        }
        public static int CountStudents(int[] students, int[] sandwiches)
        {
            int i = 0, eaten = 0;
            Queue<int> queue = new Queue<int>(students);
            while (i < sandwiches.Length && students.Contains(sandwiches[i]))
            {
                int actual = queue.Dequeue();
                if (actual == sandwiches[i])
                {
                    eaten++;
                    i++;
                }
                else
                    queue.Enqueue(actual);
            }
            return sandwiches.Length - eaten;
        }
        public static int[] DeckRevealedIncreasing(int[] deck)
        {
            Array.Sort(deck);
            Stack<int> deckSorting = new Stack<int>(deck);

            for(int i = 1; i < deck.Length-1;i++)
            {
                Stack<int> temp = new Stack<int>();
                int lastCard = deckSorting.Pop();
                for(int j = i; j > 0; j--)
                {
                    temp.Push(deckSorting.Pop());
                }
                deckSorting.Push(lastCard);
                foreach(int card in temp)
                    deckSorting.Push(card);
            }

            return deckSorting.Reverse().ToArray();
        }
        public static int[] NextGreaterElement(int[] nums1, int[] nums2)
        {
            int[] nextGreater = new int[nums2.Length];

            Stack<int> monotonicStack = new Stack<int>();
            for (int i = nums2.Length - 1; i > -1; i--)
            {
                while (monotonicStack.Count > 0 && monotonicStack.Peek() < nums2[i])
                    monotonicStack.Pop();

                if (monotonicStack.Count == 0)
                {
                    monotonicStack.Push(nums2[i]);
                    nextGreater[i] = -1;
                }
                else
                {
                    nextGreater[i] = monotonicStack.Peek();
                    monotonicStack.Push(nums2[i]);
                }
            }

            for(int i = 0; i < nums1.Length;i++)
            {
                int numIndex = Array.IndexOf(nums2, nums1[i]);
                nums1[i] = nextGreater[numIndex];
            }

            return nums1;
        }
        public static int Trap(int[] height)
        {
            int left = 0, right = height.Length-1;
            int leftMax = 0, rightMax = 0;
            int res = 0;

            while(left<right)
            {
                if (height[left] < height[right]) 
                {
                    if (height[left] >= leftMax)
                        leftMax = height[left];
                    else
                        res += leftMax - height[left];

                    left++;
                }
                else
                {
                    if (height[right] >= rightMax)
                        rightMax = height[rightMax];
                    else
                        res += rightMax - height[right];

                    right--;
                }
            }

            return res;
        }
        public static int IslandPermieter(int[][] grid)
        {
            int islandPerimeter = 0;
            int row = grid.Length, col = grid[0].Length;

            for(int i = 0; i < row; i++)
            {
                for(int j = 0; j < col; j++)
                {
                    if (grid[i][j]==1)
                    {
                        int perimeter = 4;
                        if(i!=0)
                        {
                            if (grid[i - 1][j] == 1)
                                perimeter--;
                        }
                        if(i<row-1)
                        {
                            if (grid[i + 1][j] == 1)
                                perimeter--;
                        }
                        if(j>0)
                        {
                            if (grid[i][j - 1] == 1)
                                perimeter--;
                        }
                        if(j<col-1)
                        {
                            if (grid[i][j+1]==1)
                                perimeter--;
                        }
                        islandPerimeter += perimeter;
                    }
                }
            }

            return islandPerimeter;
        }
        public static void MatrixDFS(int[][] grid)
        {
            int[] dRow = { -1, 0, 1, 0 };
            int[] dCol = { 0, -1, 0, 1 };
            int rows = grid.Length, cols = grid[0].Length;
            Stack<Tuple<int, int>> stack = new Stack<Tuple<int, int>>();
            bool[][] visited = new bool[rows][];

            for(int i = 0; i < rows; i++)
            {
                visited[i] = new bool[cols];
                for(int j = 0; j < cols; j++)
                {
                    visited[i][j] = false;
                }
            }

            bool isValid(int x, int y)
            {
                if (x < 0 || y < 0 || x > rows-1 || y > cols-1)
                    return false;

                if (visited[x][y] == true)
                    return false;

                return true;
            }

            stack.Push(new Tuple<int, int>(0,0));

            while (stack.Count>0)
            {
                Tuple<int,int> curr = stack.Pop();

                int row = curr.Item1, col = curr.Item2;

                if (!isValid(row, col))
                    continue;

                Console.WriteLine(grid[row][col]);
                visited[row][col] = true;

                for(int i = 0; i < 4; i++)
                {
                    int adjX = row + dRow[i];
                    int adjY = col + dCol[i];
                    stack.Push(new Tuple<int, int>(adjX, adjY));
                }
            }

        }
        public static int NumIslands(char[][] grid)
        {
            int islands = 0;

            int m = grid.Length, n = grid[0].Length;
            if (m == 0 || n == 0)
                return 0;

            bool[][] visited = new bool[m][];

            for (int i = 0; i < m; i++)
            {
                visited[i] = new bool[n];
                for (int j = 0; j < n; j++)
                {
                    visited[i][j] = false;
                }
            }

            for(int i = 0; i < m; i++)
            {
                for(int j = 0; j < n; j++)
                {
                    if (grid[i][j]==1)
                    {
                        islands++;
                        Dfs(i, j);
                    }
                }
            }

            void Dfs(int x, int y)
            {
                if(x < 0 || y < 0 || x > m-1 || y > n-1 || visited[x][y] || grid[x][y] == '0') 
                    return;

                grid[x][y] = '0';
                visited[x][y] = true;

                Dfs(x - 1, y);
                Dfs(x, y - 1);
                Dfs(x + 1, y);
                Dfs(x, y + 1);
            }

            return islands;
        }
        public static bool ValidPath(int n, int[][] edges, int source, int destination)
        {
            List<int>[] vertices = new List<int>[n];
            bool[] visited = new bool[n];
            bool isDestVisited = false;

            for(int i = 0; i < n; i++)
            {
                vertices[i] = new List<int>();
            }
            foreach (int[] edge in edges)
            {
                vertices[edge[0]].Add(edge[1]);
                vertices[edge[1]].Add(edge[0]);
            }

            Stack<int> stack = new Stack<int>();

            stack.Push(source);

            while(stack.Count>0)
            {
                int cur = stack.Pop();

                visited[cur] = true;

                if(cur==destination)
                {
                    isDestVisited = true;
                    break;
                }

                foreach(int vertice in vertices[cur]) 
                {
                    if (!visited[vertice])
                        stack.Push(vertice);
                }
            }

            return isDestVisited;
        }
        public static IList<int> FindMinHeightTrees(int n, int[][] edges)
        {
            List<int> roots = new List<int>();
            List<int>[] vertices = new List<int>[n];
            bool[] isLeaf = new bool[n];

            for (int i = 0; i < n; i++)
                vertices[i] = new List<int>();

            foreach (int[] edge in edges)
            {
                vertices[edge[0]].Add(edge[1]);
                vertices[edge[1]].Add(edge[0]);
            }

            while(n>2)
            {
                List<int> leaves = new List<int>();
                for(int i = 0; i < isLeaf.Length;i++)
                {
                    if (!isLeaf[i])
                    {
                        if (vertices[i].Count==1)
                            leaves.Add(i);
                    }
                }
                foreach(int leaf in leaves)
                {
                    isLeaf[leaf] = true;
                    n--;
                    vertices[vertices[leaf][0]].Remove(leaf);
                }
            }

            for(int i = 0; i < isLeaf.Length;i++)
            {
                if (!isLeaf[i])
                    roots.Add(i);
            }

            return roots;
        }
        public static int Tribonacci(int n)
        {
            int[] f = new int[38];
            f[0] = 0;
            f[1] = 1;
            f[2] = 1;

            for(int i = 3; i < n; i++)
            {
                f[i] = f[i - 1] + f[i - 2] + f[i - 3];
            }

            return f[n];
        }
        public static void Solve(char[][] board)
        {
            int m = board.Length, n = board[0].Length;
            bool isSourrounded;
            List<int> sourrounded = new List<int>();
            List<int> region;

            bool[][] visited = new bool[m][];
            
            for(int i = 0; i < m; i++)
                visited[i] = new bool[n];

            for(int i = 0; i <  m; i++)
            {
                for(int j = 0; j < n; j++)
                {
                    if (board[i][j]=='O')
                    {
                        isSourrounded = true;
                        region = new List<int>();
                        DFS(i, j);

                        if (isSourrounded&&region.Count>0)
                            sourrounded.AddRange(region);
                    }
                }
            }

            for(int i = 0; i < sourrounded.Count; i++)
            {
                board[sourrounded[i]][sourrounded[++i]] = 'X';
            }

            void DFS(int x, int y)
            {
                if (x < 0 || x > m - 1 || y < 0||y > n - 1)
                {
                    isSourrounded = false;
                    return;
                }

                if (board[x][y] == 'X' || visited[x][y])
                    return;


                region.Add(x);
                region.Add(y);

                visited[x][y] = true;

                DFS(x - 1, y);
                DFS(x, y - 1);
                DFS(x + 1, y);
                DFS(x, y + 1);
            }
        }
        public static ListNode ReverseList(ListNode head)
        {
            if (head == null || head.next == null)
                return head;

            Stack<ListNode> nodes = new Stack<ListNode>();
            
            while(head != null) 
            {
                nodes.Push(head);
                head = head.next;
            }

            head = nodes.Pop();

            ListNode visitor = head;

            while(nodes.Count>0)
            {
                ListNode tempNode = nodes.Pop();
                visitor.next = new ListNode(tempNode.val);
                visitor = visitor.next;
            }

            return head;
        }
        public static ListNode DoubleIt(ListNode head)
        {
            Stack<ListNode> nodes = new Stack<ListNode>();
            ListNode node = head;

            while(node!=null)
            {
                nodes.Push(node);
                node = node.next;
            }

            bool aboveTen = false;
            while(nodes.Count>0)
            {
                node = nodes.Pop();
                int nodeVal = node.val;

                nodeVal *= 2;

                if (aboveTen)
                    nodeVal++;

                if (nodeVal < 10)
                {
                    aboveTen = false;
                    node.val = nodeVal;
                }
                else
                {
                    aboveTen = true;
                    node.val = nodeVal % 10;
                }
            }

            if(aboveTen)
                head = new ListNode(1,node);

            return head;
        }
        public static string[] FindRelativeRanks(int[] score)
        {
            int length = score.Length;
            int[] scoreSorted = score.OrderByDescending(x=>x).ToArray();

            string[] ranks = new string[length];
            
            for(int i = 0; i < length; i++)
            {
                int index = Array.IndexOf(score, scoreSorted[i]);
                string rank = string.Empty;

                if(i > 2)
                    rank = i.ToString();
                else
                {
                    switch(scoreSorted[i]) 
                    {
                        case 2:
                            rank = "Bronze Medal";
                            break;
                        case 1:
                            rank = "Silver Medal";
                            break;
                        case 0:
                            rank = "Gold Medal";
                            break;
                    }
                }

                ranks[index] = rank;
            }

            return ranks;
        }
        public static long MaximumHappinessSum(int[] happiness, int k)
        {
            long happinessSum = 0;

            IEnumerable<int> mostHappyKids = happiness.OrderByDescending(x=>x).Take(k);

            int i = 0;
            foreach(int kid in mostHappyKids) 
            {
                if (kid < i)
                    break;

                happinessSum += kid - i;

                i++;
            }

            return happinessSum;
        }
        public static int[] KthSMallestPrimeFraction(int[] arr, int k)
        {
            List<Tuple<int, int, decimal>> fractions = new List<Tuple<int, int, decimal>>((arr.Length * arr.Length - 1) / 2);
            
            for(int i = 0; i < arr.Length; i++)
            {
                for(int j = i +1; j < arr.Length; j++)
                {
                    decimal num1 = arr[i];
                    int num2 = arr[j];
                    decimal fraction = num1 / num2;
                    Tuple<int, int, decimal> numbersFraction = Tuple.Create((int)num1,num2, fraction);
                    fractions.Add(numbersFraction);
                }
            }

            var kthSmallestFraction = fractions.OrderBy(f => f.Item3).Skip(k-1).First();
            
            return new int[] {kthSmallestFraction.Item1,kthSmallestFraction.Item2};
        }
        public static int MatrixScore(int[][] grid)
        {
            int rows = grid.Length, cols = grid[0].Length; 
            int sum = 0;

            for(int i = 0; i < rows; i++)
            {
                if (grid[i][0] == 0)
                    swapRow(i);
            }
            for(int i = 0; i < cols; i++)
            {
                int onesInCol = 0, zerosInCol = 0;
                for(int j = 0; j < rows; j++)
                {
                    if (grid[j][i] == 1)
                        onesInCol++;
                    else
                        zerosInCol++;
                }

                if (zerosInCol > onesInCol)
                    swapCol(i);

            }

            foreach (int[] row in grid)
            {
                StringBuilder number = new StringBuilder(cols);
                foreach (int n in row)
                    number.Append(n);

                sum += Convert.ToInt32(number.ToString(),2);
            }

            return sum;

            void swapRow(int rowNo)
            {
                for (int i = 0; i < cols; i++)
                {
                    if (grid[rowNo][i] == 0)
                        grid[rowNo][i] = 1;
                    else if (grid[rowNo][i] == 1)
                        grid[rowNo][i] = 0;
                }
            }
            void swapCol(int colNo)
            {
                for(int i = 0; i < rows; i++)
                {
                    if (grid[i][colNo] == 0)
                        grid[i][colNo] = 1;
                    else if (grid[i][colNo] == 1)
                        grid[i][colNo] = 0;
                }
            }
        }
        public static int BeautifulSubsets(int[] nums, int k)
        {
            int countOfSubsets = 0;

            Stack<int> subset = new Stack<int>();

            void Backtrack(int start)
            {
                if (subset.Count > 0)
                    countOfSubsets++;

                for(int i = start; i < nums.Length; i++)
                {
                    bool valid = true;
                    
                    foreach(int num in subset)
                    {
                        if(Math.Abs(num - nums[i]) == k)
                        {
                            valid = false;
                            break;
                        }
                    }
                    if(valid)
                    {
                        subset.Push(nums[i]);
                        Backtrack(i + 1);
                        subset.Pop();
                    }
                }
            }

            Backtrack(0);
            return countOfSubsets;
        }
        public static int MaxScoreWords(string[] words, char[] letters, int[] score)
        {
            int maxScoreSum = 0, currentScoreSum = 0;

            int[] wordScore = new int[words.Length];
            
            for(int i = 0; i < words.Length; i++)
                wordScore[i] = GetScore(words[i]);
            
            List<char> avalibleLetters = letters.ToList();

            Backtrack(0);

            return maxScoreSum;

            void Backtrack(int i)
            {
                if(i == words.Length)
                {
                    maxScoreSum = Math.Max(maxScoreSum, currentScoreSum);
                    return;
                }
                string word = words[i];

                if (IsValid(word,avalibleLetters))
                {
                    currentScoreSum += wordScore[i];
                    DeleteLetters(word, avalibleLetters);
                    Backtrack(i + 1);
                }
                currentScoreSum -= wordScore[i];
                RestoreLetters(word, avalibleLetters);
            }
            int GetScore(string word)
            {
                int scoreSum = 0;

                foreach (char c in word)
                    scoreSum += score[(int)c - 97];

                return scoreSum;
            }
        }
        public static bool IsValid(string word, List<char> letters)
        {
            foreach(char c in word)
            {
                if (!letters.Contains(c))
                    return false;
            }

            return true;
        }
        public static void DeleteLetters(string word, List<char> target)
        {
            foreach(char c in word)
                target.Remove(c);
        }
        public static void RestoreLetters(string word, List<char> target)
        {
            foreach (char c in word)
                target.Add(c);
        }
        public static int EqualSubstring(string s, string t, int maxCost)
        {
            int maxSubstringLen = 0;
            int[] costs = new int[s.Length];

            for(int i = 0; i < costs.Length; i++)
                costs[i] = Math.Abs((int)s[i] - (int)t[i]);

            int actualCost = 0, left = 0;
            for (int right = 0; right < costs.Length;right++)
            {
                while(actualCost>maxCost)
                {
                    actualCost -= costs[left];
                    left++;
                }
                 maxSubstringLen = Math.Max(maxSubstringLen, right - left);

                actualCost += costs[right];
            }

            if(actualCost <= maxCost)
                maxSubstringLen = Math.Max(maxSubstringLen, costs.Length-left);


            return maxSubstringLen;
        }
        public static int NumSteps(string s)
        {
            StringBuilder binaryNumString = new StringBuilder(s);

            int steps = 0;

            while(binaryNumString.Length>1)
            {
                if (binaryNumString[binaryNumString.Length - 1] == '1')
                {
                    binaryNumString[binaryNumString.Length - 1] = '0';
                    int i = binaryNumString.Length - 2;
                    while (i >= 0&& binaryNumString[i] == '1')
                    {
                        binaryNumString[i] = '0';
                        i--;
                    }
                    if (i == -1)
                        binaryNumString.Insert(0, '1');
                    else
                        binaryNumString[i] = '1';

                }
                else
                    binaryNumString.Length--;
                
                steps++;
            }

            return steps;
        }
        public static int AppendCharacters(string s, string t)
        {
            int indexInT = 0;
            int indexInS = s.IndexOf(t[indexInT]);
            while(indexInS!=-1)
            {
                indexInT++;

                if (indexInT == t.Length)
                    return 0;

                indexInS = s.IndexOf(t[indexInT], indexInS);
            }

            return t.Length-indexInT;
        }
        public static IList<string> CommonChars(string[] words)
        {
            IList<string> cChars = new List<string>();

            List<List<char>> wordsInChars = new List<List<char>>(words.Length);

            foreach(string word in words)
                wordsInChars.Add(word.ToList());
            
            for(int i = 0; i < wordsInChars[0].Count;i++)
            {
                char currentChar = wordsInChars[0][i];
                
                bool isCurrentCharCommon = true;

                foreach(List<char> chars in wordsInChars)
                {
                    if(!chars.Contains(currentChar))
                    {
                        isCurrentCharCommon = false;
                        break;
                    }
                }

                if (!isCurrentCharCommon)
                    continue;

                cChars.Add(currentChar.ToString());
                i--;

                for(int j = 0; j < wordsInChars.Count; j++)
                    wordsInChars[j].Remove(currentChar);
            }


            return cChars;
        }
        public static bool IsNStraightHand(int[] hand, int groupSize)
        {
            if(hand.Length%groupSize!=0) 
                return false;

            Dictionary<int, int> cardsFrequency = new Dictionary<int, int>();
            
            foreach(int card in hand)
            {
                if (cardsFrequency.ContainsKey(card))
                    cardsFrequency[card]++;
                else
                    cardsFrequency.Add(card, 1);
            }

            cardsFrequency = cardsFrequency.OrderBy(x => x.Key).ToDictionary(x=>x.Key,y=>y.Value);

            while (cardsFrequency.Count > 0)
            {
                List<int> currentCards = cardsFrequency.Take(groupSize).Select(x=>x.Key).ToList();
                
                if (currentCards.Count < groupSize)
                    return false;

                bool isGroupValid = true;
                for(int i = 0; i < currentCards.Count-1; i++)
                {
                    if (currentCards[i + 1] != currentCards[i]+1)
                    {
                        isGroupValid = false;
                        break;
                    }
                }
                if (isGroupValid)
                {
                    foreach (int card in currentCards)
                    {
                        cardsFrequency[card]--;
                        if (cardsFrequency[card] == 0)
                            cardsFrequency.Remove(card);
                    }
                }
                else
                    return false;
            }


            return true;
        }
        public static int[] RelativeSortArray(int[] arr1, int[] arr2)
        {
            int[] result = new int[arr1.Length];
            Dictionary<int, int> numOccurences = new Dictionary<int, int>();
            List<int> arr1ExceptArr2 = new List<int>();

            foreach(int i in arr2)
            {
                if (!numOccurences.ContainsKey(i))
                    numOccurences.Add(i, 0);
            }

            foreach(int i in arr1)
            {
                if (numOccurences.ContainsKey(i))
                    numOccurences[i]++;
                else
                    arr1ExceptArr2.Add(i);
            }

            int j = 0;
            foreach(int i in numOccurences.Keys)
            {
                while (numOccurences[i]>0)
                {
                    result[j] = i;
                    numOccurences[i]--;
                    j++;
                }
            }
            foreach(int i in arr1ExceptArr2.OrderBy(x=>x))
            {
                result[j] = i;
                j++;
            }

            return result;
        }
        public static void SortColors(int[] nums)
        {
            int red = 0, white = 0, blue = 0;
            foreach(int num in nums)
            {
                if (num == 0)
                    red++;
                else if (num == 1)
                    white++;
                else
                    blue++;
            }
            int i = 0;
            while(red>0)
            {
                nums[i] = 0;
                red--;
                i++;
            }
            while(white>0)
            {
                nums[i] = 1;
                white--;
                i++;
            }
            while(blue>0)
            {
                nums[i] = 2;
                blue--;
                i++;
            }
        }
        public static int MinMovesToSear(int[] seats, int[] students)
        {
            int moves = 0;

            Array.Sort(seats);
            Array.Sort(students);

            for (int i = 0; i < seats.Length; i++)
                moves += Math.Abs(students[i] - seats[i]);

            return moves;
        }
        public static int MinIncrementForUnique(int[] nums)
        {
            int incrementations = 0;

            Array.Sort(nums);

            for(int i = 1; i < nums.Length;i++)
            {
                if (nums[i - 1] == nums[i]-1)
                {
                    incrementations++;
                    nums[i]++;
                }
                else if (nums[i - 1] > nums[i])
                {
                    int diff = Math.Abs(nums[i] - nums[i-1]);
                    nums[i] += diff;
                    incrementations += diff;
                }
            }

            return incrementations;
        }
        public static int MinDays(int[] bloomDay, int m, int k)
        {
            if ((long)m * k > bloomDay.Length)
                return -1;

            int left = bloomDay.Min();
            int right = bloomDay.Max();

            bool CanMakeBouquets(int day)
            {
                int count = 0;
                int bouquets = 0;

                for(int i = 0; i < bloomDay.Length; i++)
                {
                    if (bloomDay[i] <= day)
                        count++;
                    else
                    {
                        bouquets += count / k;
                        count = 0;
                    }
                }
                bouquets += count / k;

                return bouquets >= m;
            }

            int minDay = right;
            while(left<=right)
            {
                int currentDay = left + (right - left) / 2;

                if (CanMakeBouquets(currentDay))
                {
                    minDay = Math.Min(currentDay, minDay);
                    right = currentDay - 1;
                }
                else
                    left = currentDay + 1;
            }
            return minDay;
        }
        public static int MaxDistance(int[] position, int m)
        {
            Array.Sort(position);
            m--;
            return BisectLeft();

            int BisectLeft()
            {
                int low = 0;
                int high = position[position.Length-1] - position[0];

                while(low < high)
                {
                    int mid = (low + high) >> 1;
                    if (IsPossible(mid))
                        low = mid + 1;
                    else
                        high = mid;
                }

                return IsPossible(low) ? low : low -1;
            }

            bool IsPossible(int mid)
            {
                int mCopy = m;
                int prev = position[0];
                foreach(int x in position)
                {
                    if(x-prev>=mid)
                    {
                        if (mCopy-- == 1)
                            return true;
                    }
                    prev = x;
                }
                return false;
            }
            
        }
        public static int MaxSatisfied(int[] customers, int[] grumpy, int minutes)
        {
            if (minutes == grumpy.Length)
                return customers.Sum();

            int satisfiedCustomers = 0;
            int maxDiff = 0;
            int techniqueActivationTime = 0;

            for(int i = 0; i < minutes;i++)
            {
                if (grumpy[i] == 1)
                    maxDiff += customers[i];
            }

            int diff = maxDiff;
            for(int left = 1, right = minutes; right < customers.Length-1; left++, right++)
            {
                if (grumpy[left-1] == 1 )
                    diff -= customers[left-1];

                if (grumpy[right] == 1)
                    diff += customers[right];

                if(diff>maxDiff)
                {
                    maxDiff = diff;
                    techniqueActivationTime = left;
                }
            }

            for(int i = techniqueActivationTime,j=0; j < minutes;i++,j++)
                grumpy[i] = 0;

            for(int i = 0; i < customers.Length; i++)
            {
                if (grumpy[i] == 0)
                    satisfiedCustomers += customers[i];
            }

            return satisfiedCustomers;
        }
        public static int NumberOfSubarrays(int[] nums, int k)
        {
            int subarraysCount = 0;
            List<int> oddNumbersIndices = new List<int>();
            List <(int, int)> oddNumbersIndicesGroups = new List<(int, int)>();

            for(int i = 0; i < nums.Length; i++)
            {
                if (isOdd(nums[i]))
                    oddNumbersIndices.Add(i);
            }

            for(int i = 0; i + (k-1) <oddNumbersIndices.Count; i++)
                oddNumbersIndicesGroups.Add((oddNumbersIndices[i], oddNumbersIndices[i + (k - 1)]));

            return subarraysCount;

            bool isOdd(int num)
            {
                return num % 2 == 1;
            }
        }
        public static int FindCenter2(int[][] edges)
        {
            Dictionary<int,int> edgesPerNode = new Dictionary<int,int>();
            foreach (int[] edge in edges)
            {
                if (edgesPerNode.ContainsKey(edge[0]))
                    edgesPerNode[edge[0]]++;
                else
                    edgesPerNode.Add(edge[0], 1);

                if (edgesPerNode.ContainsKey(edge[1]))
                    edgesPerNode[edge[1]]++;
                else
                    edgesPerNode.Add(edge[1], 1);
            }

            return edgesPerNode.Where(v => v.Value == edgesPerNode.Values.Max()).Select(k => k.Key).First();
        }
        public static IList<IList<int>> GetAncestors(int n, int[][] edges)
        {
            List<int>[] adjacencyList = new List<int>[n];
            for (int i = 0; i < n; i++)
                adjacencyList[i] = new List<int>();

            int[] indegree = new int[n];
            foreach(int[] edge in edges)
            {
                int from = edge[0];
                int to = edge[1];
                adjacencyList[from].Add(to);
                indegree[to]++;
            }

            Queue<int> zeroIndegreeNodes = new Queue<int>();
            for (int i = 0; i < indegree.Length; i++)
            {
                if (indegree[i] == 0)
                    zeroIndegreeNodes.Enqueue(i);
            }

            List<int> topologicalOrder = new List<int>();
            while(zeroIndegreeNodes.Count>0)
            {
                int currentNode = zeroIndegreeNodes.Dequeue();
                topologicalOrder.Add(currentNode);

                foreach(int neighbor in adjacencyList[currentNode])
                {
                    indegree[neighbor]--;
                    if (indegree[neighbor] == 0)
                        zeroIndegreeNodes.Enqueue(neighbor);
                }
            }

            List<IList<int>> ancestorsList = new List<IList<int>>();
            List<HashSet<int>> ancestorsHashSets = new List<HashSet<int>>();
            for(int i = 0; i < n; i++)
            {
                ancestorsList.Add(new List<int>());
                ancestorsHashSets.Add(new HashSet<int>());
            }

            foreach (int node in topologicalOrder)
            {
                foreach (int neighbor in adjacencyList[node])
                {
                    ancestorsHashSets[neighbor].Add(node);
                    ancestorsHashSets[neighbor].UnionWith(ancestorsHashSets[node]);
                }
            }
            
            for (int i = 0; i < ancestorsList.Count; i++)
            {
                List<int> ancestors = ancestorsHashSets[i].ToList();
                ancestors.Sort();
                ancestorsList[i]=ancestors;
            }

            return ancestorsList;
        }
        public static bool ThreeConsecutiveOdds(int[] arr)
        {
            int consecutiveOddsCount = 0;
            if (arr.Length < 3)
                return false;

            for(int i = 0; i < 3; i++)
            {
                if (isOdd(arr[i]))
                    consecutiveOddsCount++;
            }

            for(int left = 0, right = 2; right < arr.Length-1; left++, right++)
            {
                if (consecutiveOddsCount == 3)
                    return true;

                if (isOdd(arr[left+1]))
                    consecutiveOddsCount--;

                if (isOdd(arr[right+1]))
                    consecutiveOddsCount++;
            }

            bool isOdd(int num)
            { return num % 2 == 1; }

            return false;
        }
        public static int[] Intersect(int[] nums1, int[] nums2)
        {
            Dictionary<int, int> nums1occurences = new Dictionary<int, int>();
            Dictionary<int, int> nums2occurences = new Dictionary<int, int>();
            List<int> intersection = new List<int>();

            foreach(int num in nums1)
            {
                if (!nums1occurences.ContainsKey(num))
                    nums1occurences.Add(num, 0);

                nums1occurences[num]++;
            }    
            foreach(int num in nums2)
            {
                if (!nums2occurences.ContainsKey(num))
                    nums2occurences.Add(num, 0);

                nums2occurences[num]++;
            }    

            foreach(int num in nums1occurences.Keys.ToList().Intersect(nums2occurences.Keys.ToList()))
            {
                int occurences = Math.Min(nums1occurences[num], nums2occurences[num]);

                for (int i = 0; i < occurences; i++)
                    intersection.Add(num);
            }

            return intersection.ToArray();
        }
        public static int MinDifference(int[] nums)
        {
            if(nums.Length < 5)
                return 0;

            int minDiff = int.MaxValue;

            Array.Sort(nums);

            for(int i = 0, j = nums.Length-4; i < 3; i++)
                minDiff = Math.Min(minDiff,nums[j] - nums[i]);

            return minDiff;
        }
        public static int MinOperations(string[] logs)
        {
            int operations = 0;

            foreach(string log in logs)
            {
                if (log == "../")
                {
                    if (operations > 0)
                        operations--;
                }
                else if (log == "./")
                    continue;
                else
                    operations++;
            }   

            return operations;
        }
        public static string ReverseParentheses(string s)
        {
            char[] chars = s.ToCharArray();
            Stack<int> parenthesesIndexes = new Stack<int>();

            for(int i = 0; i < chars.Length; i++)
            {
                if (chars[i] == '(')
                    parenthesesIndexes.Push(i);

                else if (chars[i]==')')
                { 
                    int left = parenthesesIndexes.Pop()+1; 
                    int right = i-1;
                    Reverse(left,right);
                }
            }

            StringBuilder result = new StringBuilder();

            foreach(char c  in chars)
            {
                if (c == '(' || c == ')')
                    continue;

                result.Append(c);
            }

            return result.ToString();

            void Reverse(int left, int right)
            {
                while (left < right)
                {
                    char tmp = chars[left];
                    chars[left] = chars[right];
                    chars[right] = tmp;
                    left++;
                    right--;
                }
            }
        }
        public static int MaximumGain(string s, int x, int y)
        {
            int sum = 0;

            StringBuilder aAndB = new StringBuilder();

            for(int i = 0; i < s.Length; i++)
            {
                if((s[i] == 'a' || s[i]=='b'))
                    aAndB.Append(s[i]);
            }

            s = aAndB.ToString();
            string delFirst;
            string delSecond;

            if(y>x)
            {
                delFirst = "ba";
                delSecond = "ab";
            }
            else
            {
                delFirst = "ab";
                delSecond = "ba";
            }

            while (s.Contains(delFirst))
            {
                int initialLen = s.Length;
                s.Replace(delFirst, "");

                sum += ((initialLen - s.Length) / 2) * Math.Max(x, y); 
            }

            while (s.Contains(delFirst))
            {
                int initialLen = s.Length;
                s.Replace(delSecond, "");

                sum += ((initialLen - s.Length) / 2) * Math.Min(x, y); 
            }

            return sum;
        }
        public static IList<int> LuckyNumbers(int[][] matrix)
        {
            int m = matrix.Length;
            int n = matrix[0].Length;
            int[] minRowIdx = new int[m];
            int[] maxColIdx = new int[n];
            IList<int> luckyNumbers = new List<int>();

            for(int row = 0; row < m; row++)
                minRowIdx[row] = Array.FindIndex(matrix[row], num => num == matrix[row].Min());

            for(int col = 0; col < n; col ++)
            {
                List<int> columnValues = matrix.Select(row => row[col]).ToList();
                maxColIdx[col] = columnValues.FindIndex(num=>num==columnValues.Max());
            }

            for(int i = 0; i < m; i++)
            {
                if (maxColIdx[minRowIdx[i]]==i)
                    luckyNumbers.Add(matrix[i][minRowIdx[i]]);
            }

            return luckyNumbers;
        }
        public static string[] SortPeople(string[] names, int[] heights)
        {
            //All the values of heights are distinct.

            Dictionary<int, string> peopleHeights = new Dictionary<int, string>(names.Length);

            for(int i = 0; i < names.Length; i++)
                peopleHeights.Add(heights[i], names[i]);

            return peopleHeights.OrderByDescending(k => k.Key).Select(v => v.Value).ToArray();
        }
        public static int[] FrequencySort(int[] nums)
        {
            Dictionary<int, int> numsFrequency = new Dictionary<int, int>();
            foreach (int num in nums)
            {
                if (numsFrequency.ContainsKey(num))
                    numsFrequency[num]++;
                else
                    numsFrequency.Add(num, 1);
            }

            var groups = numsFrequency.GroupBy(kv => kv.Value)
                .OrderBy(g => g.Key)
                .Select(g => new
                {
                    Frequency = g.Key,
                    Nums = g.OrderByDescending(kv => kv.Key)
                });

            int i = 0;
            foreach (var group in groups)
            {
                int freq = group.Frequency;
                foreach(var num in group.Nums)
                {
                    for(int j = 0; j<freq; j++)
                    {
                        nums[i] = num.Key;
                        i++;
                    }
                }
            }

            return nums;
        }
        #region MinHeightShelves
        public static int MinHeightShelves(int[][] books, int shelfWidth)
        {
            int[][] memo = new int[books.Length][];
            for (int i = 0; i < memo.Length; i++)
            {
                memo[i] = new int[shelfWidth + 1]; 
            }
            return dpHelper(books,shelfWidth,memo,0,shelfWidth,0);
        }
        private static int dpHelper(
            int[][] books,
            int shelfWidth,
            int[][] memo,
            int i,
            int remainingShelfWidth,
            int maxHeight)
        {
            int[] currentBook = books[i];
            int maxHeightUpdated = Math.Max(maxHeight, currentBook[1]);

            if(i==books.Length-1)
            {
                if(remainingShelfWidth >= currentBook[0])
                    return maxHeightUpdated;
                else
                    return maxHeight+currentBook[1];
            }

            if (memo[i][remainingShelfWidth] != 0)
                return memo[i][remainingShelfWidth];
            else
            {
                int option1Height =
                    maxHeight + dpHelper(
                        books,
                        shelfWidth,
                        memo,
                        i + 1,
                        shelfWidth - currentBook[0],
                        currentBook[1]);

                if(remainingShelfWidth >= currentBook[0])
                { 
                    int option2Height = dpHelper(
                        books,
                        shelfWidth,
                        memo,
                        i + 1,
                        remainingShelfWidth - currentBook[0],
                        maxHeightUpdated);

                    memo[i][remainingShelfWidth] = Math.Min(option1Height, option2Height);

                    return memo[i][remainingShelfWidth];
                }
                
                memo[i][remainingShelfWidth] = option1Height;
                return memo[i][remainingShelfWidth];
            }
        }
        #endregion
        public static int CountSeniors(string[] details)
        {
            int seniors = 0;

            foreach(string detail in details)
            {
                if ((int)detail[11] > 53 && (int)detail[12] > 48)
                    seniors++;
            }

            return seniors;
        }
        public static int MinSwaps(int[] nums)
        {
            int[] extendedNums = new int[nums.Length * 2];
            int ones = nums.Where(n => n == 1).Count();

            if (ones == nums.Length - 1)
                return 0;

            for(int i = 0; i < nums.Length;i++)
            {
                int num = nums[i];

                extendedNums[i] = num;
                extendedNums[i + nums.Length] = num;
            }

            int zeros = 0;

            for(int i = 0; i < ones; i++)
            {
                if (nums[i] == 0)
                    zeros++;
            }

            int minSwaps = zeros;

            for(int left = 0, right = ones; right < extendedNums.Length-1; right++, left++)
            {
                if (extendedNums[right] == 1)
                    ones++;
                else
                    zeros++;

                if (extendedNums[left] == 0)
                    zeros--;
                else
                    ones--;

                minSwaps = Math.Min(minSwaps, zeros);
            }

            return minSwaps;
        }
    }
}
