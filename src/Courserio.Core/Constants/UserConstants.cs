using Courserio.Core.DTOs.User;
using Courserio.Core.Models;

namespace Courserio.Core.Constants
{
    public class UserConstants
    {
        public const string DefaultProfilePicture =
            "https://st4.depositphotos.com/1000507/24488/v/600/depositphotos_244889634-stock-illustration-user-profile-picture-isolate-background.jpg";

        public const string AnonymousProfilePicture =
            "https://www.pngitem.com/pimgs/m/575-5759580_anonymous-avatar-image-png-transparent-png.png";

        public static readonly UserDto AnonymousUser = new UserDto
        {
            Id = 0,
            ProfilePicture = AnonymousProfilePicture,
            Username = "Anonymous"
        };
    }
}
