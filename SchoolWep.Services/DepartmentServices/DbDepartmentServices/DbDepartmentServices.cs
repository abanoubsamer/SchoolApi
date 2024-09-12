using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.IdentityModel.Tokens;
using SchoolWep.Data.Enums.Oredring;
using SchoolWep.Data.Models;
using SchoolWep.Infrustructure.UnitOfWork;
using SchoolWep.Services.ServicesResult;
using System.Globalization;
using System.Linq.Expressions;
namespace SchoolWep.Services.DepartmentServices.DbDepartmentServices
{
    public class DbDepartmentServices: IDbDepartmentServices
    {
        #region Fields
        private readonly IUnitOfWork _UnitOfWork;
        #endregion

        #region Connstuctor
        public DbDepartmentServices(IUnitOfWork UnitOfWork)
        {
            _UnitOfWork=UnitOfWork;
        }



        #endregion

        #region Implemntation


        public async Task<DbServicesResult> AddDepartment(Department model)
        {
            if (model == null) return new DbServicesResult
            {
                Error = true,
                Msg = "Department Is Null"
            };

            try
            {

                await _UnitOfWork.Repository<Department>().AddAsync(model);
                return new DbServicesResult
                {
                    Successed = true,

                };

            }catch(Exception ex)
            {
                return new DbServicesResult
                {
                    Error = true,
                    Msg = $"Error Exception {ex.Message}"
                };
            }
        }

        public IQueryable<Department> AsQueryable()
        {
            return _UnitOfWork.Repository<Department>().GetTableAsNotTraking();
        }
     
        public Expression<Func<Department, T>> CreateExprestion<T>(Func<Department, T> func)
        {
            return e => func(e);
        }

        public async Task<DbServicesResult> DeleteDepartment(Department model)
        {
            if (model == null) return new DbServicesResult
            {
                Error = true,
                Msg = "Department Is Null"
            };

            try
            {

                await _UnitOfWork.Repository<Department>().DeleteAsync(model);
                return new DbServicesResult
                {
                    Successed = true,

                };

            }
            catch (Exception ex)
            {
                return new DbServicesResult
                {
                    Error = true,
                    Msg = $"Error Exception {ex.Message}"
                };
            }
        }

        public IQueryable<Department> FilterDepartmentPagination(DepartmentOrederingEnum? order, OrederBy? orederBy, string? search)
        {

            bool isArCuluer = CultureInfo.CurrentCulture.TwoLetterISOLanguageName.ToLower() == "ar";
            var Queryavble = GetAsQueryable();
            if (!search.IsNullOrEmpty())
            {
                Queryavble =  isArCuluer ?  Queryavble.Where(dep => dep.NameAr.Contains(search))
                    : Queryavble.Where(dep => dep.NameEn.Contains(search));

            }

            Queryavble = Oreder(Queryavble, order, orederBy, isArCuluer);
            
            return Queryavble;

        }

        private IQueryable<Department> Oreder(IQueryable<Department> Queryavble, DepartmentOrederingEnum? order, OrederBy? orederBy,bool isArCuluer)
        {
            // defult Vlaue
            order = order == null ? 0 : order;
            
            bool ascending = orederBy == null || orederBy == 0;

            switch (order)
            {

                case DepartmentOrederingEnum.ID:
                    Queryavble = ascending ? Queryavble.OrderBy(dep => dep.Id) : Queryavble.OrderByDescending(dep => dep.Id);
                    break;
                case DepartmentOrederingEnum.Name:
                    Queryavble = ascending
                        ? Queryavble.OrderBy(dep => isArCuluer ? dep.NameAr : dep.NameEn)
                        : Queryavble.OrderByDescending(dep => isArCuluer ? dep.NameAr : dep.NameEn);
                    break;
                case DepartmentOrederingEnum.Manager:
                    Queryavble = ascending
                        ? Queryavble.OrderBy(dep => isArCuluer ? dep.InstructorManger.FirstNameAr + " " + dep.InstructorManger.LastNameAr 
                        : dep.InstructorManger.FirstNameEn + " " + dep.InstructorManger.LastNameEn)
                        : Queryavble.OrderByDescending(dep => isArCuluer ? dep.InstructorManger.FirstNameAr + " " + dep.InstructorManger.LastNameAr
                        : dep.InstructorManger.FirstNameEn + " " + dep.InstructorManger.LastNameEn);
  
                    break;
                case DepartmentOrederingEnum.capsity:
                    Queryavble = ascending ? Queryavble.OrderBy(dep => dep.Capsity) : Queryavble.OrderByDescending(dep => dep.Capsity);
                    break;
                default:
                    break;
            }

            return Queryavble;
        }

        private IQueryable<Department> GetAsQueryable()
        {
            return _UnitOfWork.Repository<Department>().GetTableAsNotTraking().Include(x=>x.InstructorManger);
        }
        
        public async Task<Department> FindById(int Id)
        {
            return await _UnitOfWork.Repository<Department>().FindOneAsync(dep => dep.Id == Id);
        }

        public async Task<Department> FindByIdWihtNotTracking(int Id)
        {
            return await _UnitOfWork.Repository<Department>().FindOneAsNoTrackingAsync(dep => dep.Id == Id);
        }

        public async Task<List<Department>> GetDepartmentList()
        {
           return await _UnitOfWork.Repository<Department>().GetAllAsync();
        }

        public async Task<bool> IsDepartmentExist(string NameAr, string NameEn)
        {


            return
                 await _UnitOfWork.Repository<Department>().IsExistAsync(dep => dep.NameAr == NameAr || dep.NameEn == NameEn);
               
        }

        public async Task<DbServicesResult> UpdateDepartment(Department model)
        {
            if (model == null) return new DbServicesResult
            {
                Error = true,
                Msg = "Department Is Null"
            };

            try
            {

                await _UnitOfWork.Repository<Department>().UpdateAsync(model);
                return new DbServicesResult
                {
                    Successed = true,

                };

            }
            catch (Exception ex)
            {
                return new DbServicesResult
                {
                    Error = true,
                    Msg = $"Error Exception {ex.Message}"
                };
            }
        }

        public async Task<bool> ChackToManagerId(int? Id)
        {
            if (Id == null) return true;
            return await _UnitOfWork.Repository<Instructor>().IsExistAsync(ins => ins.Id == Id);
        }

        public  IQueryable<T> AsQueryableConvert<T>(ICollection<T>? values) where T : class
        {
            return _UnitOfWork.Repository<T>().GetTableAsNotTraking();
          
        }
        #endregion

    }
}
