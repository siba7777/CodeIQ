using System;
using System.IO;
using System.Net;
using System.Text;

namespace SpaceTalky
{
    class Program
    {
        static void Main()
        {
            string inputPath = Environment.GetCommandLineArgs()[1];
            if (!File.Exists(inputPath)) new FileNotFoundException();

            using (StreamWriter sw = new StreamWriter(@".\input.txt", false, Encoding.GetEncoding(932)))
            {
                foreach (string word in File.ReadAllLines(inputPath, Encoding.GetEncoding(932)))
                {
                    string input = GenerateInput(word);
                    string output = Web.GetOutput(input);

                    if (word == output) sw.WriteLine("{0}:{1}", input, output);
                    else sw.WriteLine("{0}:{1}", "X", word);
                }
            }
        }

        public static string GenerateInput(string word)
        {
            string input = "";
            const int baseCode = 97;    //'a'のコード
            for (int i = 0; i < word.Length; i += 2)
            {
                input += word[i];
                if (i + 1 < word.Length)
                    for (int j = 0; j < (int)word[i + 1] - baseCode; ++j, input += word[i]) ;
                else
                    input += "a";
            }
            return input;
        }

        public static string GetOutput(string input)
        {
            string output = "";
            WebRequest req = WebRequest.Create("http://spacetalky.textfile.org/api.cgi?input=" + input);
            WebResponse rsp = req.GetResponse();
            Stream stream = rsp.GetResponseStream();
            StreamReader reader = new StreamReader(stream, System.Text.Encoding.GetEncoding("Shift_JIS"));
            output = reader.ReadToEnd();
            stream.Close();
            rsp.Close();

            return output;
        }
    }
}
