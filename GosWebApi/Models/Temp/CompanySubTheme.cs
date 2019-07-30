using System;

namespace GosWebApi.Models
{
    public class CompanySubTheme
    {
        public Guid CompanyId { get; set; }
        public Company Company { get; set; }

        public Guid SubThemeId { get; set; }
        public SubTheme SubTheme { get; set; }

        public CompanySubTheme(Guid companyId, Guid subThemeId)
        {
            CompanyId = companyId;
            SubThemeId = subThemeId;
        }
    }
}