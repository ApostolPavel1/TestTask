using System.Collections.Concurrent;
using System.Text;

var path = Directory.GetCurrentDirectory().Split("/bin/Debug/net6.0").First();

var textFiles = Directory.GetFiles(path).Where(x=>x.EndsWith(".txt"));

StringBuilder sb = new StringBuilder();

foreach (var textFile in textFiles)
{
    var txt = File.ReadAllText(textFile);; 
    sb.Append(txt);
}

string text = sb.ToString();

var words = text.Split(new char[]{'.', ',', ':', ' ', '!', '?', '"', '(', ')','\n','\t','/' }).Where(x=>x != "");

ConcurrentDictionary<string, int> counter = new ConcurrentDictionary<string, int>();

foreach (var word in words)
{
    if (counter.ContainsKey(word))
    {
        counter[word]++;
        continue;
    }

    counter.TryAdd(word, 1);
}


StringBuilder resultStringBuilder = new StringBuilder();

foreach (var resultText in counter.OrderBy(x=>x.Value).Reverse())
{
    resultStringBuilder.Append( $"{resultText.Key}      -       {resultText.Value}\n");
}

File.WriteAllText($"{path}/results/results.txt",resultStringBuilder.ToString());

    
