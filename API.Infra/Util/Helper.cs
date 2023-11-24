using System;
namespace API.Infra.Util
{
	public static class Helper
	{

		public static string GenerateSlug(string str)
		{
			var acentos = new[] { "ç" ,"Ç", "á", "Á", "é", "É", "í", "Í", "ó", "Ó", "ú", "Ú", "à","À","ã","Ã"};
            var semAcento = new[] { "c", "C", "a", "A", "e", "E", "i", "I", "o", "O", "u", "U", "a", "A", "a", "A" };


			for(var i = 0; i < acentos.Length; i++)
			{
				str = str.Replace(acentos[i], semAcento[i]);
			}

			var caracteresEspeciais = new[] { "!","@", "#", "$", "%", "ˆ", "&", "*", "(", ")", "'", "˜", "`", ";",":" };

            for(var i = 0; i < caracteresEspeciais.Length; i++)
			{
                str = str.Replace(caracteresEspeciais[i], "");
            }

			return str.Trim().ToLower().Replace("  ", " ").Replace(" ", "-");
        }

	}
}

