using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// Документацию по шаблону элемента "Пустая страница" см. по адресу https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x419

namespace _3hd
{
    /// <summary>
    /// Пустая страница, которую можно использовать саму по себе или для перехода внутри фрейма.
    /// </summary>
    public sealed partial class MainPage : Page
    {
	    private Dictionary<string, BigInteger> alphabetCL;
	    private Dictionary<BigInteger, string> alphabetLC;

        public MainPage()
        {
            this.InitializeComponent();
            alphabetCL = new Dictionary<string, BigInteger>();
            alphabetLC = new Dictionary<BigInteger, string>();
            BigInteger i = 1;
			for (char c = 'a'; c <= 'z'; c++, i++)
			{
				var s = c.ToString();
				alphabetCL[s] = i;
				alphabetLC[i] = s;
			}
        }


        private void Button_OnClick(object sender, RoutedEventArgs a)
        {
	        BigInteger p = Convert.ToUInt64(tb_P.Text), e, d= 0;
	        BigInteger q = Convert.ToUInt64(tb_Q.Text);
	        var message = GetBigIntegerRepresentationOfMessage(input.Text.ToLower()).ToList();
	        BigInteger n = p * q;

	        BigInteger phi = (p - 1) * (q - 1);
	        tb_Phi.Text = phi.ToString();

	        for (e = 2; e < phi; e++)
	        {
		        if (GCD(e, phi) == 1)
		        {
			        break;
		        }
	        }
	        tb_E.Text = e.ToString();
	        d = (1 + 2 * phi) / e;
	        //for (BigInteger i = 0; i <= 9; i++)
	        //{
		       // BigInteger x = 1 + (i * phi);

		       // // d is for private key exponent
		       // if (x % e == 0)
		       // {
			      //  d = x / e;
			      //  break;
		       // }
	        //}
	        tb_D.Text = d.ToString();

	        var crypted =message.Select(x => BigInteger.Pow(x, (int)e) % n).ToList();
	        var decrypted = string.Join("",crypted.Select(x => BigInteger.Pow(x, (int)d) % n).Select(x => alphabetLC[x]));
			result_Crypted.Text = string.Join(" ", crypted);
			result_Decrypted.Text = decrypted; //GetMessageFromBigInteger(decrypted);
        }

        private IEnumerable<BigInteger> GetBigIntegerRepresentationOfMessage(string inputText) =>
	        inputText.Select(x => alphabetCL[x.ToString()]);

		private string GetMessageFromBigInteger(List<BigInteger> crypted)
		{
			var sb = new StringBuilder(crypted.ToString());
			for (var i = 0; i < sb.Length; i++)
			{
				var converted = Convert.ToUInt64(sb[i].ToString());
				sb[i] = Convert.ToChar(alphabetLC[converted]);
			}

			return sb.ToString();
		}
			


        static BigInteger GCD(BigInteger e, BigInteger phi)
        {
			BigInteger temp;
			while (true)
			{
				temp = e % phi;
				if (temp == 0)
					return phi;
				e = phi;
				phi = temp;
			}
		}

    }
}
