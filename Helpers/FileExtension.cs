﻿namespace CarsMVC.Helpers
{
    public static class FileExtension
    {
        public static async Task<string> SaveAsync(this IFormFile file, string path)
        {
            string extension = Path.GetExtension(file.FileName);
            string fileName = Path.GetFileNameWithoutExtension(file.FileName).Length > 40 ? file.FileName.Substring(0, 40) :
              Path.GetFileNameWithoutExtension(file.FileName);
            fileName = Path.Combine(path, Path.GetRandomFileName() + fileName + extension);
            using (FileStream fs = File.Create(Path.Combine(PathConstants.RootPath, fileName)))
            {
                await file.CopyToAsync(fs);
            }
            return fileName;
        }
    }
}
