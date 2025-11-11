using OpenIddict.EntityFrameworkCore.Models;
using System.ComponentModel.DataAnnotations;

namespace CCSentinelUI.Data.Contracts
{
    public class SentinelApplication : OpenIddictEntityFrameworkCoreApplication
    {
        [Timestamp]
        public byte[] Version { get; set; } = [];
    }
}
