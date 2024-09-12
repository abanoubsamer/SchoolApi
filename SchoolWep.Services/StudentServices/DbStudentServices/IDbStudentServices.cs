using Azure;
using SchoolWep.Data.Enums.Oredring;
using SchoolWep.Data.Models;
using SchoolWep.Services.ServicesResult;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SchoolWep.Services.StudentServices.DbStudentServices
{
    public interface IDbStudentServices
    {

        public Task<DbServicesResult> AddStudentAsync(Student model);
        public Task<DbServicesResult> RemoveStudentAsync(int Id);
        public Task<DbServicesResult> UpdateStudentAsync(Student model);
        public Task<Student> FindByIdAsync(int ID);
        public Task<Student> FindByNameAsync(string Name);
        public Task<List<Student>> GetAllStudentsAsync();
        public Task<bool> CheckDep(int? depId);
        public Task<bool> CheckLevel(int? depLevel);
        public Task<bool> StudentIsExist(string Name);
        public Task<bool> StudentIsExist(int Id);
        public Task<bool> IsNameExistAsSelf(string Name, int id);
        public Expression<Func<Student, TResponse>> CreateExpression<TResponse>(Func<Student, TResponse> projection);
        public IQueryable<Student> GetQueryable();
        public IQueryable<Student> FilterStudentPagination(StudentOredringEnum? oreder, OrederBy? orederBy, string? Search);
        public  Task<bool> StudentIsExistName(string NameAr, string NameEn);
    }
}
