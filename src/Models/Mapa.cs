using System.Collections.Generic;

namespace ModGeo.Models {
    public class Mapa {
        public int Id { get; set; }
        public string Pai { get; set; }
        public string Titulo { get; set; }
        public virtual MapaArquivo MapaArquivo { get; set; }
    }

    public class MapaArquivo {
        public int Id { get; set; }
        public string Pai { get; set; }
        public string Titulo { get; set; }
        public byte[] Arquivo { get; set; }
    }

    public class MapaHierarquizado {
        public int MapaId { get; set; }
        public string Titulo { get; set; }
        public IEnumerable<MapaHierarquizado> Filhos { get; set; }
    }
}