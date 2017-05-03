using System;
using System.Linq;
using System.Linq.Expressions;
using MongoDB.Bson;
using MongoDB.Driver;
using Common.Paging;
using Core.Interface;
using Core.Model.BaseModel;
using Common.Utilities;

namespace Service.BaseService
{
    public class XuLyDuLieu<T> : IXuLyDuLieu<T> where T : DoiTuongMau
    {
        protected readonly MongoConnectionHandler<T> MongoConnectionHandler;
        protected IMongoCollection<T> _collection;
        public XuLyDuLieu()
        {
            if (MongoConnectionHandler == null)
            {
                MongoConnectionHandler = new MongoConnectionHandler<T>();
            }
            _collection = MongoConnectionHandler.Collection;
        }

        public IQueryable<T> DocDanhSach()
        {
            return _collection.Find(new BsonDocument()).ToList().AsQueryable();
        }

        public IQueryable<T> DocDanhSach(int soLuong, int batdau)
        {
            return _collection.Find(new BsonDocument()).Sort(new BsonDocument("_id", -1)).Skip(batdau).Limit(soLuong).ToList().AsQueryable();
        }

        public long TongSoLuong()
        {
            return _collection.Find(new BsonDocument()).Count();
        }

        public IQueryable<T> DocDanhSach(Expression<Func<T, bool>> where, int? soLuong = null)
        {
            if (soLuong == null)
            {
                return DocDanhSach().Where(where);
            }
            var value = (int)soLuong;
            return DocDanhSach().Where(@where).Take(value).AsQueryable();
        }

        public IQueryable<T> DocDanhSach(FilterDefinition<T> dieuKienLoc, int? soluong = null)
        {
            if (soluong == null)
                return _collection.Find(dieuKienLoc).ToList().AsQueryable();
            var value = (int) soluong;
            return _collection.Find(dieuKienLoc).Limit(value).ToList().AsQueryable();
        }
        public IQueryable<T> DocDanhSach(FilterDefinition<T> dieuKienLoc, ProjectionDefinition<T> fields, int? soluong = null)
        {
            if (dieuKienLoc == null && fields == null && soluong != null)
            {
                return _collection.Find(new BsonDocument()).Limit(soluong).ToList().AsQueryable();
            }
            if (soluong == null)
                return _collection.Find(dieuKienLoc).Project<T>(fields).ToList().AsQueryable();
            var value = (int)soluong;
            return _collection.Find(dieuKienLoc).Project<T>(fields).Limit(value).ToList().AsQueryable();
        }
        public T Doc(string ma)
        {
            var dieuKienLoc = Builders<T>.Filter.Eq(p => p.Id, new ObjectId(ma));
            return _collection.Find(dieuKienLoc).FirstOrDefault();
        }

        public T Doc(FilterDefinition<T> dieuKienLoc)
        {
            return _collection.Find(dieuKienLoc).FirstOrDefault();
        }

        public bool Ghi(T entity)
        {
            try
            {
                if((int) entity.NgayTao == 0)
                {
                    entity.NgayTao = Utility.ConvertToUnixTimestamp(DateTime.UtcNow);
                    entity.NgayCapNhat = Utility.ConvertToUnixTimestamp(DateTime.UtcNow);
                }

                _collection.InsertOne(entity);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool CapNhat(T entity)
        {
            try
            {
                entity.NgayCapNhat = Utility.ConvertToUnixTimestamp(DateTime.UtcNow);
                var filter = Builders<T>.Filter.Eq(p => p.Id, entity.Id);
                _collection.ReplaceOne(filter, entity);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool Xoa(T entity)
        {
            try
            {
                _collection.DeleteOne(p => p.Id == entity.Id);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool Xoa(Expression<Func<T, bool>> @where)
        {
            try
            {
                _collection.DeleteMany(@where);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public IPage<T> Page<TKey>(Expression<Func<T, bool>> @where, Expression<Func<T, TKey>> orderBy, int currentPage, int pageSize, bool @ascending = true)
        {
            return Page(DocDanhSach().Where(where), orderBy, currentPage, pageSize, ascending);
        }

        public IPage<T> Page<TKey>(IQueryable<T> data, Expression<Func<T, TKey>> orderBy, int currentPage, int pageSize, bool @ascending = true)
        {
            var page = new Page<T>(currentPage, pageSize, data.Count());
            data = @ascending ? data.OrderBy(orderBy) : data.OrderByDescending(orderBy);
            data = data.Skip((page.CurrentPage - 1) * page.PageSize).Take(page.PageSize);
            page.Entities = data.AsEnumerable();
            return page;
        }

        IPage<T> IXuLyDuLieu<T>.Page<TKey>(IQueryable<T> data, Expression<Func<T, TKey>> orderBy, int currentPage, int pageSize, bool ascending)
        {
            throw new NotImplementedException();
        }
    }
}