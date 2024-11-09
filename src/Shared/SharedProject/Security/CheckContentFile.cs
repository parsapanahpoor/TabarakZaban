using System.Drawing;
using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Http;

namespace SharedProject.Security;

public static class CheckContentFile
{
#pragma warning disable CA1416
    public const int ImageMinimumBytes = 512;

    public static bool IsFile(this IFormFile? postedFile, bool checkFileExtension = true)
    {
        if (postedFile == null)
            return false;

        // Define the valid extensions (expand the list as needed)
        var validExtensions = new[] { ".rar", ".zip", ".pdf", ".doc", ".docx", ".xls", ".xlsx", ".jpg", ".jpeg", ".png", ".gif", ".txt", ".csv", ".mp4", ".mp3", ".avi", ".mov" };

        // Get the file extension and check against valid extensions
        var fileExtension = Path.GetExtension(postedFile.FileName)?.ToLower();

        return !checkFileExtension || validExtensions.Contains(fileExtension);
    }

    public static bool HasLength(this IFormFile postedFile, int length) =>
        postedFile.Length <= length;

    public static bool IsImage(this IFormFile postedFile)
    {
        //-------------------------------------------
        //  Check the image mime types
        //-------------------------------------------
        if (postedFile.ContentType.ToLower() != "image/jpg" &&
            postedFile.ContentType.ToLower() != "image/jpeg" &&
            postedFile.ContentType.ToLower() != "image/x-png" &&
            postedFile.ContentType.ToLower() != "image/png")
        {
            return false;
        }

        //-------------------------------------------
        //  Check the image extension
        //-------------------------------------------
        if (Path.GetExtension(postedFile.FileName).ToLower() != ".jpg"
            && Path.GetExtension(postedFile.FileName).ToLower() != ".png"
            && Path.GetExtension(postedFile.FileName).ToLower() != ".jpeg")
        {
            return false;
        }

        //-------------------------------------------
        //  Attempt to read the file and check the first bytes
        //-------------------------------------------
        try
        {
            if (!postedFile.OpenReadStream().CanRead)
            {
                return false;
            }
            //------------------------------------------
            //check whether the image size exceeding the limit or not
            //------------------------------------------ 
            if (postedFile.Length < ImageMinimumBytes)
            {
                return false;
            }

            byte[] buffer = new byte[ImageMinimumBytes];
            postedFile.OpenReadStream().Read(buffer, 0, ImageMinimumBytes);
            string content = System.Text.Encoding.UTF8.GetString(buffer);
            if (Regex.IsMatch(content, @"<script|<html|<head|<title|<body|<pre|<table|<a\s+href|<img|<plaintext|<cross\-domain\-policy",
                    RegexOptions.IgnoreCase | RegexOptions.CultureInvariant | RegexOptions.Multiline))
            {
                return false;
            }
        }
        catch (Exception)
        {
            return false;
        }

        //-------------------------------------------
        //  Try to instantiate new Bitmap, if .NET will throw exception
        //  we can assume that it's not a valid image
        //-------------------------------------------
        try
        {
            Bitmap bitmap = new(postedFile.OpenReadStream());
        }
        catch (Exception)
        {
            return false;
        }
        finally
        {
            postedFile.OpenReadStream().Position = 0;
        }

        return true;
    }
#pragma warning restore CA1416
}