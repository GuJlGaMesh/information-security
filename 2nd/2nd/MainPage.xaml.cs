using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

// Документацию по шаблону элемента "Пустая страница" см. по адресу https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x419

namespace _2nd
{
	/// <summary>
	/// Пустая страница, которую можно использовать саму по себе или для перехода внутри фрейма.
	/// </summary>
	public sealed partial class MainPage : Page
	{
		public string Alphabet
		{
			get;
			set;
		}

		public MainPage()
		{
			this.InitializeComponent();
			Alphabet = alphabet.Text.ToUpper() + alphabet.Text.ToLower() + " ,.-+=!?";
		}

		private void Button_OnClick(object sender, RoutedEventArgs e)
		{
			var crypter = new VigenereCipher(Alphabet);
			var cryptedMessage = crypter.Encrypt(input.Text, key.Text);
			var decryptedMessage = crypter.Decrypt(cryptedMessage, key.Text);
			result.Text = cryptedMessage + Environment.NewLine + decryptedMessage;

		}
	}

	public class VigenereCipher
	{
		const string defaultAlphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
		readonly string letters;

		public VigenereCipher(string alphabet = null)
		{
			letters = string.IsNullOrEmpty(alphabet) ? defaultAlphabet : alphabet;
		}

		private string GetRepeatKey(string s, int n)
		{
			var p = s;
			while (p.Length < n)
			{
				p += p;
			}

			return p.Substring(0, n);
		}

		private string Vigenere(string text, string password, bool encrypting = true)
		{
			var gamma = GetRepeatKey(password, text.Length);
			var retValue = "";
			var q = letters.Length;

			for (int i = 0; i < text.Length; i++)
			{
				var letterIndex = letters.IndexOf(text[i]);
				var codeIndex = letters.IndexOf(gamma[i]);
				if (letterIndex < 0)
				{
					retValue += text[i].ToString();
				}
				else
				{
					retValue += letters[(q + letterIndex + ((encrypting ? 1 : -1) * codeIndex)) % q].ToString();
				}
			}

			return retValue;
		}

		public string Encrypt(string plainMessage, string password)
			=> Vigenere(plainMessage, password);

		public string Decrypt(string encryptedMessage, string password)
			=> Vigenere(encryptedMessage, password, false);
	}
}
