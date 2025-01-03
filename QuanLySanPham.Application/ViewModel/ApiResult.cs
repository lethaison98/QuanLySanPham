<<<<<<< HEAD
﻿
using QuanLySanPham.Application.Interfaces;
=======
﻿//Các lớp này thường được sử dụng trong các ứng dụng API để chuẩn hóa cách trả về kết quả cho client (frontend)
//giúp frontend dễ dàng xử lý và hiển thị thông tin phù hợp (thành công hoặc lỗi) cho người dùng.
//trả về kết quả thành công hay thất bại trong các hàm thêm, sửa, xoá....
>>>>>>> 548c7868a569838c13e7124694560102abd54878

namespace QuanLySanPham.Application.ViewModel
{
    /// <summary>
    /// Wrapper around the real business API data (in Data property) to add API exec error information
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ApiResult<T>
    {
        public ApiResult()
        {

        }

        public ApiResult(T data)
        {
            Data = data;
        }

        public ApiResult(T data, bool isSuccess = true, string message = "") :
            this(isSuccess, message)
        {
            Data = data;
            IsSuccess = isSuccess;
            Message = message;
        }

        public ApiResult(bool isSuccess = true, string message = "")
        {
            IsSuccess = isSuccess;
            Message = message;
        }

        /// <summary>
        /// false: API exec success
        /// true: API exec error
        /// </summary>
        public bool IsSuccess { get; set; }

        /// <summary>
        /// if IsError is true: contains error message
        /// if IsError is false: empty or contains additional API exec information
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// The real data return by the API
        /// </summary>
        public T Data { get; set; }
        public bool IsSuccessed { get; set; }
        public virtual ApiResult<T> OnError(string errorMessage)
        {
            IsSuccess = false;
            Message = errorMessage;
            return this;
        }

        public virtual ApiResult<T> OnSuccess(string successMessage = "")
        {
            IsSuccess = true;
            Message = successMessage;
            return this;
        }

        public static implicit operator ApiResult<T>(IQuanLyLoaiSanPham.ApiResult<bool> v)
        {
            throw new NotImplementedException();
        }
    }

    public class ApiErrorResult<T> : ApiResult<T>
    {
        public string[] ValidationErrors { get; set; }

        public ApiErrorResult()
        {
        }

        public ApiErrorResult(string message)
        {
            IsSuccess = false;
            Message = message;
        }

        public ApiErrorResult(string[] validationErrors)
        {
            IsSuccess = false;
            ValidationErrors = validationErrors;
        }
    }

    public class ApiSuccessResult<T> : ApiResult<T>
    {
        public ApiSuccessResult(T resultObj)
        {
            IsSuccess = true;
            Data = resultObj;
        }

        public ApiSuccessResult()
        {
            IsSuccess = true;
        }
    }
}

