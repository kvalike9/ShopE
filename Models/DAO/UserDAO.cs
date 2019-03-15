using PagedList;
using Shop.Models.EF;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Shop.Models.DAO
{
    public class UserDAO
    {
        ShopEXSEntities db = null;
        public UserDAO()
        {
            db = new ShopEXSEntities();
        }
        public int Insert(User entity)
        {
            db.Users.Add(entity);
            db.SaveChanges();
            return entity.ID;
        }
        public int Create(string username, string password, string name, string email, string address, string phone, bool status)
        {
            object [] para = 
            {
                new SqlParameter("@UserName", username),
                new SqlParameter("@Password", password),
                new SqlParameter("@Name", name),
                new SqlParameter("@Address", address),
                new SqlParameter("@Email", email),
                new SqlParameter("@Phone", phone),
                new SqlParameter("@Status", status)
            };
            int res = db.Database.ExecuteSqlCommand("sp_AddThanhVien @UserName,@Password,@Name,@Address,@Email,@Phone,@Status", para);
            return res;
        }
        public int Update(int id, string password, string name, string address, string email,  string phone, bool status)
        {
            object[] para =
            {
                new SqlParameter("@id", id),
                new SqlParameter("@Password", password),
                new SqlParameter("@Name", name),
                new SqlParameter("@Address", address),
                new SqlParameter("@Email", email),
                new SqlParameter("@Phone", phone),
                new SqlParameter("@Status", status)
            };
            int res = db.Database.ExecuteSqlCommand("sp_UpdateThanhVien @id,@Password,@Name,@Address,@Email,@Phone,@Status", para);
            return res;
        }
        public bool Update2(User entity)
        {
            try
            {
                var user = db.Users.SingleOrDefault(x => x.ID == entity.ID);
                user.Name = entity.Name;
                user.Address = entity.Address;
                user.Email = entity.Email;
                user.UpdatedBy = entity.UpdatedBy;
                user.UpdatedDate = DateTime.Now;
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {

                return false;
            }
        }
        public IEnumerable<User> ListAllPaging(int page, int pageSize)
        {
            return db.Users.OrderByDescending(x => x.CreatedDate).ToPagedList(page, pageSize);
        }
        public User GetById(string userName)
        {
            return db.Users.SingleOrDefault(x => x.UserName == userName);
        }
        public User ViewDetail(int id)
        {
            return db.Users.SingleOrDefault(x => x.ID == id);
        }
        public int Login(string userName, string passWord)
        {
            var result = db.Users.SingleOrDefault(x => x.UserName == userName);
            if(result == null)
            {
                return 0;
            }
            else
            {
                if(result.Status == false)
                {
                    return -1;
                }
                else
                {
                    if (result.Password == passWord)
                    {
                        return 1;
                    }
                    else
                    {
                        return -2;
                    }
                }
            }
        }
    }
}