using System.Collections.Generic;
using System.Threading.Tasks;
using API.Entities;
using API.DTOs;

namespace API.Interfaces
{
    public interface IPhotoRepository
    {
         Task<IEnumerable<PhotoForApprovalDto>> GetUnapprovedPhotos();
         Task<Photo> GetPhotoById(int photoId);
         void RemovePhoto(Photo photo);
    }
}