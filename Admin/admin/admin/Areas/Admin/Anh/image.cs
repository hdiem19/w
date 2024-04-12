namespace admin.Areas.Admin.Anh
{
    public class image
    {
        public static async Task<string> Uploadfile(Microsoft.AspNetCore.Http.IFormFile file, string sDirectory, string anh = null)
        {
            try
            {
                if (anh == null) anh = file.FileName;
                string path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot","Hinh", sDirectory);
                CreateIfMissing(path);
                string pathFile = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot","Hinh", sDirectory, anh);
                var suppportedTypes = new[] { "jpg" };
                var fileExt = System.IO.Path.GetExtension(file.FileName).Substring(1);
                if (!suppportedTypes.Contains(fileExt.ToLower()))
                {
                    return null;
                }
                else
                {
                    using (var stream = new FileStream(pathFile, FileMode.Create))
                    {
                        await file.CopyToAsync(stream);
                    }
                    return anh;
                }

            }
            catch
            {
                return null;
            }
        }
        public static void CreateIfMissing(string path)
        {
            bool folderExists = Directory.Exists(path);
            if(!folderExists)
            {
                Directory.CreateDirectory(path);
            }
        }
    }
}
