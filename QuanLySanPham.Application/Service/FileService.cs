﻿using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using QuanLySanPham.Application.Common;
using QuanLySanPham.Application.Interfaces;
using QuanLySanPham.Application.Request;
using QuanLySanPham.Application.ViewModel;
using QuanLySanPham.Data.EF;
using QuanLySanPham.Data.Entities;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace QuanLySanPham.Application.Service
{
    public class FileService : IFileService
    {
        private readonly QuanLySanPhamDbContext _context;
        public IHttpContextAccessor _accessor;
        public FileService(QuanLySanPhamDbContext context, IHttpContextAccessor HttpContextAccessor)
        {
            _context = context;
            _accessor = HttpContextAccessor;
        }

        public async Task<ApiResult<int>> Insert(FileUploadRequest req)
        {
            return new ApiSuccessResult<int>();
        }
        public async static Task<string> UploadFile(IFormFile file, string path)
        {
            try
            {
                var folderName = "UploadFile";
                var pathToDB = Path.Combine(folderName, path);

                var fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
                var fileNameOutput = fileName;
                var extension = Path.GetExtension(fileName).ToLower();

                if (!Directory.Exists(pathToDB))
                {
                    Directory.CreateDirectory(pathToDB);
                }

                var newFileName = DateTime.Now.TimeOfDay.TotalMilliseconds.ToString()+"_" + RemoveSpecialCharacters(CommonUtils.RemoveSign4VietnameseString(fileName)).Replace("-", "");
                var newFilePath = Path.Combine(pathToDB, newFileName);
                using (Stream fileStream = new FileStream(newFilePath, FileMode.Create))
                {
                    await file.CopyToAsync(fileStream);
                }
                return Path.Combine(pathToDB, newFileName);

            }
            catch (Exception ex)
            {
                return null;
            }
        }
        private static string RemoveSpecialCharacters(string str)
        {
            return Regex.Replace(str, "[^a-zA-Z0-9_.]+", "", RegexOptions.Compiled);
        }
        public async Task<ApiResult<bool>> Delete(int idFile)
        {
            var result = false;
            var data = _context.Files.FirstOrDefault(x => x.IdFile == idFile);
            if (data != null)
            {
                _context.Files.Remove(data);
                await _context.SaveChangesAsync();
                result = true;
                return new ApiSuccessResult<bool>() { Data = result };
            }
            else
            {
                result = false;
                return new ApiErrorResult<bool>() { Data = result };
            }

        }
    }
}
