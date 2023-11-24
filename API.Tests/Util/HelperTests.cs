using System;
using API.Infra.Util;
using Xunit;

namespace API.Tests.Util
{
	public class HelperTests
	{

		[Fact]
		public void Should_return_Validate_Slug()
		{
			//Arange
			var title = "Mourinho aconselha Ancelloti a recusar Seleção: 'Só um louco sai do Madrid";

			//Act
			var slug = Helper.GenerateSlug(title);

			//Assert
			Assert.Equal("mourinho-aconselha-ancelloti-a-recusar-selecao-so-um-louco-sai-do-madrid", slug);

		}
			
	}
}

