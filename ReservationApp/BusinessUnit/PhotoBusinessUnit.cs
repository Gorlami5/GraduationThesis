using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.Extensions.Options;
using ReservationApp.BusinessUnit.Interfaces;
using ReservationApp.DataAccessUnit.Interfaces;
using ReservationApp.Dto;
using ReservationApp.Extensions;
using ReservationApp.Model;
using ReservationApp.Results;
using System.Security.Claims;

namespace ReservationApp.BusinessUnit
{
    public class PhotoBusinessUnit : IPhotoBusinessUnit
    {
        private readonly IPhotoDataAccess _photoDataAccess;
        private readonly IOptions<CloudinaryInformation> _cloudinarySettings;
        private Cloudinary cloudinary;
        private readonly ICityDataAccess _cityDataAccess;
        //private readonly IHttpContextAccessor _httpContextAccessor;

        public PhotoBusinessUnit(IPhotoDataAccess photoDataAccess,IOptions<CloudinaryInformation> cloudinarySettings, ICityDataAccess cityDataAccess /*IHttpContextAccessor httpContextAccessor*/)
        {
                _photoDataAccess = photoDataAccess;
                _cloudinarySettings = cloudinarySettings;
                _cityDataAccess = cityDataAccess;
                //_httpContextAccessor = httpContextAccessor;


            Account account = new Account(
                _cloudinarySettings.Value.CloudName,
                _cloudinarySettings.Value.ApiKey,
                _cloudinarySettings.Value.ApiSecret);
           
               cloudinary = new Cloudinary(account);
        }
        public DataResult<Photo> GetPhotoByCityId(int cityId)
        {
            var photo = _photoDataAccess.GetPhotoByCityId(cityId);
            if(photo == null)
            {
                return new ErrorDataResult<Photo>(ConstantsMessages.PhotoNotFound);
            }
            return new SuccessDataResult<Photo>(photo, ConstantsMessages.PhotoListed);

        }

        public DataResult<PhotoForReturnDto> GetPhotoById(int id)
        {
            var photo = GetPhotoByCityId(id);
            PhotoForReturnDto photoForReturnDto = new PhotoForReturnDto()
            {
                Id = photo.Data.Id,               
                Url = photo.Data.Url,
                Description = photo.Data.Description,
                DateAdded = photo.Data.DateAdded,
                PublicId = photo.Data.PublicId,
                IsMain = photo.Data.IsMain,
            };
            return new SuccessDataResult<PhotoForReturnDto>(photoForReturnDto);
            
        }

        public DataResult<List<Photo>> GetPhotoList()
        {
            var photos = _photoDataAccess.GetPhotoList();
            if(photos.Count == 0)
            {
                return new ErrorDataResult<List<Photo>>(ConstantsMessages.PhotoNotFound);
            }
            return new SuccessDataResult<List<Photo>>(photos,ConstantsMessages.PhotoListed);
        }
        public DataResult<PhotoForReturnDto> AddPhotoForCity(int cityId,PhotoForCreationDto photoForCreationDto)
        {
            var city = _cityDataAccess.GetCityById(cityId);
            if(city == null)
            {
                return new ErrorDataResult<PhotoForReturnDto>(ConstantsMessages.CityNotFound);
            }
            //var currentuserıd = int.parse(_httpcontextaccessor.httpcontext.user.findfirst(claimtypes.nameıdentifier).value);

            //if (currentuserıd != city.userıd)
            //{
            //    return new errordataresult<photoforreturndto>(constantsmessages.unauthorize);
            //}
            var file = photoForCreationDto.file;
            var uploadResult = new ImageUploadResult();
            if (file.Length > 0)
            {
                using (var stream = file.OpenReadStream())
                {
                    var uploadParams = new ImageUploadParams
                    {
                        File = new FileDescription(file.Name, stream)
                    };

                    uploadResult = cloudinary.Upload(uploadParams);
                }

            }
            photoForCreationDto.Url = uploadResult.Uri.ToString();
            photoForCreationDto.PublicId = uploadResult.PublicId;

            Photo photo = new Photo()
            {
                Url = photoForCreationDto.Url,
                Description = photoForCreationDto.Description,
                DateAdded = photoForCreationDto.DateAdded,
                PublicId = photoForCreationDto.PublicId,
                City = city
            };
            if (!city.Photos.Any(p => p.IsMain))
            {
                photo.IsMain = true;
            }
            var result =_photoDataAccess.AddPhoto(photo);
            if(result > 0)
            {
                
                PhotoForReturnDto photoForReturnDto = new PhotoForReturnDto()
                {
                    Id = photo.Id,
                    Url = photo.Url,
                    Description = photo.Description,
                    DateAdded = photo.DateAdded,
                    PublicId = photo.PublicId,
                    IsMain = photo.IsMain,
                };

                return new SuccessDataResult<PhotoForReturnDto>(photoForReturnDto, ConstantsMessages.PhotoAdded);
                
            }

            return new ErrorDataResult<PhotoForReturnDto>(ConstantsMessages.PhotoAddError);




        }
        public DataResult<CompanyPhotoForReturnDto> AddCompanyPhotoForCompany(int cityId, CompanyPhotoForCreationDto companyPhotoForCreationDto)
        {
            var city = _cityDataAccess.GetCityById(cityId);
            if (city == null)
            {
                return new ErrorDataResult<CompanyPhotoForReturnDto>(ConstantsMessages.CityNotFound);
            }
            //var currentuserıd = int.parse(_httpcontextaccessor.httpcontext.user.findfirst(claimtypes.nameıdentifier).value);

            //if (currentuserıd != city.userıd)
            //{
            //    return new errordataresult<photoforreturndto>(constantsmessages.unauthorize);
            //}
            var file = companyPhotoForCreationDto.file;
            var uploadResult = new ImageUploadResult();
            if (file.Length > 0)
            {
                using (var stream = file.OpenReadStream())
                {
                    var uploadParams = new ImageUploadParams
                    {
                        File = new FileDescription(file.Name, stream)
                    };

                    uploadResult = cloudinary.Upload(uploadParams);
                }

            }
            companyPhotoForCreationDto.Url = uploadResult.Uri.ToString();
            companyPhotoForCreationDto.PublicId = uploadResult.PublicId;

            Photo photo = new Photo()
            {
                Url = companyPhotoForCreationDto.Url,
                Description = companyPhotoForCreationDto.Description,
                DateAdded = companyPhotoForCreationDto.DateAdded,
                PublicId = companyPhotoForCreationDto.PublicId,
                City = city
            };
            if (!city.Photos.Any(p => p.IsMain))
            {
                photo.IsMain = true;
            }
            var result = _photoDataAccess.AddPhoto(photo);
            if (result > 0)
            {

                CompanyPhotoForReturnDto companyPhotoForReturnDto = new CompanyPhotoForReturnDto()
                {
                    Id = photo.Id,
                    Url = photo.Url,
                    Description = photo.Description,
                    DateAdded = photo.DateAdded,
                    PublicId = photo.PublicId,
                    IsMain = photo.IsMain,
                };

                return new SuccessDataResult<CompanyPhotoForReturnDto>(companyPhotoForReturnDto, ConstantsMessages.PhotoAdded);

            }

            return new ErrorDataResult<CompanyPhotoForReturnDto>(ConstantsMessages.PhotoAddError);




        }
        public DataResult<Photo> GetCompanyPhotoByCompanyId(int companyId)
        {
            var photo = _photoDataAccess.GetPhotoByCityId(companyId);
            if (photo == null)
            {
                return new ErrorDataResult<Photo>(ConstantsMessages.PhotoNotFound);
            }
            return new SuccessDataResult<Photo>(photo, ConstantsMessages.PhotoListed);

        }
        public DataResult<CompanyPhotoForReturnDto> GetCompanyPhotoById(int id)
        {
            var photo = _photoDataAccess.GetCompanyPhotoByCityId(id);
            CompanyPhotoForReturnDto companyPhotoForReturnDto = new CompanyPhotoForReturnDto()
            {
                Id = photo.Id,
                Url = photo.Url,
                Description = photo.Description,
                DateAdded = photo.DateAdded,
                PublicId = photo.PublicId,
                IsMain = photo.IsMain
            };
            return new SuccessDataResult<CompanyPhotoForReturnDto>(companyPhotoForReturnDto);

        }
    }
}
