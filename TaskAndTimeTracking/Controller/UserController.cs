using System;
using System.Threading.Tasks;
using TaskAndTimeTracking.Common;
using TaskAndTimeTracking.Controller.DTO;
using TaskAndTimeTracking.Controller.DTO.response;
using TaskAndTimeTracking.Controller.interfaces;
using TaskAndTimeTracking.Persistence.Entity;
using TaskAndTimeTracking.Persistence.Repository.Interfaces;

namespace TaskAndTimeTracking.Controller
{
    public class UserController : BaseController<UserResponseDTO, UserRequestDTO, UserEntity>, IUserController
    {
        private IUserRepository UserRepo;

        public UserController(IUserRepository userRepository) : base(userRepository)
        {
            UserRepo = userRepository;
        }
        

        public override async Task<ResponseDTO<UserResponseDTO>> add(UserRequestDTO userRequestDto)
        {
            var userEntity = await Task.Run(() =>
            {
                return ControllerMapper.Map<UserRequestDTO, UserEntity>(userRequestDto, opt =>
                {
                    opt.AfterMap((dto, entity) =>
                    {
                        byte[] generatedSalt = PasswordManager.GenerateSalt();
                        entity.Salt = Convert.ToBase64String(generatedSalt);
                        entity.Password = PasswordManager.GeneratePasswordHash(generatedSalt, dto.Password);
                    });
                });
            });
            var newUserEntity = await Repo.add(userEntity);
            return new ResponseDTO<UserResponseDTO>(ControllerMapper.Map<UserEntity, UserResponseDTO>(newUserEntity));
        }

        public override async Task<ResponseDTO<UserResponseDTO>> update(UserRequestDTO dto)
        {
            if (dto.Id == 0)
            {
                new ResponseDTO<UserRequestDTO>("No ID was given for update");
            }
            var oldEntity = await Repo.getById(dto.Id);
            if (dto.PasswordModified)
            {
                oldEntity.Password = PasswordManager.GeneratePasswordHash(
                    Convert.FromBase64String(oldEntity.Salt), dto.Password);
            }

            oldEntity.FirstName = dto.FirstName;
            oldEntity.LastName = dto.LastName;
            oldEntity.AuthorizationLevel = dto.AuthorizationLevel;

            await Repo.update(oldEntity);
            return new ResponseDTO<UserResponseDTO>();
        }

        public async Task<ResponseDTO<UserResponseDTO>> getByEMail(string email)
        {
            var userEntity = await UserRepo.getByEmail(email);
            if (userEntity == null)
            {
                return new ResponseDTO<UserResponseDTO>("Given user E-Mail not found");
            }
            return new ResponseDTO<UserResponseDTO>(ControllerMapper.Map<UserEntity, UserResponseDTO>(userEntity));
        }
        
        public override Task SafeMap(UserEntity toEntity, UserRequestDTO dto)
        {
            throw new NotImplementedException();
        }
    }
}