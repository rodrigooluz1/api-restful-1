using System;
using API.Domain.Entities;
using API.Domain.Enum;
using API.Util;
using FluentAssertions;
using Xunit;

namespace API.Tests.Util
{
	public class UploadTests
	{
		[Theory]
		[InlineData(Media.Image, "image.webp")]
        [InlineData(Media.Video, "video.mp4")]
        public void Should_verify_if_Type_is_Image_or_Video(Media media, string fileName)
		{
			var upload = new Upload();

			var result = upload.GetTypeMedia(fileName);

			Assert.Equal(media, result);
		}

        [Theory]
        [InlineData(Media.Image, "image.psd")]
        [InlineData(Media.Video, "video.mp3")]
        public void Should_verify_if_Type_isnot_Image_or_Video(Media media, string fileName)
        {
            var upload = new Upload();

            var result = () => upload.GetTypeMedia(fileName);

            result.Should().ThrowExactly<DomainException>().WithMessage("Formato de arquivo inválido!");
        }
    }
}

