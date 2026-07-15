using ECommerceProject.Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace ECommerceProject.Application.Repositories
{
    public interface IGenericRepository<T> where T: BaseEntity
    {
        // T: BaseEntity güvenlik duvarıdır. Dışarıdan repository'ye sadece BaseEntity'den türeyen sınıflar gönderilebilir.
        // Bu sayede, repository sadece belirli bir türdeki varlıklarla çalışabilir ve yanlış türdeki varlıkların kullanılmasını önler.

        //okuma operasyonlarım.
        //tracking sadece db den veri okurken kullanılan bir özelliktir.
        Task<List<T>> GetAll(bool tracking = true);
        Task<List<T>> GetWhere(Expression<Func<T, bool>> method, bool tracking = true);
        Task<T> GetByIdAsync(string id, bool tracking = true, bool ignoreQueryFilters = false);

        //Yazma operasyonlarım.
        //ekleme işleminde Ef Core takip etmek zorundadır. default tracking true gelir yani
        Task<bool> AddAsync(T model);
        //500 tane veriyi aynı anda eklemek için AddRangeAsync metodu kullanılır.
        Task<bool> AddRangeAsync(List<T> datas);
        bool Remove(T model);
        bool Update(T model);
        bool Restore(T model);


        //SaveAsync kullanmamızın sebebi Unit Of Work. yani işlerin tek bir transaction ile yapılması.
        //yani birden fazla işlem yapıldığında hepsi başarılı olursa commit edilir,
        //biri başarısız olursa rollback edilir.
        Task<int> SaveAsync();


    }
}
