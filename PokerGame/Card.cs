using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokerGame
{
    public class Card
    {
        public string Value { get; set; }
        public int Number { get; set; }
        public string Suit { get; set; }

        public Card() { }

        public Card (string value)
        {
            Value = value;
            string num = Value.Substring(0, 1); // get the card number, first character in string
            SetCardNumber(num);
            Suit = Value.Substring(1); // get the suit, second character in string
        }

        public void SetCardNumber(string number)
        {
            switch(number)
            {
                case "A":
                    Number = 14;
                    break;
                case "K":
                    Number = 13;
                    break;
                case "Q":
                    Number = 12;
                    break;
                case "J":
                    Number = 11;
                    break;
                case "T":
                    Number = 10;
                    break;
                case "2": case "3": case "4": case "5": case "6": case "7": case "8": case "9" :
                    int.TryParse(number, out int num);
                    Number = num;
                    break;
                default:
                    Number = 0;
                    break;
            }
        }
    }
}
