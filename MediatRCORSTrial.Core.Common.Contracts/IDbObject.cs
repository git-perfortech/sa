using System;
using System.Collections.Generic;
using System.Text;

namespace MediatRCORSTrial.Core.Common.Contracts
{
    public interface IDbObject
    {
        string Code { get; }
        string Name { get; }
        bool IsDefault { get; }
        DbObject DbObject();
        DbObject DbObject(bool entityLazyLoad = false);
        void Migrate();
    }

    public class DbObject
    {
        /// <summary>
        /// Proviver enum bilgisi
        /// </summary>
        public DbProvider DbProvider { get; set; }

        /// <summary>
        /// Üretilen context nesnesi
        /// </summary>
        public object DbObjectContext { get; set; }
    }

    public enum DbProvider
    {
        EntityFramework = 1,
        NHibernate = 2
    }

}
