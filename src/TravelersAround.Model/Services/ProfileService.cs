using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TravelersAround.Model.Entities;
using TravelersAround.Model.Factories;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;
using TravelersAround.Model.Exceptions;

namespace TravelersAround.Model.Services
{
    public class ProfileService
    {
        private IRepository _repository;

        public ProfileService(IRepository repository)
        {
            _repository = repository;
        }

        public void UpdateProfile(Traveler traveler)
        {
            _repository.Save<Traveler>(traveler);
            _repository.Commit();
        }

        public MemoryStream ResizeProfilePicture(Stream imageStream)
        {
            Size portrait = new Size(180, 300);
            Size landscape = new Size(300, 225);

            Size resize = portrait;
            
            Image originalImg = Image.FromStream(imageStream, true, true);

            if (!(ImageFormat.Jpeg.Equals(originalImg.RawFormat) || ImageFormat.Png.Equals(originalImg.RawFormat))) throw new InvalidImageFormatException();
                 
            if (originalImg.Size.Width > originalImg.Height) resize = landscape;
            
            MemoryStream buffer = new MemoryStream();
            Bitmap resizedImg = new Bitmap(originalImg, resize);
            resizedImg.Save(buffer, ImageFormat.Jpeg);
            
            imageStream.Close();
            resizedImg.Dispose();
            originalImg.Dispose();
            return buffer;
        }
    }
}
