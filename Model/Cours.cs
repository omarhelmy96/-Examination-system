//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace test.Model
{
    using System;
    using System.Collections.Generic;
    
    public partial class Cours
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Cours()
        {
            this.QuestionPools = new HashSet<QuestionPool>();
        }
    
        public int Course_ID { get; set; }
        public string Course_Name { get; set; }
        public string Course_Desc { get; set; }
        public Nullable<int> Course_Max_Degree { get; set; }
        public Nullable<int> Course_Min_Degree { get; set; }
        public Nullable<int> Instructor_ID { get; set; }
        public Nullable<int> Track_ID { get; set; }
    
        public virtual Instructor Instructor { get; set; }
        public virtual Track Track { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<QuestionPool> QuestionPools { get; set; }
    }
}
