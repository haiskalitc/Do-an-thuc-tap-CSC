using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using MongoDB.Driver;
using Common.Paging;

namespace Core.Interface
{
    public interface IXuLyDuLieu<T>  where T : class
    {
        /// <summary>
        /// Đọc tất cả các phần tử thuộc kiểu T
        /// </summary>
        /// <returns></returns>
        IQueryable<T> DocDanhSach();

        long TongSoLuong();

        IQueryable<T> DocDanhSach(int soLuong, int batdau);

        /// <summary>
        /// Đọc danh sách theo điều kiện và số lượng truyền vào
        /// </summary>
        /// <param name="where"></param>
        /// <param name="soLuong"></param>
        /// <returns></returns>
        IQueryable<T> DocDanhSach(Expression<Func<T, bool>> where, int? soLuong = null);
        /// <summary>
        /// Đọc danh sách theo điều kiện và số lượng truyền vào và các field muốn lấy ra
        /// </summary>
        /// <param name="dieuKienLoc"></param>
        /// <param name="fields"></param>
        /// <param name="soluong"></param>
        /// <returns></returns>
        IQueryable<T> DocDanhSach(FilterDefinition<T> dieuKienLoc, ProjectionDefinition<T> fields, int? soluong = null);

        /// <summary>
        /// Lấy ra 1 phần tử thuộc kiểu T bằng id
        /// </summary>
        /// <param name="ma">id</param>
        /// <returns></returns>
        T Doc(string ma);
        
        /// <summary>
        /// Lấy ra 1 phần tử thuộc kiểu T theo điều kiện
        /// </summary>
        /// <param name="dieuKienLoc"></param>
        /// <returns></returns>
        T Doc(FilterDefinition<T> dieuKienLoc);

        /// <summary>
        /// Thêm mới 1 phần tử
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        bool Ghi(T entity);

        /// <summary>
        /// Cập nhật 1 phần tử
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        bool CapNhat(T entity);

        /// <summary>
        /// Xóa 1 phần tử khỏi csdl
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        bool Xoa(T entity);

        /// <summary>
        /// Xóa 1 phần tử thỏa điều kiện truyền vào
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        bool Xoa(Expression<Func<T, bool>> where);

        /// <summary>
        /// lấy về dữ liệu thuộc về trang currentpage với số phần tử trên 1 trang là pageSize,
        /// điều kiện tìm kiếm cho bởi where và sắp xếp kết quả đó theo orderBy.
        /// </summary>
        /// <typeparam name="TKey"></typeparam>
        /// <param name="data"></param>
        /// <param name="orderBy"></param>
        /// <param name="currentPage"></param>
        /// <param name="pageSize"></param>
        /// <param name="ascending"></param>
        /// <returns></returns>
        //IPage<T> Page<TKey>(Expression<Func<T, bool>> where, Expression<Func<T, TKey>> orderBy, int currentPage,
        //    int pageSize, bool ascending = true);

        IPage<T> Page<TKey>(IQueryable<T> data, Expression<Func<T, TKey>> orderBy, int currentPage,
            int pageSize, bool ascending = true);
        

        ///// <summary>
        ///// Trả về các phần tử thuộc kiểu T theo điều kiện lọc và sắp xếp truyền vào.
        ///// </summary>
        ///// <param name="dieuKienLoc"></param>
        ///// <param name="sapXep"></param>
        ///// <returns></returns>
        //List<T> DocDanhSach(FilterDefinition<T> dieuKienLoc = null, SortDefinition<T> sapXep = null);

        ///// <summary>
        ///// Trả về 1 phần tử thuộc kiểu T theo id truyền vào.
        ///// </summary>
        ///// <param name="dieuKienLoc"></param>
        ///// <returns></returns>
        //Task<T> Doc(FilterDefinition<T> dieuKienLoc);

        /////// <summary>
        /////// Trả về 1 phần tử thuộc kiểu T theo mã truyền vào.
        /////// </summary>
        /////// <param name="ma"></param>
        /////// <returns></returns>
        ////Task<T> Doc(string ma);
        ///// <summary>
        ///// Thêm mới 1 phần tử.
        ///// </summary>
        ///// <param name="entity"></param>
        ///// <returns></returns>
        //bool Them(T entity);

        ///// <summary>
        ///// Cập nhật 1 phần tử.
        ///// </summary>
        ///// <param name="entity"></param>
        ///// <returns></returns>
        //bool ChinhSua(T entity);

        ///// <summary>
        ///// Xóa 1 phần tử khỏi csdl.
        ///// </summary>
        ///// <param name="entity"></param>
        ///// <returns></returns>
        //bool Xoa(T entity);

        ///// <summary>
        ///// Xóa các phần tử thỏa điều kiện truyền vào.
        ///// </summary>
        ///// <param name="where"></param>
        ///// <returns></returns>
        //bool Xoa(Expression<Func<T, bool>> where);
    }
}