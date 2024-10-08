namespace Company.Route2.PL.Helper
{
    public class DocumentSetting
    {
        // Upload Image fun 
        public static string Upload(IFormFile file,string FolderName)
        {
            // 1) Folder Path 
            string FolderPath = Path.Combine(Directory.GetCurrentDirectory(), $"wwwroot\\Files\\{FolderName}");

            //2 file name =>unique
            string FileName = $"{Guid.NewGuid()}{file.FileName}";

            // 3 File Path => Folder Path +file name

            string FilePath= Path.Combine(FolderPath,FileName);

            // 4 create file stream

            var FileStream = new FileStream(FilePath, FileMode.Create);

            // 5 copy file 
            file.CopyTo(FileStream);    

            return FileName;

        }


        // Delete Image fun 
        public static void Delete(string FileName, string FolderName)
        {
            string FilePath = Path.Combine(Directory.GetCurrentDirectory(), $"wwwroot\\Files\\{FolderName}",FileName);
          if (File.Exists(FilePath))
                File.Delete(FilePath);

        }
    }
}
