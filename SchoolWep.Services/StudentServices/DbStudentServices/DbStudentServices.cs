
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using SchoolWep.Data.Enums.Oredring;
using SchoolWep.Data.Models;
using SchoolWep.Infrustructure.UnitOfWork;
using SchoolWep.Services.ServicesResult;
using System.Globalization;
using System.Linq.Expressions;

namespace SchoolWep.Services.StudentServices.DbStudentServices
{
    public class DbStudentServices : IDbStudentServices
    {

        #region Fialds
        private readonly IUnitOfWork _UnitOfWork;
        #endregion

        
        
        #region Constructor
        public DbStudentServices(IUnitOfWork unitOfWork)
        {
            _UnitOfWork=unitOfWork;
        }
        #endregion



        #region Implimntations
        public async Task<DbServicesResult> AddStudentAsync(Student model)
        {
            if (model == null) return new DbServicesResult
            {
                Error = true,
                Msg = "Student Is Null"
            };

           
            try
            {
          
                await _UnitOfWork.Repository<Student>().AddAsync(model);
             
               return new DbServicesResult()
               {
                Successed = true,
               };

            }
            catch (Exception ex)
            {
               return new DbServicesResult
                {
                    Error = true,
                    Msg = $"Error Exception Is {ex.Message}"
                };
            }
        }

        public async Task<Student> FindByIdAsync(int ID)
        {
            return await _UnitOfWork.Repository<Student>().FindOneAsNoTrackingAsync(s => s.Id == ID);

        }

        public async Task<Student> FindByNameAsync(string Name)
        {
            return IsCultuerAr()
                ? await _UnitOfWork.Repository<Student>().FindOneAsync(s => s.FirstNameAr == Name)
                : await _UnitOfWork.Repository<Student>().FindOneAsync(s => s.FirstNameEn == Name);
        }

        public async Task<List<Student>> GetAllStudentsAsync()
        {
            return await _UnitOfWork.Repository<Student>().GetAllAsync();
        }

        public async Task<DbServicesResult> RemoveStudentAsync(int Id)
        {
            var std = await FindByIdAsync(Id);
            if (std == null) return new DbServicesResult
            {
                 Error = true,
                 Msg= "Not Found"
            };
            try
            {
               await  _UnitOfWork.Repository<Student>().DeleteAsync(std);
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

        public async Task<bool> StudentIsExistName(string NameAr ,string NameEn )
        {

            var FullNameAr = NameAr.Split(" ");
            var FullNameEn = NameEn.Split(" ");
            return 
                    await _UnitOfWork.Repository<Student>().IsExistAsync(s =>( s.FirstNameAr == FullNameAr[0] && s.LastNameAr == FullNameAr[1]) || ( s.FirstNameEn == FullNameEn[0] && s.LastNameEn == FullNameEn[1]))
;

        }

        public async Task<bool> StudentIsExist(string Name)
        {
          
            var FullName = Name.Split(" ");
          return IsCultuerAr()
                ?  await _UnitOfWork.Repository<Student>()
                .IsExistAsync(s => s.FirstNameAr == FullName[0] && s.LastNameAr == FullName[1])
                : await _UnitOfWork.Repository<Student>()
                .IsExistAsync(s => s.FirstNameEn == FullName[0] && s.LastNameEn== FullName[1]); 

        }

        public async Task<bool> IsNameExistAsSelf(string Name,int id)
        {
            var FullName = Name.Split(" ");
            return IsCultuerAr()
               ? await _UnitOfWork.Repository<Student>()
               .IsExistAsync(s => s.FirstNameAr == FullName[0] && s.LastNameAr == FullName[1] && s.Id != id)
               : await _UnitOfWork.Repository<Student>()
               .IsExistAsync(s => s.FirstNameEn == FullName[0] && s.LastNameEn == FullName[1] && s.Id != id);
            
        }

        public async Task<bool> StudentIsExist(int Id)
        {        
            return await _UnitOfWork.Repository<Student>()
                  .IsExistAsync(s => s.Id == Id);
        }

        public async Task<DbServicesResult> UpdateStudentAsync(Student model)
        {
            if (model == null) return new DbServicesResult
            {
                Error = true,
                Msg = "Student Is Null"
            };      
            try
            {
               

                await _UnitOfWork.Repository<Student>().UpdateAsync(model);

                return new DbServicesResult()
                {
                    Successed = true,
                };

            }
            catch (Exception ex)
            {
                return new DbServicesResult
                {
                    Error = true,
                    Msg = $"Error Exception Is {ex.Message}"
                };
            }
        }
        
        
        public Expression<Func<Student, TResponse>> CreateExpression<TResponse>(Func<Student, TResponse> projection)
        {
            return x => projection(x);
        }

        public IQueryable<Student> FilterStudentPagination(StudentOredringEnum? oreder, OrederBy? orederBy, string? Search)
        {
           

            var Queryable = GetQueryable();

            if (!Search.IsNullOrEmpty())
                Queryable = Queryable.Where(
                    x => (x.FirstNameEn + " " + x.LastNameEn).Contains(Search) ||
                    (x.FirstNameAr + " " + x.LastNameAr).Contains(Search));
           
            return OrederBy(Queryable, oreder, orederBy); 
        }
        
        
        public  IQueryable<Student> GetQueryable()
        {
            return _UnitOfWork.Repository<Student>().GetTableAsNotTraking()
                .Include(x=>x.Department)
                .Include(x=>x.level);
        }



        public async Task<bool> CheckDep(int? depId)
        {
            if (depId == null) return true;
            return await _UnitOfWork.Repository<Department>().IsExistAsync(D => D.Id == depId);
        }

        public async Task<bool> CheckLevel(int? depLevel)
        {
            if (depLevel == null) return true;
            
            return await _UnitOfWork.Repository<Level>().IsExistAsync(D => D.Id == depLevel);
        }


        private bool IsCultuerAr()
        {
            CultureInfo culture = CultureInfo.CurrentCulture;
            return culture.TwoLetterISOLanguageName.ToLower().Equals("ar");
        }
        
        private IQueryable<Student> OrederBy(IQueryable<Student> Queryable, StudentOredringEnum? Oreder, OrederBy? orederBy)
        {
            bool currentCultuerAr = CultureInfo.CurrentCulture.TwoLetterISOLanguageName.ToLower().Equals("ar");

            //defult Value 
            Oreder = Oreder == null ? 0 : Oreder;

            bool ascending = orederBy == null || orederBy == 0;

            switch (Oreder)
            {
                case StudentOredringEnum.StudID:
                    Queryable = ascending
                    ? Queryable.OrderBy(x => x.Id)
                    : Queryable.OrderByDescending(x => x.Id);
                    break;
                case StudentOredringEnum.Name:
                    Queryable = ascending
                     ? Queryable.OrderBy(x => currentCultuerAr ? x.FirstNameAr + " " + x.LastNameAr : x.FirstNameEn + " " + x.LastNameEn)
                     : Queryable.OrderByDescending(x => currentCultuerAr ? x.FirstNameAr + " " + x.LastNameAr : x.FirstNameEn + " " + x.LastNameEn);
                    break;
                case StudentOredringEnum.Address:
                    Queryable = ascending
                    ? Queryable.OrderBy(x => x.Address.Country)
                    : Queryable = Queryable.OrderByDescending(x => x.Address.Country);
                    break;
                case StudentOredringEnum.DepartmentName:
                    Queryable = ascending
                    ? Queryable.OrderBy(x => currentCultuerAr ? x.Department.NameAr : x.Department.NameEn)
                    : Queryable.OrderByDescending(x => currentCultuerAr ? x.Department.NameAr : x.Department.NameEn);
                    break;
                case StudentOredringEnum.LevelName:
                    Queryable = ascending
                    ? Queryable.OrderBy(x => currentCultuerAr ? x.level.NameAr : x.level.NameEn)
                    : Queryable.OrderByDescending(x => currentCultuerAr ? x.level.NameAr : x.level.NameEn);
                    break;
                case StudentOredringEnum.Age:
                    Queryable = ascending
                    ? Queryable.OrderBy(x => DateTime.Now.Year - x.BirthDay.Year)
                    : Queryable.OrderByDescending(x => DateTime.Now.Year - x.BirthDay.Year);
                    break;
                default:
                    break;
            }

            return Queryable;
        }
        #endregion

    }
}
