using System;
using System.Collections.Generic;
using System.Text;

namespace SubstitutionCipher
{
    class CSharpSubstitutionCipher
    {
        static void Main()
        {
            const string alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            string shuffledAlphabet;

            string logo =
    @"
░▒▓████████▓▒░▒▓██████▓▒░▒▓████████▓▒░▒▓███████▓▒░░▒▓██████████████▓▒░░▒▓█▓▒░ 
░▒▓█▓▒░     ░▒▓█▓▒░░▒▓█▓▒░ ░▒▓█▓▒░          ░▒▓█▓▒░▒▓█▓▒░░▒▓█▓▒░░▒▓█▓▒░▒▓█▓▒░ 
░▒▓█▓▒░     ░▒▓█▓▒░░▒▓█▓▒░ ░▒▓█▓▒░          ░▒▓█▓▒░▒▓█▓▒░░▒▓█▓▒░░▒▓█▓▒░▒▓█▓▒░ 
░▒▓██████▓▒░░▒▓████████▓▒░ ░▒▓█▓▒░   ░▒▓███████▓▒░░▒▓█▓▒░░▒▓█▓▒░░▒▓█▓▒░▒▓█▓▒░ 
░▒▓█▓▒░     ░▒▓█▓▒░░▒▓█▓▒░ ░▒▓█▓▒░          ░▒▓█▓▒░▒▓█▓▒░░▒▓█▓▒░░▒▓█▓▒░▒▓█▓▒░ 
░▒▓█▓▒░     ░▒▓█▓▒░░▒▓█▓▒░ ░▒▓█▓▒░          ░▒▓█▓▒░▒▓█▓▒░░▒▓█▓▒░░▒▓█▓▒░▒▓█▓▒░ 
░▒▓█▓▒░     ░▒▓█▓▒░░▒▓█▓▒░ ░▒▓█▓▒░   ░▒▓███████▓▒░░▒▓█▓▒░░▒▓█▓▒░░▒▓█▓▒░▒▓█▓▒░ 
";
            Console.WriteLine(logo);
            string input = "Substitution Cipher";
            int totalWidth = 77;
            int padding = totalWidth - input.Length;
            if (padding < 0)
            {
                input = input.Substring(0, totalWidth);
                padding = 0;
            }
            int leftPadding = padding / 2;
            int rightPadding = padding - leftPadding;
            string output = new string('=', leftPadding) + input + new string('=', rightPadding);
            Console.WriteLine(output);
            Console.Write("Enter your shuffled alphabet (or press Enter to generate): ");
            string userInput = Console.ReadLine().ToUpper();

            if (string.IsNullOrWhiteSpace(userInput))
            {
                shuffledAlphabet = GenerateShuffledAlphabet(alphabet);
                Console.WriteLine($"Generated Shuffled Alphabet (Decryption Key): {shuffledAlphabet}");
            }
            else
            {
                shuffledAlphabet = userInput;

                while (!IsValidShuffledAlphabet(shuffledAlphabet, alphabet))
                {
                    Console.WriteLine("Invalid shuffled alphabet! Please make sure it contains all letters A-Z exactly once.");
                    Console.Write("Enter a valid shuffled alphabet: ");
                    shuffledAlphabet = Console.ReadLine().ToUpper();
                }
            }

            // Create encryption and decryption mappings
            Dictionary<char, char> encryptionMapping = CreateMapping(alphabet, shuffledAlphabet);
            Dictionary<char, char> decryptionMapping = CreateMapping(shuffledAlphabet, alphabet);

            while (true)
            {
                Console.Write("Enter your text : ");
                string textInput = Console.ReadLine().ToUpper();

                Console.Write("Choose Encrypt(1) or Decrypt(2): ");
                int choice = int.Parse(Console.ReadLine());

                if (choice == 1)
                {
                    // Encrypt
                    string encryptedText = EncryptText(textInput, encryptionMapping);
                    Console.WriteLine($"Encrypted text: {encryptedText}");
                }
                else if (choice == 2)
                {
                    // Decrypt
                    string decryptedText = DecryptText(textInput, decryptionMapping);
                    Console.WriteLine($"Decrypted text: {decryptedText}");
                }
                else
                {
                    Console.WriteLine("Please choose 1 (Encrypt) or 2 (Decrypt)!");
                    continue;
                }

                Console.Write("Do you want to perform another operation? (yes/no): ");
                string continueChoice = Console.ReadLine().ToLower();
                if (continueChoice != "yes")
                {
                    break;
                }
            }

            Console.WriteLine("Thank You For Using The Substitution Cipher Program. FAT3MI");
        }

        static string GenerateShuffledAlphabet(string alphabet)
        {
            Random random = new Random();
            char[] shuffledArray = alphabet.ToCharArray();

            // Shuffle using the Fisher-Yates algorithm
            for (int i = shuffledArray.Length - 1; i > 0; i--)
            {
                int j = random.Next(i + 1);
                char temp = shuffledArray[i];
                shuffledArray[i] = shuffledArray[j];
                shuffledArray[j] = temp;
            }

            return new string(shuffledArray);
        }

        static bool IsValidShuffledAlphabet(string shuffledAlphabet, string alphabet)
        {
            if (shuffledAlphabet.Length != alphabet.Length)
            {
                return false; // Length check
            }

            HashSet<char> seen = new HashSet<char>();
            foreach (char c in shuffledAlphabet)
            {
                if (!alphabet.Contains(c) || seen.Contains(c))
                {
                    return false; // Check for duplicates and only valid A-Z characters
                }
                seen.Add(c);
            }

            return true;
        }

        static Dictionary<char, char> CreateMapping(string fromAlphabet, string toAlphabet)
        {
            Dictionary<char, char> mapping = new Dictionary<char, char>();

            for (int i = 0; i < fromAlphabet.Length; i++)
            {
                mapping[fromAlphabet[i]] = toAlphabet[i];
            }

            return mapping;
        }

        static string EncryptText(string plaintext, Dictionary<char, char> mapping)
        {
            StringBuilder encryptedText = new StringBuilder();

            foreach (char c in plaintext)
            {
                if (mapping.ContainsKey(c))
                {
                    encryptedText.Append(mapping[c]);
                }
                else
                {
                    encryptedText.Append(c); // Keep non-alphabetic characters unchanged
                }
            }

            return encryptedText.ToString();
        }

        static string DecryptText(string encryptedText, Dictionary<char, char> mapping)
        {
            StringBuilder decryptedText = new StringBuilder();

            foreach (char c in encryptedText)
            {
                if (mapping.ContainsKey(c))
                {
                    decryptedText.Append(mapping[c]);
                }
                else
                {
                    decryptedText.Append(c); // Keep non-alphabetic characters unchanged
                }
            }

            return decryptedText.ToString();
        }
    }
}
