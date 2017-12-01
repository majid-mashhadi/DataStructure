using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithms
{
    public class Huffman
    {
        class LetterFrequency
        {
            public char letter;
            public int Frequency;
            public double Weight;
        }

        Dictionary<char, string> dict;
        LetterFrequency[] letters;
        public Huffman()
        {
            dict = new Dictionary<char, string>();
            dict.Add('a', "1");
            dict.Add('b', "01");

            Build();

            string s = Compress("babababbbbbbbbbbbb");
            s = Uncompress(s);
        }


        public void Build()
        {

            letters = new LetterFrequency[] { 
                new LetterFrequency() { letter= 'A',Frequency= 77},
                new LetterFrequency() { letter= 'B',Frequency= 17} ,
                new LetterFrequency() { letter= 'C',Frequency= 32},
                new LetterFrequency() { letter= 'D',Frequency=42 },
                new LetterFrequency() { letter= 'E',Frequency=120 },
                new LetterFrequency() { letter= 'F',Frequency= 24},
                new LetterFrequency() { letter= 'G',Frequency=17 },
                new LetterFrequency() { letter= 'H',Frequency= 50},
                new LetterFrequency() { letter= 'I',Frequency=76 },
                new LetterFrequency() { letter= 'J',Frequency=4 },
                new LetterFrequency() { letter= 'K',Frequency=7 },
                new LetterFrequency() { letter= 'L',Frequency=42 },
                new LetterFrequency() { letter= 'M',Frequency=24 },
                new LetterFrequency() { letter= 'N',Frequency=67 },
                new LetterFrequency() { letter= 'O',Frequency=67 },
                new LetterFrequency() { letter= 'P',Frequency=20 },
                new LetterFrequency() { letter= 'Q',Frequency= 5},
                new LetterFrequency() { letter= 'R',Frequency= 59},
                new LetterFrequency() { letter= 'S',Frequency=67 },
                new LetterFrequency() { letter= 'T',Frequency=85 },
                new LetterFrequency() { letter= 'U',Frequency=37 },
                new LetterFrequency() { letter= 'V',Frequency=12 },
                new LetterFrequency() { letter= 'W',Frequency= 22},
                new LetterFrequency() { letter= 'X',Frequency=4 },
                new LetterFrequency() { letter= 'Y',Frequency=22 },
                new LetterFrequency() { letter= 'Z',Frequency= 2},
                new LetterFrequency() { letter= 'a',Frequency= 77},
                new LetterFrequency() { letter= 'b',Frequency= 17},
                new LetterFrequency() { letter= 'c',Frequency= 32},
                new LetterFrequency() { letter= 'd',Frequency=42 },
                new LetterFrequency() { letter= 'e',Frequency=120 },
                new LetterFrequency() { letter= 'f',Frequency= 24},
                new LetterFrequency() { letter= 'g',Frequency=17 },
                new LetterFrequency() { letter= 'h',Frequency= 50},
                new LetterFrequency() { letter= 'i',Frequency=76 },
                new LetterFrequency() { letter= 'j',Frequency=4 },
                new LetterFrequency() { letter= 'k',Frequency=7 },
                new LetterFrequency() { letter= 'l',Frequency=42 },
                new LetterFrequency() { letter= 'm',Frequency=24 },
                new LetterFrequency() { letter= 'n',Frequency=67 },
                new LetterFrequency() { letter= 'o',Frequency=67 },
                new LetterFrequency() { letter= 'p',Frequency=20 },
                new LetterFrequency() { letter= 'q',Frequency= 5},
                new LetterFrequency() { letter= 'r',Frequency= 59},
                new LetterFrequency() { letter= 's',Frequency=67 },
                new LetterFrequency() { letter= 't',Frequency=85 },
                new LetterFrequency() { letter= 'u',Frequency=37 },
                new LetterFrequency() { letter= 'v',Frequency=12 },
                new LetterFrequency() { letter= 'w',Frequency= 22},
                new LetterFrequency() { letter= 'x',Frequency=4 },
                new LetterFrequency() { letter= 'y',Frequency=22 },
                new LetterFrequency() { letter= 'z',Frequency= 2}
            };

            int total = 0;
            foreach (LetterFrequency letter in letters)
            {
                total += letter.Frequency;
            }

            double sum = 0;
            foreach (LetterFrequency letter in letters)
            {
                letter.Weight = Math.Round( letter.Frequency * 1.0 / total,4) ;
                sum += letter.Weight;
            }
        }

        public string Compress(string str)
        {
            StringBuilder sb = new StringBuilder();
            foreach (char c in str)
            {
                if (dict.ContainsKey(c))
                {
                    sb.Append(dict[c]);
                }
            }
            return ConvertStringToByte(sb.ToString());
        }

        private string ConvertStringToByte(string str)
        {
            int index = 0;
            StringBuilder sb = new StringBuilder();
            int b = 0;
            int ZeorIndex = 0;
            foreach (char c in str)
            {
                if (c == '1')
                {
                    b = b | 1 << index;
                    ZeorIndex = 0;
                }
                else
                {
                    ZeorIndex++;
                }
                index++;
                if (index % 8 == 0)
                {
                    sb.Append(Convert.ToChar(b));
                    index = ZeorIndex;
                    b = 0;
                }
            }
            if (b != 0)
            {
                sb.Append(Convert.ToChar(b));
            }
            return sb.ToString();
        }

        private string Uncompress(string str)
        {
            StringBuilder sb = new StringBuilder();
            foreach (char c in str)
            {
                sb.Append(ConvertToHuffmanCode(c));
            }
            return GetStringFromHuffmanCode(sb);

        }

        private string ConvertToHuffmanCode(char c)
        {
            int b = 0;
            StringBuilder sb = new StringBuilder();
            b = Convert.ToInt32(c);
            while (b != 0)
            {
                if ((b & 1) == 0)
                {
                    sb.Append("0");
                }
                else
                {
                    sb.Append("1");
                }
                b >>= 1;
            }
            return sb.ToString();
        }

        private string GetStringFromHuffmanCode(StringBuilder sb)
        {
            StringBuilder result = new StringBuilder();
            int index = 0;
            StringBuilder s = new StringBuilder();
            while (index < sb.Length)
            {
                s.Append(sb[index]);
                while (!dict.ContainsValue(s.ToString()) && index < sb.Length)
                {
                    index++;
                    s.Append(sb[index]);
                }
                result.Append(dict.First(x => x.Value == s.ToString()).Key);
                s.Length = 0;
                index++;
            }

            return result.ToString();
        }
    }
}
