using System;
using System.Linq;
using System.Text;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

// Документацию по шаблону элемента "Пустая страница" см. по адресу https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x419

namespace _1st
{
	/// <summary>
	/// Пустая страница, которую можно использовать саму по себе или для перехода внутри фрейма.
	/// </summary>
	public sealed partial class MainPage : Page
	{

		public char[,] Matrix { get; set; } = new char[5, 5];

		public int[] Key1
		{
			get;
			set;
		}

		public int[] Key2
		{
			get;
			set;
		}

		public MainPage()
		{
			this.InitializeComponent();
			rbCrypt.IsChecked = true;
		}

		private void InputString_OnLostFocus(object sender, RoutedEventArgs e)
		{
			try
			{
				if (!string.IsNullOrEmpty(inputString.Text))
				{
					if (inputString.Text.Length > 25)
					{
						tbResult.Text = "Текст больше 25 символов";
						return;
					}

					var input = inputString.Text;//.Replace(' ', '_');
					input = input.PadRight(25, ' ');
					var crypt = new StringBuilder("".PadRight(25));

					Key1 = inputKey1.Text.Select(x => Convert.ToInt32(x.ToString()) - 1).ToArray();
					Key2 = inputKey2.Text.Select(x => Convert.ToInt32(x.ToString()) - 1).ToArray();

					var matrix1 = Matrix;

					for (int i = 0, index = 0; i < 5; i++)
					{
						for (var j = 0; j < 5; j++, index++)
						{
							if (rbCrypt.IsChecked ?? false)
								Matrix[Key1[i], j] = input[index];
							if (rbDecrypt.IsChecked ?? false)
								matrix1[j, Key2[i]] = input[index];

						}
					}

					for (int i = 0, index = 0; i < 5; i++)
					{
						for (var j = 0; j < 5; j++, index++)
						{
							if (rbCrypt.IsChecked ?? false)
							{
								crypt[index] = Matrix[j, Key2[i]];
							}

							if (rbDecrypt.IsChecked ?? false)
							{
								crypt[index] = Matrix[Key1[i], j];
							}
						}
					}

					tbResult.Text = crypt.ToString();


				}
			}
			catch
			{
				return;
			}
		}

		private void Button_OnClick(object sender, RoutedEventArgs e)
		{
			inputString.Text = tbResult.Text;
		}
	}
}
