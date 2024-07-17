using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace leetcode.Solutions
{
    public class TrieNode
    {
        public Dictionary<char, TrieNode> Children { get; private set; }
        public bool IsEndOfWord { get; set; }
        public TrieNode() 
        {
            Children = new Dictionary<char, TrieNode>();
            IsEndOfWord = false;
        }
    }
    public class Trie
    {
        private readonly TrieNode root;
        public Trie()
        {
            root = new TrieNode();
        }

        public void Insert(string word)
        {
            TrieNode currentNode = root;
            
            foreach(char c in word)
            {
                if(!currentNode.Children.ContainsKey(c))
                    currentNode.Children[c]= new TrieNode();

                currentNode = currentNode.Children[c];
            }
            currentNode.IsEndOfWord = true;
        }
        public bool StartWith(string word)
        {
            TrieNode currentNode = root;
            foreach(char c in word)
            {
                if (!currentNode.Children.ContainsKey(c))
                    return false;

                currentNode = currentNode.Children[c];
            }
            return true;
        }
        public static string ReplaceWords(IList<string> dictionary,  string sentence)
        {
            dictionary = dictionary.OrderBy(x=>x.Length).ToList();
            string[] words = sentence.Split(new char[] { ' ' });

            for(int i = 0; i < words.Length; i++)
            {
                Trie trie = new Trie();
                trie.Insert(words[i]);

                foreach(string word in dictionary)
                {
                    if (trie.StartWith(word))
                    { 
                        words[i] = word;
                        break;
                    }
                }
            }

            return String.Join(" ", words);
        }
    }
}
