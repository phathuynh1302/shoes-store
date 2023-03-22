using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PRN211_ShoesStore.Models.DTO;
using PRN211_ShoesStore.Models.Entity;
using PRN211_ShoesStore.Repository;
using PRN211_ShoesStore.Utils;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PRN211_ShoesStore.Service
{
    public class UserService
    {
        private UserRepository _userRepository;

        private RoleRepository _roleRepository;

        private ShoesRepository _shoesRepository;

        private ShoesImageRepository _shoesImageRepository;

        public UserService(UserRepository userRepository, RoleRepository roleRepository, ShoesRepository shoesRepository, ShoesImageRepository shoesImageRepository)
        {
            _userRepository = userRepository;
            _roleRepository = roleRepository;
            _shoesRepository= shoesRepository;
            _shoesImageRepository = shoesImageRepository;

        }

        public User checkUsernameIsExisted(string username)
        {
            User user = _userRepository.GetAll().ToList().Where(u => u.username== username).FirstOrDefault();
            return user;
        }

        public User Register(string name, string username, string pwd, string phone, string email, string address)
        {
            var user = new User();
            user.name = name;
            user.username = username;
            user.password = pwd;
            user.phone = phone;
            user.email = email;
            user.address = address;
            Role role = _roleRepository.GetAll().ToList().Where(r => r.roleName.Equals("User")).First();
            //List<User> users = _userRepository.GetAll().Include(x => x.role).ToList();
            if (role != null)
            {
                user.roleId = role.id;
            }
            user.createDate = System.DateTime.Now;
            user.status = true;
            _userRepository.Create(user);
            return user;
        }

		public User GetUserById(int userId)
		{
			return _userRepository.GetById(userId);
		}


		public User login(string Username, string password)
        {
            var user = _userRepository.GetAll().Include(r => r.role).ToList().Where(r => r.username.Equals(Username) && r.password.Equals(password)).FirstOrDefault();
            return user;
        }

        private ShoesDTO convertShoesToShoesDTO(Shoes shoes, byte[] image)
        {
            ShoesDTO shoesDTO = new ShoesDTO();
            shoesDTO.id= shoes.id;
            shoesDTO.name= shoes.name;
            shoesDTO.price= shoes.price;
            shoesDTO.quantity= shoes.quantity;
            shoesDTO.description = shoes.shoesDetails;
            shoesDTO.image = image;
            return shoesDTO;
        }

        private List<ShoesDTO> convertListShoestoListShoesDTO(List<Shoes> shoesList)
        {
            ShoesDTO shoesDTO = new ShoesDTO();
            List<ShoesDTO> res = new List<ShoesDTO>();
            foreach (var item in shoesList)
            {
                List<ShoesImage> shoesImg = _shoesImageRepository.GetAll().Where(p => p.shoesId == item.id).Include(p => p.image).ToList();
                foreach (var img in shoesImg)
                {
                    shoesDTO = convertShoesToShoesDTO(item, img.image.image);
                    res.Add(shoesDTO);
                }
            }
            return res;
        }


        public List<ShoesDTO> Search(string name)
        {
            List<Shoes> shoes = new List<Shoes>();
            if (!String.IsNullOrEmpty(name))
            {
                shoes = _shoesRepository.GetAll().Where(p => p.name.Contains(name)).ToList();
            }
            else
            {
                shoes = _shoesRepository.GetAll().ToList();
            }
            return convertListShoestoListShoesDTO(shoes);
        }

        public List<ShoesDTO> ShowShoes()
        {
            List<Shoes> shoes = _shoesRepository.GetAll().ToList();
            return convertListShoestoListShoesDTO(shoes);
        }
    }
}
