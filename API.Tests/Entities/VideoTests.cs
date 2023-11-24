using System;
using API.Domain.Entities;
using Xunit;

namespace API.Tests.Entities
{
	public class VideoTests
	{
		[Fact]
		public void Video_Validate_Title_Length()
		{
			
			var result = Assert.Throws<DomainException>(() => new Video(
					"Entretenimento",
                    "Chef se emocionou ao assistir apresentação da cantora Maria Rita e do Maestro João Carlos Martins",
					"Da redação",
                    "https://pubimg.band.uol.com.br/files/0e1973594486e928bf38.webp",
                    "https://pubimg.band.uol.com.br/files/0e1973594486e928bf38.mp4",
                    status: API.Domain.Enum.Status.Active
                ));
			Assert.Equal("O título deve ter até 90 caracteres!", result.Message);
		}

        [Fact]
        public void Video_Validate_Hat_Length()
        {

            var result = Assert.Throws<DomainException>(() => new Video(
                    "Chef se emocionou ao assistir apresentação da cantora Maria Rita ",
                    "Chef se emocionou ao assistir apresentação da cantora Maria Rita e do Maestro João Carlos Martins",
                    "Da redação",
                    "https://pubimg.band.uol.com.br/files/0e1973594486e928bf38.webp",
                    "https://pubimg.band.uol.com.br/files/0e1973594486e928bf38.mp4",
                    status: API.Domain.Enum.Status.Active
                ));
            Assert.Equal("O chapéu deve ter até 40 caracteres!", result.Message);
        }

        [Fact]
        public void Video_Validate_Title_Empty()
        {

            var result = Assert.Throws<DomainException>(() => new News(
                    "Entretenimento",
                    "",
                    "Da redação",
                    "https://pubimg.band.uol.com.br/files/0e1973594486e928bf38.webp",
                    "https://pubimg.band.uol.com.br/files/0e1973594486e928bf38.mp4",
                    status: API.Domain.Enum.Status.Active
                ));
            Assert.Equal("O título não pode estar vazio!", result.Message);
        }

        [Fact]
        public void Video_Validate_Hat_Empty()
        {

            var result = Assert.Throws<DomainException>(() => new News(
                    "",
                    "Chef se emocionou ao assistir apresentação da cantora Maria Rita",
                    "Da redação",
                    "https://pubimg.band.uol.com.br/files/0e1973594486e928bf38.webp",
                    "https://pubimg.band.uol.com.br/files/0e1973594486e928bf38.mp4",
                    status: API.Domain.Enum.Status.Active
                ));
            Assert.Equal("O chapéu não pode estar vazio!", result.Message);
        }
    }
}

