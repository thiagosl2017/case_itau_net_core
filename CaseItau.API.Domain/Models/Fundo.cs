namespace CaseItau.API.Domain.Models
{
    public class Fundo
    {
        public string Codigo { get; set; }
        public string Nome { get; set; }
        public string Cnpj { get; set; }
        public int TipoFundoCodigo { get; set; }
        public decimal? Patrimonio { get; set; }
        public TipoFundo TipoFundo { get; set; }
    }
}
