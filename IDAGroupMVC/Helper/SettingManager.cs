using IDAGroupMVC.Models;
using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IDAGroupMVC.Helper
{
    public class SettingManager
    {

        public static bool SettingExists(int id, DataContext context)
        {
            return context.Settings.Any(e => e.Id == id);
        }
        public static void EditChange(Setting setting, Setting settingExist)
        {
            settingExist.Value = setting.Value;
            settingExist.ModifiedDate = DateTime.UtcNow.AddHours(4);
        }
        public static void SaveChange(DataContext context)
        {
            context.SaveChanges();
        }
        public static void AddContext(Setting setting, DataContext context)
        {
            context.Add(setting);

        }
        public static string FileSave(Setting setting, IWebHostEnvironment _env, string folder)
        {
            string image = FileManager.Save(_env.WebRootPath, $"uploads/settings/{folder}", setting.KeyImageFile);
            return image;
        }
        public static void EditPosterImageSave(Setting setting, Setting settingExist, IWebHostEnvironment _env, string folder)
        {
            var posterFile = setting.KeyImageFile;

            var filename = FileManager.Save(_env.WebRootPath, "uploads/settings/" + folder, setting.KeyImageFile); ;
            FileManager.Delete(_env.WebRootPath, "uploads/settings/" + folder, settingExist.Value);

            settingExist.Value = filename;
            settingExist.ModifiedDate = DateTime.UtcNow.AddHours(4);
        }
        public static void DeleteFile(Setting image, IWebHostEnvironment _env, string folder)
        {
            FileManager.Delete(_env.WebRootPath, $"uploads/settings/{folder}", image.Value);
        }

    }
}
