using System;
using System.Collections.Concurrent;
using System.Threading;

namespace TextHandler
{
    public class TextHandler
    {
        private ConcurrentDictionary<string, int> dict;
        public TextHandler()
        {

        }

        private void AddWord(object input)
        {
            string word = (string)input;
            if (!String.IsNullOrWhiteSpace(word))
            {

                if (!dict.TryAdd(word.ToLower(), 1))
                {
                    dict[word.ToLower()]++;
                }
            }
        }

        public ConcurrentDictionary<string, int> MultiThreadedResult(string text)
        {
            dict = new ConcurrentDictionary<string, int>();
            const char V = '\n';
            string[] words = text.Split(new[] { '-', '.', '?', '!', ')', '(', ',', ':', ' ', '\"', '«', '»', V }, StringSplitOptions.RemoveEmptyEntries);
            foreach (var word in words)
            {
                Thread thread = new Thread(new ParameterizedThreadStart(AddWord));
                thread.Start(word);
            }
            return dict;
        }

        private ConcurrentDictionary<string, int> SingleThreadedResult(string text)
        {
            dict = new ConcurrentDictionary<string, int>();
            const char V = '\n';
            string[] words = text.Split(new[] { '-', '.', '?', '!', ')', '(', ',', ':', ' ', '\"', '«', '»', V }, StringSplitOptions.RemoveEmptyEntries);
            foreach (var word in words)
            {
                AddWord(word);
            }
            return dict;
        }
    }
}