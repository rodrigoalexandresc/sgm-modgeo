using System;
using System.Collections.Generic;

namespace ModGeo.Models {
    public class Lote {
        public int Id { get; set; }        
        public int GeoId { get; set; }
        public string Endereco { get; set; }
        public string InscricaoImovel { get; set; }
        public bool Ativo { get; set; }
        public IList<LoteHistorico> Historicos { get; set; }
    }

    [Serializable]
    public class LoteHistorico {
        public int Id { get; set; }
        public int LoteId { get; set; }
        public Lote Lote { get; set; }
        public decimal AreaTerreno { get; set; }
        public decimal AreaConstruida { get; set; }
        public DateTime DataAtualizacao { get; set; }  
        public DateTime? DataIntegracao { get; set; }
    }

    [Serializable]
    public class LoteMessage {
        public LoteMessage(Lote lote, LoteHistorico historico)
        {
            InscricaoImovel = lote.InscricaoImovel;
            AreaConstruida = historico.AreaConstruida;
            AreaTerreno = historico.AreaTerreno;
            DataAtualizacao = historico.DataAtualizacao;            
        }

        public string InscricaoImovel { get; set; }
        public decimal AreaTerreno { get; set; }
        public decimal AreaConstruida { get; set; }
        public DateTime DataAtualizacao { get; set; }  
    }
}