using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VinylExchange.Models.Utility;
using VinylExchange.Services.Files.SettingEnums;


namespace VinylExchange.Services.Files
{
    public class FileManager : IFileManager
    {
        private const string storagePath = @"wwwroot\";
        public async Task <IEnumerable<KeyValuePair<string, string>>> SaveFiles
            (IEnumerable<UploadFileUtilityModel> files, string directoryName)
        {
            
            foreach (var uploadFileUtility in files)
            {

                var fileNameSplitted = uploadFileUtility.FileName.Split(".");
                if (fileNameSplitted.Length == 2)
                {
                    

                    var fileName = fileNameSplitted[0];
                    var extension = "." +fileNameSplitted[1];
                    var fileNameBytes = System.Text.Encoding.UTF8.GetBytes(fileName);
                    var base64Name = System.Convert.ToBase64String(fileNameBytes);

                    var fileSystemName = uploadFileUtility.FileGuid.ToString() + "@---@" +base64Name + extension;

                    string savePath = storagePath + directoryName;

                    if (extension == ".jpg" || extension == ".png")
                    {
                        savePath += @"\Image\";
                    }
                    else if (extension == ".mp3")
                    {
                        savePath += @"\Audio\";
                    }
                                        

                    using (var fileStream = File.OpenWrite(savePath + fileSystemName))
                    {
                        await fileStream.WriteAsync(uploadFileUtility.FileByteContent);
                    }
                       
                   
                }

            }
                        

            return null;
        }



    }
}
