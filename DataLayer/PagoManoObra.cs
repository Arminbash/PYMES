//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DataLayer
{
    using System;
    using System.Collections.Generic;
    
    public partial class PagoManoObra
    {
        public int IdPagoManoObra { get; set; }
        public int IdPersona { get; set; }
        public int HorasTrabajadas { get; set; }
        public decimal PagoXHora { get; set; }
        public decimal Total { get; set; }
    
        public virtual Persona Persona { get; set; }
    }
}
