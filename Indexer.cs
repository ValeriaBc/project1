// Вставьте сюда финальное содержимое файла Indexer.cs
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
 Add. Этот метод должен индексировать все слова в документе. 
 Разделители слов: { ' ', '.', ',', '!', '?', ':', '-','\r','\n' }; Сложность – O(document.Length)

GetIds. Этот метод должен искать по слову все id документов, где оно встречается. 
Сложность — O(result), где result — размер ответа на запрос

GetPositions. Этот метод по слову и id документа должен искать все позиции, в которых слово начинается. Сложность — O(result)

Remove. Этот метод должен удалять документ из индекса, после чего слова в нем искаться больше не должны. Сложность — O(document.Length)
     
     
     */
namespace PocketGoogle
{
    public class Indexer : IIndexer
    {
        public Dictionary<int, string> dictionary = new Dictionary<int, string>();
        public Dictionary<int, List<string>> dictionary_of_words = new Dictionary<int, List<string>>();

        public void Add(int id, string documentText)
        {
            var words = documentText.Split(' ', '.', ',', '!', '?', ':', '-', '\r', '\n');
            var list = new List<string>();
            foreach (var e in words)
            {
                list.Add(e);
            }
            if (!dictionary_of_words.ContainsKey(id))
                dictionary_of_words.Add(id, list);

            dictionary.Add(id, documentText);
        }

        public List<int> GetIds(string word)
        {
            var list_result = new List<int>();
            if (word.CompareTo(" ") == 0)
                return list_result;
            foreach (var e in dictionary_of_words)
            {
                if (e.Value.Contains(word))
                {
                    list_result.Add(e.Key);
                    continue;
                }
            }
            return list_result;
        }

        public List<int> GetPositions(int id, string word)
        {
            var str = dictionary[id];
            var str2 = dictionary_of_words[id];
            var list_result = new List<int>();

            int index = str.IndexOf(word, 0);
            int index2 = str2.IndexOf(word, 0);
            while (index > -1 && index2 > -1)
            {
                list_result.Add(index);
                index2 = str2.IndexOf(word, index2 + 1);
                index = str.IndexOf(word, index + word.Length);
            }
            return list_result;
        }

        public void Remove(int id)
        {
            dictionary_of_words.Remove(id);
            dictionary.Remove(id);
        }
    }
}


