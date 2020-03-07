using System;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace RotatingChores.Areas.Identity.Pages.Account.Manage
{
    public static class ManageNavPages
    {
        public static string Index => "Index";

        public static string ChangeEmail => "ChangeEmail";

        public static string CreateLocalAccount => "CreateLocalAccount";

        public static string ExternalLogins => "ExternalLogins";

        public static string DeleteAccount => "DeleteAccount";

        public static string TwoFactorAuthentication => "TwoFactorAuthentication";

        public static string IndexNavClass(ViewContext viewContext) => PageNavClass(viewContext, Index);

        public static string ChangeEmailNavClass(ViewContext viewContext) => PageNavClass(viewContext, ChangeEmail);

        public static string CreateLocalAccountNavClass(ViewContext viewContext) => PageNavClass(viewContext, CreateLocalAccount);

        public static string ExternalLoginsNavClass(ViewContext viewContext) => PageNavClass(viewContext, ExternalLogins);

        public static string DeleteAccountNavClass(ViewContext viewContext) => PageNavClass(viewContext, DeleteAccount);

        public static string TwoFactorAuthenticationNavClass(ViewContext viewContext) => PageNavClass(viewContext, TwoFactorAuthentication);

        private static string PageNavClass(ViewContext viewContext, string page)
        {
            var activePage = viewContext.ViewData["ActivePage"] as string
                ?? System.IO.Path.GetFileNameWithoutExtension(viewContext.ActionDescriptor.DisplayName);
            return string.Equals(activePage, page, StringComparison.OrdinalIgnoreCase) ? "active" : null;
        }
    }
}