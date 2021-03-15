using System;
using System.Collections.Generic;

namespace ModGeo.Models {
    public class LoteQuery {
        public string Endereco { get; set; }
        public string InscricaoImovel { get; set; }
        public int? GeoId { get; set; }

        public bool IsValid() {
            return !string.IsNullOrEmpty(Endereco) 
                || !string.IsNullOrEmpty(InscricaoImovel)
                || GeoId.HasValue;
        }

        public IEnumerable<string> Errors() {
            if (IsValid()) return null;

            var errors = new List<string>();

            if (!IsValid())
                errors.Add("Preencher Endereço, Inscrição do imóvel ou GeoId");

            return errors;            
        }
    }
}