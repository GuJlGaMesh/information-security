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

					var input = inputString.Text.Replace(' ', '_');
					input = input.PadRight(25, '_');
					var crypt = new StringBuilder("".PadRight(25));

					Key1 = inputKey1.Text.Select(x => Convert.ToInt32(x.ToString()) - 1).ToArray();
					Key2 = inputKey2.Text.Select(x => Convert.ToInt32(x.ToString()) - 1).ToArray();

					for (var i = 0; i < 5; i++)
					{
						for (var j = 0; j < 5; j++)
						{
							if (rbCrypt.IsChecked ?? false)
								Matrix[Key1[i], Key2[j]] = input[j + i * 5];
						}
					}

					for (var i = 0; i < 5; i++)
					{
						for (var j = 0; j < 5; j++)
						{
							if (rbCrypt.IsChecked ?? false)
							{
								crypt[j + 5 * i] = Matrix[Key2[i], Key1[j]];
							}

							if (rbDecrypt.IsChecked ?? false)
							{
								crypt[i + j * 5] = Matrix[Key1[j], Key2[i]];
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
