using OpenIddict.EntityFrameworkCore.Models;
using System.ComponentModel.DataAnnotations;

namespace CCSentinelUI.Data.Contracts
{
    public class SentinelScope : OpenIddictEntityFrameworkCoreScope
    {
        [Timestamp]
        public byte[] Version { get; set; } = [];
    }
}
