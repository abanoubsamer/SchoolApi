using SchoolWep.Data.Enums.Oredring;
using SchoolWep.Data.Models;
using SchoolWep.Services.ServicesResult;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SchoolWep.Services.DepartmentServices.DbDepartmentServices
{
    public interface IDbDepartmentServices
    {

        public Task<DbServicesResult> AddDepartment(Department model);
        public Task<List<Department>> GetDepartmentList();
        public Task<DbServicesResult> UpdateDepartment(Department model);
        public Task<DbServicesResult> DeleteDepartment(Department model);
        public Task<bool> IsDepartmentExist(string NameAr, string NameEn);
        public Task<Department> FindByIdWihtNotTracking(int Id);
        public Task<Department> FindById(int Id);
        public  IQueryable<Department> AsQueryable();
        public Expression<Func<Department, T>> CreateExprestion<T>(Func<Department, T> func);
        public IQueryable<Department> FilterDepartmentPagination(DepartmentOrederingEnum? order, OrederBy? orederBy,string? search);
        public Task<bool> ChackToManagerId(int? Id);
        public IQueryable<T> AsQueryableConvert<T>(ICollection<T>? values) where T : class;




    }
}
