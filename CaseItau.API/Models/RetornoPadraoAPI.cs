using Newtonsoft.Json;

namespace CaseItau.API.Models
{
    public class RetornoPadraoAPI
    {
        public RetornoPadraoAPI(){}

        public RetornoPadraoAPI(dynamic dados, string message)
        {
            Dados = dados;
            Message = message;
        }

        [JsonProperty(PropertyName = "dados")]
        public dynamic Dados { get; set; }

        [JsonProperty(PropertyName = "message")]
        public dynamic Message { get; set; }
    }
}