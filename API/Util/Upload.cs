using System;
using API.Domain.Entities;
using API.Domain.Enum;
using ImageProcessor;
using ImageProcessor.Plugins.WebP.Imaging.Formats;

namespace API.Util
{
	public class Upload
	{
		public string UploadFile(IFormFile file)
		{
			var validateTypeMedia = GetTypeMedia(file.FileName);
			return validateTypeMedia == Media.Image ? UploadImage(file) : UploadVideo(file);
		}

		public Media GetTypeMedia(string fileName)
		{
			string[] imageExtensions = { ".png", ".jpg", ".jpeg", ".webp" };
            string[] videoExtensions = { ".avi", ".mp4" };

			var fileInfo = new FileInfo(fileName);

			return imageExtensions.Contains(fileInfo.Extension) ? Media.Image :
										videoExtensions.Contains(fileInfo.Extension) ? Media.Video :
										throw new DomainException("Formato de arquivo inválido!");
        }

		private string UploadImage(IFormFile file)
		{
            using (var stream = new FileStream(Path.Combine("Medias/Imagens", file.FileName), FileMode.Create))
            {
                file.CopyTo(stream);
            }

            var urlFile = Guid.NewGuid() + ".webp";

            using (var webpFileStream = new FileStream(Path.Combine("Medias/Imagens", urlFile), FileMode.Create))
            {
                using (ImageFactory imageFactory = new ImageFactory(preserveExifData: false))
                {
                    imageFactory.Load(file.OpenReadStream()) //carrega os dados da imagem
                        .Format(new WebPFormat()) //seta o formato webp
                        .Quality(100) //manter qualidade do original
                        .Save(webpFileStream); //salva imagem
                }
            }

            return $"http://localhost:5082/medias/imagens/{urlFile}";
        }


        private string UploadVideo(IFormFile file)
        {
            FileInfo fi = new FileInfo(file.FileName);

            var fileName = Guid.NewGuid() + fi.Extension;


            using (var stream = new FileStream(Path.Combine("Medias/Videos", fileName), FileMode.Create))
            {
                file.CopyTo(stream);
            }

            return $"http://localhost:5082/medias/videos/{fileName}";
        }

    }
}

