namespace SaipaShop.Application.Services.Storage;

public interface IStorageService
{
    /// <summary>
    /// ذخیره فایل
    /// </summary>
    /// <param name="bucket">محل ذخیره سازی برا جداسازی بخش هایی مثل تصویر پروفایل، امضا و ..</param>
    /// <param name="fileName">نام فایل</param>
    /// <param name="contentType">نوع فایل</param>
    /// <param name="stream">استریم فابل</param>
    /// <param name="errorCallback">رویداد دریافت خطا</param>
    /// <returns>نام فایل انتخاب شده</returns>
    Task<string> SaveFile(string bucket, string fileName,string contentType,Stream stream);
    
    /// <summary>
    /// دریاف فایل
    /// </summary>
    /// <param name="bucket">بخش</param>
    /// <param name="fileName">نام فایل</param>
    /// <param name="successCallback">در صورت بازکردانی موفق فایل</param>
    /// <param name="errorCallback">خطای کال بک</param>
    /// <returns></returns>
    Task<Stream> GetFile(string bucket, string fileName);
    
    /// <summary>
    /// حذف فایل
    /// </summary>
    /// <param name="bucket">بخش</param>
    /// <param name="objectName">نام فایل</param>
    /// <returns></returns>
    Task RemoveFile(string bucket, string objectName);


}