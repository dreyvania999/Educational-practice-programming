//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace UPM
{
    using System;
    using System.Collections.Generic;
    
    public partial class District
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public District()
        {
            this.FactAdress = new HashSet<FactAdress>();
            this.PositionInform = new HashSet<PositionInform>();
        }
    
        public int ID { get; set; }
        public string Title { get; set; }
        public int BuildingType { get; set; }
        public double Square { get; set; }
        public int Population { get; set; }
        public int CountMetroStations { get; set; }
    
        public virtual BildingTypes BildingTypes { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<FactAdress> FactAdress { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PositionInform> PositionInform { get; set; }
    }
}
